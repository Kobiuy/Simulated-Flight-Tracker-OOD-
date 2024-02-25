using System.Text.Json;

namespace OOD_Proj_1
{
    public class GetData
    {
        Factory factory = new();
        public List<Product> FromTextFile(string FileName)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var sr = new StreamReader(FileName))
                {
                    while(!sr.EndOfStream)
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
    public class SerializeData
    {
        public void JsonSerialization(List<Product> products)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            string s;
            using (var sw = new StreamWriter("Products.json"))
            {
                foreach (Product p in products)
                {
                    s = JsonSerializer.Serialize(p, options);
                    sw.WriteLine(s);
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products;
            string FileName = "example_data.ftr";
            GetData getdata = new GetData();
            SerializeData serialize = new SerializeData();
            products = getdata.FromTextFile(FileName);
            foreach (Product p in products) { Console.WriteLine(p.Type); }
            serialize.JsonSerialization(products);
        }
    }
}