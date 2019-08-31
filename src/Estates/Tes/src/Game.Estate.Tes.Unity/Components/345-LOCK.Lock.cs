﻿using Game.Estate.Tes.Records;

namespace Game.Estate.Tes.Components
{
    public class LOCKComponent : BASEComponent
    {
        void Start()
        {
            usable = true;
            pickable = false;
            var LOCK = (LOCKRecord)record;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(WPDT.ITEX.value, "icons"); 
            objData.name = LOCK.FNAM.Value;
            objData.weight = LOCK.LKDT.Weight.ToString();
            objData.value = LOCK.LKDT.Value.ToString();
        }
    }
}