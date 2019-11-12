using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInterface
{
    public class MapData
    {
        private int latlongid;
        private int ncols;
        private int nrows;
        private double xllcorner;
        private double yllcorner;
        private int cellsize;
        private string byteorder;
        private byte[] elevation_data;

        public MapData(int latlongid = 0, int ncols = 0, int nrows = 0, double xllcorner = 0.0, double yllcorner = 0.0, int cellsize = 0, string byteorder = "", byte[] elevation_data = null)
        {
            this.latlongid = latlongid;
            this.ncols = ncols;
            this.nrows = nrows;
            this.xllcorner = xllcorner;
            this.yllcorner = yllcorner;
            this.cellsize = cellsize;
            this.byteorder = byteorder;
            this.elevation_data = elevation_data;
        }
    }
}
