using System.Text.Json;

namespace OOD_Proj_1
{
    public static class Settings
    {
        public static string SerializationType = "JSON";
        public static string DataSource = "FILE";
        public static string FileName = "example_data.ftr";
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