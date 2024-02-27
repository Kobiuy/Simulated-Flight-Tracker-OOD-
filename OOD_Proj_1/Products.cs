using System.Globalization;
using System.Text.Json.Serialization;

namespace OOD_Proj_1
{
    public class Factory
    {
        Dictionary<string, Generator> generators = new Dictionary<string, Generator>()
        {
            {"C", new CrewGenerator() },
            {"P", new PassengerGenerator() },
            {"CA", new CargoGenerator() },
            {"CP", new CargoPlaneGenerator() },
            {"PP", new PassengerPlaneGenerator() },
            {"AI", new AirportGenerator() },
            {"FL", new FlightGenerator() },
        };
        public Product Create(string txt)
        {
            string[] words = txt.Split(',');
            return generators[words[0]].Create(words);
        }
    }
    [JsonDerivedType(typeof(CargoPlane), "CP")]
    [JsonDerivedType(typeof(PassengerPlane), "PP")]
    [JsonDerivedType(typeof(Passenger), "P")]
    [JsonDerivedType(typeof(Crew), "C")]
    [JsonDerivedType(typeof(Cargo), "CA")]
    [JsonDerivedType(typeof(Airport), "AI")]
    [JsonDerivedType(typeof(Fligth), "FL")]
    public abstract class Product
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        readonly protected CultureInfo culture = CultureInfo.InvariantCulture;
    }
    public abstract class Person : Product
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public abstract class Plane : Product
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
    }
    public class CargoPlane : Plane
    {
        public CargoPlane() { }

        public Single MaxLoad { get; set; }

        public CargoPlane(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Serial = words[2];
            Country = words[3];
            Model = words[4];
            MaxLoad = Single.Parse(words[5], culture);
        }
    }
    public class PassengerPlane : Plane
    {
        public PassengerPlane() { }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Serial = words[2];
            Country = words[3];
            Model = words[4];
            FirstClassSize = UInt16.Parse(words[5]);
            BusinessClassSize = UInt16.Parse(words[6]);
            EconomyClassSize = UInt16.Parse(words[7]);
        }
    }
    public class Passenger : Person
    {
        public Passenger() { }
        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Age = UInt64.Parse(words[3]);
            Phone = words[4];
            Email = words[5];
            Class = words[6];
            Miles = UInt64.Parse(words[7]);
        }
    }
    public class Crew : Person
    {
        public Crew() { }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }
        public Crew(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Age = UInt64.Parse(words[3]);
            Phone = words[4];
            Email = words[5];
            Practice = UInt16.Parse(words[6]);
            Role = words[7];
        }
    }
    public class Cargo : Product
    {
        public Cargo() { }
        public Single Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Cargo(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Weight = Single.Parse(words[2], culture);
            Code = words[3];
            Description = words[4];
        }

    }

    public class Airport : Product
    {
        public Airport() { }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public string Country { get; set; }

        public Airport(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            Name = words[2];
            Code = words[3];
            Longitude = Single.Parse(words[4], culture);
            Latitude = Single.Parse(words[5], culture);
            AMSL = Single.Parse(words[6], culture);
            Country = words[7];
        }
    }
    public class Fligth : Product
    {
        public Fligth() { }
        public UInt64 OriginAsID { get; set; }
        public UInt64 TargetAsID { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public List<UInt64> CrewAsIDs { get; set; }
        public List<UInt64> LoadAsIDs { get; set; }
        public Fligth(string[] words)
        {
            Type = words[0];
            ID = UInt64.Parse(words[1]);
            OriginAsID = UInt64.Parse(words[2]);
            TargetAsID = UInt64.Parse(words[3]);
            TakeOffTime = words[4];
            LandingTime = words[5];
            Longitude = Single.Parse(words[6], culture);
            Latitude = Single.Parse(words[7], culture);
            AMSL = Single.Parse(words[8], culture);
            PlaneID = UInt64.Parse(words[9]);
            CrewAsIDs = words[10].ToUInt64List();
            LoadAsIDs = words[11].ToUInt64List();
        }
    }
}
