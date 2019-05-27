using ImageMagick;
using System;
using System.Diagnostics;
using System.IO;

namespace Gamer.Conversion.Interop
{
    class AstcencWrapper
    {
        const string encoder = "astcenc.exe";
        const int MAGIC_FILE_CONSTANT = 0x5CA1AB13;

        public static void EncodeASTC(byte[] inputBytes, int width, int height, int block_xsize, int block_ysize, out byte[] dstBytes)
        {
            dstBytes = null;
            var tastcpath = Path.Combine(Environment.CurrentDirectory, "temp.astc");
            var ttgapath = Path.Combine(Environment.CurrentDirectory, "temp.png");
            if (File.Exists(tastcpath)) File.Delete(tastcpath);
            if (File.Exists(ttgapath)) File.Delete(ttgapath);
            var settings = new MagickReadSettings
            {
                Format = MagickFormat.Rgba,
                Width = width,
                Height = height
            };
            var im = new MagickImage(inputBytes, settings);
            im.Flip();
            im.ToBitmap().Save(ttgapath);
            if (File.Exists(ttgapath))
            {
                var process = new Process();
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.FileName = encoder;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = string.Format(@"-c ""{0}"" ""{1}"" {2}x{3} -medium", ttgapath, tastcpath, block_xsize, block_ysize);
                Console.WriteLine(string.Format(@"-c ""{0}"" ""{1}"" {2}x{3} -medium", ttgapath, tastcpath, block_xsize, block_ysize));
                process.Start();
                process.WaitForExit();
            }

            if (File.Exists(tastcpath))
                using (var fs = File.Open(tastcpath, FileMode.Open))
                {
                    dstBytes = new byte[(int)fs.Length - 0x10];
                    fs.Seek(0x10, SeekOrigin.Begin);
                    fs.Read(dstBytes, 0, (int)fs.Length - 0x10);
                }
            if (File.Exists(tastcpath)) File.Delete(tastcpath);
            if (File.Exists(ttgapath)) File.Delete(ttgapath);
        }

        public static void DecodeASTC(byte[] inputBytes, int width, int height, int block_xsize, int block_ysize, out byte[] dstBytes)
        {
            var tastcpath = Path.Combine(Environment.CurrentDirectory, "temp.astc");
            var ttgapath = Path.Combine(Environment.CurrentDirectory, "temp.tga");
            if (File.Exists(tastcpath)) File.Delete(tastcpath);
            if (File.Exists(ttgapath)) File.Delete(ttgapath);
            dstBytes = null;
            GenerateASTCFile(inputBytes, width, height, block_xsize, block_ysize);
            if (File.Exists(tastcpath))
            {
                var process = new Process();
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.FileName = encoder;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = string.Format(@"-d ""{0}"" ""{1}""", tastcpath, ttgapath);
                Console.WriteLine(string.Format(@"-d ""{0}"" ""{1}""", tastcpath, ttgapath));
                process.Start();
                process.WaitForExit();
            }
            if (File.Exists(ttgapath))
            {
                Console.WriteLine("load temp png");
                using (var im = new MagickImage(ttgapath))
                {
                    im.Flip();
                    dstBytes = im.GetPixels().ToByteArray(0, 0, im.Width, im.Height, "RGBA");
                }
            }
            else Console.WriteLine("ERR: astcenc.exe encoding error");
            if (File.Exists(tastcpath)) File.Delete(tastcpath);
            if (File.Exists(ttgapath)) File.Delete(ttgapath);
        }

        static void GenerateASTCFile(byte[] inputBytes, int width, int height, int block_xsize, int block_ysize)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(MAGIC_FILE_CONSTANT);
                w.Write(block_xsize + block_ysize * 0x100 + 1 * 0x10000);
                w.Seek(-1, SeekOrigin.Current);
                w.Write(width);
                w.Seek(-1, SeekOrigin.Current);
                w.Write(height);
                w.Seek(-1, SeekOrigin.Current);
                w.Write(1);
                w.Seek(-1, SeekOrigin.Current);
                w.Write(inputBytes);
                var dst = ms.ToArray();
                File.WriteAllBytes("temp.astc", dst);
            }
        }
    }
}
