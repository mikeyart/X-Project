using SRTM;
using SRTM.Sources.NASA;
using SRTM.Sources.USGS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Terrain_Reading
{
    class Program
    {
        static void Main(string[] args)
        {
            /* USGS data is immediately available, but is of a lower resolution.
            var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_USGS", new USGSSource());*/

            // NASA data is of a higher resolution, but requires creating an account at https://urs.earthdata.nasa.gov/users/new/.
            var credentials = new NetworkCredential("mikeyart", "Prince5498");
            var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_NASA", new NASASource(credentials));

            // get elevations for some locations 46°42'14.5"N 29°35'48.3"E
            int? elevation = srtmData.GetElevation(46.421450, 29.354830);
            Console.WriteLine("Elevation of Innsbruck: {0}m", elevation);

        }
    }
}
