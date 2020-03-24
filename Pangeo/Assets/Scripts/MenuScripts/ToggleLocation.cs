using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the keyboard version for toggling the UI panel
/// that displays the user's current location in the real world 
/// latitude and longitude coordinates.
/// </summary>
public class ToggleLocation : MonoBehaviour
{
    GameObject LocDisplay;
    bool active;

    /// <summary>
    /// Finds the canvas object to be toggled and makes it inactive
    /// </summary>
    void Start()
    {
        LocDisplay = GameObject.Find("LocationCanvas");
        active = false;
        LocDisplay.SetActive(false);
    }

    /// <summary>
    /// If the user presses and releases the 'E' key, toggle the 
    /// active status of the location panel
    /// </summary>
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(active)
            {
                active = false;
                LocDisplay.SetActive(false);
            }
            else
            {
                active = true;
                LocDisplay.SetActive(true);
            }
        }
    }
}
