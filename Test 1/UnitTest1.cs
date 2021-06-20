using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Test_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AuthenticationTest()
        {
            var auth = AuthenticationLib.Class1.Auth("Manager","Manager");

            Assert.AreEqual(2, auth);
        }
    }
}
