using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ToggleTooltips : MonoBehaviour
{
    public GameObject ControllerTooltips;
    public SteamVR_Action_Boolean toggleTooltips;
    public SteamVR_Input_Sources handType;

    /// <summary>
    /// Adds listeners to the Bumper buttons of the controllers
    /// </summary>
    void Start()
    {
        toggleTooltips.AddOnStateUpListener(TooltipInactive, handType);
        toggleTooltips.AddOnStateDownListener(TooltipActive, handType);
    }

    /// <summary>
    /// Activates the Menu Scene.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    public void TooltipActive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Activating tooltip");
        ControllerTooltips.SetActive(true);
    }

    /// <summary>
    /// Deactivates the Menu Scene.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    public void TooltipInactive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Deactivating tooltip");
        ControllerTooltips.SetActive(false);
    }
}
