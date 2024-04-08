using NetworkSourceSimulator;
using OOD_Proj_1;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OOD_Proj_1
{
    abstract class Generator // Base class of classes generating "Products"
    {
        readonly protected CultureInfo culture = CultureInfo.InvariantCulture;
        abstract public Product Create(string[] words, ProductLists  poductLists);
        abstract public Product Create(Message message, ProductLists productLists);
    }
    class PassengerPlaneGenerator : Generator
    {
        public override PassengerPlane Create(string[] words, ProductLists productLists)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Serial = words[2];
            string Country = words[3];
            string Model = words[4];
            UInt16 FirstClassSize = UInt16.Parse(words[5]);
            UInt16 BusinessClassSize = UInt16.Parse(words[6]);
            UInt16 EconomyClassSize = UInt16.Parse(words[7]);
            PassengerPlane passengerPlane = new PassengerPlane(Type, ID, Serial, Country, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);
            //StaticProductLists.passengerPlanes.Add(passengerPlane);
            productLists.passengerPlanes.Add(passengerPlane);
            return passengerPlane;
        }
        public override PassengerPlane Create(Message message, ProductLists productLists)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            string Serial = Encoding.ASCII.GetString(message.MessageBytes[15..25]).TrimEnd('\0');
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[25..28]);
            UInt16 ML = BitConverter.ToUInt16(message.MessageBytes[28..30]);
            string Model = Encoding.ASCII.GetString(message.MessageBytes[30..(30 + ML)]);
            UInt16 FirstClassSize = BitConverter.ToUInt16(message.MessageBytes[(30 + ML)..(32 + ML)]);
            UInt16 BusinessClassSize = BitConverter.ToUInt16(message.MessageBytes[(32 + ML)..(34 + ML)]);
            UInt16 EconomyClassSize = BitConverter.ToUInt16(message.MessageBytes[(34 + ML)..(36 + ML)]);
            PassengerPlane passengerPlane = new PassengerPlane("PP", ID, Serial, ISOCC, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);
            //StaticProductLists.passengerPlanes.Add(passengerPlane);
            productLists.passengerPlanes.Add(passengerPlane);
            return passengerPlane;
        }
    }
    class CargoPlaneGenerator : Generator
    {
        public override CargoPlane Create(string[] words, ProductLists productLists)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Serial = words[2];
            string Country = words[3];
            string Model = words[4];
            Single MaxLoad = Single.Parse(words[5], culture);
            CargoPlane cargoPlane = new CargoPlane(Type, ID, Serial, Country, Model, MaxLoad);
            //StaticProductLists.cargoPlanes.Add(cargoPlane);
            productLists.cargoPlanes.Add(cargoPlane);
            return cargoPlane;
        }
        public override CargoPlane Create(Message message, ProductLists productLists)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            string Serial = Encoding.ASCII.GetString(message.MessageBytes[15..25]).TrimEnd('\0');
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[25..28]);
            UInt16 ML = BitConverter.ToUInt16(message.MessageBytes[28..30]);
            string Model = Encoding.ASCII.GetString(message.MessageBytes[30..(30 + ML)]);
            Single MaxLoad = BitConverter.ToSingle(message.MessageBytes[(30 + ML)..(34 + ML)]);
            CargoPlane cargoPlane = new CargoPlane("CP", ID, Serial, ISOCC, Model, MaxLoad);
            //StaticProductLists.cargoPlanes.Add(cargoPlane);
            productLists.cargoPlanes.Add(cargoPlane);
            return cargoPlane;
        }
    }
    class PassengerGenerator : Generator
    {
        public override Passenger Create(string[] words, ProductLists productLists)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            UInt64 Age = UInt64.Parse(words[3]);
            string Phone = words[4];
            string Email = words[5];
            string Class = words[6];
            UInt64 Miles = UInt64.Parse(words[7]);
            Passenger passenger = new Passenger(Type, ID, Name, Age, Phone, Email, Class, Miles);
            //StaticProductLists.passengers.Add(passenger);
            productLists.passengers.Add(passenger);
            return passenger;
        }
        public override Passenger Create(Message message, ProductLists productLists)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes, 7);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes, 15);
            string Name = Encoding.ASCII.GetString(message.MessageBytes, 17, NL);
            UInt16 Age = BitConverter.ToUInt16(message.MessageBytes, 17 + NL);
            string Phone = Encoding.ASCII.GetString(message.MessageBytes, 19 + NL, 12);
            UInt16 EL = BitConverter.ToUInt16(message.MessageBytes, 31 + NL);
            string Email = Encoding.ASCII.GetString(message.MessageBytes[(33 + NL)..(33 + NL + EL)]);
            char Class = Encoding.ASCII.GetChars(message.MessageBytes[(33 + NL + EL)..(34 + NL + EL)])[0];
            UInt64 Miles = BitConverter.ToUInt64(message.MessageBytes[(34 + NL + EL)..(42 + EL + NL)]);
            Passenger passenger = new Passenger("P", ID, Name, Age, Phone, Email, Class.ToString(), Miles);
            //StaticProductLists.passengers.Add(passenger);
            productLists.passengers.Add(passenger);
            return passenger;
        }
    }
    class CrewGenerator : Generator
    {
        public override Crew Create(string[] words, ProductLists productLists)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            UInt64 Age = UInt64.Parse(words[3]);
            string Phone = words[4];
            string Email = words[5];
            UInt16 Practice = UInt16.Parse(words[6]);
            string Role = words[7];
            Crew crew = new Crew(Type, ID, Name, Age, Phone, Email, Practice, Role);
            //StaticProductLists.crews.Add(crew);
            productLists.crews.Add(crew);
            return crew;
        }
        public override Crew Create(Message message, ProductLists productLists)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes[7..15]);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes[15..17]);
            string Name = Encoding.ASCII.GetString(message.MessageBytes[17..(17 + NL)]);
            UInt16 Age = BitConverter.ToUInt16(message.MessageBytes[(17 + NL)..(19 + NL)]);
            string Phone = Encoding.ASCII.GetString(message.MessageBytes[(19 + NL)..(31 + NL)]);
            UInt16 EL = BitConverter.ToUInt16(message.MessageBytes[(31 + NL)..(33 + NL)]);
            string Email = Encoding.ASCII.GetString(message.MessageBytes[(33 + NL)..(33 + NL + EL)]);
            UInt16 Practice = BitConverter.ToUInt16(message.MessageBytes[(33 + NL + EL)..(35 + EL + NL)]);
            char Role = Encoding.ASCII.GetChars(message.MessageBytes[(35 + NL + EL)..(36 + NL + EL)])[0];
            Crew crew = new Crew("C", ID, Name, Age, Phone, Email, Practice, Role.ToString());
            //StaticProductLists.crews.Add(crew);
            productLists.crews.Add(crew);
            return crew;
        }
    }
    class CargoGenerator : Generator
    {
        public override Cargo Create(string[] words, ProductLists productLists)
        {
            string Type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            Single Weight = Single.Parse(words[2], culture);
            string Code = words[3];
            string Description = words[4];
            Cargo cargo = new Cargo("CA", ID, Weight, Code, Description);
            //StaticProductLists.cargos.Add(cargo);
            productLists.cargos.Add(cargo);
            return cargo;
        }
        public override Cargo Create(Message message, ProductLists productLists)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes[15..17]);
            Single Weight = BitConverter.ToSingle(message.MessageBytes[15..19]);
            string Code = Encoding.ASCII.GetString(message.MessageBytes[19..25]);
            UInt16 DL = BitConverter.ToUInt16(message.MessageBytes[25..27]);
            string Description = Encoding.ASCII.GetString(message.MessageBytes[27..(27 + DL)]);
            Cargo cargo = new Cargo("CA", ID, Weight, Code, Description);
            //StaticProductLists.cargos.Add(cargo);
            productLists.cargos.Add(cargo);
            return cargo;
        }
    }
    class AirportGenerator : Generator
    {
        public override Airport Create(string[] words, ProductLists productLists)
        {
            string type = words[0];
            UInt64 ID = UInt64.Parse(words[1]);
            string Name = words[2];
            string Code = words[3];
            Single Longitude = Single.Parse(words[4], culture);
            Single Latitude = Single.Parse(words[5], culture);
            Single AMSL = Single.Parse(words[6], culture);
            string Country = words[7];
            Airport airport = new Airport(type, ID, Name, Code, Longitude, Latitude, AMSL, Country);
            //StaticProductLists.airports.Add(airport);
            productLists.airports.Add(airport);
            return airport;
        }
        public override Airport Create(Message message, ProductLists productLists)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes[7..15]);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes[15..17]);
            string Name = Encoding.ASCII.GetString(message.MessageBytes[17..(17 + NL)]);
            string Code = Encoding.ASCII.GetString(message.MessageBytes[(17 + NL)..(20 + NL)]);
            Single Longitude = BitConverter.ToSingle(message.MessageBytes[(20 + NL)..(24 + NL)]);
            Single Latitude = BitConverter.ToSingle(message.MessageBytes[(24 + NL)..(28 + NL)]);
            Single AMSL = BitConverter.ToSingle(message.MessageBytes[(28 + NL)..(32 + NL)]);
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[(32 + NL)..(35 + NL)]);
            Airport airport = new Airport("AI", ID, Name, Code, Longitude, Latitude, AMSL, ISOCC);
            //StaticProductLists.airports.Add(airport);
            productLists.airports.Add(airport);
            return airport;
        }
    }
    class FlightGenerator : Generator
    {
        public override Fligth Create(string[] words, ProductLists productLists)
        {
            string type = words[0];
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
            Fligth fligth = new Fligth(type, ID, OriginAsID, TargetAsID, TakeOffTime, LandingTime, Longitude, Latitude, AMSL, PlaneID, CrewAsIDs, LoadAsIDs);
            //StaticProductLists.fligths.Add(fligth);
            productLists.fligths.Add(fligth);
            return fligth;
        }
        public override Fligth Create(Message message, ProductLists productLists)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes, 7);
            UInt64 OriginID = BitConverter.ToUInt64(message.MessageBytes[15..23]);
            UInt64 TargetID = BitConverter.ToUInt64(message.MessageBytes[23..31]);
            Int64 TakeOff = BitConverter.ToInt64(message.MessageBytes[31..39]);
            string TO = DateTime.UnixEpoch.AddMilliseconds(TakeOff).ToString("HH:mm");
            Int64 Landing = BitConverter.ToInt64(message.MessageBytes[39..47]);
            string LD = DateTime.UnixEpoch.AddMilliseconds(Landing).ToString("HH:mm");
            UInt64 PlaneID = BitConverter.ToUInt64(message.MessageBytes[47..55]);
            UInt16 CC = BitConverter.ToUInt16(message.MessageBytes[55..57]);
            List<UInt64> Crew = new List<ulong>();
            for (int i = 0; i < CC; i++)
            {
                Crew.Add(BitConverter.ToUInt64(message.MessageBytes[(57 + i * 8)..(57 + 8 + i * 8)]));
            }
            UInt16 PCC = BitConverter.ToUInt16(message.MessageBytes[(57 + 8 * CC)..(59 + 8 * CC)]);
            List<UInt64> Load = new List<ulong>();
            int temp;
            for (int i = 0; i < PCC; i++)
            {
                temp = 59 + i * 8 + 8 * CC;
                Load.Add(BitConverter.ToUInt64(message.MessageBytes, temp));
            }
            Fligth fligth = new Fligth("FL", ID, OriginID, TargetID, TO, LD, 0, 0, 0, PlaneID, Crew, Load);
            //StaticProductLists.fligths.Add(fligth);
            productLists.fligths.Add(fligth);
            return fligth;
        }
    }
}