using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerUtils;
using UnityEngine.UI;

/// <summary>
/// This class is used to display text on the location panel that changes
/// to reflect the user's current position
/// </summary>
public class LocationDisplay : MonoBehaviour
{
    Text display;
    double lat_diff;
    double long_diff;

    /// <summary>
    /// The start function that initializes the test of the location panel
    /// and finds the coordinate difference between tiles.
    /// </summary>
    void Start()
    {
        double lat0 = 0;
        double longitude0 = 0;
        double lat1 = 0;
        double longitude1 = 0;
        int pixelX = 0;
        int pixelY = 0;

        // Find coordinates at Tile 0,0
        QuadKeyFuncs.TileXYToPixelXY(0, 0, out pixelX, out pixelY);
        QuadKeyFuncs.PixelXYToLatLong(pixelX, pixelY, 14, out lat0, out longitude0);
        // Find coordinates at Tile 1,1
        QuadKeyFuncs.TileXYToPixelXY(1, 1, out pixelX, out pixelY);
        QuadKeyFuncs.PixelXYToLatLong(pixelX, pixelY, 14, out lat1, out longitude1);

        lat_diff = (lat1 - lat0) / 256;
        long_diff = (longitude1 - longitude0) / 256;

        display = GetComponent<Text>();

        display.text = "Latitude:" +
            "\nLongitude:";
    }

    /// <summary>
    /// Every frame, update the panel text with the current location of the user
    /// </summary>
    private void Update()
    {
        // Check current position
        Vector3 currpos = Globals.position;
        int x = Convert.ToInt32(Math.Round(currpos[0] / 32));
        int z = Convert.ToInt32(Math.Round(currpos[2] / 32));

        double latitude;
        double longitude;

        if (x > 0)
        {
            latitude = Globals.latitude + lat_diff * x;
        }
        else
        {
            latitude = Globals.latitude - lat_diff * x;
        }

        if (z > 0)
        {
            longitude = Globals.longitude + long_diff * z;
        }
        else
        {
            longitude = Globals.longitude - long_diff * z;
        }


        String text = "Latitude: \n" + latitude.ToString() + "\nLongitude: \n" + longitude.ToString();
        display = GetComponent<Text>();
        display.text = text;
    }
}
