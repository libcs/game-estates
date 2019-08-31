using Game.Core.Netstream.Server;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace Game.Core.Netstream
{
    public class StreamTargetTest
    {
        IEventChannel _channel;
        StreamTarget _target;

        [SetUp]
        public void ConfigureLogger()
        {
            var channelFactory = Substitute.For<ChannelFactory>();
            _channel = Substitute.For<IEventChannel>();
            channelFactory.Create("localhost", 3365, null, 1).Returns(_channel);
            _target = new StreamTarget(channelFactory)
            {
                Active = false,
                Host = "localhost",
                Port = 3365,
                ReplayBufferSize = 1,
            };
        }

        [Test]
        public void Should_have_no_side_effect_if_active_flag_set_to_false()
        {
            // given
            _target.Active = false;
            // when
            _target.Emit("INFO","message");
            // then
            _channel.DidNotReceive().Send(Arg.Any<ServerSentEvent>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Should_send_an_sse_message_when_receiving_a_logging_event()
        {
            // given
            _target.Active = true;
            // when
            _target.Emit("INFO", "message");
            // then
            _channel.Received().Send(Arg.Is<ServerSentEvent>(evt => evt.ToString().Contains("message")), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Should_send_an_sse_message_with_a_type_matching_received_logging_event_level()
        {
            // given
            _target.Active = true;
            // when
            _target.Emit("WARN", "message");
            // then
            _channel.Received().Send(Arg.Is<ServerSentEvent>(evt => evt.ToString().StartsWith("event: WARN")), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Should_send_an_sse_message_without_type_when_received_logging_event_level_has_no_matching_level_on_browser()
        {
            // given
            _target.Active = true;
            // when
            _target.Emit("FATAL", "message");
            // then
            _channel.Received().Send(Arg.Is<ServerSentEvent>(evt => evt.ToString().StartsWith("data:")), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Should_send_a_multiline_sse_message_received_logging_event_for_an_exception()
        {
            // given
            _target.Active = true;
            // when
            _target.Emit("FATAL", "message1\nmessage2");
            // then
            var lineSeparator = new string[] { "\r\n" };
            _channel.Received().Send(
                Arg.Is<ServerSentEvent>(
                    evt => evt.ToString()
                        .Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries)
                        .Skip(1)
                        .All(l => l.StartsWith("data:"))
                ), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Should_dispose_channel_on_shutdown()
        {
            // given
            _target.Active = true;
            // when
            _target.Dispose();
            // then
            _channel.Received().Dispose();
        }
    }
}
