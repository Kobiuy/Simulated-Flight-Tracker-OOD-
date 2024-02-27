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
        public const string DataSource = "FILE"; // Choose from ["FILE"]
        public const string FileName = "example_data.ftr"; // Set source file name
        public const string OutputFileName = "Products.json"; // Set output file name
    }
}
