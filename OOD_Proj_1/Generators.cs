namespace OOD_Proj_1
{
    abstract class Generator
    {
        abstract public Product Create(string[] words);
    }
    class PassangerPlaneGenerator : Generator
    {
        public override PassangerPlane Create(string[] words)
        {
            return new PassangerPlane(words);
        }
    }
    class CargoPlaneGenerator : Generator
    {
        public override CargoPlane Create(string[] words)
        {
            return new CargoPlane(words);
        }
    }
    class PassangerGenerator : Generator
    {
        public override Passanger Create(string[] words)
        {
            return new Passanger(words);
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
