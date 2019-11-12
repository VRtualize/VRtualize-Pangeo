using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInterface
{
    class MapData
    {
        private int latlongid;
        private int ncols;
        private int nrows;
        private double xllcorner;
        private double yllcorner;
        private int cellsize;
        private string byteorder;
        private byte[] elevation_data;
    }
}
