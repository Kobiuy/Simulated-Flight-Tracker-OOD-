using OOD_Proj_1.ManageData;
namespace OOD_Proj_1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            LogManager.NewRun();
            ProductLists productLists = new ProductLists();
            DataImporter importer = new DataImporter();
            importer.ImportData(productLists);
            Menu.StartMenu(productLists);
        }
    }
}