using Gamer.Estate.Ultima.Resources.Fonts;
using Gamer.Estate.Ultima.Resources.UI;
using System;
using System.IO;

namespace Gamer.Estate.Ultima.Resources
{
    public class FontsResource
    {
        bool _initialized;
        object _graphicsDevice;

        public const int UniFontCount = 3;
        readonly AFont[] _unicodeFonts = new AFont[UniFontCount];

        public const int AsciiFontCount = 10;
        readonly AFont[] _asciiFonts = new AFont[AsciiFontCount];

        public AFont GetUniFont(int index) => index < 0 || index >= UniFontCount ? _unicodeFonts[0] : _unicodeFonts[index];

        public AFont GetAsciiFont(int index) => index < 0 || index >= AsciiFontCount ? _asciiFonts[9] : _asciiFonts[index];

        public FontsResource(object graphics)
        {
            _graphicsDevice = graphics;
            Initialize();
        }

        void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                //_graphicsDevice.DeviceReset -= GraphicsDeviceReset;
                //_graphicsDevice.DeviceReset += GraphicsDeviceReset;
                LoadFonts();
            }
        }

        void GraphicsDeviceReset(object sender, EventArgs e) => LoadFonts();

        void LoadFonts()
        {
            // load Ascii fonts
            using (var r = new BinaryReader(new FileStream(FileManager.GetFilePath("fonts.mul"), FileMode.Open, FileAccess.Read)))
                for (var i = 0; i < AsciiFontCount; i++)
                {
                    _asciiFonts[i] = new FontAscii();
                    _asciiFonts[i].Initialize(r);
                    _asciiFonts[i].HasBuiltInOutline = true;
                }
            // load Unicode fonts
            var maxHeight = 0; // because all unifonts are designed to be used together, they must all share a single maxheight value.
            for (var i = 0; i < UniFontCount; i++)
            {
                var path = FileManager.GetFilePath($"unifont{(i == 0 ? string.Empty : i.ToString())}.mul");
                if (path != null)
                {
                    _unicodeFonts[i] = new FontUnicode();
                    _unicodeFonts[i].Initialize(new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)));
                    if (_unicodeFonts[i].Height > maxHeight)
                        maxHeight = _unicodeFonts[i].Height;
                }
            }
            for (var i = 0; i < UniFontCount; i++)
            {
                if (_unicodeFonts[i] == null)
                    continue;
                _unicodeFonts[i].Height = maxHeight;
            }
        }
    }
}
