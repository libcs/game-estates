﻿using Game.Estate.Tes.Records;

namespace Game.Estate.Tes.Components
{
    public class PROBComponent : BASEComponent
    {
        void Start()
        {
            var PROB = (PROBRecord)record;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(WPDT.ITEX.value, "icons"); 
            objData.name = PROB.FNAM.Value;
            objData.weight = PROB.PBDT.Weight.ToString();
            objData.value = PROB.PBDT.Value.ToString();
            objData.interactionPrefix = "Take ";
        }
    }
}