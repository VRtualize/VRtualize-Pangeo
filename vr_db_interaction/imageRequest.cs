using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace vr_db_interaction
{
    class imageRequest
    {
        private double lowerLeftCornerLatitude;
        private double LowerLeftCornerLongitude;
        private string URLExample;
        private HttpClient currClient;
        public imageRequest(double lowerLeftCornerLatitude, double LowerLeftCornerLongitude)
        {
            this.currClient = new HttpClient();
            this.LowerLeftCornerLongitude = LowerLeftCornerLongitude;
            this.lowerLeftCornerLatitude = lowerLeftCornerLatitude;
        }

        async public Task<string> GetQuadKeyURL(double latitude, double longitude, int zoomLevel)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            //Get the imagery sample metadata
            var content = await client.GetStringAsync("http://dev.virtualearth.net/REST/V1/Imagery/Metadata/Road?output=json&include=ImageryProviders&key=AlHEgop1yfMfViPQcjrKUd3Wduq1PqTPno4RvpsjVaxl2-EvolkG6DNyFZGUXbPD");

            //Get example URL
            int exampleURLBegin = content.IndexOf("imageUrl") + 11;
            int exampleURLEnd = content.IndexOf("imageUrlSubdomains");
            string exampleURL = content.Substring(exampleURLBegin, exampleURLEnd - (3 + exampleURLBegin));

            //Get the subdomain
            int subdomainsBegin = content.IndexOf("imageUrlSubdomains") + 22;
            int subdomainsEnd = content.IndexOf("\"", subdomainsBegin);
            string subdomain = content.Substring(subdomainsBegin, subdomainsEnd - subdomainsBegin);

            //Calculate the QuadKey
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            int pixelX = (int)((longitude + 180) / 360 * 256 * Math.Pow(2.0, zoomLevel));
            int pixelY = (int)((0.5 - (Math.Log((1.0 + sinLatitude) / (1.0 - sinLatitude)) / (4.0 * Math.PI))) * 256.0 * Math.Pow(2.0, zoomLevel));

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
            exampleURL = exampleURL.Replace("{subdomain}", subdomain);
            exampleURL = exampleURL.Replace("r{quadkey}", "a" + (string)quadKey.ToString());
            exampleURL = exampleURL.Replace("{culture}", "en-US");
            return exampleURL;
        }

    }
}
