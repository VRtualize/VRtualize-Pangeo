//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Input
    {
        
        public static Valve.VR.SteamVR_Input_ActionSet_default _default;
        
        public static Valve.VR.SteamVR_Input_ActionSet_NewSet NewSet;
        
        public static void Dynamic_InitializeActionSets()
        {
            SteamVR_Input._default.Initialize();
            SteamVR_Input.NewSet.Initialize();
        }
        
        public static void Dynamic_InitializeInstanceActionSets()
        {
            Valve.VR.SteamVR_Input._default = ((SteamVR_Input_ActionSet_default)(SteamVR_Input_References.GetActionSet("_default")));
            Valve.VR.SteamVR_Input.NewSet = ((SteamVR_Input_ActionSet_NewSet)(SteamVR_Input_References.GetActionSet("NewSet")));
            Valve.VR.SteamVR_Input.actionSets = new Valve.VR.SteamVR_ActionSet[] {
                    Valve.VR.SteamVR_Input._default,
                    Valve.VR.SteamVR_Input.NewSet};
        }
    }
}
