using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1
{
    public class FieldsForTable
    {
        public (string[][], int[]) Get(List<string> fields, List<Product> result)
        {
            string[][] items = new string[result.Count][];
            int[] lengths = new int[fields.Count];
            for (int i = 0; i < result.Count; i++)
            {
                items[i] = new string[fields.Count];
                for (int j = 0; j < fields.Count; j++)
                {
                    if (fields[j].Length > lengths[j]) lengths[j] = fields[j].Length;
                    items[i][j] = result[i].Properties[fields[j]].ToString();
                    if (items[i][j].Length > lengths[j]) lengths[j] = items[i][j].Length;
                }
            }
            return (items, lengths);
        }
    }
    public class TableGenerator
    {
        public void Generate(string[] ColNames, int[] ColLengths, string[][] items)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < ColNames.Length; i++)
            {
                output.Append(String.Format(" {0, " + (-ColLengths[i]).ToString() + "}", ColNames[i]));
                if (i != ColNames.Length - 1) output.Append("|");
            }
            output.Append("\n");

            for (int j = 0; j < ColLengths.Length; j++)
            {
                for (int k = 0; k <= ColLengths[j]; k++)
                {
                    output.Append('-');
                }
                if (j != ColLengths.Length - 1) output.Append("+");
            }
            output.Append("\n");

            for (int i = 0; i < items.Length; i++)
            {

                for (int j = 0; j < items[i].Length; j++)
                {
                    output.Append(String.Format(" {0, " + ColLengths[j].ToString() + "}", items[i][j].ToString()));
                    if (j != ColNames.Length - 1) output.Append("|");
                }
                output.Append("\n");
            }
            Console.Write(output.ToString());
        }
    }
}
