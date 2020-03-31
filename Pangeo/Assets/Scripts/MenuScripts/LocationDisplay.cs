using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to display text on the location panel that changes
/// to reflect the user's current position
/// </summary>
public class LocationDisplay : MonoBehaviour
{
    Text display;

    /// <summary>
    /// The start function that initializes the test of the location panel
    /// </summary>
    void Start()
    {
        display = GetComponent<Text>();

        display.text = "Latitude:" +
            "\nLongitude:";
    }

    /// <summary>
    /// Every frame, update the panel text with the current location of the user
    /// </summary>
    private void Update()
    {
        display = GetComponent<Text>();

        display.text = "Latitude: 0.000000000" +
            "\nLongitude: 0.00000000";
    }
}
