using Bomblix.SunnyPortal.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bomblix.SunnyPortal.Test
{
    [TestClass]
    public class SunnyPortalTest
    {

        private string userlogin = "user";
        private string userPassword = "passw";

        [TestMethod]
        public void ConnectInvalidPasswordTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal("user","user");

            var result = SunnyPortal.Connect();

            Assert.IsFalse( SunnyPortal.IsConnected );
            Assert.AreEqual( result.Message, "Invalid login or password" );
        }


        [TestMethod]
        public void ConnectSuccessfullTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal( userlogin, userPassword );

            var result = SunnyPortal.Connect();

            Assert.IsTrue( SunnyPortal.IsConnected );
            Assert.AreEqual( result.Message, string.Empty );
        }

        [TestMethod]
        public void GetCurrentPowerTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal( userlogin, userPassword );

            SunnyPortal.Connect();
            var result = SunnyPortal.GetCurrentPower();

            Assert.AreNotEqual( -1, result );
        }

        [TestMethod]
        public void DownloadDiagramDataTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal( userlogin, userPassword );

            SunnyPortal.Connect();
            var result = SunnyPortal.GetHistoricalData(DateTime.Now.AddDays(-2));

            Assert.IsNotNull( result );
        }

        [TestMethod]
        public void ExtractDataFromEmptyCsvTest()
        {
            var z = CsvHelper.ExtractToDictionary( "" );
            Assert.AreEqual( 0, z.Count );
        }

        [TestMethod]
        public void ExtractDataFromInvalidCsvTest()
        {
            var invalidCsv = 
                @"test tehst <html>;asasas
                laslkas;as;as;as;as;s
                asasasas";

            var z = CsvHelper.ExtractToDictionary( invalidCsv );
            Assert.AreEqual( 0, z.Count );
        }

        [TestMethod]
        public void ExtractDataFromValidCsvTest()
        {
            var invalidCsv =
                @"Time;Value;
                12:32;9.32;
                1:30;3.2;";

            var z = CsvHelper.ExtractToDictionary( invalidCsv );
            Assert.AreEqual( 2, z.Count );
        }

        [TestMethod]
        public void ExtractDataFromCsvWithOneColumn()
        {
            var invalidCsv =
                @"Time
                12:32
                1:30";

            var z = CsvHelper.ExtractToDictionary( invalidCsv );
            Assert.AreEqual( 0, z.Count );
        }
    }
}
