using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationDisplay : MonoBehaviour
{
    Text display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Text>();

        display.text = "Latitude:" +
            "\nLongitude:";
    }

    private void Update()
    {
        display = GetComponent<Text>();

        display.text = "Latitude: 0.000000000" +
            "\nLongitude: 0.00000000";
    }
}
