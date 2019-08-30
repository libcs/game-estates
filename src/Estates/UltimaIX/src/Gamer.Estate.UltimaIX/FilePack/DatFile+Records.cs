using Gamer.Core;
using Gamer.Estate.UltimaIX.Records;
using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

//http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format
namespace Gamer.Estate.UltimaIX.FilePack
{
    public partial class RecordGroup
    {
        public RecordGroup Next;
        public List<Record> Records = new List<Record>();
        protected readonly ProxySink _proxySink;
        public readonly string FilePath;

        public RecordGroup(ProxySink proxySink, string filePath)
        {
            _proxySink = proxySink;
            FilePath = filePath;
        }
    }
}
