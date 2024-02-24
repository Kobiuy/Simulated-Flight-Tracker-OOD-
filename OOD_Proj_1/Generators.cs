using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    abstract class Generator
    {
        abstract public Product Create(string txt);
    }
    class PassangerPlaneGenerator : Generator
    {
        public override PassangerPlane Create(string txt)
        {
            return new PassangerPlane(txt);
        }
    }
    class CargoPlaneGenerator : Generator
    {
        public override CargoPlane Create(string txt)
        {
            return new CargoPlane(txt);
        }
    }
    class PassangerGenerator : Generator
    {
        public override Passanger Create(string txt)
        {
            return new Passanger(txt);
        }
    }
    class CrewGenerator : Generator
    {
        public override Crew Create(string txt)
        {
            return new Crew(txt);
        }
    }
    class CargoGenerator : Generator
    {
        public override Cargo Create(string txt)
        {
            return new Cargo(txt);
        }
    }
    class AirportGenerator : Generator
    {
        public override Airport Create(string txt)
        {
            return new Airport(txt);
        }
    }
    class FlightGenerator : Generator
    {
        public override Fligth Create(string txt)
        {
            return new Fligth(txt);
        }
    }
}
