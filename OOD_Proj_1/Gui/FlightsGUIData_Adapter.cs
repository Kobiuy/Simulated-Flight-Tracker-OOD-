using Mapsui.Projections;
using Mapsui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Index.Strtree;
using System.Collections;
using ExCSS;

namespace OOD_Proj_1.Gui
{
    public class Adapter : FlightsGUIData
    {
        private Dictionary<ulong, Fligth> fligths;
        private Dictionary<ulong, Airport> airports;

        public void UpdateFlights(Dictionary<ulong, Fligth> NewFlights)
        {
            fligths = NewFlights;
        }

        public void UpdateAirports(Dictionary<ulong, Airport> NewAirports)
        {
            airports = NewAirports;
        }
        public override int GetFlightsCount()
        {
            return fligths.Count;
        }
        public override ulong GetID(int index)
        {
            return fligths.ElementAt(index).Value.ID;
        }
        public override WorldPosition GetPosition(int index)
        {
            //// Plan:    Create wrapper class overriding getposition method on a plane.
            ////          Add get position method to plane class
            //          Every list should actually be a dictionary 
            //          Serialize class ProductLists, not just lists
            //          In case of position change replace item with wrapper
            

            return fligths.ElementAt(index).Value.IteratePosition(airports);
        }
        public override double GetRotation(int index)
        {
            Airport start = airports[fligths.ElementAt(index).Value.OriginAsID];
            Airport target = airports[fligths.ElementAt(index).Value.TargetAsID];
            //MPoint startPoint = new MPoint(start.Longitude, start.Latitude);
            MPoint startPoint = new MPoint(fligths.ElementAt(index).Value.Longitude, fligths.ElementAt(index).Value.Latitude);
            MPoint targetPoint = new MPoint(target.Longitude, target.Latitude);
            startPoint = SphericalMercator.FromLonLat(startPoint);
            targetPoint = SphericalMercator.FromLonLat(targetPoint);

            MPoint v = new MPoint(targetPoint - startPoint);
            MPoint w = new MPoint(0, 1);
            return Math.Atan2(w.Y * v.X - w.X * v.Y, w.X * v.X + w.Y * v.Y);
        }
        
    }
}
