using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1.Products
{
    public class ProductLists
    {
        public Dictionary<ulong, Airport> airportsdict = new Dictionary<ulong, Airport>();
        public Dictionary<ulong, Crew> crewsdict = new Dictionary<ulong, Crew>();
        public Dictionary<ulong, Cargo> cargotsdict = new Dictionary<ulong, Cargo>();
        public Dictionary<ulong, Passenger> passangersdict = new Dictionary<ulong, Passenger>();
        public Dictionary<ulong, PassengerPlane> passangerPlanesdict = new Dictionary<ulong, PassengerPlane>();
        public Dictionary<ulong, CargoPlane> cargoPlanesdict = new Dictionary<ulong, CargoPlane>();
        public Dictionary<ulong, Fligth> flightsdict = new Dictionary<ulong, Fligth>();

        /*
        public List<Cargo> cargos = new List<Cargo>();
        public List<Crew> crews = new List<Crew>();
        public List<Passenger> passengers = new List<Passenger>();
        public List<PassengerPlane> passengerPlanes = new List<PassengerPlane>();
        public List<CargoPlane> cargoPlanes = new List<CargoPlane>();
        public List<Fligth> fligths = new List<Fligth>();
        public List<Airport> airports = new List<Airport>();
        */

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
