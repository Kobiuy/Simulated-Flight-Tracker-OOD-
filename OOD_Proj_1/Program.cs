using System.Text.Json;

namespace OOD_Proj_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataImporter importer = new DataImporter();
            SerializeData serializator = new SerializeData();
            List<Product> products;
            products = importer.ImportData();
            serializator.Serialize(products);
        }
    }
}