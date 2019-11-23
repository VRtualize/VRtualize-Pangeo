using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace vr_db_interaction
{
    class main
    {
        private HttpClient currClient;

        static async Task Main(string[] args)
        {
            DBConnection currDBConnection = new DBConnection();
            currDBConnection.printYeet();
            currDBConnection.open();
            currDBConnection.sampleQuery();
            imageURLRequest Test = new imageRequest("AlHEgop1yfMfViPQcjrKUd3Wduq1PqTPno4RvpsjVaxl2-EvolkG6DNyFZGUXbPD");

            Console.WriteLine(await Test.GetQuadKeyURL(44.069, -103.228, 14));

        }

    }
}
