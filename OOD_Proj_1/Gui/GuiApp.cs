using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1.Gui
{
    public static class GuiApp
    {
        public static Thread UpdateGuiThread;
        public static void StartGUI(ProductLists productLists)
        {
            Thread RunnerThread = new Thread(Runner.Run);
            RunnerThread.Start();

            UpdateGuiThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    UpdatingGui(RunnerThread, productLists);
                }
                catch (ThreadInterruptedException ex) { }
            }));

            UpdateGuiThread.Start();
        }
        private static void UpdatingGui(Thread RunnerThread, ProductLists productLists)
        {
            Adapter adapter = new Adapter();
            adapter.UpdateFlights(productLists.fligths);
            adapter.UpdateAirports(productLists.airports);
            while (RunnerThread.IsAlive)
            {
                Runner.UpdateGUI(adapter);
                Thread.Sleep(1000);
            }
        }
    }
}
