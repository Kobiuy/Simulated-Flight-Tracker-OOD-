using ExCSS;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOD_Proj_1.ManageData
{
    public static class LogManager
    {
        public static void NewRun()
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine("----------------------------------NewRun----------------------------------");
            }
        }
        public static void ChangeID(IDUpdateArgs args)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("{HH:mm:ss")} {args.ObjectID}| ID changed to {args.NewObjectID}");
            }
        }
        public static void ChangePosition(PositionUpdateArgs args)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("{HH:mm:ss")} {args.ObjectID}| Position changed to {args.Latitude}, {args.Longitude}, {args.AMSL}");
            }
        }
        public static void ContactInfo(ContactInfoUpdateArgs args)
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("{HH:mm:ss")} {args.ObjectID}| Contact Info changed to {args.PhoneNumber}, {args.EmailAddress}");
            }
        }
        public static void InvalidData()
        {
            using (var sw = new StreamWriter($"{DateTime.Now.ToString("dd.MM.yyyy")}.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("{HH:mm:ss")}| INVALID DATA");
            }
        }

    }
}
