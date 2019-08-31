using Game.Estate.Tes.Records;

namespace Game.Estate.Tes.Components
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