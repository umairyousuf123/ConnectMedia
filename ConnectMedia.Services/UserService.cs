using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace ConnectMedia.Services
{
    public class UserService : IUserServices
    {
        IUserRepository _userRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<UserService> _logger;
        readonly EmailSetting _emailSetting;


        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IConfiguration configuration, IOptionsSnapshot<EmailSetting> emailSetting)
        {
            _logger = logger;
            this._userRepository = userRepository;
            this._configuration = configuration;
            this._emailSetting = emailSetting.Value;
        }

        public async Task<ClaimModel> UserLogin(string email, string userPassword)
        {
            ClaimModel claimModel = new ClaimModel();
            try
            {
                User user = _userRepository.UserLogin(email);
                SecureString userPasswordSecure = new NetworkCredential("", userPassword).SecurePassword;
                SecureString userPasswordSecureDb = new NetworkCredential("", user.password).SecurePassword;
                bool isValid = Rules.ComparePassword(userPasswordSecure, userPasswordSecureDb);
                if (isValid)
                {
                    claimModel.UserId = user.Id;
                    claimModel.UserName = user.firstName + " " + user.lastName;
                    claimModel.RoleId = user.roleId;
                    claimModel.RoleName = _userRepository.getRoleName(user.roleId);
                    claimModel.EmailAddress = user.email;
                    return claimModel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), claimModel.ToString());
            }
            return claimModel = null;
        }

        public async Task<RequestConfirmPwdDTO> GetUserDetail(string hashKey)
        {
            RequestConfirmPwdDTO ConfirmPwdRequest = new RequestConfirmPwdDTO();
            try
            {
                var user = _userRepository.GetUserDetail(hashKey);
                if (user != null)
                {
                    ConfirmPwdRequest.Email = user.email;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return ConfirmPwdRequest;
        }

        public async Task<bool> ConfirmPwdRequest(RequestConfirmPwdDTO ConfirmPwdRequest)
        {
            bool updatesStatus = false;
            try
            {
                User user = new User();
                user.email = ConfirmPwdRequest.Email;
                user.password = ConfirmPwdRequest.Password;
                updatesStatus = _userRepository.ConfirmPwdRequest(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return updatesStatus;
        }

        public async Task<string> ForgotPassword(string Email)
        {
            string messages = "";
            try
            {
                User user = new User();
                user = _userRepository.UserLogin(Email);
                if (user == null)
                {
                    messages = "Email address in not valid. Please ask your administrator";
                }
                else
                {
                    SendEmail sendEmail = new SendEmail(_configuration, this._emailSetting);
                    bool isEmailSent = sendEmail.ForgotPassword(user);
                    if (isEmailSent)
                        messages = "Email send sucessfully.";
                    else
                        messages = "Email server is down due to some maintanance.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                messages = ex.Message;
            }
            return messages;
        }
    }
}
