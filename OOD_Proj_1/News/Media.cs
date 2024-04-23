using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1.News
{
    public abstract class Media()
    {
        public abstract string doForArp(Airport airport);
        public abstract string doForPP(PassengerPlane passengerPlane);
        public abstract string doForCP(CargoPlane cargoPlane);
    }

    public class Television : Media
    {
        private string Name;
        public Television(string name) { Name = name; }
        public override string doForArp(Airport airport)
        {
            return ($"<An image of {airport.Name} airport>");
        }

        public override string doForCP(CargoPlane cargoPlane)
        {
            return ($"<An image of {cargoPlane.Serial} cargo plane>");
        }

        public override string doForPP(PassengerPlane passengerPlane)
        {
            return ($"<An image of {passengerPlane.Serial} passenger plane>");
        }
    }

    public class Radio : Media
    {
        private string Name;
        public Radio(string name) { Name = name; }
        public override string doForArp(Airport airport)
        {
            return ($"Reporting for {this.Name}, Ladies and gentelmen, we are at the {airport.Name} airport.");
        }

        public override string doForCP(CargoPlane cargoPlane)
        {
            return ($"Reporting for {this.Name}, Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.");
        }

        public override string doForPP(PassengerPlane passengerPlane)
        {
            return ($"Reporting for {this.Name}, Ladies and gentelmen, we’ve just witnessed {passengerPlane.Serial} take off.");
        }
    }

    public class Newspaper : Media
    {
        private string Name;
        public Newspaper(string name) { Name = name; }
        public override string doForArp(Airport airport)
        {
            return ($"{this.Name} - A report from the {airport.Name} airport, {airport.Country}");
        }

        public override string doForCP(CargoPlane cargoPlane)
        {
            return ($"{this.Name} - An interview with the crew of {cargoPlane.Serial}.");
        }

        public override string doForPP(PassengerPlane passengerPlane)
        {
            return ($"{this.Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}");
        }
    }

    public class Reporter
    {
        public void Report(ProductLists productLists)
        {
            List<Media> medias = new List<Media>
            {
                new Television("Telewizja Abelowa"),
                new Television("Kanał TV-tensor"),
                new Radio("Radio Kwantyfikator"),
                new Radio("Radio Shmem"),
                new Newspaper("Gazeta Kategoryczna"),
                new Newspaper("Dziennik Politechniczny")
            };
            List<IReportable> reportables = new List<IReportable>();
            reportables.AddRange(productLists.airportsdict.Values.ToList());
            reportables.AddRange(productLists.passangerPlanesdict.Values.ToList());
            reportables.AddRange(productLists.cargoPlanesdict.Values.ToList());
            NewsGenerator newsGenerator = new NewsGenerator(medias, reportables);
            string news;
            while ((news = newsGenerator.GenerateNews()) != null) { Console.WriteLine(news); }

        }
    }

}
