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
            var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_USGS", new USGSSource());

            //var credentials = new NetworkCredential("mikeyart", "Prince5498");
            //var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_NASA", new NASASource(credentials));

            int? elevation1 = srtmData.GetElevation(latitude, longitude);
            double elevation = Convert.ToDouble(elevation1);

            return elevation;
        }

        public static double Coords(double lat, double lon, double distance, double theta)
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

            height = ReadTerrain(finalLat, finalLon);
            return height;
        }

        public static List<double> Heff(double latitude, double longitude)
        {
            double height = 0;
            double hef = 0;
            List<double> h1 = new List<double>();
            List<double> heff = new List<double>();
            double average = 0;
            for (double theta = 0; theta < 360; theta += 10)
            {
                for (double dist = 3; dist < 15; dist += 0.09)
                {
                    height = Coords(latitude, longitude, dist, theta);
                    h1.Add(height);
                    average = Math.Round(h1.Average(), 0);
                }
                h1.Clear();

                double altitude = ReadTerrain(47.021573, 28.947393);
                double hant = altitude + 100;
                hef = hant - average;
                heff.Add(hef);
            }
            return heff;
        }

        public static double Bearing(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 *= (Math.PI / 180);
            lon1 *= (Math.PI / 180);
            lat2 *= (Math.PI / 180);
            lon2 *= (Math.PI / 180);
            double y = lon2 - lon1;

            double dX = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(y);
            double dY = Math.Cos(lat2) * Math.Sin(y);

            double Azimuth = Math.Atan2(dX, dY);

            double bearing = Azimuth * 180 / Math.PI;
            return bearing;
        }

        public static double TCA(double lat1, double lon1, double lat2, double lon2)
        {
            double bearing = Bearing(lat1, lon1, lat2, lon2);
            Dictionary<double, double> h1 = new Dictionary<double, double>();
            double height = 0;
            for (double dist = 0; dist < 15; dist += 0.1)
            {
                height = Coords(lat1, lon1, dist, bearing);
                h1.Add(dist, height);
            }
            double max = h1.Values.Max();
            var distance = Math.Round(h1.FirstOrDefault(x => x.Value == max).Key, 2);
            double ang1 = Math.Atan2(distance * 1000, max) * 180 / Math.PI;
            double tca = Math.Round(90 - ang1, 1);

            return tca;
        }


        public static double CalculateField(double sea_percent, int time, int freq, string option43, double angle, bool use_rTCA, double power, int ag12)
        {
            var nCoord = new GeoCoordinate(latitudeTx, longitudeTx);
            var eCoord = new GeoCoordinate(latitudeRx, lonigutdeRx);
            double distance = Math.Round(nCoord.GetDistanceTo(eCoord) / 1000, 1);

            double height = Heff(latitudeTx,longitudeTx);

            double rTCA = TCA(latitudeTx, longitudeTx, latitudeRx, lonigutdeRx);
            double fsl_10m, Kh2, correction;

            /*double distance = 0;
            double height = 0;*/

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
