using Mapsui.Projections;
using Mapsui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Index.Strtree;
using System.Collections;

namespace OOD_Proj_1
{
    public class Adapter : FlightsGUIData
    {
        private List<Fligth> fligths;
        private Dictionary<UInt64, Airport> airports = new Dictionary<UInt64, Airport>();

        public void UpdateFlights(List<Fligth> NewFlights)
        {
            fligths = NewFlights;
        }
        public void UpdateAirports(List<Airport> NewAirports)
        {
            airports = new Dictionary<UInt64, Airport>();
            foreach (Airport Airport in NewAirports)
                airports.Add(Airport.ID, Airport);
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
            Airport start = airports[fligths[index].OriginAsID];
            Airport target = airports[fligths[index].TargetAsID];
            int toftime = DateTime.Parse(fligths[index].TakeOffTime).GetSeconds();
            int lndtime = DateTime.Parse(fligths[index].LandingTime).GetSeconds();
            int nwtime = DateTime.Now.GetSeconds();

            wps.Longitude = start.Longitude + (target.Longitude - start.Longitude) * GetProgress(fligths[index], toftime, lndtime, nwtime);
            wps.Latitude = start.Latitude + (target.Latitude - start.Latitude) * GetProgress(fligths[index], toftime, lndtime, nwtime);
            return wps;
        }
        public override double GetRotation(int index)
        {
            Airport start = airports[fligths[index].OriginAsID];
            Airport target = airports[fligths[index].TargetAsID];
            MPoint startPoint = new MPoint(start.Longitude, start.Latitude);
            MPoint targetPoint = new MPoint(target.Longitude, target.Latitude);
            startPoint = SphericalMercator.FromLonLat(startPoint);
            targetPoint = SphericalMercator.FromLonLat(targetPoint);

            MPoint v = new MPoint(targetPoint - startPoint);
            MPoint w = new MPoint(0, 1);
            return Math.Atan2(w.Y * v.X - w.X * v.Y, w.X * v.X + w.Y * v.Y);
        }
        private static double GetProgress(Fligth fligth, int toftime, int lndtime, int nwtime)
        {
            double a, b;

            if (toftime > nwtime && lndtime > toftime) return 0;
            if (toftime > nwtime && lndtime < toftime && lndtime < nwtime) return 1;
            if (toftime < nwtime && lndtime < nwtime && lndtime > toftime) return 1;

            if (toftime > lndtime)
            {
                if (nwtime < toftime)
                {
                    lndtime += 24 * 60 * 60; // Add one day
                    nwtime += 24 * 60 * 60; // Add one day
                }
                else
                    lndtime += 24 * 60 * 60; // Add one day
            }
            a = (nwtime - toftime);
            b = (lndtime - toftime);
            double progress = a / b;

            return progress;
        }
    }
}
