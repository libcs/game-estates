using Abbotware.Interop.NUnit;
using NFluent;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gamer.Proxy.Server
{
    public class LineParserTest
    {
        [Test, Timeout(100)]
        public async Task Should_parse_one_line()
        {
            // given
            var buffer = Encoding.ASCII.GetBytes("first line\n\n");
            var stream = new MemoryStream(buffer);
            var parser = new LineParser();
            // when
            var lines = await parser.Parse(stream, CancellationToken.None);
            // then 
            Check.That(lines).HasSize(1);
            Check.That(lines.ElementAt(0)).IsEqualTo("first line");
        }

        [Test]
        public async Task Should_parse_two_lines()
        {
            // given
            var buffer = Encoding.ASCII.GetBytes("first line\nsecond line\n\n");
            var stream = new MemoryStream(buffer);

            var parser = new LineParser();
            // when
            var lines = await parser.Parse(stream, CancellationToken.None);
            // then 
            Check.That(lines).HasSize(2);
            Check.That(lines.ElementAt(0)).IsEqualTo("first line");
            Check.That(lines.ElementAt(1)).IsEqualTo("second line");
        }

        [Test, Timeout(10000)]
        public async Task Should_parse_lines_till_cancelled()
        {
            // given
            var buffer = Encoding.ASCII.GetBytes("first line\nsecond");
            var stream = new MemoryStream(buffer);

            var parser = new LineParser();
            // when
            var source = new CancellationTokenSource();
            var parsingTask = Task.Run(() => parser.Parse(stream, source.Token));
            await Task.Delay(200);
            source.Cancel();
            await Task.Delay(1000);
            // then
            Check.That(parsingTask.Status).IsNotEqualTo(TaskStatus.Running);
        }
    }
}
