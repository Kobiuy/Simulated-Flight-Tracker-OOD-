using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NetworkSourceSimulator;
namespace OOD_Proj_1
{
    public static class ProductList
    {
        public static List<Product> Products = new List<Product>();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataImporter importer = new DataImporter();
            List<Product> products;
            products = importer.ImportData();
            Menu.StartMenu();
        }
    }
}