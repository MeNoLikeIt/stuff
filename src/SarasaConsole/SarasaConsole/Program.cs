using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SarasaConsole
{
    class Program
    {
        static void Main(string[] args)
        
        {

            Console.WriteLine("Veamos el {0:D2} con :D2", 1);
            Console.WriteLine("Veamos el {0:d} con d", 1);
            Console.ReadKey();

        }

        private static void mergeDictionaryStringListInt()
        {
            var global = new Dictionary<string, List<int>> { { "sarasa", new List<int> { 1, 2, 3, 4 } }, { "agustin", new List<int> { 5, 6, 7 } } };
            var newDic = new Dictionary<string, List<int>> { { "sarasa", new List<int> { 3, 4, 5, 6 } }, { "wikitiki", new List<int> { 5, 6, 7 } } };

            foreach (var item in global)
            {
                var result = new List<int>(item.Value);
                foreach (var newItem in newDic.Where(newItem => item.Key == newItem.Key))
                {
                    result.AddRange(newItem.Value.Where(n => item.Value.All(g => g.ToString() != n.ToString())));
                }
                item.Value.Clear();
                item.Value.AddRange(result);
            }
            var final = newDic.Where(x => !global.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            foreach (var sarasa in final)
            {
                global.Add(sarasa.Key, sarasa.Value);
            }

        }

        private void mergeDictionaryStringString()
        {
            var global = new Dictionary<string, string> { { "A01", "A cero uno" }, { "A02", "A cero dos" }, { "A03", "A cero tres" }, { "A04", "A cero cuatro" } };

            var newDic = new Dictionary<string, string> { { "A03", "A cero tres" }, { "A04", "A cero cuatro" }, { "A05", "A cero cinco" }, { "A06", "A cero seis" } };

            var result = newDic.Where(x => !global.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);

            foreach (var sarasa in result)
            {
                global.Add(sarasa.Key, sarasa.Value);
            }
        }

        private static void handlingTuples()
        {
            var serializer = new JavaScriptSerializer();
            var sarasaKpi = new BenchmarkCellsDTO
                {
                    DatasetId = Guid.NewGuid(),
                    CalculatedCells = serializer.Serialize(new List<Cells>
                        {
                            new Cells
                                {
                                    CountryCode = 323,
                                    KPI = "TRIM INDEX",
                                    Industry = "Telecommunications",
                                    Value = 76.0
                                },
                            new Cells {CountryCode = 421, Industry = "Food", KPI = "PERFOMANCE", Value = 51.3},
                            new Cells {CountryCode = 555, Industry = "Software", KPI = "PREFERENCE", Value = 45.9}
                        })
                };
            
            Console.WriteLine(serializer.Serialize(sarasaKpi));
            var boom = serializer.Deserialize<BenchmarkCellsDTO>(serializer.Serialize(sarasaKpi));
            var paf = serializer.Deserialize<IEnumerable<Cells>>(boom.CalculatedCells);
            Console.ReadKey();
        }
    }

    /// <summary>
    /// KPI calculation results for a benchmark
    /// </summary>
    public class BenchmarkCellsDTO
    {
        /// <summary>
        /// Identifies the dataset/benchmark
        /// </summary>
        public Guid DatasetId { get; set; }

        /// <summary>
        /// A collection of the cells calculated for the current benchmark
        /// </summary>
        public string CalculatedCells { get; set; }
    }

    /// <summary>
    /// KPI calculation result class
    /// </summary>
    public class Cells
    {
        /// <summary>
        /// country for the country
        /// </summary>
        public int CountryCode { get; set; }
        /// <summary>
        /// Industry
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// KPI 
        /// </summary>
        public string KPI { get; set; }
        /// <summary>
        /// Calculation result
        /// </summary>
        public double Value { get; set; }
    }
}
