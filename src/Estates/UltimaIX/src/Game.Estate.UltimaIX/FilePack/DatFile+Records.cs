using Game.Core;
using Game.Estate.UltimaIX.Records;
using Game.Core.Netstream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Core.Debug;

//http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format
//http://hacki.bootstrike.com/english/articles_orig_maps.htm
namespace Game.Estate.UltimaIX.FilePack
{
    public partial class RecordGroup
    {
        public RecordGroup Next;
        public List<Record> Records = new List<Record>();
        protected readonly StreamSink _streamSink;
        public readonly string FilePath;

        public RecordGroup(StreamSink streamSink, string filePath)
        {
            _streamSink = streamSink;
            FilePath = filePath;
        }
    }
}
