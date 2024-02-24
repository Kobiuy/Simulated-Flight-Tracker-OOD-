using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace OOD_Proj_1
{    
    public class Factory
    {
        public object Create(string txt)
        {
            string[] words = txt.Split(' ');
            switch (words[0])
            {
                case ("C"):
                    return new Crew(txt);
                    break;
                case ("P"):
                    return new Passanger(txt);
                    break;
                case ("CA"):
                    return new Cargo(txt);
                    break;
                case ("CP"):
                    return new CargoPlane(txt);
                    break;
                case ("PP"):
                    return new PassangerPlane(txt);
                    break;
                case ("AI"):
                    return new Airport(txt);
                    break;
                case ("FL"):
                    return new Fligth(txt);
                    break;
            }
            return null;
        }
    }
    
    //public class 
    internal class Program
    {
        static void Main(string[] args)
        {
            string FileName = "ExampleData.ftr";
            Factory factory = new Factory();
            
        }
    }
}