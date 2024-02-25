using System.Text.Json;

namespace OOD_Proj_1
{
    public class Settings
    {
        public string SerializationType = "JSON";
        public string DataSource = "FILE";
        public string FileName = "example_data.ftr";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings();
            GetData getdata = new GetData();
            SerializeData serialize = new SerializeData();
            List<Product> products;
            products = getdata.FromTextFile(settings.FileName);
            serialize.JsonSerialization(products);
        }
    }
}