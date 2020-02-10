/*OpenMenuScript.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;

/// <summary>
/// OpenMenuScript class that opens the menu with the click of the menu button.
/// </summary>
public class OpenMenuScript : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenMenu;
    public SteamVR_Input_Sources handType;

    /// <summary>
    /// Adds listeners to the menu buttons of the controllers
    /// </summary>
    void Start()
    {
        OpenMenu.AddOnStateUpListener(MenuInactive, handType);
        OpenMenu.AddOnStateDownListener(MenuActive, handType);
    }

    /// <summary>
    /// Activates the Menu Scene.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    public void MenuActive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Activating menu");
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Deactivates the Menu Scene.
    /// </summary>
    /// <param name="fromAction">Action that triggered the behavior.</param>
    /// <param name="fromSource">Source of the action.</param>
    public void MenuInactive(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Deactivating menu");
    }

    void Update()
    {
        
    }
}
