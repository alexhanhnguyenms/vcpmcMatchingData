using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Legacy;
using Vcpmc.Mis.UnicodeConverter.enums;

namespace Vcpmc.Mis.UnicodeConverterTest
{
    /// <summary>
    ///This is a test class for LegacyXLSTest and is intended
    ///to contain all LegacyXLSTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem("Test-data/vni.xls", "Test-data")]
    [DeploymentItem("Test-data/vni.xlsx", "Test-data")]
    public class LegacyXLSTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTestXls()
        {
            VietEncodings sourceEncoding = VietEncodings.VNI;
            LegacyXLS target = new LegacyXLS(sourceEncoding);
            string output = TestContext.TestDeploymentDir;
            DirectoryInfo outputDir = new DirectoryInfo("Test-data_Unicode");
            outputDir.Create();
            FileInfo file = new FileInfo(@"Test-data\vni.xls");
            Assert.IsTrue(file.Exists);
            target.Convert(outputDir, file);
            FileInfo outputFile = new FileInfo(Path.Combine(outputDir.FullName, file.Name));
            Assert.IsTrue(outputFile.Exists);
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTestXlsx()
        {
            VietEncodings sourceEncoding = VietEncodings.VNI;
            LegacyXLS target = new LegacyXLS(sourceEncoding);
            string output = TestContext.TestDeploymentDir;
            DirectoryInfo outputDir = new DirectoryInfo("Test-data_Unicode");
            outputDir.Create();
            FileInfo file = new FileInfo(@"Test-data\vni.xlsx");
            Assert.IsTrue(file.Exists);
            target.Convert(outputDir, file);
            FileInfo outputFile = new FileInfo(Path.Combine(outputDir.FullName, file.Name));
            Assert.IsTrue(outputFile.Exists);
        }
    }
}
