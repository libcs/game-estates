﻿namespace Game.Estate.Ultima.Resources.UI
{
    public interface ICharacter
    {
        int Height { get; set; }
        int Width { get; set; }
        int ExtraWidth { get; set; }

        int YOffset { get; }
        int XOffset { get; }

        unsafe void WriteToBuffer(uint* dstPtr, int dx, int dy, int linewidth, int maxHeight, int baseLine,
            bool isBold, bool isItalic, bool isUnderlined, bool isOutlined, uint color, uint outline);
    }

    public abstract class ACharacter : ICharacter
    {
        protected bool HuePassedColor;
        protected uint[] _pixelData;

        public int Width { get; set; }

        public int Height { get; set; }

        public int ExtraWidth { get; set; }

        public int XOffset { get; protected set; }

        public int YOffset { get; set; }

        public unsafe void WriteToBuffer(uint* dstPtr, int dx, int dy, int linewidth, int maxHeight, int baseLine, bool isBold, bool isItalic, bool isUnderlined, bool isOutlined, uint color, uint outline)
        {
            var inColor = color;
            if (_pixelData != null)
                fixed (uint* srcPtr = _pixelData)
                {
                    for (var y = 0; (y < Height) && (y + dy < maxHeight); y++)
                    {
                        uint* src = (srcPtr + (Width * y));
                        uint* dest = (dstPtr + (linewidth * (y + dy + YOffset)) + dx);
                        if (isItalic)
                            dest += (baseLine - YOffset - y - 1) / 2;
                        for (var x = 0; x < Width; x++)
                        {
                            if (*src != 0x00000000)
                            {
                                if (HuePassedColor)
                                {
                                    var r = (uint)((*src & 0x000000ff) * ((float)(inColor & 0x000000ff) / 0xff));
                                    var g = (uint)((*src & 0x0000ff00) * ((float)((inColor >> 08) & 0x000000ff) / 0xff)) & 0x0000ff00;
                                    var b = (uint)((*src & 0x00ff0000) * ((float)((inColor >> 16) & 0x000000ff) / 0xff)) & 0x00ff0000;
                                    color = 0xff000000 + b + g + r;
                                }
                                if (isOutlined)
                                    for (int iy = -1; iy <= 1; iy++)
                                    {
                                        uint* idest = (dest + (iy * linewidth));
                                        if (*idest == 0x00000000)
                                            *idest = outline;
                                        if (iy == 0)
                                        {
                                            if (isBold)
                                            {
                                                if (*(src - 1) == 0x00000000)
                                                {
                                                    *(idest) = outline;
                                                    *(idest + 1) = color;
                                                }
                                                else
                                                    *(idest + 1) = color;
                                                *(idest + 2) = color;
                                            }
                                            else
                                                *(idest + 1) = color;
                                        }
                                        else
                                        {
                                            if (*(idest + 1) == 0x00000000)
                                                *(idest + 1) = outline;
                                        }
                                        if (*(idest + 2) == 0x00000000)
                                            *(idest + 2) = outline;
                                        if (isBold)
                                            if (*(idest + 3) == 0x00000000)
                                                *(idest + 3) = outline;
                                    }
                                else
                                {
                                    *dest = color;
                                    if (isBold)
                                        *(dest + 1) = color;
                                }
                            }
                            dest++;
                            src++;
                        }
                    }
                }

            if (isUnderlined)
            {
                var underlineAtY = dy + baseLine + 2;
                if (underlineAtY >= maxHeight)
                    return;
                uint* dest = dstPtr + (linewidth * (underlineAtY)) + dx;
                var w = isBold ? Width + 2 : Width + 1;
                for (var k = 0; k < w; k++)
                {
                    if (isOutlined)
                        for (var iy = -1; iy <= 1; iy++)
                        {
                            uint* idest = dest + (iy * linewidth);
                            if (*idest == 0x00000000)
                                *idest = outline;
                            if (iy == 0)
                                *(idest + 1) = color;
                            else if (*(idest + 1) == 0x00000000)
                                *(idest + 1) = outline;
                            if (*(idest + 2) == 0x00000000)
                                *(idest + 2) = outline;
                        }
                    else
                        *dest = color;
                    dest++;
                }
            }
        }
    }
}