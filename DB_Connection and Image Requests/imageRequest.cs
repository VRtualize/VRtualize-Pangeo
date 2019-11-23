using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace vr_db_interaction
{
    /****************************************************************************
     * The imageRequest class creates an HTTP Client to access image data from a
     * Bing REST API. It takes in a longitude and latitude for the initial request
     * to Bing's REST API. The API then returns a sample URL which is stored by 
     * the imageRequest class. Any later calls to the imageRequest class are done
     * using this sampleURL. This complies with Microsoft's Terms of Service. 
     * *************************************************************************/
    class imageURLRequest
    {
        private double lowerLeftCornerLatitude;
        private double LowerLeftCornerLongitude;
        private string exampleURL;
        private string BingMapsAPIKey;
        private string subdomain;
        private HttpClient currClient;

        public imageURLRequest(string BingMapskey)
        {
            this.currClient = new HttpClient();
            this.LowerLeftCornerLongitude = LowerLeftCornerLongitude;
            this.lowerLeftCornerLatitude = lowerLeftCornerLatitude;
            this.BingMapsAPIKey = key;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            //Get the imagery sample metadata
            var content = await client.GetStringAsync("http://dev.virtualearth.net/REST/V1/Imagery/Metadata/Road?output=json&include=ImageryProviders&key=" + BingMapskey);

            //Get example URL
            int exampleURLBegin = content.IndexOf("imageUrl") + 11;
            int exampleURLEnd = content.IndexOf("imageUrlSubdomains");
            this.exampleURL = content.Substring(exampleURLBegin, exampleURLEnd - (3 + exampleURLBegin));

            //Get the subdomain
            int subdomainsBegin = content.IndexOf("imageUrlSubdomains") + 22;
            int subdomainsEnd = content.IndexOf("\"", subdomainsBegin);
            this.subdomain = content.Substring(subdomainsBegin, subdomainsEnd - subdomainsBegin);
        }

        async public Task<string> GetQuadKeyURL(double latitude, double longitude, int zoomLevel)
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

            //Create the full request URL
            quadKeyURL = this.exampleURL;
            quadKeyURL = quadKeyURL.Replace("{subdomain}", this.subdomain);
            quadKeyURL = quadKeyURL.Replace("r{quadkey}", "a" + (string)quadKey.ToString());
            quadKeyURL = quadKeyURL.Replace("{culture}", "en-US");
            return quadKeyURL;
        }

    }
}
