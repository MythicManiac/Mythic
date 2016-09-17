using System.Collections.Generic;
using System.IO;

namespace Mythic.Utils
{
    public class CsvUtils
    {
        public static string[][] ReadCsv(string path)
        {
            var result = new List<string[]>();
            var csvDatum = File.ReadAllLines(path);
            foreach(var line in csvDatum)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;

                result.Add(line.Split(','));
            }
            return result.ToArray();
        }

        public static void WriteCsv(string path, object[][] data)
        {
            var buffer = new MemoryStream();
            var writer = new StreamWriter(buffer);
            foreach(var entry in data)
            {
                writer.WriteLine(string.Join(",", entry));
            }
            writer.Flush();
            File.WriteAllBytes(path, buffer.ToArray());
        }
    }
}
