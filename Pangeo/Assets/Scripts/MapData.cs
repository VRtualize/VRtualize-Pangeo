using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagerUtils
{
    /// <summary>
    /// Class <c>MapData</c> contains all information for a Map chunk.
    /// </summary>
    public class MapData
    {
        public int latlongid;
        public int ncols;
        public int nrows;
        public double xulcorner;
        public double yulcorner;
        public double cellsize;
        public string byteorder;
        public byte[] elevation_data;

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
        public MapData(int latlongid = 0, int ncols = 0, int nrows = 0, double xulcorner = 0.0, double yulcorner = 0.0, double cellsize = 0, string byteorder = "", byte[] elevation_data = null)
        {
            this.latlongid = latlongid;
            this.ncols = ncols;
            this.nrows = nrows;
            this.xulcorner = xulcorner;
            this.yulcorner = yulcorner;
            this.cellsize = cellsize;
            this.byteorder = byteorder;
            this.elevation_data = elevation_data;
        }

        /// <summary>
        /// Getter for the LatLongId
        /// </summary>
        /// <returns>latlongid</returns>
        public int GetLatLongId()
        {
            return this.latlongid;
        }

        /// <summary>
        /// Getter for the ncols.
        /// </summary>
        /// <returns>number of columns</returns>
        public int GetNCols()
        {
            return this.ncols;
        }

        /// <summary>
        /// Getter for nrows
        /// </summary>
        /// <returns>number of rows</returns>
        public int GetNRows()
        {
            return this.nrows;
        }

        /// <summary>
        /// Getter for xllcorner
        /// </summary>
        /// <returns>x-coordinate of lower left corner</returns>
        public double GetXllcorner()
        {
            return this.xulcorner;
        }

        /// <summary>
        /// Getter for yllcorner
        /// </summary>
        /// <returns>y-coordinate of lower left corner</returns>
        public double GetYllcorner()
        {
            return this.yulcorner;
        }

        /// <summary>
        /// Getter for cellsize
        /// </summary>
        /// <returns>cellsize</returns>
        public double GetCellsize()
        {
            return this.cellsize;
        }

        /// <summary>
        /// Getter for byteorder
        /// </summary>
        /// <returns>byteorder</returns>
        public string GetByteorder()
        {
            return this.byteorder;
        }

        /// <summary>
        /// Getter for elevation_data
        /// </summary>
        /// <returns>elevation data</returns>
        public byte[] GetElevationData()
        {
            return this.elevation_data.ToArray();
        }
    }
}