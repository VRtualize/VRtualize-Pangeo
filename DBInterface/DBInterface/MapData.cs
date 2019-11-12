using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInterface
{
    /// <summary>
    /// Class <c>MapData</c> contains all information for a Map chunk.
    /// </summary>
    public class MapData
    {
        private int latlongid;
        private int ncols;
        private int nrows;
        private double xllcorner;
        private double yllcorner;
        private double cellsize;
        private string byteorder;
        private byte[] elevation_data;

        /// <summary>
        /// Constructor to initialize class variables.
        /// </summary>
        /// <param name="latlongid">id from the database</param>
        /// <param name="ncols">number of columns</param>
        /// <param name="nrows">number of rows</param>
        /// <param name="xllcorner">lower left latitude coordinate</param>
        /// <param name="yllcorner">lower left longtitude coordinate</param>
        /// <param name="cellsize">size of a cell</param>
        /// <param name="byteorder">byteorder</param>
        /// <param name="elevation_data">elevation height data</param>
        public MapData(int latlongid = 0, int ncols = 0, int nrows = 0, double xllcorner = 0.0, double yllcorner = 0.0, double cellsize = 0, string byteorder = "", byte[] elevation_data = null)
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

        public int GetLatLongId()
        {
            return this.latlongid;
        }
    }
}
