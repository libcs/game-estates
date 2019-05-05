using Gamer.Core;
using Gamer.Estate.Ultima.Resources.IO;
using System.IO;

namespace Gamer.Estate.Ultima.Resources
{
    public class AnimationResource
    {
        public const int HUMANOID_STAND_INDEX = 0x04;
        public const int HUMANOID_MOUNT_INDEX = 0x19;
        public const int HUMANOID_SIT_INDEX = 0x23; // 35

        const int COUNT_ANIMS = 0x1000;
        const int COUNT_ACTIONS = 36; // max UO action index is 34 (0-based, thus 35), we add one additional index for the humanoid sitting action.
        const int COUNT_DIRECTIONS = 8;
        AAnimationFrame[][][][] _cache;
        object _graphics;

        public AFileIndex FileIndex { get; } = FileManager.CreateFileIndex("Anim.idx", "Anim.mul", 0x40000, 6);
        public AFileIndex FileIndex2 { get; } = FileManager.CreateFileIndex("Anim2.idx", "Anim2.mul", 0x10000, -1);
        public AFileIndex FileIndex3 { get; } = FileManager.CreateFileIndex("Anim3.idx", "Anim3.mul", 0x20000, -1);
        public AFileIndex FileIndex4 { get; } = FileManager.CreateFileIndex("Anim4.idx", "Anim4.mul", 0x20000, -1);
        public AFileIndex FileIndex5 { get; } = FileManager.CreateFileIndex("Anim5.idx", "Anim5.mul", 0x20000, -1);

        public AnimationResource(object graphics)
        {
            _graphics = graphics;
            _cache = new AAnimationFrame[COUNT_ANIMS][][][];
        }

        public AAnimationFrame[] GetAnimation(int body, ref int hue, int action, int direction)
        {
            int animIndex;
            AFileIndex fileIndex;
            var sitting = AnimationFrame.SittingTransformation.None;
            if (body <= 0)
                return null;
            if (!DoesBodyExist(body))
                BodyDef.TranslateBodyAndHue(ref body, ref hue);
            var frames = CheckCache(body, action, direction);
            if (frames != null)
                return frames;
            if (action == HUMANOID_SIT_INDEX)
            {
                if (direction == 3 || direction == 5)
                {
                    sitting = AnimationFrame.SittingTransformation.MountNorth;
                    GetIndexes(body, HUMANOID_MOUNT_INDEX, direction, out animIndex, out fileIndex);
                }
                else if (direction == 1 || direction == 7)
                {
                    sitting = AnimationFrame.SittingTransformation.StandSouth;
                    GetIndexes(body, HUMANOID_STAND_INDEX, direction, out animIndex, out fileIndex);
                }
                else GetIndexes(body, action, direction, out animIndex, out fileIndex);
            }
            else GetIndexes(body, action, direction, out animIndex, out fileIndex);
            var reader = fileIndex.Seek(animIndex, out int length, out int extra, out bool patched);
            if (reader == null)
                return null;
            var uniqueAnimationIndex = ((body & 0xfff) << 20) + ((action & 0x3f) << 12) + ((direction & 0x0f) << 8);
            frames = LoadAnimation(reader, uniqueAnimationIndex, sitting);
            return _cache[body][action][direction] = frames;
        }

        AAnimationFrame[] LoadAnimation(BinaryFileReader r, int uniqueAnimationIndex, AnimationFrame.SittingTransformation sitting)
        {
            var palette = GetPalette(r); // 0x100 * 2 = 0x0200 bytes
            var read_start = (int)r.Position; // save file position after palette.
            var frameCount = r.ReadInt32(); // 0x04 bytes
            var lookups = new int[frameCount]; // frameCount * 0x04 bytes
            for (var i = 0; i < frameCount; ++i)
                lookups[i] = r.ReadInt32();
            var frames = new AnimationFrame[frameCount];
            for (var i = 0; i < frameCount; ++i)
                if (lookups[i] < lookups[0])
                    frames[i] = AnimationFrame.NullFrame; // Fix for broken animations, per issue13
                else
                {
                    r.Seek(read_start + lookups[i], SeekOrigin.Begin);
                    frames[i] = new AnimationFrame(uniqueAnimationIndex + (i & 0xff), _graphics, palette, r, sitting);
                }
            return frames;
        }

        ushort[] GetPalette(BinaryFileReader r)
        {
            var pal = new ushort[0x100];
            for (var i = 0; i < 0x100; ++i)
                pal[i] = (ushort)(r.ReadUInt16() | 0x8000);
            return pal;
        }

        AAnimationFrame[] CheckCache(int body, int action, int direction)
        {
            // Make sure the cache is complete.
            if (_cache[body] == null)
                _cache[body] = new AAnimationFrame[COUNT_ACTIONS][][]; // max 35 actions
            if (_cache[body][action] == null)
                _cache[body][action] = new AAnimationFrame[COUNT_DIRECTIONS][];
            if (_cache[body][action][direction] == null)
                _cache[body][action][direction] = new AAnimationFrame[1];
            if (_cache[body][action][direction][0] != null)
                return _cache[body][action][direction];
            else return null;
        }

        void GetIndexes(int body, int action, int direction, out int index, out AFileIndex fileIndex)
        {
            if (body < 0 || body >= COUNT_ANIMS)
                body = 0;
            var animIndex = BodyConverter.Convert(ref body);
            switch (animIndex)
            {
                default:
                case 1:
                    {
                        fileIndex = FileIndex;
                        if (body < 200) index = body * 110;
                        else if (body < 400) index = 22000 + ((body - 200) * 65);
                        else index = 35000 + ((body - 400) * 175);
                        break;
                    }
                case 2:
                    {
                        fileIndex = FileIndex2;
                        if (body < 200) index = body * 110;
                        else index = 22000 + ((body - 200) * 65);
                        break;
                    }
                case 3:
                    {
                        fileIndex = FileIndex3;
                        if (body < 300) index = body * 65;
                        else if (body < 400) index = 33000 + ((body - 300) * 110);
                        else index = 35000 + ((body - 400) * 175);
                        break;
                    }
                case 4:
                    {
                        fileIndex = FileIndex4;
                        if (body < 200) index = body * 110;
                        else if (body < 400) index = 22000 + ((body - 200) * 65);
                        else index = 35000 + ((body - 400) * 175);
                        break;
                    }
                case 5:
                    {
                        fileIndex = FileIndex5;
                        if (body < 200 && body != 34) index = body * 110; // looks strange, though it works.
                        else if (body < 400) index = 22000 + ((body - 200) * 65);
                        else index = 35000 + ((body - 400) * 175);
                        break;
                    }
            }
            index += action * 5;
            if (direction <= 4) index += direction;
            else index += direction - (direction - 4) * 2;
        }

        bool DoesBodyExist(int body)
        {
            GetIndexes(body, 0, 0, out var animIndex, out var fileIndex);
            return fileIndex.Seek(animIndex, out var length, out var extra, out var patched) != null;
        }
    }
}
