using System.Data;
using System;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using OOD_Proj_1.News;

namespace OOD_Proj_1.Products
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
        public Product Create(string txt, ProductLists productLists)
        {
            string[] words = txt.Split(',');
            return generators[words[0]].Create(words, productLists);
        }
    }

    [JsonDerivedType(typeof(CargoPlane), "CP")]
    [JsonDerivedType(typeof(PassengerPlane), "PP")]
    [JsonDerivedType(typeof(Passenger), "P")]
    [JsonDerivedType(typeof(Crew), "C")]
    [JsonDerivedType(typeof(Cargo), "CA")]
    [JsonDerivedType(typeof(Airport), "AI")]
    [JsonDerivedType(typeof(Fligth), "FL")]
    public interface IReportable
    {
        public string Accept(Media medium);
    }
    public abstract class Product // Base class of objects created by "Factory"
    {
        public string Type { get; set; }
        public ulong ID { get; set; }
    }
    public abstract class Person : Product
    {
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public abstract class Plane : Product
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
    }
    public class CargoPlane : Plane, IReportable
    {
        public CargoPlane() { }
        public float MaxLoad { get; set; }

        public CargoPlane(string type, ulong id, string serial, string country, string model, float maxload)
        {
            Type = type;
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxload;
        }

        public string Accept(Media medium)
        {
            return medium.doForCP(this);
        }
    }
    public class PassengerPlane : Plane, IReportable
    {
        public PassengerPlane() { }
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(string type, ulong id, string serial, string country, string model, ushort fcs, ushort bcs, ushort ecs)
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

        public string Accept(Media medium)
        {
            return medium.doForPP(this);
        }
    }
    public class Passenger : Person
    {
        public Passenger() { }
        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger(string type, ulong id, string name, ulong age, string phone, string email, string cclass, ulong miles)
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
        public ushort Practice { get; set; }
        public string Role { get; set; }
        public Crew(string type, ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
        {
            Type = type;
            ID = id;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }
    }
    public class Cargo : Product
    {
        public Cargo() { }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(string type, ulong id, float weight, string code, string description)
        {
            Type = type;
            ID = id;
            Weight = weight;
            Code = code;
            Description = description;
        }
    }
    public class Airport : Product, IReportable
    {
        public Airport() { }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string Country { get; set; }

        public Airport(string type, ulong id, string name, string code, float longitude, float latitude, float amsl, string country)
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

        public string Accept(Media medium)
        {
            return medium.doForArp(this);
        }
    }
    public class Fligth : Product
    {
        public Fligth() { }
        public ulong OriginAsID { get; set; }
        public ulong TargetAsID { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; set; }
        public List<ulong> CrewAsIDs { get; set; }
        public List<ulong> LoadAsIDs { get; set; }

        public Fligth(string type, ulong id, ulong originID, ulong targetID, string takeOffTime, string landingTime,
            float longitude, float latitude, float amsl, ulong planeID, List<ulong> crewAsID, List<ulong> loadaAsID)
        {
            Type = type;
            ID = id;
            OriginAsID = originID;
            TargetAsID = targetID;
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            PlaneID = planeID;
            CrewAsIDs = crewAsID;
            LoadAsIDs = loadaAsID;
            if (CrewAsIDs.Count == 0)
            {
                throw new Exception("Plane in flight " + ID.ToString() + " has no crew members. Check data source.");
            }
        }

        public virtual WorldPosition IteratePosition(Dictionary<ulong, Airport> airports)
        {
            WorldPosition wps = new WorldPosition();
            Airport start = airports[OriginAsID];
            Airport target = airports[TargetAsID];
            int toftime = DateTime.Parse(TakeOffTime).GetSeconds();
            int lndtime = DateTime.Parse(LandingTime).GetSeconds();
            int nwtime = DateTime.Now.GetSeconds();

            var progress = MathUtils.GetProgress(toftime, lndtime, nwtime);
            wps.Longitude = start.Longitude + (target.Longitude - start.Longitude) * progress;
            wps.Latitude = start.Latitude + (target.Latitude - start.Latitude) * progress;
            Latitude = (float)wps.Latitude;
            Longitude = (float)wps.Longitude;
            return wps;
        }

    }
}
