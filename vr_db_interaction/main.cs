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
            //DBConnection currDBConnection = new DBConnection();
            //currDBConnection.printYeet();
            //currDBConnection.open();
            imageRequest Test = new imageRequest(1.0, 1.0);

            Console.WriteLine(await Test.GetQuadKeyURL(44.069, -103.228, 14));

        }

    }
}
