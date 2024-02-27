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
        Dictionary<string, Serializator> serializators = new Dictionary<string, Serializator>()
        {
            { "JSON", new JsonSerializator() },
        };
        public void Serialize(List<Product> products)
        {
            try
            {
                serializators[Settings.SerializationType].Serialize(products);
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
                WriteIndented = true,
            };
            string s;
            using (var sw = new StreamWriter(Settings.OutputFileName))
            {
                s = JsonSerializer.Serialize(products, options);
                sw.WriteLine(s);
            }
        }
    }
}
