using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OOD_Proj_1
{
    abstract class Generator // Base class of classes generating "Products"
    {
        readonly protected CultureInfo culture = CultureInfo.InvariantCulture;
        abstract public Product Create(string[] words);
    }
    class PassengerPlaneGenerator : Generator
    {
        public override PassengerPlane Create(string[] words)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Serial = words[2];
            string Country = words[3];
            string Model = words[4];
            UInt16 FirstClassSize = UInt16.Parse(words[5]);
            UInt16 BusinessClassSize = UInt16.Parse(words[6]);
            UInt16 EconomyClassSize = UInt16.Parse(words[7]);
            return new PassengerPlane(Type, ID, Serial, Country, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);
        }
    }
    class CargoPlaneGenerator : Generator
    {
        public override CargoPlane Create(string[] words)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Serial = words[2];
            string Country = words[3];
            string Model = words[4];
            Single MaxLoad = Single.Parse(words[5], culture);
            return new CargoPlane(Type, ID, Serial, Country, Model, MaxLoad);
        }
    }
    class PassengerGenerator : Generator
    {
        public override Passenger Create(string[] words)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            UInt64 Age = UInt64.Parse(words[3]);
            string Phone = words[4];
            string Email = words[5];
            string Class = words[6];
            UInt64 Miles = UInt64.Parse(words[7]);
            return new Passenger(Type, ID, Name, Age, Phone, Email, Class, Miles);
        }
    }
    class CrewGenerator : Generator
    {
        public override Crew Create(string[] words)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            UInt64 Age = UInt64.Parse(words[3]);
            string Phone = words[4];
            string Email = words[5];
            UInt16 Practice = UInt16.Parse(words[6]);
            string Role = words[7];
            return new Crew(Type, ID, Name, Age, Phone, Email, Practice, Role);
        }
    }
    class CargoGenerator : Generator
    {
        public override Cargo Create(string[] words)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            Single Weight = Single.Parse(words[2], culture);
            string Code = words[3];
            string Description = words[4];
            return new Cargo(Type, ID, Weight, Code, Description);
        }
    }
    class AirportGenerator : Generator
    {
        public override Airport Create(string[] words)
        {
            string type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            string Code = words[3];
            Single Longitude = Single.Parse(words[4]);
            Single Latitude = Single.Parse(words[5]);
            Single AMSL = Single.Parse(words[6]);
            string Country = words[7];
            return new Airport(type, ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
    }
    class FlightGenerator : Generator
    {
        public override Fligth Create(string[] words)
        {
            UInt64 ID = UInt64.Parse(words[1]);
            UInt64 OriginAsID = UInt64.Parse(words[2]);
            UInt64 TargetAsID = UInt64.Parse(words[3]);
            string TakeOffTime = words[4];
            string LandingTime = words[5];
            Single Longitude = Single.Parse(words[6], culture);
            Single Latitude = Single.Parse(words[7], culture);
            Single AMSL = Single.Parse(words[8], culture);
            UInt64 PlaneID = UInt64.Parse(words[9]);
            List<UInt64> CrewAsIDs = words[10].ToUInt64List();
            List<UInt64> LoadAsIDs = words[11].ToUInt64List();
            return new Fligth();
        }
    }
}
