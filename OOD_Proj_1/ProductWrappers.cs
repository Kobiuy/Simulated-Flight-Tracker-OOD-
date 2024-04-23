using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    abstract public class FligthWrapper : Fligth
    {
        public FligthWrapper(Fligth fligth) : base(fligth.Type, fligth.ID, fligth.OriginAsID, fligth.TargetAsID, fligth.TakeOffTime, fligth.LandingTime, fligth.Longitude, fligth.Latitude, fligth.AMSL, fligth.PlaneID, fligth.CrewAsIDs, fligth.LoadAsIDs)
        {
        }
    }
    public class FligthPositionWrapper : FligthWrapper
    {
        private int CreationTimeSeconds;
        public FligthPositionWrapper(Fligth fligth, Single Longitude, Single Latitude, Single AMSL) : base(fligth) {
            base.Latitude = Latitude;
            base.Longitude = Longitude;
            base.AMSL = AMSL;
            CreationTimeSeconds = DateTime.Now.GetSeconds();
        }

        public override WorldPosition IteratePosition(Dictionary<ulong, Airport> airports)
        {
            WorldPosition wps = new WorldPosition();
            Airport target = airports[base.TargetAsID];
            int lndtime = DateTime.Parse(base.LandingTime).GetSeconds();
            int nwtime = DateTime.Now.GetSeconds();

            var progress = Utils.GetProgress(CreationTimeSeconds, lndtime, nwtime);
            if (progress != 0 && progress != 1)
            {
                progress = Utils.GetProgress(nwtime-1, lndtime, nwtime);
                wps.Longitude = base.Longitude + (target.Longitude - base.Longitude) * progress;
                wps.Latitude = base.Latitude + (target.Latitude - base.Latitude) * progress;

            }
            else
            {
                wps.Longitude = target.Longitude;
                wps.Latitude = target.Latitude;
            }
            base.Longitude = (float)wps.Longitude;
            base.Latitude = (float)wps.Latitude;
            return wps;
        }
    }


}
