using System;
using System.Text;

namespace DataManagerUtils
{
    /// <summary>
    /// The imageRequest class creates an HTTP Client to access image data from a
    /// Bing REST API. It takes in a longitude and latitude for the initial request
    /// to Bing's REST API. The API then returns a sample URL which is stored by 
    /// the imageRequest class. Any later calls to the imageRequest class are done
    /// using this sampleURL. This complies with Microsoft's Terms of Service. 
    /// </summary>
    public static class QuadKeyFuncs
    {
        /// <summary>
        /// This function allows you to put in a longitude, latitiude, and zoomLevel into it
        /// and returns a string with the quadkey containing that coordinate at that zoom 
        /// level. This function was originally created by Microsoft for Bing Maps and
        /// is owned by Microsoft.
        /// </summary>
        /// <param name="latitude">Latitude of coordinate</param>
        /// <param name="longitude">Longitude of coordinate</param>
        /// <param name="zoomLevel">Zoom Level of desired quadkey (As described by Bing Maps API)
        /// https://docs.microsoft.com/en-us/bingmaps/articles/understanding-scale-and-resolution
        /// </param>
        /// <returns></returns>
        public static string getQuadKey(double latitude, double longitude, int zoomLevel)
        {
            //Calculate the QuadKey
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            int pixelX = (int)((longitude + 180) / 360 * 256 * Math.Pow(2.0, zoomLevel));
            int pixelY = (int)((0.5 - (Math.Log((1.0 + sinLatitude) / (1.0 - sinLatitude)) / (4.0 * Math.PI))) * 256.0 * Math.Pow(2.0, zoomLevel));

            //Create the string for the quadkey
            StringBuilder quadKey = new StringBuilder();
            for (int i = zoomLevel; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if (((pixelX / 256) & mask) != 0)
                {
                    digit++;
                }
                if (((pixelY / 256) & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }

            return quadKey.ToString();
        }

        /// <summary>
        /// Converts a quadkey to a latitude and longitude. This function is only as accurate as 
        /// the Mercator projection provided by Bing Maps
        /// </summary>
        /// <param name="quadKey">quadkey of the tile in Bing Maps</param>
        /// <param name="latitude">A variable to return the latitiude through</param>
        /// <param name="longitude">A variable to return the longitude through</param>
        public static void QuadKeyToLatLong(string quadKey, out double latitude, out double longitude)
        {

            int tileX, tileY, pixelX, pixelY;
            int zoomLevel = quadKey.Length;

            QuadKeyToTileXY(quadKey, out tileX, out tileY, out zoomLevel);
            TileXYToPixelXY(tileX, tileY, out pixelX, out pixelY);
            PixelXYToLatLong(pixelX, pixelY, zoomLevel, out latitude, out longitude);

        }

        //------------------------------------------------------------------------------  
        // <copyright company="Microsoft">  
        //     Copyright (c) 2006-2009 Microsoft Corporation.  All rights reserved.  
        // </copyright>  
        //------------------------------------------------------------------------------  

        /// <summary>  
        /// BELONGS TO BING
        /// Converts a QuadKey into tile XY coordinates.  
        /// </summary>  
        /// <param name="quadKey">QuadKey of the tile.</param>  
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>  
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>  
        /// <param name="levelOfDetail">Output parameter receiving the level of detail.</param>  
        public static void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int levelOfDetail)
        {
            tileX = tileY = 0;
            levelOfDetail = quadKey.Length;
            for (int i = levelOfDetail; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (quadKey[levelOfDetail - i])
                {
                    case '0':
                        break;

                    case '1':
                        tileX |= mask;
                        break;

                    case '2':
                        tileY |= mask;
                        break;

                    case '3':
                        tileX |= mask;
                        tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }
        }

        /// <summary>  
        /// BELONGS TO BING
        /// Converts tile XY coordinates into pixel XY coordinates of the upper-left pixel  
        /// of the specified tile.  
        /// </summary>  
        /// <param name="tileX">Tile X coordinate.</param>  
        /// <param name="tileY">Tile Y coordinate.</param>  
        /// <param name="pixelX">Output parameter receiving the pixel X coordinate.</param>  
        /// <param name="pixelY">Output parameter receiving the pixel Y coordinate.</param>  
        public static void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY)
        {
            pixelX = tileX * 256;
            pixelY = tileY * 256;
        }

        /// <summary>  
        /// BELONGS TO BING
        /// Converts a pixel from pixel XY coordinates at a specified level of detail  
        /// into latitude/longitude WGS-84 coordinates (in degrees).  
        /// </summary>  
        /// <param name="pixelX">X coordinate of the point, in pixels.</param>  
        /// <param name="pixelY">Y coordinates of the point, in pixels.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <param name="latitude">Output parameter receiving the latitude in degrees.</param>  
        /// <param name="longitude">Output parameter receiving the longitude in degrees.</param>  
        public static void PixelXYToLatLong(int pixelX, int pixelY, int levelOfDetail, out double latitude, out double longitude)
        {
            double mapSize = MapSize(levelOfDetail);
            double x = (Clip(pixelX, 0, mapSize - 1) / mapSize) - 0.5;
            double y = 0.5 - (Clip(pixelY, 0, mapSize - 1) / mapSize);

            latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
            longitude = 360 * x;
        }

        /// <summary>  
        /// Belongs to Bing
        /// Clips a number to the specified minimum and maximum values.  
        /// </summary>  
        /// <param name="n">The number to clip.</param>  
        /// <param name="minValue">Minimum allowable value.</param>  
        /// <param name="maxValue">Maximum allowable value.</param>  
        /// <returns>The clipped value.</returns>  
        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>  
        /// Determines the map width and height (in pixels) at a specified level  
        /// of detail.  
        /// </summary>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>The map width and height in pixels.</returns>  
        public static uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        /// <summary>  
        /// Converts tile XY coordinates into a QuadKey at a specified level of detail.  
        /// </summary>  
        /// <param name="tileX">Tile X coordinate.</param>  
        /// <param name="tileY">Tile Y coordinate.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>A string containing the QuadKey.</returns>  
        public static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)  
        {  
            StringBuilder quadKey = new StringBuilder();  
            for (int i = levelOfDetail; i > 0; i--)  
            {  
                char digit = '0';  
                int mask = 1 << (i - 1);  
                if ((tileX & mask) != 0)  
                {  
                    digit++;  
                }  
                if ((tileY & mask) != 0)  
                {  
                    digit++;  
                    digit++;  
                }  
                quadKey.Append(digit);  
            }  
            return quadKey.ToString();  
        }  

    }
}