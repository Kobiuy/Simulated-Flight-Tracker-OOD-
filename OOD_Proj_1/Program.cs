using System.Text.Json;

namespace OOD_Proj_1
{
    public static class Settings
    {
        public static string SerializationType = "JSON"; // Choose from ["JSON"]
        public static string DataSource = "FILE"; // Choose from ["FILE"]
        public static string FileName = "example_data.ftr"; // Set source file name
        public static string OutputFileName = "Products.json"; // Set output file name
    }

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