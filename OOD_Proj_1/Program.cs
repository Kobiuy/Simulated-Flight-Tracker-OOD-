using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NetworkSourceSimulator;
using FlightTrackerGUI;
using ExCSS;
using Mapsui.Projections;
using Mapsui;
namespace OOD_Proj_1
{
    public static class ProductList
    {
        public static List<Product> Products = new List<Product>();
    }

    /*
    public class Adapter : FlightGUI
    {
        MPoint current;
        MPoint prev;
        public bool isFlying = false;
        public Adapter(Fligth fligth) // POWINIEN BYĆ ADAPTER FLIGHTGUISDATA
        {
            ID = fligth.ID;
            WorldPosition wps = new WorldPosition();
            int SecondsNow = (DateTime.Now.Hour * 60 + DateTime.Now.Minute) * 60 + DateTime.Now.Second;
            if (fligth.TakeOffSecondsInDay > SecondsNow || fligth.LandingSecondsInDay < SecondsNow) return;
            isFlying = true;
            Airport start = ((Airport)ProductList.Products[(int)fligth.OriginAsID]);
            Airport target = ((Airport)ProductList.Products[(int)fligth.TargetAsID]);
            wps.Longitude = start.Longitude + (target.Longitude - start.Longitude) * (SecondsNow - fligth.TakeOffSecondsInDay) / (fligth.LandingSecondsInDay - fligth.TakeOffSecondsInDay);
            wps.Latitude = start.Latitude + (target.Latitude - start.Latitude) * (SecondsNow - fligth.TakeOffSecondsInDay) / (fligth.LandingSecondsInDay - fligth.TakeOffSecondsInDay);
            base.WorldPosition = wps;

            MPoint startPoint = new MPoint(start.Longitude, start.Latitude);
            MPoint targetPoint = new MPoint(target.Longitude, target.Latitude);
            startPoint = SphericalMercator.FromLonLat(startPoint);
            targetPoint = SphericalMercator.FromLonLat(targetPoint);
            MPoint Vector = new MPoint(targetPoint - startPoint);
            MPoint baseVector = new MPoint(0, 1);
            base.MapCoordRotation = GetAngle(baseVector, Vector);
        }
        double GetAngle(MPoint w, MPoint v)
        {
            double angle = Math.Atan2(w.Y * v.X - w.X * v.Y, w.X * v.X + w.Y * v.Y);
            return angle;
        }
    }
    */

    public class Adapter : FlightsGUIData
    {
        private List<Fligth> fligths;

        public void UpdateFlights(List<Fligth> NewFlights)
        {
            fligths = NewFlights;
        }
        public override int GetFlightsCount()
        {
            return fligths.Count;
        }

        public override UInt64 GetID(int index)
        {
            return fligths[index].ID;
        }

        public override WorldPosition GetPosition(int index)
        {

            WorldPosition wps = new WorldPosition();
            int SecondsNow = (DateTime.Now.Hour * 60 + DateTime.Now.Minute) * 60 + DateTime.Now.Second;
            Airport start = ((Airport)ProductList.Products[(int)fligths[index].OriginAsID]);
            Airport target = ((Airport)ProductList.Products[(int)fligths[index].TargetAsID]);
            if (fligths[index].TakeOffSecondsInDay > SecondsNow) return new WorldPosition(start.Latitude, start.Longitude);
            if (fligths[index].LandingSecondsInDay < SecondsNow) return new WorldPosition(target.Latitude, target.Longitude);
            wps.Longitude = start.Longitude + (target.Longitude - start.Longitude) * (SecondsNow - fligths[index].TakeOffSecondsInDay) / (fligths[index].LandingSecondsInDay - fligths[index].TakeOffSecondsInDay);
            wps.Latitude = start.Latitude + (target.Latitude - start.Latitude) * (SecondsNow - fligths[index].TakeOffSecondsInDay) / (fligths[index].LandingSecondsInDay - fligths[index].TakeOffSecondsInDay);
            return wps;
        }

        public override double GetRotation(int index)
        {
            Airport start = ((Airport)ProductList.Products[(int)fligths[index].OriginAsID]);
            Airport target = ((Airport)ProductList.Products[(int)fligths[index].TargetAsID]);
            MPoint startPoint = new MPoint(start.Longitude, start.Latitude);
            MPoint targetPoint = new MPoint(target.Longitude, target.Latitude);
            startPoint = SphericalMercator.FromLonLat(startPoint);
            targetPoint = SphericalMercator.FromLonLat(targetPoint);
            MPoint v = new MPoint(targetPoint - startPoint);
            MPoint w = new MPoint(0, 1);
            return Math.Atan2(w.Y * v.X - w.X * v.Y, w.X * v.X + w.Y * v.Y);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataImporter importer = new DataImporter();
            List<Product> products;
            products = importer.ImportData();
            Menu.StartMenu();

            Thread GuiThread = new Thread(Runner.Run);
            GuiThread.Start();
            Adapter adapter = new Adapter();

            //while (GuiThread.IsAlive)
            //{
            /*List<FlightGUI> flightGUIs = new List<FlightGUI>();
            for (int i = 0; i < StaticProductLists.fligths.Count; i++)
            {
                Adapter flightGUI = new Adapter(StaticProductLists.fligths[i]);
                if(flightGUI.isFlying)
                    flightGUIs.Add(flightGUI);
            }
            */
            //FlightsGUIData flightsGUIData = new FlightsGUIData();
            ////flightsGUIData.UpdateFlights(flightGUIs);
            //adapter.UpdateFlights(StaticProductLists.fligths);
            //Runner.UpdateGUI(adapter);


            //}
            //FlightsGUIData flightsGUIData = new FlightsGUIData();
            //flightsGUIData.UpdateFlights(flightGUIs);
            adapter.UpdateFlights(StaticProductLists.fligths);
            while (GuiThread.IsAlive)
                Runner.UpdateGUI(adapter);

        }
    }
}