using System.Text;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace OOD_Proj_1
{
    // Kreacja komend
    public class ComandParser
    {
        public Dictionary<string, Action<string[], ProductLists>> QueriesDict = new Dictionary<string, Action<string[], ProductLists>>()
        {
                            { "Display", (string[] query, ProductLists productLists)=>(new Displayer()).Parse(query, productLists) },
                            { "Update",(string[] query, ProductLists productLists)=>(new Displayer()).Parse(query, productLists) },
                            { "Add",(string[] query, ProductLists productLists)=>(new Displayer()).Parse(query, productLists) },
                            { "Delete", (string[] query, ProductLists productLists)=>(new Displayer()).Parse(query, productLists) },

        };
        public void ChooseCommand(string query, ProductLists productLists)
        {
            Action<string[], ProductLists> method;
            var query_sep = query.Split(' ');
            if (QueriesDict.TryGetValue(query_sep[0], out method))
            {
                method(query_sep, productLists);
            }
            else
            {
                Console.WriteLine($"[{query}] nie jest poprawną komendą");
            }

        }
    }


    public class temp<T> where T : Product
    {
        public void SART(List<string> conditions, List<string> fields, List<T> list)
        {
            TableGenerator tableGenerator = new TableGenerator();

            List<T> result = list;
            List<T> tempResult;
            List<T> tempResultForAndCondition;
            if (conditions.Count > 0)
            {
                result = new List<T>();
                string NowQuery = conditions[0] + conditions[1] + conditions[2];
                result.AddRange(from item in list where item.IsQueryTrue(NowQuery) select item);
                for (int i = 3; i < conditions.Count; i = i + 3)
                {
                    NowQuery = conditions[i] + conditions[i + 1] + conditions[i + 2];
                    tempResult = (from item in list where item.IsQueryTrue(NowQuery) select item).ToList();
                    if (conditions[i - 1] == "or")
                    {
                        for (int j = 0; j < tempResult.Count; j++)
                        {
                            if (!result.Contains(tempResult[j])) result.Add(tempResult[j]);
                        }
                    }
                    else
                    {
                        tempResultForAndCondition = new List<T>();
                        for (int j = 0; j < result.Count; j++)
                        {
                            if (tempResult.Contains(result[j])) tempResultForAndCondition.Add(tempResult[j]);
                        }
                        result = tempResultForAndCondition;
                    }
                }
            }
            FieldsForTable<T> fieldsForTable = new();
            (string[][] items, int[] lengths) = fieldsForTable.Get(fields, result);
            tableGenerator.Generate(fields.ToArray(), lengths, items);
        }
    }

    public interface IGetData
    {
        public void SelectAndRunTable(List<string> conditions, List<string> fields, ProductLists productLists);
    }
    public class FromPP : IGetData
    {
        public void SelectAndRunTable(List<string> conditions, List<string> fields, ProductLists productLists)
        {
            TableGenerator tableGenerator = new TableGenerator();

            List<PassengerPlane> result = productLists.passangerPlanesdict.Values.ToList();
            List<PassengerPlane> tempResult;
            List<PassengerPlane> tempResultForAndCondition;
            if (conditions.Count > 0)
            {
                result = new List<PassengerPlane>();
                string NowQuery = conditions[0] + conditions[1] + conditions[2];
                result.AddRange(from item in productLists.passangerPlanesdict.Values where item.IsQueryTrue(NowQuery) select item);
                for (int i = 3; i < conditions.Count; i = i + 3)
                {
                    NowQuery = conditions[i] + conditions[i + 1] + conditions[i + 2];
                    tempResult = (from item in productLists.passangerPlanesdict.Values where item.IsQueryTrue(NowQuery) select item).ToList();
                    if (conditions[i - 1] == "or")
                    {
                        for (int j = 0; j < tempResult.Count; j++)
                        {
                            if (!result.Contains(tempResult[j])) result.Add(tempResult[j]);
                        }
                    }
                    else
                    {
                        tempResultForAndCondition = new List<PassengerPlane>();
                        for (int j = 0; j < result.Count; j++)
                        {
                            if (tempResult.Contains(result[j])) tempResultForAndCondition.Add(tempResult[j]);
                        }
                        result = tempResultForAndCondition;
                    }
                }
            }
            FieldsForTable<PassengerPlane> fieldsForTable = new();
            (string[][] items, int[] lengths) = fieldsForTable.Get(fields, result);
            tableGenerator.Generate(fields.ToArray(), lengths, items);
        }
    }

    public class FieldsForTable<T> where T : Product
    {
        public (string[][], int[]) Get(List<string> fields, List<T> result)
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

    public abstract class Query
    {
        public Dictionary<string, IGetData> stringToAction = new Dictionary<string, IGetData>
        {
            {"PassangerPlane",  new FromPP()}
        };

        public abstract void Parse(string[] query, ProductLists productLists);
    }

    public class Displayer : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {

            int ID = 0;
            string from;
            List<string> conditions = new List<string>();
            List<string> fields = new List<string>();
            while (ID < query.Length - 2 && query[++ID] != "from")
            {
                fields.Add(query[ID]);
            }
            if (!(query[ID] == "from"))
                throw new Exception("Zła składnia komendy");

            from = query[++ID];
            ID++;
            while (ID < query.Length - 1)
            {
                conditions.Add(query[++ID]);
            }
            stringToAction[from].SelectAndRunTable(conditions, fields, productLists);
        }

    }
    public class Updater : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {
            throw new NotImplementedException();
        }
    }
    public class Deleter : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {
            throw new NotImplementedException();
        }
    }
    public class Adder : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {
            throw new NotImplementedException();
        }
    }

    public class TableGenerator
    {
        public void Generate(string[] ColNames, int[] ColLengths, string[][] items)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < ColNames.Length; i++)
            {
                output.Append(String.Format(" {0, " + ColLengths[i].ToString() + "}", ColNames[i]));
                if (i != ColNames.Length - 1) output.Append("|");
            }
            output.Append("\n");
            // Empty row
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
