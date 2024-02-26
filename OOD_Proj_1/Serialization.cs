using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public class SerializeData
    {
        Settings settings = new Settings();
        Dictionary<string, Serializator> serializators = new Dictionary<string, Serializator>()
        {
            { "JSON", new JsonSerializator() },
        };
        public void Serialize(List<Product> products)
        {
            try
            {
                serializators[settings.SerializationType].Serialize(products);
            }  
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    public abstract class Serializator
    {
        abstract public void Serialize(List<Product> products);
    }

    public class JsonSerializator : Serializator
    {
        public override void Serialize(List<Product> products)
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
}
