using Game.Estate.Tes.Records;

namespace Game.Estate.Tes.Components
{
    public class INGRComponent : BASEComponent
    {
        void Start()
        {
            var INGR = (INGRRecord)record;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(INGR.ITEX.value, "icons"); 
            objData.name = INGR.FULL.Value;
            objData.weight = INGR.DATA.Weight.ToString();
            objData.value = INGR.DATA.Value.ToString();
            objData.interactionPrefix = "Take ";
        }
    }
}
