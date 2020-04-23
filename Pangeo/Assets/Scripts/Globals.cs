using System.Threading;
using UnityEngine;
/// <summary>
/// A class containing variables with a lifetime of the application.
/// </summary>
public static class Globals
{
    public static double latitude = 39.5, longitude = -106.5;
    public static double curr_latitude, curr_longitude;
    public static int length, meshLength;
    public static Mutex mut = new Mutex();
    public static string BingAPIKey;


    //Bing Maps meters per pixel at different zoom levels
    public static double[] zoomLevelMetersPerPixel = new double[20]{0, 78271.52, 39135.76, 19567.88, 9783.94, 4891.97, 2445.98, 1222.99, 611.50, 305.75, 152.87, 76.44, 38.22, 19.11, 9.55, 4.78, 2.39, 1.19, .60, .30 };
    public static Vector3 position;
}
