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
        public const string DataSource = "SIM_SERVER"; // Choose from ["FILE", "SIM_SERVER"]
        public const string FileName = "example_data.ftr"; // Set source file name
        public const int SimMin = 1;
        public const int SimMax = 10;
        //public const string OutputFileName = "„snapshot_HH_MM_SS.json"; // Set output file name
    }
}
