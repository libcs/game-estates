using System;
using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkController_826 : ChunkController
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            //Log($"ID is:  {id}");
            ControllerType = (CtrlType)Enum.ToObject(typeof(CtrlType), r.ReadUInt32());
            NumKeys = r.ReadUInt32();
            ControllerFlags = r.ReadUInt32();
            ControllerID = r.ReadUInt32();
            Keys = new Key[NumKeys];
            for (var i = 0; i < NumKeys; i++)
            {
                // Will implement fully later.  Not sure I understand the structure, or if it's necessary.
                Keys[i].Time = r.ReadInt32();
                //Log($"Time {Keys[i].Time}", );
                Keys[i].AbsPos.x = r.ReadSingle();
                Keys[i].AbsPos.y = r.ReadSingle();
                Keys[i].AbsPos.z = r.ReadSingle();
                //Log($"Abs Pos: {Keys[i].AbsPos.x:F7}  {Keys[i].AbsPos.y:F7}  {Keys[i].AbsPos.z:F7}");
                Keys[i].RelPos.x = r.ReadSingle();
                Keys[i].RelPos.y = r.ReadSingle();
                Keys[i].RelPos.z = r.ReadSingle();
                //Log($"Rel Pos: {Keys[i].RelPos.x:F7}  {Keys[i].RelPos.y:F7}  {Keys[i].RelPos.z:F7}");
            }
        }
    }
}