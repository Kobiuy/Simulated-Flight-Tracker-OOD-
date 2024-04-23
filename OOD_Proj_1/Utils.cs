using NetworkSourceSimulator;
using OOD_Proj_1.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class MathUtils
    {
        public static double GetProgress(int toftime, int lndtime, int nwtime)
        {
            double a, b;

            if (toftime > nwtime && lndtime > toftime) return 0;
            if (toftime > nwtime && lndtime < toftime && lndtime < nwtime) return 1;
            if (toftime < nwtime && lndtime < nwtime && lndtime > toftime) return 1;

            if (toftime > lndtime)
            {
                if (nwtime < toftime)
                {
                    lndtime += 24 * 60 * 60; // Add one day
                    nwtime += 24 * 60 * 60; // Add one day
                }
                else
                    lndtime += 24 * 60 * 60; // Add one day
            }
            a = nwtime - toftime;
            b = lndtime - toftime;
            double progress = a / b;

            return progress;
        }
        public static WorldPosition InterpolatePosition(double StartLatitude, double StartLongitude, Airport target, int starttime, Fligth fligth)
        {
            WorldPosition wps = new WorldPosition();
            int lndtime = DateTime.Parse(fligth.LandingTime).GetSeconds();
            int nwtime = DateTime.Now.GetSeconds();

            var progress = MathUtils.GetProgress(starttime, lndtime, nwtime);
            wps.Longitude = StartLongitude + (target.Longitude - StartLongitude) * progress;
            wps.Latitude = StartLatitude + (target.Latitude - StartLatitude) * progress;
            fligth.Latitude = (float)wps.Latitude;
            fligth.Longitude = (float)wps.Longitude;
            return wps;
        }

    }
    public static class LogManager
    {
        public static void NewRun()
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine("----------------------------------NewRun----------------------------------");
            }
        }
        public static void ChangedID(Single FromID, Single ToID)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {FromID}| ID changed to {ToID}");
            }
        }
        public static void ChangePosition(PositionUpdateArgs args, Single lat, Single lon, Single amsl)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {args.ObjectID}| Position changed from {lat}, {lon}, {amsl} to {args.Latitude}, {args.Longitude}, {args.AMSL}");
            }
        }
        public static void ContactInfo(ContactInfoUpdateArgs args, Person person)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {args.ObjectID}| Contact Info changed from {person.Phone}, {person.Email} to {args.PhoneNumber}, {args.EmailAddress}");
            }
        }
        public static void InvalidData()
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}| INVALID DATA");
            }
        }
    }
}
