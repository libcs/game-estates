using Gamer.Estate.Tes.Records;

namespace Gamer.Estate.Tes.Components
{
    public class CONTComponent : BASEComponent
    {
        void Start()
        {
            pickable = false;
            objData.name = ((CONTRecord)record).FULL.Value;
            objData.interactionPrefix = "Open ";
        }
    }
}