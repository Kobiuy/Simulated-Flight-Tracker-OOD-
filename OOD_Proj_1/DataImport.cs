using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    internal class DataImporter
    {
        Dictionary<string, Importer> Importers = new Dictionary<string, Importer>()
        {
            { "FILE", new FileImporter() },
        };
        public List<Product> ImportData()
        {
            return Importers[Settings.DataSource].Import();
        }
    }
    public abstract class Importer
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
                        products.Add(factory.Create(sr.ReadLine()));
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
}
