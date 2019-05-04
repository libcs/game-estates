using Gamer.Estate.Ultima.Resources.UI;
using System;
using System.IO;

namespace Gamer.Estate.Ultima.Resources.Fonts
{
    class CharacterUnicode : ACharacter
    {
        public CharacterUnicode() { }
        public CharacterUnicode(BinaryReader r)
        {
            XOffset = r.ReadSByte();
            YOffset = r.ReadSByte();
            Width = r.ReadByte();
            Height = r.ReadByte();
            ExtraWidth = 1;

            // only read data if there is IO...
            if (Width > 0 && Height > 0)
            {
                _pixelData = new uint[Width * Height];
                // At this point, we know we have data, so go ahead and start reading!
                for (var y = 0; y < Height; ++y)
                {
                    var scanline = r.ReadBytes(((Width - 1) / 8) + 1);
                    var bitX = 7;
                    var byteX = 0;
                    for (var x = 0; x < Width; ++x)
                    {
                        var color = 0x00000000U;
                        if ((scanline[byteX] & (byte)Math.Pow(2, bitX)) != 0)
                            color = 0xFFFFFFFF;
                        _pixelData[y * Width + x] = color;
                        bitX--;
                        if (bitX < 0)
                        {
                            bitX = 7;
                            byteX++;
                        }
                    }
                }
            }
        }
    }
}
