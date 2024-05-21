using System.Text;

namespace OOD_Proj_1
{
    public class ComandParser
    {
        public Dictionary<string, Action<string[], ProductLists>> QueriesDict = new Dictionary<string, Action<string[], ProductLists>>()
        {
                            { "Display", (string[] query, ProductLists productLists)=>(new Displayer()).Parse(query, productLists) },
                            { "Update",(string[] query, ProductLists productLists)=>(new Updater()).Parse(query, productLists) },
                            { "Add",(string[] query, ProductLists productLists)=>(new Adder()).Parse(query, productLists) },
                            { "Delete", (string[] query, ProductLists productLists)=>(new Deleter()).Parse(query, productLists) },
        };
        public void ChooseCommand(string query, ProductLists productLists)
        {
            Action<string[], ProductLists> method;
            var query_sep = query.Split(' ');
            if (QueriesDict.TryGetValue(query_sep[0], out method))
                method(query_sep, productLists);
            else
                Console.WriteLine($"[{query}] nie jest poprawną komendą");
        }
    }

    public class GetData
    {
        public List<Product> WhereClause(List<string> conditions, List<Product> list)
        {
            List<Product> result = list;
            List<Product> getDataResult;
            if (conditions.Count > 0)
            {
                result = new List<Product>();
                string NowQuery = conditions[0] + " " + conditions[1] + " " + conditions[2];
                result.AddRange(from item in list where item.IsQueryTrue(NowQuery) select item);
                for (int i = 4; i < conditions.Count; i = i + 4)
                {
                    NowQuery = conditions[i] + " " + conditions[i + 1] + " " + conditions[i + 2];
                    if (conditions[i - 1] == "or")
                    {
                        getDataResult = (from item in list where item.IsQueryTrue(NowQuery) select item).ToList();
                        for (int j = 0; j < getDataResult.Count; j++)
                            if (!result.Contains(getDataResult[j])) result.Add(getDataResult[j]);
                    }
                    else
                        result = (from item in result where item.IsQueryTrue(NowQuery) select item).ToList();
                }
            }
            return result;
        }
        public void GetDataAndGenerateTable(List<string> conditions, List<string> fields, List<Product> list) 
        {
            if (fields[0] == "*")
                fields = list[0].Properties.Keys.ToList();

            TableGenerator tableGenerator = new TableGenerator();
            var result = WhereClause(conditions, list);
            FieldsForTable fieldsForTable = new();
            (string[][] items, int[] lengths) = fieldsForTable.GetDataAndLengths(fields, result);
            tableGenerator.Generate(fields.ToArray(), lengths, items);
        }
    }

    public abstract class Query
    {
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
                fields.Add(query[ID]);
            if (!(query[ID] == "from"))
                throw new Exception("Zła składnia komendy");

            from = query[++ID];
            ID++;
            while (ID < query.Length - 1)
                conditions.Add(query[++ID]);

            GetData getData = new GetData();
            getData.GetDataAndGenerateTable(conditions, fields, productLists.StringToProductList[from]());
        }
    }

    public class Updater : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {
            string class_name = query[1];
            int ID = 2;
            List<string> KVL = new List<string>();

            while (ID < query.Length - 1 && query[++ID] != "where")
                KVL.Add(query[ID]);

            var items = productLists.StringToProductList[class_name]();
            if (query[ID] == "where")
            {
                GetData getData = new GetData();
                items = (getData.WhereClause(query[(ID + 1)..(query.Length)].ToList(), productLists.StringToProductList[class_name]())).ToList<Product>();
            }

            foreach (var item in items)
            {
                for (int i = 0; i < KVL.Count(); i++)
                {
                    var splitted = KVL[i].Split('=');
                    item.Properties[splitted[0]] = item.Parser[splitted[0]](splitted[1]);
                }
            }
        }
    }
    public class Deleter : Query
    {
        Dictionary<string, Action<Product, ProductLists>> RemoveItemFromAdequateDictionary = new Dictionary<string, Action<Product, ProductLists>>
        {
            {"PassangerPlane", (Product item, ProductLists productLists )=>
                    productLists.passangerPlanesdict.Remove(item.ID) },
            {"CargoPlane",  (Product item, ProductLists productLists )=>
                    productLists.cargoPlanesdict.Remove(item.ID) },
            {"Cargo",  (Product item, ProductLists productLists )=>
                    productLists.cargotsdict.Remove(item.ID) },
            {"Crew", (Product item, ProductLists productLists )=>
                    productLists.crewsdict.Remove(item.ID) },
            {"Airport",  (Product item, ProductLists productLists )=>
                    productLists.airportsdict.Remove(item.ID) },
            {"Passenger", (Product item, ProductLists productLists )=>
                    productLists.passangersdict.Remove(item.ID) },
            {"Flight", (Product item, ProductLists productLists )=>
                    productLists.flightsdict.Remove(item.ID) },
        };
        public override void Parse(string[] query, ProductLists productLists)
        {
            string class_name = query[1];
            GetData getData = new GetData();
            var items = getData.WhereClause(query[3..(query.Length)].ToList(), productLists.StringToProductList[class_name]());
            foreach (var item in items)
                RemoveItemFromAdequateDictionary[class_name](item, productLists);
        }
    }
    public class Adder : Query
    {
        public override void Parse(string[] query, ProductLists productLists)
        {
            string class_name = query[1];
            Factory factory = new Factory();
            int ID = 2;
            StringBuilder sb = new StringBuilder();
            sb.Append(class_name);
            while (++ID < query.Length)
            {
                sb.Append(',');
                sb.Append(query[ID].Split('=')[1]);
            }
            factory.Create(sb.ToString(), productLists);
        }
    }
}
