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
            var reader = new StreamReader(new FileStream(path, FileMode.Open));
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
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
            var result = new List<T[]>();
            var reader = new StreamReader(new FileStream(path, FileMode.Open));
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var strings = line.Split(',');
                var values = new T[strings.Length];
                for(var i = 0; i < strings.Length; i++)
                {
                    values[i] = (T)Convert.ChangeType(strings[i], typeof(T));
                }
                result.Add(values);
            }
            return result.ToArray();
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

        public static void WriteDataListPair<T1, T2>(string path1, string path2, Tuple<T1[], T2[]>[] data)
        {
            var writer1 = new StreamWriter(new FileStream(path1, FileMode.Create));
            var writer2 = new StreamWriter(new FileStream(path2, FileMode.Create));
            foreach (var entry in data)
            {
                writer1.WriteLine(string.Join(",", entry.Item1));
                writer2.WriteLine(string.Join(",", entry.Item2));
            }
            writer1.Close();
            writer2.Close();
        }
    }

    public class DataListPairLengthException : Exception
    {
        public DataListPairLengthException() : base("Data list pairs length don't match") { }
    }
}
