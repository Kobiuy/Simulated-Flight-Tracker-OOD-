using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    internal class DataImporter
    {
        Settings settings = new Settings();
        Dictionary<string, Importer> Importers = new Dictionary<string, Importer>()
        {
            { "FILE", new FileImporter() },
        };
        public List<Product> ImportData()
        {
            return Importers[settings.DataSource].Import();
        }
    }
    public abstract class Importer
    {
        protected Settings settings = new Settings();
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
                using (var sr = new StreamReader(settings.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        products.Add(factory.Create(sr.ReadLine()));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
    }
}
