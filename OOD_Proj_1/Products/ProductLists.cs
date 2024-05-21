namespace OOD_Proj_1
{
    public class ProductLists
    {
        public List<IObserver> observers = new List<IObserver>();
        public Dictionary<ulong, Airport> airportsdict = new Dictionary<ulong, Airport>();
        public Dictionary<ulong, Crew> crewsdict = new Dictionary<ulong, Crew>();
        public Dictionary<ulong, Cargo> cargotsdict = new Dictionary<ulong, Cargo>();
        public Dictionary<ulong, Passenger> passangersdict = new Dictionary<ulong, Passenger>();
        public Dictionary<ulong, PassengerPlane> passangerPlanesdict = new Dictionary<ulong, PassengerPlane>();
        public Dictionary<ulong, CargoPlane> cargoPlanesdict = new Dictionary<ulong, CargoPlane>();
        public Dictionary<ulong, Flight> flightsdict = new Dictionary<ulong, Flight>();

        //public Dictionary<string, List<Product>> stringToList = new Dictionary<string, List<Product>>();
        public Dictionary<string, Func<List<Product>>> StringToProductList;

        public ProductLists()
        {
            observers.Add(new Observer<Airport>(airportsdict));
            observers.Add(new Observer<Crew>(crewsdict));
            observers.Add(new Observer<CargoPlane>(cargoPlanesdict));
            observers.Add(new Observer<Cargo>(cargotsdict));
            observers.Add(new Observer<PassengerPlane>(passangerPlanesdict));
            observers.Add(new Observer<Passenger>(passangersdict));
            observers.Add(new Observer<Flight>(flightsdict));

            StringToProductList = new Dictionary<string, Func<List<Product>>>
            {
                { "PassengerPlane", () => passangerPlanesdict.Values.ToList<Product>() },
                { "CargoPlane", () => cargoPlanesdict.Values.ToList<Product>() },
                { "Passenger", () => passangersdict.Values.ToList<Product>() },
                { "Cargo", () => cargotsdict.Values.ToList<Product>() },
                { "Crew", () => crewsdict.Values.ToList<Product>() },
                { "Airport", () => airportsdict.Values.ToList<Product>() },
                { "Flight", () => flightsdict.Values.ToList<Product>() },
            };
        }
        public List<Product> GetAllDataList()
        {
            List<Product> result = new List<Product>();
            result.AddRange(cargotsdict.Values.ToList());
            result.AddRange(crewsdict.Values.ToList());
            result.AddRange(passangersdict.Values.ToList());
            result.AddRange(passangerPlanesdict.Values.ToList());
            result.AddRange(cargoPlanesdict.Values.ToList());
            result.AddRange(flightsdict.Values.ToList());
            result.AddRange(airportsdict.Values.ToList());
            return result;
        }

    }

}
