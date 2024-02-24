using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    internal class CargoPlane
    {
        UInt64 ID;
        string Serial;
        string Country;
        string Model;
        Single MaxLoad;

        public CargoPlane(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Serial = words[2];
            Country = words[3];
            Model = words[4];
            MaxLoad = Single.Parse(words[5]);
        }
    }
    internal class PassangerPlane
    {
        UInt64 ID;
        string Serial;
        string Country;
        string Model;
        UInt16 FirstClassSize;
        UInt16 BusinessClassSize;
        UInt16 EconomyClassSize;

        public PassangerPlane(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Serial = words[2];
            Country = words[3];
            Model = words[4];
            FirstClassSize = UInt16.Parse(words[5]);
            BusinessClassSize = UInt16.Parse(words[6]);
            EconomyClassSize = UInt16.Parse(words[7]);
        }
    }
    internal class Passanger
    {
        UInt64 ID;
        string Name;
        UInt64 Age;
        string Phone;
        string Email;
        string Class;
        UInt64 Miles;

        public Passanger(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Age = UInt64.Parse(words[3]);
            Phone = words[4];
            Email = words[5];
            Class = words[6];
            Miles = UInt64.Parse(words[7]);
        }
    }
    internal class Crew 
    {
        UInt64 ID;
        string Name;
        UInt64 Age;
        string Phone;
        string Email;
        UInt16 Practice;
        string Role;
        public Crew(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Age = UInt64.Parse(words[3]);
            Phone = words[4];
            Email = words[5];
            Practice = UInt16.Parse(words[6]);
            Role = words[7];
        }
    }
    internal class Cargo 
    {
        UInt64 ID;
        Single Weight;
        string Code;
        string Description;
        public Cargo(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Weight = Single.Parse(words[2]);
            Code = words[3];
            Description = words[4];
        }

    }

    internal class Airport
    {
        UInt64 ID;
        string Name;
        string Code;
        Single Longitude;
        Single Latitude;
        Single AMSL;
        string Country;
        public Airport(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Code = words[3];
            Longitude = UInt64.Parse(words[4]);
            Latitude = UInt64.Parse(words[5]);
            AMSL = UInt64.Parse(words[6]);
            Country = words[7];
        }
    }
    internal class Fligth
    {
        UInt64 ID;
        UInt64 OriginAsID;
        string TakeOffTime;
        string LandingTime;
        Single Longitude;
        Single Latitude;
        Single AMSL;
        UInt64 PlaneID;
        UInt64[] CrewAsIDs;
        UInt64[] LoadAsIDs;
        public Fligth(string txt)
        {
            int i = 9; //Index of the first CrewID
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            OriginAsID = UInt64.Parse(words[2]);
            TakeOffTime = words[3];
            LandingTime = words[4];
            Longitude = Single.Parse(words[5]);
            Latitude = Single.Parse(words[6]);
            AMSL= Single.Parse(words[7]);
            PlaneID = UInt64.Parse(words[8]);
            string[] crewIDs = words[9].Replace('[', ' ').Replace(']', ' ').Trim().Split(',');
            foreach (string member in crewIDs)
            {
                CrewAsIDs.Append(UInt64.Parse(member));
            }
            string[] LoadIDs = words[9].Replace('[', ' ').Replace(']', ' ').Trim().Split(',');
            foreach (string LoadID in LoadIDs)
            {
                LoadAsIDs.Append(UInt64.Parse(LoadID));
            }
        }
    }
}
