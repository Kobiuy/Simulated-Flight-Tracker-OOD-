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
        public static void StartGUI()
        {
            Thread RunnerThread = new Thread(Runner.Run);
            RunnerThread.Start();

            UpdateGuiThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    UpdatingGui(RunnerThread);
                }
                catch (ThreadInterruptedException ex) { }
            }));

            UpdateGuiThread.Start();
        }
        private static void UpdatingGui(Thread RunnerThread)
        {
            Adapter adapter = new Adapter();
            adapter.UpdateFlights(StaticProductLists.fligths);
            adapter.UpdateAirports(StaticProductLists.airports);
            while (RunnerThread.IsAlive)
            {
                Runner.UpdateGUI(adapter);
                Thread.Sleep(1000);
            }
        }
    }
}
