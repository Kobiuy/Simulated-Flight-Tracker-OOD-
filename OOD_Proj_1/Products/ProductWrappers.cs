using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1.Products
{
    abstract public class FligthWrapper : Fligth
    {
        public FligthWrapper(Fligth fligth) : base(fligth.Type, fligth.ID, fligth.OriginAsID, fligth.TargetAsID, fligth.TakeOffTime, fligth.LandingTime, fligth.Longitude, fligth.Latitude, fligth.AMSL, fligth.PlaneID, fligth.CrewAsIDs, fligth.LoadAsIDs) { }
    }
    public class FligthPositionWrapper : FligthWrapper
    {
        private int CreationTimeSeconds;
        public FligthPositionWrapper(Fligth fligth, float Longitude, float Latitude, float AMSL) : base(fligth)
        {
            base.Latitude = Latitude;
            base.Longitude = Longitude;
            base.AMSL = AMSL;
            CreationTimeSeconds = DateTime.Now.GetSeconds();
        }

        public override WorldPosition IteratePosition(Dictionary<ulong, Airport> airports)
        {
            WorldPosition wps = new WorldPosition();
            Airport target = airports[TargetAsID];
            int lndtime = DateTime.Parse(LandingTime).GetSeconds();
            int nwtime = DateTime.Now.GetSeconds();

            var progress = MathUtils.GetProgress(CreationTimeSeconds, lndtime, nwtime);
            if (progress != 0 && progress != 1)
            {
                progress = MathUtils.GetProgress(nwtime - 1, lndtime, nwtime);
                wps.Longitude = Longitude + (target.Longitude - Longitude) * progress;
                wps.Latitude = Latitude + (target.Latitude - Latitude) * progress;

            }
            else
            {
                wps.Longitude = target.Longitude;
                wps.Latitude = target.Latitude;
            }
            Longitude = (float)wps.Longitude;
            Latitude = (float)wps.Latitude;
            return wps;
        }
    }


}
