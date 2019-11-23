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

        static async Task Main(string[] args)
        {
            imageURLRequest Test = new imageURLRequest("AlHEgop1yfMfViPQcjrKUd3Wduq1PqTPno4RvpsjVaxl2-EvolkG6DNyFZGUXbPD");
            await Test.initializeURL();
            Console.WriteLine(Test.GetQuadKeyURL(44.069, -103.228, 14));

        }

    }
}
