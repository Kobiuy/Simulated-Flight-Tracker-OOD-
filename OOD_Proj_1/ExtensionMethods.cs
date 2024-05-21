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
    public static class DictionaryExtension
    {
        public static bool UpdateKeyAndID<TValue>(this IDictionary<UInt64, TValue> dict, UInt64 From, UInt64 To) where TValue : Product
        {
            if (dict.ContainsKey(From))
            {
                TValue value = dict[From];
                value.ID = To;
                dict.Remove(From);
                dict.Add(To, value);
                LogManager.ChangedID(From, To);
                return true;
            }
            return false;
        }
    }
}
