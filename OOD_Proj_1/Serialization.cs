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
}
