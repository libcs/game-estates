﻿using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;

namespace Game.Core.Netstream.Server
{
    public class MulticastChannelTest
    {
        [Test]
        public void Should_send_event_to_added_channel()
        {
            // given
            var multicastChannel = new MulticastChannel();
            var channel = Substitute.For<IEventChannel>();
            multicastChannel.AddChannel(channel, CancellationToken.None);
            // when
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data"), CancellationToken.None);
            // then
            channel.Received().Send(Arg.Any<ServerSentEvent>(), CancellationToken.None);
        }

        [Test]
        public void Should_stop_sending_events_on_a_close_channel()
        {
            // given
            var multicastChannel = new MulticastChannel();
            var channel = Substitute.For<IEventChannel>();
            channel.When(c => c.Send(Arg.Any<ServerSentEvent>(), CancellationToken.None)).Do(x => { throw new Exception(); });
            multicastChannel.AddChannel(channel, CancellationToken.None);
            // when
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data"), CancellationToken.None); // exception raised
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data"), CancellationToken.None); // channel should be removed
            // then
            channel.Received(1).Send(Arg.Any<ServerSentEvent>(), CancellationToken.None);
        }

        [Test]
        public void Should_replay_last_event_when_adding_a_channel()
        {
            // given
            var multicastChannel = new MulticastChannel();
            var channel = Substitute.For<IEventChannel>();
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data"), CancellationToken.None);
            // when
            multicastChannel.AddChannel(channel, CancellationToken.None);
            // then
            channel.Received().Send(Arg.Any<ServerSentEvent>(), CancellationToken.None);
        }

        [Test]
        public void Should_replay_only_last_events_when_adding_a_channel()
        {
            // given
            var multicastChannel = new MulticastChannel(2);
            var channel = Substitute.For<IEventChannel>();
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data1"), CancellationToken.None);
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data2"), CancellationToken.None);
            multicastChannel.Send(new ServerSentEvent("DEBUG", "data3"), CancellationToken.None);
            // when
            multicastChannel.AddChannel(channel, CancellationToken.None);
            // then
            channel.Received(2).Send(Arg.Any<ServerSentEvent>(), CancellationToken.None);
            channel.DidNotReceive().Send(Arg.Is<ServerSentEvent>(e => e.ToString().Contains("data1")), CancellationToken.None);
        }
    }
}
