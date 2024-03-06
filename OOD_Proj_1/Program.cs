using System.Data;
using System.Text.Json;
using NetworkSourceSimulator;
namespace OOD_Proj_1
{

    public static class ProductList
    {
        public static List<Product> Products = new List<Product>();

    }

    public static class Listening
    {
        public static void Listen()
        {
            string UserInput;
            SerializeData serializator = new SerializeData();
            while (true)
            {
                UserInput = Console.ReadLine();
                switch (UserInput)
                {
                    case "print":
                        serializator.Serialize(ProductList.Products);
                        break;
                    case "exit":
                        ServerSimulator.ServerThread.Interrupt();
                        ServerSimulator.ServerThread.Join();
                        return;
                }
            }
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            DataImporter importer = new DataImporter();
            SerializeData serializator = new SerializeData();
            List<Product> products;
            //products = importer.ImportData();
            //serializator.Serialize(products);
            //erializator.Serialize(ProductList.Products);
            products = importer.ImportData();
            Listening.Listen();
        }
    }
}