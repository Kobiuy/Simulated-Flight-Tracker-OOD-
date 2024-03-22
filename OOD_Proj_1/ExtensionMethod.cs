using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class DateTimeExtension
    {
        public static int GetSeconds(this DateTime dt)
        {
            return ((dt.Hour * 60 + dt.Minute) * 60 + dt.Second);
        }
    }  
    public static class StringExtension
    {
        public static List<UInt64> ToUInt64List(this string str) // Method converting string starting with "[", ending with "]" containing values separated by ";" to list of UInt64 these values.
        {
            List<UInt64> UInt64List = new List<UInt64>();
            string[] Elements = str.Replace('[', ' ').Replace(']', ' ').Trim().Split(';');
            if (Elements[0] == "") { return new List<UInt64>(); } // Returning empty list if there are no values inside string "str" .
            foreach (string element in Elements)
            {
                UInt64List.Add(UInt64.Parse(element));
            }
            return UInt64List;
        }
    }
   
}
