using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mythic.Utils;
using System.IO;

namespace Mythic.UtilsTests
{
    [TestClass]
    public class CsvUtilsTests
    {
        [TestMethod]
        public void TestReadCsv()
        {
            var data = CsvUtils.ReadCsv("Data/TestCsv.csv");
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
        public void TestWriteCsv()
        {
            var testFilePath = "TestCsv.csv";
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            var data = new object[][]
            {
                new object[] {"kek", 43, 2 },
                new object[] {true, 23.3f, .4d }
            };
            CsvUtils.WriteCsv(testFilePath, data);
            var read = CsvUtils.ReadCsv(testFilePath);
            Assert.AreEqual("kek", read[0][0]);
            Assert.AreEqual("43", read[0][1]);
            Assert.AreEqual("2", read[0][2]);
            Assert.AreEqual("True", read[1][0]);
            Assert.AreEqual("23.3", read[1][1]);
            Assert.AreEqual("0.4", read[1][2]);
        }
    }
}
