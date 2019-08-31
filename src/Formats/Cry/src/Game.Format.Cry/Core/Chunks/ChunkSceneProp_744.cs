using System;
using System.IO;

namespace Game.Format.Cry.Core
{
    public class ChunkSceneProp_744 : ChunkSceneProp
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            NumProps = r.ReadUInt32(); // Should be 31 for 0x744
            PropKey = new string[NumProps];
            PropValue = new string[NumProps];
            // Read the array of scene props and their associated values
            for (var i = 0; i < NumProps; i++)
            {
                PropKey[i] = r.ReadFString(32);
                PropValue[i] = r.ReadFString(64);
            }
        }
    }
}
