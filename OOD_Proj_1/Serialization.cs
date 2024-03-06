using Microsoft.VisualBasic;
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
    public abstract class Serializator // Class inherited by classes serializing data
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
            DateTime time = DateAndTime.Now;
            using (var sw = new StreamWriter($"snapshot_{time.Hour}_{time.Minute}_{time.Second}.json"))
            {
                s = JsonSerializer.Serialize(products, options);
                sw.WriteLine(s);
            }
        }
    }
}
