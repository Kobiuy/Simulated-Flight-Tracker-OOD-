using System.Data;
using System;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using OOD_Proj_1.News;
using ExCSS;
using NetTopologySuite.Mathematics;
using Avalonia;

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

            {"Crew", new CrewGenerator() },
            {"PassengerPlane", new PassengerGenerator() },
            {"Cargo", new CargoGenerator() },
            {"CargoPlane", new CargoPlaneGenerator() },
            {"PassangerPlane", new PassengerPlaneGenerator() },
            {"Airport", new AirportGenerator() },
            {"Flight", new FlightGenerator() },
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
    [JsonDerivedType(typeof(Flight), "FL")]
    public interface IReportable
    {
        public string Accept(Media medium);
    }
    public abstract class Product // Base class of objects created by "Factory"
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public Dictionary<string, dynamic> Properties { get; set; }
        public Dictionary<string, Func<string, IComparable>> Parser = new Dictionary<string, Func<string, IComparable>>
        {
            {"ID", (string data)=>UInt64.Parse(data)}
        };

        Dictionary<string, Func<IComparable, IComparable, bool>> operators = new Dictionary<string, Func<IComparable, IComparable, bool>>()
        {
            {"!=", (str, str2) => str.CompareTo(str2) != 0 },
            {"=",  (str, str2) => str.CompareTo(str2) == 0 },
            {">=", (str, str2) => str.CompareTo(str2) >= 0 },
            {"<=",  (str, str2) => str.CompareTo(str2) <= 0 }

        };
        public bool IsQueryTrue(string query)
        {
            var splitted = query.Split(' ');
            return operators[splitted[1]](Properties[splitted[0]], Parser[splitted[0]](splitted[2]));
        }
    }
    public abstract class Person : Product
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Person()
        {
            Parser.Add("Name", (string data) => data);
            Parser.Add("Age", (string data) => UInt64.Parse(data));
            Parser.Add("Phone", (string data) => data);
            Parser.Add("Email", (string data) => data);
        }
    }
    public abstract class Plane : Product
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }

        public Plane()
        {
            Parser.Add("Serial", (string data) => data);
            Parser.Add("Country", (string data) => data);
            Parser.Add("Model", (string data) => data);
        }
    }
    public class CargoPlane : Plane, IReportable
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
            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Serial", Serial },
                { "Country", Country },
                { "Model", Model },
                { "MaxLoad", MaxLoad }
            };
            Parser.Add("MaxLoad", (string data) => Single.Parse(data));
        }

        public string Accept(Media medium)
        {
            return medium.doForCP(this);
        }
    }
    public class PassengerPlane : Plane, IReportable
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

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Serial", Serial },
                { "Country", Country },
                { "Model", Model },
                { "FirstClassSize", FirstClassSize },
                { "BusinessClassSize", BusinessClassSize },
                { "EconomyClassSize", EconomyClassSize }
            };
            Parser.Add("FirstClassSize", (string data) => UInt16.Parse(data));
            Parser.Add("BusinessClassSize", (string data) => UInt16.Parse(data));
            Parser.Add("EconomyClassSize", (string data) => UInt16.Parse(data));

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

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Name", Name },
                { "Age", Age },
                { "Phone", Phone },
                { "Email", Email },
                { "Class", Class },
                { "Miles", Miles }
            };
            Parser.Add("Miles", (string data) => UInt64.Parse(data));
            Parser.Add("Class", (string data) => data);
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
            Practice = practice;
            Role = role;

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Name", Name },
                { "Age", Age },
                { "Phone", Phone },
                { "Email", Email },
                { "Practice", Practice },
                { "Role", Role }
            };
            Parser.Add("Practice", (string data) => UInt16.Parse(data));
            Parser.Add("Role", (string data) => data);
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

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Weight", Weight },
                { "Code", Code },
                { "Description", Description },
            };

            Parser.Add("Weight", (string data) => Single.Parse(data));
            Parser.Add("Code", (string data) => data);
            Parser.Add("Description", (string data) => data);
        }
    }
    public class Airport : Product, IReportable
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

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Name", Name },
                { "Code", Code },
                { "Longitude", Longitude },
                { "Latitude", Latitude },
                { "AMSL", AMSL },
                { "Country", Country }
            };
            Parser.Add("Name", (string data) => data);
            Parser.Add("Code", (string data) => data);
            Parser.Add("Longitude", (string data) => Single.Parse(data));
            Parser.Add("Latitude", (string data) => Single.Parse(data));
            Parser.Add("AMSL", (string data) => Single.Parse(data));
            Parser.Add("Country", (string data) => data);
        }

        public string Accept(Media medium)
        {
            return medium.doForArp(this);
        }
    }
    public class Flight : Product
    {
        public Flight() { }
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

        public Flight(string type, UInt64 id, UInt64 originID, UInt64 targetID, string takeOffTime, string landingTime,
            Single longitude, Single latitude, Single amsl, UInt64 planeID, List<UInt64> crewAsID, List<UInt64> loadaAsID)
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

            Properties = new Dictionary<string, object>
            {
                { "ID", ID },
                { "OriginAsID", OriginAsID },
                { "TargetAsID", TargetAsID },
                { "Longitude", Longitude },
                { "Latitude", Latitude },
                { "AMSL", AMSL },
                { "TakeOffTime", TakeOffTime },
                { "LandingTime", LandingTime },
                { "PlaneID", PlaneID }
            };
            Parser.Add("OriginAsID", (string data) => UInt64.Parse(data));
            Parser.Add("TargetAsID", (string data) => UInt64.Parse(data));
            Parser.Add("TakeOffTime", (string data) => data);
            Parser.Add("LandingTime", (string data) => data);
            Parser.Add("Longitude", (string data) => Single.Parse(data));
            Parser.Add("Latitude", (string data) => Single.Parse(data));
            Parser.Add("AMSL", (string data) => Single.Parse(data));
            Parser.Add("PlaneID", (string data) => UInt64.Parse(data));
        }

        public virtual WorldPosition IteratePosition(Dictionary<ulong, Airport> airports)
        {
            return MathUtils.InterpolatePosition(airports[OriginAsID].Latitude, airports[OriginAsID].Longitude, airports[TargetAsID], DateTime.Parse(TakeOffTime).GetSeconds(), this);
        }
    }
}
