using System.Globalization;

namespace OOD_Proj_1
{
    class Factory
    {
        Dictionary<string, Generator> generators = new Dictionary<string, Generator>()
        {
            {"C", new CrewGenerator() },
            {"P", new PassangerGenerator() },
            {"CA", new CargoGenerator() },
            {"CP", new CargoPlaneGenerator() },
            {"PP", new PassangerPlaneGenerator() },
            {"AI", new AirportGenerator() },
            {"FL", new FlightGenerator() },
        };
        public Product Create(string txt)
        {
            string[] words = txt.Split(',');
            return generators[words[0]].Create(txt);
        }
    }
    public abstract class Product
    {
        readonly protected CultureInfo culture = CultureInfo.InvariantCulture;
    }
    internal class CargoPlane : Product
    {
        public UInt64 ID;
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
            MaxLoad = Single.Parse(words[5], culture);
        }
    }
    internal class PassangerPlane : Product
    {
        public UInt64 ID;
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
    internal class Passanger : Product
    {
        public UInt64 ID;
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
    internal class Crew : Product
    {
        public UInt64 ID;
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
    internal class Cargo : Product
    {
        public UInt64 ID;
        Single Weight;
        string Code;
        string Description;
        public Cargo(string txt)
        {
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            Weight = Single.Parse(words[2], culture);
            Code = words[3];
            Description = words[4];
        }

    }

    internal class Airport : Product
    {
        public UInt64 ID;
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
            Longitude = Single.Parse(words[4], culture);
            Latitude = Single.Parse(words[5], culture);
            AMSL = Single.Parse(words[6], culture);
            Country = words[7];
        }
    }
    internal class Fligth : Product
    {
        public UInt64 ID;
        UInt64 OriginAsID;
        UInt64 TargetAsID;
        string TakeOffTime;
        string LandingTime;
        Single Longitude;
        Single Latitude;
        Single AMSL;
        UInt64 PlaneID;
        List<UInt64>CrewAsIDs = new List<UInt64>();
        List<UInt64> LoadAsIDs = new List<UInt64>();
        public Fligth(string txt)
        {
            int i = 9; //Index of the first CrewID
            string[] words = txt.Split(",");
            ID = UInt64.Parse(words[1]);
            OriginAsID = UInt64.Parse(words[2]);
            TargetAsID = UInt64.Parse(words[3]);
            TakeOffTime = words[4];
            LandingTime = words[5];
            Longitude = Single.Parse(words[6], culture);
            Latitude = Single.Parse(words[7], culture);
            AMSL= Single.Parse(words[8],culture);
            PlaneID = UInt64.Parse(words[9]);
            string[] crewIDs = words[10].Replace('[', ' ').Replace(']', ' ').Trim().Split(';');
            foreach (string member in crewIDs)
            {
                CrewAsIDs.Add(UInt64.Parse(member));
            }
            string[] LoadIDs = words[11].Replace('[', ' ').Replace(']', ' ').Trim().Split(';');
            foreach (string LoadID in LoadIDs)
            {
                LoadAsIDs.Append(UInt64.Parse(LoadID));
            }
        }
    }
}
