using System.Data;
using System;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;

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
    public abstract class Product // Base class of objects created by "Factory"
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
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

        public CargoPlane(string type, UInt64 id, string serial, string country, string model, Single maxload)
        {
            Type = type;
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxload;
        }
    }
    public class PassengerPlane : Plane
    {
        public PassengerPlane() { }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane(string type, UInt64 id, string serial, string country, string model, UInt16 fcs, UInt16 bcs, UInt16 ecs)
        {
            Type = type;
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            FirstClassSize = fcs;
            BusinessClassSize = bcs;
            EconomyClassSize = ecs;
        }
    }
    public class Passenger : Person
    {
        public Passenger() { }
        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger(string type, UInt64 id, string name, UInt64 age, string phone, string email, string cclass, UInt64 miles)
        {
            Type = type;
            ID = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = cclass;
            Miles = miles;
        }
    }
    public class Crew : Person
    {
        public Crew() { }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }
        public Crew(string type, UInt64 id, string name, UInt64 age, string phone, string email, UInt16 practice, string role)
        {
            Type = type;
            ID = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Role = role;
        }
    }
    public class Cargo : Product
    {
        public Cargo() { }
        public Single Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(string type, UInt64 id, Single weight, string code, string description)
        {
            Type = type;
            ID = id;
            Weight = weight;
            Code = code;
            Description = description;
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

        public Airport(string type, UInt64 id, string name, string code, Single longitude, Single latitude, Single amsl, string country)
        {
            Type = type;
            ID = id;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            Country = country;
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

        public Fligth(UInt64 ID, UInt64 OriginID, UInt64 TargetID, string TakeOffTime, string LandingTime,
            Single Longitude, Single Latitude, Single AMSL, UInt64 PlaneID, List<UInt64> CrewAsID, List<UInt64> LoadaAsID)
        {
            this.ID = ID;
            this.OriginAsID = OriginID;
            this.TargetAsID = TargetID;
            this.TakeOffTime = TakeOffTime;
            this.LandingTime = LandingTime;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.AMSL = AMSL;
            this.PlaneID = PlaneID;
            this.CrewAsIDs = CrewAsID;
            this.LoadAsIDs = LoadaAsID;
            if (CrewAsIDs.Count == 0)
            {
                throw new Exception("Plane in flight " + ID.ToString() + " has no crew members. Check data source.");
            }
        }

    }
}
