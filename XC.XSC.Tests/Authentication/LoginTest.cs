using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XC.XSC.ViewModels.Authentication;

namespace XC.XSC.Tests.Authentication
{
    [TestClass]
    public class LoginTest
    {
        private readonly TokenRequest _tokenRequest;
        
        public LoginTest() {
            _tokenRequest = new TokenRequest()
            {
                Password = "testuser",
                UserName = "testuser@xceed.com"
            };
        }

        [TestMethod]
        [DataRow("testuser@xceed.com", "testuser")]
        public void Log_In_With_Valid_UserName_And_Password(string userName, string password)
        {            
            bool is_Login_Success = string.Equals(userName, _tokenRequest.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(password, _tokenRequest.Password, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Login_Success, true);
        }

        [TestMethod]
        [DataRow("test_user@xceed.com", "testuser")]
        public void Log_In_With_InValid_UserName_And_Password(string userName, string password)
        {
            bool is_Login_Success = string.Equals(userName, _tokenRequest.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(password, _tokenRequest.Password, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Login_Success, true);
        }

        [TestMethod]
        [DataRow("", "testuser")]
        public void Log_In_With_Blank_UserName(string userName, string password)
        {
            bool is_Login_Success = string.Equals(userName, _tokenRequest.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(password, _tokenRequest.Password, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Login_Success, true);
        }

        [TestMethod]
        [DataRow("test_user@xceed.com", "")]
        public void Log_In_With_Blank_Password(string userName, string password)
        {
            bool is_Login_Success = string.Equals(userName, _tokenRequest.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(password, _tokenRequest.Password, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Login_Success, true);
        }

        [TestMethod]
        [DataRow("", "")]
        public void Log_In_With_Blank_UserName_And_Password(string userName, string password)
        {
            bool is_Login_Success = string.Equals(userName, _tokenRequest.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(password, _tokenRequest.Password, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Login_Success, true);
        }


    }
}
