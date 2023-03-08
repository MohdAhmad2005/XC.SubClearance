using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Authentication;

namespace XC.XSC.Tests.Authentication
{
    [TestClass]
    public class ForgotPasswordTest
    {
        private readonly ForgotPasswordRequest _forgotPasswordRequest;

        public ForgotPasswordTest()
        {
            _forgotPasswordRequest = new ForgotPasswordRequest()
            {
                UserId = "mohd.ahmad@xceedance.com",
                NewPassword = "mohd@dtu",
                ConfirmPassword = "mohd@dtu"
            };
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com", "mohd@dtu", "mohd@dtu")]
        public void Forgot_Password_With_Valid_UserId_And_NewPassword_And_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase) 
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com", "mohd@dtu", "mohd@dtu1")]
        public void Forgot_Password_With_Valid_UserId_And_Different_NewPassword_And_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com1", "mohd@dtu", "mohd@dtu1")]
        public void Forgot_Password_With_InValid_UserId_And_Different_NewPassword_And_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("", "", "")]
        public void Forgot_Password_With_Blank_UserId_And_Blank_NewPassword_And_Blank_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com", "", "")]
        public void Forgot_Password_With_Valid_UserId_And_Blank_NewPassword_And_Blank_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com", "mohd@dtu", "")]
        public void Forgot_Password_With_Valid_UserId_And_NewPassword_And_Blank_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("mohd.ahmad@xceedance.com", "", "mohd@dtu")]
        public void Forgot_Password_With_Valid_UserId_And_Blank_NewPassword_And_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }

        [TestMethod]
        [DataRow("", "mohd@dtu", "mohd@dtu")]
        public void Forgot_Password_With_Blank_UserId_And_Match_NewPassword_And_ConfirmPassword(string userId, string newPassword, string confirmPassword)
        {
            bool is_Forgot_Password_Success = string.Equals(userId, _forgotPasswordRequest.UserId, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(newPassword, _forgotPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                 && string.Equals(confirmPassword, _forgotPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Forgot_Password_Success, true);
        }
    }
}
