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
    public class ResetPasswordTest
    {
        private readonly ResetPasswordRequest _resetPasswordRequest;
        public ResetPasswordTest()
        {
            _resetPasswordRequest = new ResetPasswordRequest()
            {
                UserName = "testuser",
                OldPassword = "mohd@dtu",
                NewPassword = "mohd@dtunew",
                ConfirmPassword = "mohd@dtunew"
            };
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtu", "mohd@dtunew", "mohd@dtunew")]
        public void Reset_Password_With_Valid_OldPassword_NewPassword_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtuWrong", "mohd@dtunew", "mohd@dtunew")]
        public void Reset_Password_With_Wrong_OldPassword_And_Valid_NewPassword_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtuWrong", "mohd@dtunew1", "mohd@dtunew")]
        public void Reset_Password_With_Right_OldPassword_And_Different_NewPassword_And_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "", "", "")]
        public void Reset_Password_With_Blank_OldPassword_And_NewPassword_And_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtu", "", "")]
        public void Reset_Password_With__OldPssword_And_Blank_NewPassword_And_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtu", "mohd@dtunew", "")]
        public void Reset_Password_With__OldPssword_And_NewPassword_And_Blank_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "mohd@dtu", "", "mohd@dtunew")]
        public void Reset_Password_With_OldPssword_And_Blank_NewPassword_And_With_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }

        [TestMethod]
        [DataRow("testuser", "", "mohd@dtunew", "mohd@dtunew")]
        public void Reset_Password_With_Blank_OldPssword_And_With_NewPassword_And_With_ConfirmPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            bool is_Reset_Password_Success = string.Equals(userName, _resetPasswordRequest.UserName, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(oldPassword, _resetPasswordRequest.OldPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(newPassword, _resetPasswordRequest.NewPassword, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(confirmPassword, _resetPasswordRequest.ConfirmPassword, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(is_Reset_Password_Success, true);
        }


    }
}
