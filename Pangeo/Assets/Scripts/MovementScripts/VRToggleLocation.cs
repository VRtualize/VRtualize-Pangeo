/*ToggeleTooltips.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// toggleLocation class handles the activation and deactivation of the 
/// location coordinates panel with the use of the left and right controller triggers.
/// </summary>
public class VRToggleLocation : MonoBehaviour
{
    public GameObject LocationPanel;
    public SteamVR_Action_Boolean toggleLocation;
    public SteamVR_Input_Sources handType;

    bool active;

    /// <summary>
    /// Adds listeners to the trigger buttons of the controllers
    /// </summary>
    void Start()
    {
        toggleLocation.AddOnStateUpListener(TooltipActive, handType);
        LocationPanel.SetActive(false);
        active = false;
        //toggleLocation.AddOnStateDownListener(TooltipActive, handType);
    }

    /// <summary>
    /// Toggles the active state of the location panel.
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
}
