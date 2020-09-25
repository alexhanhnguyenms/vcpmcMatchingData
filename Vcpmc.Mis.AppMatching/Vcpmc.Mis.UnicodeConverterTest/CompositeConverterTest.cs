using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vcpmc.Mis.UnicodeConverter;

namespace Vcpmc.Mis.UnicodeConverterTest
{
    // <summary>
    ///This is a test class for CompositeConverterTest and is intended
    ///to contain all CompositeConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompositeConverterTest
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
        public void ConvertTextTestComposite()
        {
            //VniConverter targetVni = new VniConverter();
            CompositeConverter target = new CompositeConverter(); // TODO: Initialize to an appropriate value
            //string str = "Hoâm nay ñi chuøa Höông/Hoa coû môø hôi söông";
            string str = "Hôm nay đi chùa Hương/Hoa cỏ mờ hơi sương";
            string expected = "Hôm nay đi chùa Hương/Hoa cỏ mờ hơi sương";
            string actual;            
            //actual = targetVni.ConvertText(str);
            actual = target.ConvertText(str);
            Assert.AreEqual(expected, actual);
        }
    }
}
