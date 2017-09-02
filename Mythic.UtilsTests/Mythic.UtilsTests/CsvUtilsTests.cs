using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mythic.Utils;
using System.IO;

namespace Mythic.UtilsTests
{
    [TestClass]
    public class CsvUtilsTests
    {
        [TestMethod]
        public void TestRead()
        {
            var data = CsvUtils.Read("Data/TestRead.csv");
            Assert.AreEqual("firstColumn", data[0][0]);
            Assert.AreEqual("secondColumn", data[0][1]);
            Assert.AreEqual("thirdColumn", data[0][2]);
            Assert.AreEqual("25", data[1][0]);
            Assert.AreEqual("39", data[1][1]);
            Assert.AreEqual("50", data[1][2]);
            Assert.AreEqual("12", data[2][0]);
            Assert.AreEqual("30", data[2][1]);
            Assert.AreEqual("53", data[2][2]);
        }

        [TestMethod]
        public void TestWrite()
        {
            var path = "TestWrite.csv";
            Clear(path);
            var data = new object[][]
            {
                new object[] {"kek", 43, 2 },
                new object[] {true, 23.3f, .4d }
            };
            CsvUtils.Write(path, data);
            var read = CsvUtils.Read(path);
            Assert.AreEqual("kek", read[0][0]);
            Assert.AreEqual("43", read[0][1]);
            Assert.AreEqual("2", read[0][2]);
            Assert.AreEqual("True", read[1][0]);
            Assert.AreEqual("23.3", read[1][1]);
            Assert.AreEqual("0.4", read[1][2]);
        }

        [TestMethod]
        public void TestConvertValues()
        {
            var path = "TestConvertValues.csv";
            Clear(path);
            var data = new double[][]
            {
                new double[] { 2.4, 2.5, 7.3 },
                new double[] { -2.3, -0.01, -6 }
            };
            CsvUtils.Write(path, data);
            var read = CsvUtils.Read(path);
            var converted = CsvUtils.ConvertValues<double>(read);
            for(var i = 0; i < data.Length; i++)
            {
                CollectionAssert.AreEqual(data[i], converted[i]);
            }
        }

        [TestMethod]
        public void TestReadAndConvert()
        {
            var path = "TestReadAndConvert.csv";
            Clear(path);
            var data = new double[][]
            {
                new double[] { 2.4, 2.5, 7.3 },
                new double[] { -2.3, -0.01, -6 }
            };
            CsvUtils.Write(path, data);
            var converted = CsvUtils.ReadAndConvert<double>(path);
            for (var i = 0; i < data.Length; i++)
            {
                CollectionAssert.AreEqual(data[i], converted[i]);
            }
        }

        [TestMethod]
        public void TestReadDataListPairSuccess()
        {
            var file1 = Clear("TestCsv1.csv");
            var file2 = Clear("TestCsv2.csv");

            var data1 = new double[][]
            {
                new double[] { 2.3, 93.2, -28 },
                new double[] { 0.00001, -329.45, 9.9999 }
            };
            var data2 = new string[][]
            {
                new string[] { "Madness?", "...", "No..." },
                new string[] { "This", "IS", "SPARTA!" }
            };

            CsvUtils.Write(file1, data1);
            CsvUtils.Write(file2, data2);

            var pairs = CsvUtils.ReadDataListPair<double, string>(file1, file2);
            for(var i = 0; i < pairs.Length; i++)
            {
                CollectionAssert.AreEqual(data1[i], pairs[i].Item1);
                CollectionAssert.AreEqual(data2[i], pairs[i].Item2);
            }
        }

        private string Clear(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            return path;
        }
    }
}
