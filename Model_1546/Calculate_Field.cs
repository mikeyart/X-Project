using SRTM;
using SRTM.Sources.NASA;
using SRTM.Sources.USGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Model_1546
{
    public class Calculate_Field
    {
        public static double ReadTerrain(double latitude, double longitude)
        {
            // USGS data is immediately available, but is of a lower resolution.
           var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_USGS", new USGSSource());

            // NASA data is of a higher resolution, but requires creating an account at https://urs.earthdata.nasa.gov/users/new/.
            //var credentials = new NetworkCredential("mikeyart", "Prince5498");
            //var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_NASA", new NASASource(credentials));

            // get elevations for some locations 46°42'14.5"N 29°35'48.3"E
            int? elevation = srtmData.GetElevation(latitude, longitude);
            double height = Convert.ToDouble(elevation);

            return height;
        }
        public static double CalculateField(double sea_percent, int time, int freq, string option43, double angle, bool use_rTCA, int rTCA, double power, int ag12)
        {
            var nCoord = new GeoCoordinate(46.421450, 29.354830);
            var eCoord = new GeoCoordinate(46.651245, 29.521465);
            double distance = Math.Round(nCoord.GetDistanceTo(eCoord)/1000,1);
            double fsl_10m, Kh2, correction, tca, t, tr;
            double height1 = ReadTerrain(46.421450,29.354830);
            double height2 = ReadTerrain(46.625247,29.527946);

            tr = Math.Atan((height1 - height2) / 1000 * distance);
            tca = t - tr;

            fsl_10m = Corrections.fsl(distance, sea_percent, time, height, freq, option43, angle, use_rTCA, rTCA, power);

            if (ag12 == 10)
                return Math.Round(fsl_10m, 2);
            else
            {
                Kh2 = 3.2 + 6.2 * Math.Log10(freq);
                correction = Kh2 * Math.Log10(ag12 * 1.0 / 10);
                return Math.Round(fsl_10m, 2);
            }
        }
        
    }
}
