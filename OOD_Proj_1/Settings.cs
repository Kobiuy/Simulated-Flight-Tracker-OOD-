using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class Settings
    {
        public const string SerializationType = "JSON"; // Choose from ["JSON"]
        public const string DataSource = "FILEwUPDATES"; // Choose from ["FILE", "SIM_SERVER", "FILEwUPDATES"]
        public const string FileName = "example_data.ftr"; // Set source file name
        public const string UpdatesFileName = "example.ftre"; // Set source file name for updates
        public const int SimMin = 0; // Set minimum time between messages 
        public const int SimMax = 1; // Set maximum time between messages 
    }
}