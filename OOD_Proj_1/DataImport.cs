using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOD_Proj_1
{
    internal class DataImporter
    {
        Dictionary<string, Importer> Importers = new Dictionary<string, Importer>()
        {
            { "FILE", new FileImporter() },
            { "SIM_SERVER", new ServerImporter() },
        };
        public List<Product> ImportData()
        {
            return Importers[Settings.DataSource].Import();
        }
    }
    public abstract class Importer // Class inherited by classes importing data
    {
        protected Factory factory = new();
        abstract public List<Product> Import();
    }
    public class FileImporter : Importer
    {
        public override List<Product> Import()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var sr = new StreamReader(Settings.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        Product product = factory.Create(sr.ReadLine());
                        products.Add(product);
                        ProductList.Products.Add(product);
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Error in DataSource");
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException occured while reading from DataSource");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
    }
    public class ServerImporter : Importer
    {
        public override List<Product> Import()
        {
            ServerSimulator.StartServer();
            return ProductList.Products;
        }

        public void Parser(Message message)
        {
            Dictionary<string, Builder> builders = new Dictionary<string, Builder>()
            {
                            { "NCR", new BuildCrew() },
                            { "NPA", new BuildPassenger() },
                            { "NCA", new BuildCargo() },
                            { "NPP", new BuildPassengerPlane() },
                            { "NCP", new BuildCargoPlane() },
                            { "NAI", new BuildAirport() },
                            { "NFL", new BuildFlight() },
            };

            builders[Encoding.ASCII.GetString(message.MessageBytes[0..3])].build(message);
        }
    }

    public abstract class Builder
    {
        abstract public void build(Message message);
    }
    public class BuildCrew : Builder
    {
        public override void build(Message message)
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
            ProductList.Products.Add(new Crew("C", ID, Name, Age, Phone, Email, Practice, Role.ToString()));
        }
    }
    public class BuildPassenger : Builder
    {
        public override void build(Message message)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes, 7);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes, 15);
            string Name = Encoding.ASCII.GetString(message.MessageBytes, 17, NL);
            UInt16 Age = BitConverter.ToUInt16(message.MessageBytes, 17 + NL);
            string Phone = Encoding.ASCII.GetString(message.MessageBytes, 19 + NL, 12);
            UInt16 EL = BitConverter.ToUInt16(message.MessageBytes, 31 + NL);
            string Email = Encoding.ASCII.GetString(message.MessageBytes[(33 + NL)..(33 + NL + EL)]);
            char Class = Encoding.ASCII.GetChars(message.MessageBytes[(33 + NL + EL)..(34 + NL + EL)])[0];
            UInt64 Miles = BitConverter.ToUInt64(message.MessageBytes[(33 + NL + EL)..(42 + EL + NL)]);
            ProductList.Products.Add(new Passenger("P", ID, Name, Age, Phone, Email, Class.ToString(), Miles));
        }
    }
    public class BuildCargo : Builder
    {
        public override void build(Message message)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes[15..17]);
            Single Weight = BitConverter.ToSingle(message.MessageBytes[15..19]);
            string Code = Encoding.ASCII.GetString(message.MessageBytes[19..25]);
            UInt16 DL = BitConverter.ToUInt16(message.MessageBytes[25..27]);
            string Description = Encoding.ASCII.GetString(message.MessageBytes[27..(27 + DL)]);
            ProductList.Products.Add(new Cargo("CA", ID, Weight, Code, Description));

        }
    }
    public class BuildCargoPlane : Builder
    {
        public override void build(Message message)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            string Serial = Encoding.ASCII.GetString(message.MessageBytes[15..25]);
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[25..28]);
            UInt16 ML = BitConverter.ToUInt16(message.MessageBytes[28..30]);
            string Model = Encoding.ASCII.GetString(message.MessageBytes[30..(30 + ML)]);
            Single MaxLoad = BitConverter.ToSingle(message.MessageBytes[(30 + ML)..(34 + ML)]);
            ProductList.Products.Add(new CargoPlane("CP", ID, Serial, ISOCC, Model, MaxLoad));

        }
    }
    public class BuildPassengerPlane : Builder
    {
        public override void build(Message message)
        {
            UInt16 ID = BitConverter.ToUInt16(message.MessageBytes[7..15]);
            string Serial = Encoding.ASCII.GetString(message.MessageBytes[15..25]).Replace("\u0000","");
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[25..28]);
            UInt16 ML = BitConverter.ToUInt16(message.MessageBytes[28..30]);
            string Model = Encoding.ASCII.GetString(message.MessageBytes[30..(30 + ML)]);
            UInt16 FirstClassSize = BitConverter.ToUInt16(message.MessageBytes[(30 + ML)..(32 + ML)]);
            UInt16 BusinessClassSize = BitConverter.ToUInt16(message.MessageBytes[(32 + ML)..(34 + ML)]);
            UInt16 EconomyClassSize = BitConverter.ToUInt16(message.MessageBytes[(34 + ML)..(36 + ML)]);
            ProductList.Products.Add(new PassengerPlane("PP", ID, Serial, ISOCC, Model, FirstClassSize, BusinessClassSize, EconomyClassSize));

        }
    }
    public class BuildAirport : Builder
    {
        public override void build(Message message)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes[7..15]);
            UInt16 NL = BitConverter.ToUInt16(message.MessageBytes[15..17]);
            string Name = Encoding.ASCII.GetString(message.MessageBytes[17..(17 + NL)]);
            string Code = Encoding.ASCII.GetString(message.MessageBytes[(17 + NL)..(20 + NL)]);
            Single Longitude = BitConverter.ToSingle(message.MessageBytes[(20 + NL)..(24 + NL)]);
            Single Latitude = BitConverter.ToSingle(message.MessageBytes[(24 + NL)..(28 + NL)]);
            Single AMSL = BitConverter.ToSingle(message.MessageBytes[(28 + NL)..(32 + NL)]);
            string ISOCC = Encoding.ASCII.GetString(message.MessageBytes[(32 + NL)..(35 + NL)]);
            ProductList.Products.Add(new Airport("AI", ID, Name, Code, Longitude, Latitude, AMSL, ISOCC));

        }
    }
    public class BuildFlight : Builder
    {
        public override void build(Message message)
        {
            UInt64 ID = BitConverter.ToUInt64(message.MessageBytes, 7);
            UInt64 OriginID = BitConverter.ToUInt64(message.MessageBytes[15..23]);
            UInt64 TargetID = BitConverter.ToUInt64(message.MessageBytes[23..31]);
            Int64 TakeOff = BitConverter.ToInt64(message.MessageBytes[31..39]);
            Int64 Landing = BitConverter.ToInt64(message.MessageBytes[39..47]);
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
            ProductList.Products.Add(new Fligth("FL", ID, OriginID, TargetID, TakeOff.ToString(), Landing.ToString(), 0, 0, 0, PlaneID, Crew, Load));
        }
    }
}
