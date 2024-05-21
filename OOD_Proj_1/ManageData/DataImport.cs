using NetworkSourceSimulator;
using System.Text;

namespace OOD_Proj_1.ManageData
{
    public class DataImporter
    {
        Dictionary<string, Importer> Importers = new Dictionary<string, Importer>()
        {
            { "FILE", new FileImporter() },
            { "SIM_SERVER", new ServerImporter() },
            {"FILEwUPDATES", new FileAndServerImporter() },
        };
        public List<Product> ImportData(ProductLists productLists)
        {
            return Importers[Settings.DataSource].Import(productLists);
        }
    }
    public abstract class Importer // Class inherited by classes importing data
    {
        protected Factory factory = new();
        abstract public List<Product> Import(ProductLists productLists);
    }
    public class FileImporter : Importer
    {
        public override List<Product> Import(ProductLists productLists)
        {
            try
            {
                using (var sr = new StreamReader(Settings.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        factory.Create(sr.ReadLine(), productLists);
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
            return productLists.GetAllDataList();
        }
    }
    public class ServerImporter : Importer
    {
        public override List<Product> Import(ProductLists productLists)
        {
            ServerSimulator.StartServer(productLists);
            return productLists.GetAllDataList();
        }

        public void ParseMessage(Message message, ProductLists productLists)
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

            builders[Encoding.ASCII.GetString(message.MessageBytes[0..3])].Create(message, productLists);
        }
    }
    public class FileAndServerImporter : Importer
    {

        public override List<Product> Import(ProductLists productLists)
        {
            FileImporter fileImporter = new FileImporter();
            fileImporter.Import(productLists);
            ServerImporter serverImporter = new ServerImporter();
            serverImporter.Import(productLists);
            return productLists.GetAllDataList();
        }
    }
}