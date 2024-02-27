namespace OOD_Proj_1
{
    abstract class Generator // Base class of classes generating "Products"
    {
        abstract public Product Create(string[] words);
    }
    class PassengerPlaneGenerator : Generator
    {
        public override PassengerPlane Create(string[] words)
        {
            return new PassengerPlane(words);
        }
    }
    class CargoPlaneGenerator : Generator
    {
        public override CargoPlane Create(string[] words)
        {
            return new CargoPlane(words);
        }
    }
    class PassengerGenerator : Generator
    {
        public override Passenger Create(string[] words)
        {
            return new Passenger(words);
        }
    }
    class CrewGenerator : Generator
    {
        public override Crew Create(string[] words)
        {
            return new Crew(words);
        }
    }
    class CargoGenerator : Generator
    {
        public override Cargo Create(string[] words)
        {
            return new Cargo(words);
        }
    }
    class AirportGenerator : Generator
    {
        public override Airport Create(string[] words)
        {
            return new Airport(words);
        }
    }
    class FlightGenerator : Generator
    {
        public override Fligth Create(string[] words)
        {
            return new Fligth(words);
        }
    }
}
