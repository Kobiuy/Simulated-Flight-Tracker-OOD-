using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using NetworkSourceSimulator;
using FlightTrackerGUI;
using ExCSS;
using Mapsui.Projections;
using Mapsui;
using System;
namespace OOD_Proj_1
{
    public abstract class Media()
    {
        public abstract void doForArp();
        public abstract void doForPP();
        public abstract void doForCP();
    }
    public interface IReportable
    {
        public void Accept(Media medium);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataImporter importer = new DataImporter();
            importer.ImportData();
            Menu.StartMenu();
        }
    }
}