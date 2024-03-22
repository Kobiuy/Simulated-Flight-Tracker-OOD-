using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOD_Proj_1
{
    public class DataImporter
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
            try
            {
                using (var sr = new StreamReader(Settings.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        Product product = factory.Create(sr.ReadLine());
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
            return ProductList.Products;
        }
    }
    public class ServerImporter : Importer
    {
        public override List<Product> Import()
        {
            ServerSimulator.StartServer();
            return ProductList.Products;
        }

        public void ParseMessage(Message message)
        {
            Dictionary<string, Generator> builders = new Dictionary<string, Generator>()
            {
                            { "NCR", new CrewGenerator() },
                            { "NPA", new PassengerGenerator() },
                            { "NCA", new CargoGenerator() },
                            { "NPP", new PassengerPlaneGenerator() },
                            { "NCP", new CargoPlaneGenerator() },
                            { "NAI", new AirportGenerator() },
                            { "NFL", new FlightGenerator() },
            };

            builders[Encoding.ASCII.GetString(message.MessageBytes[0..3])].Create(message);
        }
    }
}