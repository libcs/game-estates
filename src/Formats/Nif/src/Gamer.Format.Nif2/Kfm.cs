using System;
using System.Collections.Generic;
using System.IO;

namespace Niflib
{
    //KFM Data Structure
    public class KfmEventString
    {
        public uint unk_int;
        public string event_;

        public KfmEventString() { unk_int = 0; event_ = null; }
        public void Read(IStream s, uint version)
        {
            unk_int = Nif.ReadUInt(s);
            event_ = Nif.ReadString(s);
        }
        public void Write(OStream s, uint version)
        {
            Nif.WriteUInt(unk_int, s);
            Nif.WriteString(event_, s);
        }
    }

    public class KfmEvent
    {
        public uint id;
        public uint type;
        public float unk_float;
        public KfmEventString[] event_strings;
        public uint unk_int3;

        public KfmEvent() { id = 0; type = 0; unk_float = 0.5f; event_strings = new KfmEventString[0]; unk_int3 = 0; }
        public void Read(IStream s, uint version)
        {
            id = Nif.ReadUInt(s);
            type = Nif.ReadUInt(s);
            if (type != 5)
            {
                unk_float = Nif.ReadFloat(s);
                event_strings = new KfmEventString[Nif.ReadUInt(s)]; //: .Resize(
                foreach (var it in event_strings) it.Read(s, version);
                unk_int3 = Nif.ReadUInt(s);
            }
        }
    }

    public class KfmAction
    {
        public string action_name;
        public string action_filename;
        public uint unk_int1;
        public KfmEvent[] events;
        public uint unk_int2;

        public void Read(IStream s, uint version)
        {
            if (version <= Kfm.VER_KFM_1_2_4b) action_name = Nif.ReadString(s);
            action_filename = Nif.ReadString(s);
            unk_int1 = Nif.ReadUInt(s);
            events = new KfmEvent[Nif.ReadUInt(s)]; //: .Resize
            foreach (var it in events) it.Read(s, version);
            unk_int2 = Nif.ReadUInt(s);
        }
    }

    public class Kfm
    {
        //--KFM File Format--//

        //KFM Versions
        public const uint VER_KFM_1_0 = 0x01000000; /*!< Kfm Version 1.0 */
        public const uint VER_KFM_1_2_4b = 0x01020400; /*!< Kfm Version 1.2.4b */
        public const uint VER_KFM_2_0_0_0b = 0x02000000; /*!< Kfm Version 2.0.0.0b */

        public uint version;
        public byte unk_byte;
        public string nif_filename;
        public string master;
        public uint unk_int1;
        public uint unk_int2;
        public float unk_float1;
        public float unk_float2;
        public uint unk_int3;
        public KfmAction[] actions;

        // Reads the given file and returns the KFM version.
        public uint Read(string file_name) // returns Kfm version
        {
            var s = new IStream(File.OpenRead(file_name));
            var version = Read(s);
            if (s.IsEof)
                throw new Exception("End of file reached prematurely. This KFM may be corrupt or improperly supported.");
            Nif.ReadByte(s); // this should fail, and trigger the in.eof() flag
            if (!s.IsEof)
                throw new Exception("End of file not reached. This KFM may be corrupt or improperly supported.");
            return version;
        }

        public uint Read(IStream s) // returns Kfm version
        {
            //--Read Header--//
            var header_string = new byte[64];
            var headerstr = s.GetLine(header_string, 64);
            // make sure this is a KFM file
            if (headerstr.Substring(0, 26) != ";Gamebryo KFM File Version")
            {
                version = Nif.VER_INVALID;
                return version;
            }
            // supported versions
            if (headerstr == ";Gamebryo KFM File Version 2.0.0.0b") version = VER_KFM_2_0_0_0b;
            else if (headerstr == ";Gamebryo KFM File Version 1.2.4b") version = VER_KFM_1_2_4b;
            //else if ( headerstr == ";Gamebryo KFM File Version 1.0" ) version = VER_KFM_1_0;
            //else if ( headerstr == ";Gamebryo KFM File Version 1.0\r" ) version = VER_KFM_1_0; // Windows eol style
            else
            {
                version = Nif.VER_UNSUPPORTED;
                return version;
            }
            //--Read remainder--//
            if (version == VER_KFM_1_0)
            {
                // TODO write a parser
            }
            else
            {
                if (version >= VER_KFM_2_0_0_0b) unk_byte = Nif.ReadByte(s);
                else unk_byte = 1;
                nif_filename = Nif.ReadString(s);
                master = Nif.ReadString(s);
                unk_int1 = Nif.ReadUInt(s);
                unk_int2 = Nif.ReadUInt(s);
                unk_float1 = Nif.ReadFloat(s);
                unk_float2 = Nif.ReadFloat(s);
                actions = new KfmAction[Nif.ReadUInt(s)]; //: .Resize(
                unk_int3 = Nif.ReadUInt(s);
                foreach (var it in actions) it.Read(s, version);
            }

            // Retrieve action names
            if (version >= VER_KFM_2_0_0_0b)
            {
                var model_name = nif_filename.Substring(0, nif_filename.Length - 4); // strip .nif extension
                foreach (var it in actions)
                {
                    var action_name = it.action_filename.Substring(0, it.action_filename.Length - 3); // strip .kf extension
                    if (action_name.IndexOf(model_name + "_") == 0)
                        action_name = action_name.Substring(model_name.Length + 1);
                    if (action_name.IndexOf(master + "_") == 0)
                        action_name = action_name.Substring(master.Length + 1);
                    it.action_name = action_name;
                }
            }
            return version;
        }

        //public void Write(OStream s, uint version)
        //{
        //    if (version == VER_KFM_1_0)
        //    {
        //        // handle this case seperately
        //        s += ";Gamebryo KFM File Version 1.0" + Environment.NewLine;
        //        // TODO write the rest of the data
        //    }
        //    else
        //    {
        //        if (version == VER_KFM_1_2_4b)
        //            s.Write(Encoding.ASCII.GetBytes(";Gamebryo KFM File Version 1.2.4b\n"), 0, 34);
        //        else if (version == VER_KFM_2_0_0_0b)
        //            s.Write(Encoding.ASCII.GetBytes(";Gamebryo KFM File Version 2.0.0.0b\n"), 0, 37);
        //        else throw new Exception("Cannot write KFM file of this version.");
        //    }
        //}

        // Reads the NIF file and all KF files referred to in this KFM, and returns the root object of the resulting NIF tree.
        public NiObject MergeActions(string path)
        {
            // Read NIF file
            var nif = Nif.ReadNifTree(path + '\\' + nif_filename);

            // Read Kf files
            var kf = new List<NiObject>();
            foreach (var it in actions)
            {
                var action_filename = path + '\\' + it.action_filename;
                // Check if the file exists.
                // Probably we should check some other field in the Kfm file to determine this...
                var exists = File.Exists(action_filename);
                // Import it, if it exists.
                if (exists)
                    kf.Add(Nif.ReadNifTree(action_filename));
            }
            // TODO: merge everything into the nif file
            return nif;
        }
    }
}