using OOD_Proj_1;
using System.Reflection.Metadata.Ecma335;

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
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products;
            string FileName = "example_data.ftr";
            GetData getdata = new GetData();
            products = getdata.FromTextFile(FileName);
            foreach (Product p in products) { Console.WriteLine(p); }
        }
    }
}