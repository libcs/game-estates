using Gamer.Estate.Tes.Records;

namespace Gamer.Estate.Tes.Components
{
    public class APPAComponent : BASEComponent
    {
        void Start()
        {
            var APPA = (APPARecord)record;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(WPDT.ITEX.value, "icons"); 
            objData.name = APPA.FULL.Value;
            objData.weight = APPA.DATA.Weight.ToString();
            objData.value = APPA.DATA.Value.ToString();
            objData.interactionPrefix = "Take ";
        }
    }
}