using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp2;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Program.TestConnect();
        }

        [TestMethod]
        public void TestMethod2()
        {

            Program.TestSerializer();
        }

        [TestMethod]
        public void TestMethod3()
        {

            DateTime tt = DateTime.Now;

            long unixTimeStampInSeconds = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        [TestMethod]
        public void TestMethod4()
        {

        }

    }
}
