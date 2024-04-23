using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Dictionary<ulong, Fligth> flightsdict = new Dictionary<ulong, Fligth>();
        public ProductLists()
        {
            observers.Add(new Observer<Airport>(airportsdict));
            observers.Add(new Observer<Crew>(crewsdict));
            observers.Add(new Observer<CargoPlane>(cargoPlanesdict));
            observers.Add(new Observer<Cargo>(cargotsdict));
            observers.Add(new Observer<PassengerPlane>(passangerPlanesdict));
            observers.Add(new Observer<Passenger>(passangersdict));
            observers.Add(new Observer<Fligth>(flightsdict));
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
