using Microsoft.VisualStudio.TestTools.UnitTesting;
using RULEtraining.model;
using RULEtraining.pages;
using System;
using System.Windows;

namespace UnitTestTraining
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAuthorization_Successful()
        {
            Authorization user = new Authorization();
            user.ID = 5;
            user.Login = "admin";
            user.Password = "serega";
            user.ID_Role = 3;
            AuthPage authPage = new AuthPage();
            Assert.IsTrue(authPage.Auth(user));
        }
    }
}
