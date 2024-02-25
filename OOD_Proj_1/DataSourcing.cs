using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
