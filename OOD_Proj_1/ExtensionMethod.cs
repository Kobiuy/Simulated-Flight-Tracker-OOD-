using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public static class StringExtension
    {
        public static List<UInt64> ToUInt64List(this string str)
        {
            List<UInt64> UInt64List = new List<UInt64>();
            string[] Elements = str.Replace('[', ' ').Replace(']', ' ').Trim().Split(';');
            foreach (string element in Elements)
            {
                UInt64List.Add(UInt64.Parse(element));
            }
            return UInt64List;
        }
    }
}
