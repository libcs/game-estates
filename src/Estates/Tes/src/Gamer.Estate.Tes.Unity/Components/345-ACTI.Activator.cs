namespace Gamer.Estate.Tes.Components
{
    public class ACTIComponent : BASEComponent
    {
        void Start()
        {
            usable = true;
            pickable = false;
            var ACTI = (ACTIRecord)record; 
            objData.name = ACTI.FULL.Value;
            objData.interactionPrefix = "Use ";
        }
    }
}