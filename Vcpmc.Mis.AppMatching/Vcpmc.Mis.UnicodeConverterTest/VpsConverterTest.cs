using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.UnicodeConverter;

namespace Vcpmc.Mis.UnicodeConverterTest
{
    /// <summary>
    ///This is a test class for VpsConverterTest and is intended
    ///to contain all VpsConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VpsConverterTest
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
        ///A test for ConvertText
        ///</summary>
        [TestMethod()]
        public void ConvertTextTestVps()
        {
            VpsConverter target = new VpsConverter();
            string str = "Hôm nay Çi chùa HÜÖng/Hoa cÕ m© hÖi sÜÖng";
            string expected = "Hôm nay đi chùa Hương/Hoa cỏ mờ hơi sương";
            string actual;
            actual = target.ConvertText(str);
            Assert.AreEqual(expected, actual);
        }
    }
}
