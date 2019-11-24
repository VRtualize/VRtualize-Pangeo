using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataManagerUtils
{
    class main
    {
        static async Task Main(string[] args)
        {
            imageURLRequest Test = new imageURLRequest("9jZ0HHINNLU7kcFYeQl6~XFYbAjS6FYvcXH-iPNVsPg~AgOFExaKkIpdR0kX6qUzR-8HqOONNa9lpAF0l0d6gMoC-U7MLEddz8iMqdesaCCn");
            await Test.initializeURL();
            string testURL = Test.GetQuadKeyURL(44.069, -103.228, 14);
            DataManager tempDataManager = new DataManager();
            MapData tempMap = await tempDataManager.ElevationRequest(-83, 44, -82.5, 43.5);

        }
    }
}
