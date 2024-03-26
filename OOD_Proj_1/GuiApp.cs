using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class GuiApp
    {
        public static void StartGUI()
        {
            GuiLogic();
        }

        private static void GuiLogic()
        {
            Thread GuiThread = new Thread(Runner.Run);
            GuiThread.Start();
            Adapter adapter = new Adapter();
            adapter.UpdateFlights(StaticProductLists.fligths);
            adapter.UpdateAirports(StaticProductLists.airports);
            while (GuiThread.IsAlive)
            {
                Runner.UpdateGUI(adapter);
            }
        }
    }
}
