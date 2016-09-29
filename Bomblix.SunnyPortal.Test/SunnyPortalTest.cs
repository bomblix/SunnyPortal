using Bomblix.SunnyPortal.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Bomblix.SunnyPortal.Test
{
    [TestClass]
    public class SunnyPortalTest
    {

        private string userlogin = "user";
        private string userPassword = "passw";

        [TestMethod]
        public async Task ConnectInvalidPasswordTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal();

            var result = await SunnyPortal.Connect( "user", "user" );

            Assert.IsFalse( SunnyPortal.IsConnected );
            Assert.AreEqual( result.Message, "Invalid login or password" );
        }


        [TestMethod]
        public async Task ConnectSuccessfullTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal( );

            var result = await SunnyPortal.Connect( userlogin, userPassword );

            Assert.IsTrue( SunnyPortal.IsConnected );
            Assert.AreEqual( result.Message, string.Empty );
        }

        [TestMethod]
        public async Task GetCurrentPowerTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal( );

            await SunnyPortal.Connect( userlogin, userPassword );
            var result = await SunnyPortal.GetCurrentPower();

            Assert.AreNotEqual( -1, result );
        }

        [TestMethod]
        public async Task DownloadDiagramDataTest()
        {
            var SunnyPortal = new SunnyPortal.Core.SunnyPortal();

            await SunnyPortal.Connect (userlogin, userPassword );
            var result = await SunnyPortal.GetHistoricalDailyData(DateTime.Now.AddDays(-2));

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
