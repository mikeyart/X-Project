using ChoETL;
using Newtonsoft.Json;
using SRTM;
using SRTM.Sources.NASA;
using SRTM.Sources.USGS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model_1546
{
    public class Test
    {
        public static double ReadTerrain(double latitude, double longitude)
        {
            // USGS data is immediately available, but is of a lower resolution.
            var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_USGS", new USGSSource());

            // NASA data is of a higher resolution, but requires creating an account at https://urs.earthdata.nasa.gov/users/new/.
            //var credentials = new NetworkCredential("mikeyart", "Prince5498");
            //var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_NASA", new NASASource(credentials));
            double? elevation1 = srtmData.GetElevation(latitude, longitude);
            double elevation = Convert.ToDouble(elevation1);

            return elevation;
        }

        public static double heff(double lat, double lon, double distance, double theta)
        {
            double lat2, lon2, angdist, height;
            lat *= (Math.PI / 180);
            lon *= (Math.PI / 180);

            theta *= (Math.PI / 180);
            angdist = distance / 6371;
            lat2 = Math.Asin(Math.Sin(lat) * Math.Cos(angdist) + Math.Cos(lat) * Math.Sin(angdist) * Math.Cos(theta));

            double forAtana = Math.Sin(theta) * Math.Sin(angdist) * Math.Cos(lat);
            double forAtanb = Math.Cos(angdist) - Math.Sin(lat) * Math.Sin(lat2);

            lon2 = lon + Math.Atan2(forAtana, forAtanb);

            double finalLat = lat2 * 180 / Math.PI;
            double finalLon = lon2 * 180 / Math.PI;

            height = ReadTerrain(finalLat,finalLon);
            return height;
        }

        public static double heights()
        {
            double height = 0;
            List<double> h1 = new List<double>();
            double average = 0;
            for (double theta = 0; theta < 360; theta += 10)
            {
                for (double dist = 3; dist < 15; dist += 0.09)
                {
                    height = heff(47.021573, 28.947393, dist, theta);
                    h1.Add(height);
                    average = Math.Round(h1.Average(), 0);
                }
                h1.Clear();

                double altitude = ReadTerrain(47.021573, 28.947393);
                double hant = altitude + 100;
                double hef = hant - average;
                Console.WriteLine(String.Format("Altitude: {0} For theta {1}, this height {2}", altitude, theta, hef));
            }
            return 0;
        }

    }
}