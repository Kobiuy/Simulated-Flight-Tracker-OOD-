using NetworkSourceSimulator;
using OOD_Proj_1.ManageData;
using OOD_Proj_1.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class ServerSimulator
    {
        public static Thread ServerThread;
        public static ProductLists productLists;
        public static void StartServer(ProductLists PL)
        {
            productLists = PL;
            NetworkSourceSimulator.NetworkSourceSimulator simulator = new NetworkSourceSimulator.NetworkSourceSimulator(Settings.UpdatesFileName, Settings.SimMin, Settings.SimMax);
            ServerImporter serverImporter = new ServerImporter();
            simulator.OnNewDataReady += (object sender, NewDataReadyArgs args) =>
            {
                Message message = simulator.GetMessageAt(args.MessageIndex);
                serverImporter.ParseMessage(message, productLists);
            };
            simulator.OnIDUpdate += Simulator_OnIDUpdate;
            simulator.OnPositionUpdate += Simulator_OnPositionUpdate;
            simulator.OnContactInfoUpdate += Simulator_OnContactInfoUpdate;

            ServerThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    simulator.Run();
                }
                catch (ThreadInterruptedException ex) { }
            }));
            ServerThread.Start();
        }

        private static void Simulator_OnContactInfoUpdate(object sender, ContactInfoUpdateArgs args)
        {
            foreach (var person in productLists.passangersdict.Values)
            {
                if (person.ID == args.ObjectID)
                {
                    LogManager.ContactInfo(args, person);
                    person.Email = args.EmailAddress;
                    person.Phone = args.PhoneNumber;
                    return;
                }
            }
            foreach (var person in productLists.crewsdict.Values)
            {
                if (person.ID == args.ObjectID)
                {
                    LogManager.ContactInfo(args, person);
                    person.Email = args.EmailAddress;
                    person.Phone = args.PhoneNumber;
                    return;
                }
            }
            LogManager.InvalidData();

        }
        private static void Simulator_OnIDUpdate(object sender, IDUpdateArgs args)
        {
            foreach (var product in productLists.GetAllDataList())
            {
                if (product.ID == args.NewObjectID) { LogManager.InvalidData(); return; }
            }
            foreach (var obs in productLists.observers)
            {
                if (obs.FindAndUpdateID(args.ObjectID, args.NewObjectID)) return;
            }
            LogManager.InvalidData();
        }
        private static void Simulator_OnPositionUpdate(object sender, PositionUpdateArgs args)
        {
            foreach (var flight in productLists.flightsdict.Values)
            {
                if (flight.ID == args.ObjectID)
                {
                    LogManager.ChangePosition(args, flight.Latitude, flight.Longitude, flight.AMSL);
                    FligthPositionWrapper fligthPositionWrapper = new(flight, args.Longitude, args.Latitude, args.AMSL);
                    productLists.flightsdict[flight.ID] = fligthPositionWrapper;
                    return;
                }
            }
            foreach (var airport in productLists.airportsdict.Values)
            {
                if (airport.ID == args.ObjectID)
                {
                    LogManager.ChangePosition(args, airport.Latitude, airport.Longitude, airport.AMSL);
                    airport.Latitude = args.Latitude; airport.Longitude = args.Longitude;
                    airport.AMSL = args.AMSL;
                    return;
                }
            }
            LogManager.InvalidData();
        }

        public static void StopServer()
        {
            if (ServerThread != null)
            {
                ServerThread.Interrupt();
                ServerThread.Join();
            }
        }
    }
}
