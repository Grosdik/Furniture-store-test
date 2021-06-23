using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Test_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckPassword1()
        {
            string password = "123";
            bool expected = true;

            bool actual = AuthenticationLib.Class1.ValidetePassword(password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckPassword2()
        {
            string password = "_Manager_";
            bool expected = true;

            bool actual = AuthenticationLib.Class1.ValidetePassword(password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckPassword3()
        {
            string password = "Manager";
            bool expected = false;

            bool actual = AuthenticationLib.Class1.ValidetePassword(password);
            Assert.AreEqual(expected, actual);
        }
    }
}
