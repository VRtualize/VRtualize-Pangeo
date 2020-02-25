using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagerUtils
{
    public static class DataRequest
    {
        public static Queue requestQueue;

        //DataRequest has a main queue that takes data requests from tiles
        //These requests are sorted into a secondary queue of requests that exist in the database and those that need to be taken from Bing's REST API
        
    }
}
