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
        private double CreationLongitude;
        private double CreationLatitude;
        public FligthPositionWrapper(Fligth fligth, float Longitude, float Latitude, float AMSL) : base(fligth)
        {
            this.Latitude = Latitude;
            CreationLatitude = Latitude;
            this.Longitude = Longitude;
            CreationLongitude = Longitude;
            this.AMSL = AMSL;
            CreationTimeSeconds = DateTime.Now.GetSeconds();
        }

        public override WorldPosition IteratePosition(Dictionary<ulong, Airport> airports)
        {
            return MathUtils.InterpolatePosition(CreationLatitude, CreationLongitude, airports[TargetAsID], CreationTimeSeconds, this);
        }
    }


}
