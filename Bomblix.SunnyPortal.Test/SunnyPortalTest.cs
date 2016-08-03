using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bomblix.SunnyPortal.Test
{
    [TestClass]
    public class SunnyPortalTest
    {

        private string userlogin = "YourLogin";
        private string userPassword = "YourPassword";

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
    }
}
