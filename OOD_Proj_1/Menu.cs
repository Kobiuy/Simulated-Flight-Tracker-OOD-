using OOD_Proj_1.Gui;
using OOD_Proj_1.ManageData;
using OOD_Proj_1.News;

namespace OOD_Proj_1
{
    public static class Menu
    {
        public static SerializeData serializator = new SerializeData();
        public static ComandParser comandParser = new ComandParser();
        public static Dictionary<string, Action<ProductLists>> menuDict = new Dictionary<string, Action<ProductLists>>()
            {
                { "print", serializator.Serialize },
                { "gui", GuiApp.StartGUI },
                { "report", (ProductLists p)=>(new Reporter()).Report(p)},
                { "exit", StopAll }
            };
        public static void StartMenu(ProductLists productLists)
        {
            string UserInput = "";
            Action<ProductLists> method;
            Console.WriteLine("Write \"print\" to make a snapshot, \"gui\" for GUI, \"report\" or \"exit\" to exit menu.");
            Console.WriteLine("You can use queries like \"Display\" \"Add\" \"Delete\" and \"Update\" as well");
            while (UserInput != "exit")
            {
                UserInput = Console.ReadLine();
                if (menuDict.TryGetValue(UserInput, out method))
                {
                    method(productLists);
                }
                else
                {
                    try
                    {
                        comandParser.ChooseCommand(UserInput, productLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Command");
                    }
                }
                }
            }
            public static void StopAll(ProductLists p)
            {
                ServerSimulator.StopServer();
                if (GuiApp.UpdateGuiThread != null && GuiApp.UpdateGuiThread.IsAlive)
                    Console.WriteLine("In order to fully exit application close GUI");
            }
        }
    }
