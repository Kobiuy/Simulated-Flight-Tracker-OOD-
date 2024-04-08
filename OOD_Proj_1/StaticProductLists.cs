using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    /*public static class StaticProductLists
    {
        public static List<Cargo> cargos = new List<Cargo>();
        public static List<Crew> crews = new List<Crew>();
        public static List<Passenger> passengers = new List<Passenger>();
        public static List<PassengerPlane> passengerPlanes = new List<PassengerPlane>();
        public static List<CargoPlane> cargoPlanes = new List<CargoPlane>();
        public static List<Fligth> fligths = new List<Fligth>();
        public static List<Airport> airports = new List<Airport>();

        public static List<Product> GetAllDataList()
        {
            List<Product> result = new List<Product>();
            result.AddRange(cargos);
            result.AddRange(crews);
            result.AddRange(passengers);
            result.AddRange(passengerPlanes);
            result.AddRange(cargoPlanes);
            result.AddRange(fligths);
            result.AddRange(airports);
            return result;
        }
    }*/
    public class ProductLists
    {
        public List<Cargo> cargos = new List<Cargo>();
        public List<Crew> crews = new List<Crew>();
        public List<Passenger> passengers = new List<Passenger>();
        public List<PassengerPlane> passengerPlanes = new List<PassengerPlane>();
        public List<CargoPlane> cargoPlanes = new List<CargoPlane>();
        public List<Fligth> fligths = new List<Fligth>();
        public List<Airport> airports = new List<Airport>();

        public List<Product> GetAllDataList()
        {
            List<Product> result = new List<Product>();
            result.AddRange(cargos);
            result.AddRange(crews);
            result.AddRange(passengers);
            result.AddRange(passengerPlanes);
            result.AddRange(cargoPlanes);
            result.AddRange(fligths);
            result.AddRange(airports);
            return result;
        }
    }
}
