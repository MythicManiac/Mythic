using System;
using System.Collections.Generic;
using System.IO;

namespace Mythic.Utils
{
    public class CsvUtils
    {
        public static string[][] Read(string path)
        {
            var result = new List<string[]>();
            var csvDatum = File.ReadAllLines(path);
            foreach (var line in csvDatum)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                result.Add(line.Split(','));
            }
            return result.ToArray();
        }

        public static void Write<T>(string path, T[][] data)
        {
            var stream = new FileStream(path, FileMode.Create);
            var writer = new StreamWriter(stream);
            foreach (var entry in data)
            {
                writer.WriteLine(string.Join(",", entry));
            }
            writer.Flush();
            stream.Close();
            stream.Dispose();
        }

        public static T[][] ConvertValues<T>(string[][] values)
        {
            var result = new T[values.Length][];
            for (var i = 0; i < values.Length; i++)
            {
                result[i] = new T[values[i].Length];
                for (var j = 0; j < values[i].Length; j++)
                {
                    result[i][j] = (T)Convert.ChangeType(values[i][j], typeof(T));
                }
            }
            return result;
        }

        public static T[][] ReadAndConvert<T>(string path)
        {
            var values = Read(path);
            return ConvertValues<T>(values);
        }

        public static Tuple<T1[], T2[]>[] ReadDataListPair<T1, T2>(string file1, string file2)
        {
            var items1 = ReadAndConvert<T1>(file1);
            var items2 = ReadAndConvert<T2>(file2);

            if (items1.Length != items2.Length)
                throw new DataListPairLengthException();

            var result = new Tuple<T1[], T2[]>[items1.Length];
            for(var i = 0; i < items1.Length; i++)
            {
                result[i] = Tuple.Create(items1[i], items2[i]);
            }
            return result;
        }
    }

    public class DataListPairLengthException : Exception
    {
        public DataListPairLengthException() : base("Data list pairs length don't match") { }
    }
}
