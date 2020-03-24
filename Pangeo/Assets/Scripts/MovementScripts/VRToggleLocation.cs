/*ToggeleTooltips.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// toggleLocation class handles the activation and deactivation of the 
/// controller tooltips with the use of the left and right controller bumpers.
/// </summary>
public class VRToggleLocation : MonoBehaviour
{
    public GameObject LocationPanel;
    public SteamVR_Action_Boolean toggleLocation;
    public SteamVR_Input_Sources handType;

    bool active;

    /// <summary>
    /// Adds listeners to the Bumper buttons of the controllers
    /// </summary>
    void Start()
    {
        toggleLocation.AddOnStateUpListener(TooltipActive, handType);
        LocationPanel.SetActive(false);
        active = false;
        //toggleLocation.AddOnStateDownListener(TooltipActive, handType);
    }

    /// <summary>
    /// Activates the tooltips.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    public void TooltipActive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Activating tooltip");
        if (active)
        {
            LocationPanel.SetActive(false);
            active = false;
        }
        else{
            LocationPanel.SetActive(true);
            active = true;            
        }

    }

    /// <summary>
    /// Deactivates the tooltips.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    //public void TooltipInactive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Deactivating tooltip");
    //    ControllerTooltips.SetActive(false);
    //}
}
