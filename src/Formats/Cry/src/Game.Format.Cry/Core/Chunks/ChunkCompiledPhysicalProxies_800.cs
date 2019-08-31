using System.IO;
using System.Linq;

namespace Game.Format.Cry.Core
{
    public class ChunkCompiledPhysicalProxies_800 : ChunkCompiledPhysicalProxies
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            var skin = GetSkinningInfo();
            NumPhysicalProxies = r.ReadUInt32(); // number of Bones in this chunk.
            //Log($"Number of bones (physical proxies): {NumPhysicalProxies}");
            PhysicalProxies = new PhysicalStream[NumPhysicalProxies];    // now have an array of physical proxies
            for (var i = 0; i < NumPhysicalProxies; i++)
            {
                // Start populating the physical stream array.  This is the Header.
                PhysicalProxies[i].ID = r.ReadUInt32();
                PhysicalProxies[i].NumVertices = r.ReadUInt32();
                PhysicalProxies[i].NumIndices = r.ReadUInt32();
                PhysicalProxies[i].Material = r.ReadUInt32();      // Probably a fill of some sort?
                PhysicalProxies[i].Vertices = new Vector3[PhysicalProxies[i].NumVertices];
                PhysicalProxies[i].Indices = new ushort[PhysicalProxies[i].NumIndices];
                for (var j = 0; j < PhysicalProxies[i].NumVertices; j++)
                {
                    PhysicalProxies[i].Vertices[j].x = r.ReadSingle();
                    PhysicalProxies[i].Vertices[j].y = r.ReadSingle();
                    PhysicalProxies[i].Vertices[j].z = r.ReadSingle();
                }
                // Read the indices
                for (var j = 0; j < PhysicalProxies[i].NumIndices; j++)
                    PhysicalProxies[i].Indices[j] = r.ReadUInt16(); //Log("Indices: {HitBoxes[i].Indices[j]}");
                //Log($"Index 0 is {HitBoxes[i].Indices[0]}, Index 9 is {HitBoxes[i].Indices[9]}");
                // read the crap at the end so we can move on.
                for (var j = 0; j < PhysicalProxies[i].Material; j++)
                    r.ReadByte();
            }
            skin.PhysicalBoneMeshes = PhysicalProxies.ToList();
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
