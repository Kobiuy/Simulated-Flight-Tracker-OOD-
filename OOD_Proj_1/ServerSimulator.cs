using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class ServerSimulator
    {
        public static Thread ServerThread;
        public static void StartServer()
        {
            NetworkSourceSimulator.NetworkSourceSimulator simulator = new NetworkSourceSimulator.NetworkSourceSimulator(Settings.FileName, Settings.SimMin, Settings.SimMax);
            ServerImporter serverImporter = new ServerImporter();
            simulator.OnNewDataReady += (object sender, NewDataReadyArgs args) =>
            {
                Message message = simulator.GetMessageAt(args.MessageIndex);
                serverImporter.ParseMessage(message);
            };

            ServerThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    simulator.Run();
                }
                catch (ThreadInterruptedException ex) { }
            }));
            ServerThread.Start();
        }
    }
}
