using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace DataManagerUtils
{
    /// <summary>
    /// The imageRequest class creates an HTTP Client to access image data from a
    /// Bing REST API. It takes in a longitude and latitude for the initial request
    /// to Bing's REST API. The API then returns a sample URL which is stored by 
    /// the imageRequest class. Any later calls to the imageRequest class are done
    /// using this sampleURL. This complies with Microsoft's Terms of Service. 
    /// </summary>
    public class imageURLRequest
    {
        public string exampleURL;
        private string BingMapsAPIKey;
        public string subdomain;

        public imageURLRequest(string BingMapsKey)
        {
            this.BingMapsAPIKey = BingMapsKey;
        }
        /// <summary>
        /// This function initializes the URL fro the imageURLRequest. Microsoft 
        /// requires this before use of their Bing Maps service.One request must
        /// be made for an example URL with updated subdomain before directly 
        /// accessing Bing Maps Tiles. More information can be found in the 
        /// following URL.https://docs.microsoft.com/en-us/bingmaps/rest-services/directly-accessing-the-bing-maps-tiles
        /// </summary>
        /// <returns>Success Status</returns>
        /*************************************************************************
         * 
         * **********************************************************************/
        async public Task<int> initializeURL()
        {
            HttpClient client = new HttpClient();
            string requestString = "http://dev.virtualearth.net/REST/V1/Imagery/Metadata/Road?output=json&include=ImageryProviders&key=" + this.BingMapsAPIKey;

                //while (DateTime.Now.Millisecond - start < 200) { await Task.Delay(1); }
                bool tooManyRequestFlag = false;
                do
                {
                    tooManyRequestFlag = false;
                    try
                    {

                        var content = await client.GetStringAsync(requestString);

                        //Get example URL
                        int exampleURLBegin = content.IndexOf("imageUrl") + 11;
                        int exampleURLEnd = content.IndexOf("imageUrlSubdomains");
                        this.exampleURL = content.Substring(exampleURLBegin, exampleURLEnd - (3 + exampleURLBegin));

                        //Get the subdomain
                        int subdomainsBegin = content.IndexOf("imageUrlSubdomains") + 22;
                        int subdomainsEnd = content.IndexOf("\"", subdomainsBegin);
                        this.subdomain = content.Substring(subdomainsBegin, subdomainsEnd - subdomainsBegin);
                }
                    catch (HttpRequestException e)
                    {
                        if (e.Message == "429 (Too Many Requests)")
                            tooManyRequestFlag = true;
                        else
                            throw e;
                    }
                } while (tooManyRequestFlag);
            return 0;
        }

        /// <summary>
        /// This function takes in a longitude, latitude, and zoomLevel and returns
        /// the URL to access a Bing Maps Tile of that zoomLevel that contains the 
        /// longitude and latitude requested.The Create the string for the quadkey
        /// section's code was originally created publicly by Microsoft and modified
        /// for this project.
        /// </summary>
        /// <param name="latitude"> Requested User Latitude</param>
        /// <param name="longitude"> Requested User Longitude</param>
        /// <param name="zoomLevel"> Requested Zoom Level for Tile</param>
        /// <returns>The Bing REST API url to retrieve data from</returns>
        public string GetQuadKeyURL(double latitude, double longitude, int zoomLevel)
        {
            String quadKeyURL = " ";

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

            //Create the full request URL
            quadKeyURL = this.exampleURL;
            quadKeyURL = quadKeyURL.Replace("{subdomain}", this.subdomain);
            quadKeyURL = quadKeyURL.Replace("r{quadkey}", "a" + (string)quadKey.ToString());
            quadKeyURL = quadKeyURL.Replace("{culture}", "en-US");
            return quadKeyURL;
        }

    }
}
