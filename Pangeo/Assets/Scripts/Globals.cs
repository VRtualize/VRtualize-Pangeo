using System.Threading;
using UnityEngine;
public static class Globals
{
    public static double latitude = 39.5, longitude = -108.5;
    public static int length, meshLength;
    public static Mutex mut = new Mutex();


    //Bing Maps meters per pixel at different zoom levels
    public static double[] zoomLevelMetersPerPixel = new double[20]{0, 78271.52, 39135.76, 19567.88, 9783.94, 4891.97, 2445.98, 1222.99, 611.50, 305.75, 152.87, 76.44, 38.22, 19.11, 9.55, 4.78, 2.39, 1.19, .60, .30 };
    public static Vector3 position;

    public static double Latitude {
        get{
            return latitude;
        }
        set{
            latitude = value;
        }
    }

    public static double Longitude
    {
        get {
            return longitude;
        }
        set {
            longitude = value;
        }
    }

    public static int Length
    {
        get {
            return length;
        }
        set {
            length = value;
        }
    }

    public static int MeshLength
    {
        get {
            return meshLength;
        }
        set {
            meshLength = value;
        }
    }
}
