namespace OOD_Proj_1
{
    public static class Settings
    {
        public const string SerializationType = "JSON"; // Choose from ["JSON"]
        public const string DataSource = "FILEwUPDATES"; // Choose from ["FILE", "SIM_SERVER", "FILEwUPDATES"]
        public const string FileName = "example_data.ftr"; // Set source file name
        public const string ServerFileName = "example.ftre"; // Set source file name for updates | When using "SIM_SERVER" set as equal to FileName
        public const int SimMin = 1; // Set minimum time between messages 
        public const int SimMax = 10; // Set maximum time between messages 
    }
}