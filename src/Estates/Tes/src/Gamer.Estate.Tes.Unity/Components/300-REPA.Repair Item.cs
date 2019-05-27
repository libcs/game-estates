using Gamer.Estate.Tes.Records;

namespace Gamer.Estate.Tes.Components
{
    public class REPAComponent : BASEComponent
    {
        void Start()
        {
            var REPA = (REPARecord)record;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(WPDT.ITEX.value, "icons"); 
            objData.name = REPA.FNAM.Value;
            objData.weight = REPA.RIDT.Weight.ToString();
            objData.value = REPA.RIDT.Value.ToString();
            objData.interactionPrefix = "Take ";
        }
    }
}