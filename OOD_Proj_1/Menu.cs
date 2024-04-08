using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_Proj_1.Gui;
using OOD_Proj_1.ManageData;
using OOD_Proj_1.News;

namespace OOD_Proj_1
{
    public static class Menu
    {
        public static SerializeData serializator = new SerializeData();
        public static Dictionary<string, Action> menuDict = new Dictionary<string, Action>()
            {
                { "print", serializator.Serialize },
                { "gui", GuiApp.StartGUI },
                {"report", ()=>(new Reporter()).Report()},
                { "exit", StopAll }
            };
        public static void StartMenu()
        {
            string UserInput = "";
            Action method;
            Console.WriteLine("Write \"print\" to make a snapshot, \"gui\" for GUI, \"report\" or \"exit\" to exit menu");
            while (UserInput != "exit")
            {
                UserInput = Console.ReadLine();
                if (menuDict.TryGetValue(UserInput, out method))
                {
                    method();
                }
                else
                {
                    Console.WriteLine($"[{UserInput}] nie jest poprawną komendą");
                }
            }
        }
        public static void StopAll()
        {
            ServerSimulator.StopServer();
            if (GuiApp.UpdateGuiThread!=null && GuiApp.UpdateGuiThread.IsAlive)
                Console.WriteLine("In order to fully exit application close GUI");
        }
    }
}
