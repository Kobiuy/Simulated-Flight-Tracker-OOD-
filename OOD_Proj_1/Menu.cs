using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class Menu
    {
        public static SerializeData serializator = new SerializeData();
        public static Dictionary<string, Action> menuDict = new Dictionary<string, Action>()
            {
                { "print", serializator.Serialize },
                { "gui", GuiApp.StartGUI },
                { "exit", ServerSimulator.StopServer }
            };
        public static void StartMenu()
        {
            string UserInput = "";
            Action method;
            Console.WriteLine("Write \"print\" to make a snapshot, \"gui\" for GUI or \"exit\" to exit");
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
    }
}
