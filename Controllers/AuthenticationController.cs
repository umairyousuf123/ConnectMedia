using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConnectMedia.Controllers
{
    //[AllowAnonymous]
    //// (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
    public class AuthenticationController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserServices userServices)
        {
            _logger = logger;
            this._userServices = userServices;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "News");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO data)
        {
            if (ModelState.IsValid)
            {
                ClaimModel claimModel = new ClaimModel();
                string Email = !string.IsNullOrEmpty(data.Email) ? data.Email.Trim() : "";
                string Password = !string.IsNullOrEmpty(data.Password) ? data.Password : "";
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    claimModel = await _userServices.UserLogin(Email, Password);
                    if (claimModel != null)
                    {

                        var claim = new List<Claim> {
                    new Claim (ClaimTypes.NameIdentifier ,claimModel.UserId.ToString()),
                    new Claim (ClaimTypes.Name ,claimModel.UserName.ToString()),
                    new Claim ("RoleId" ,claimModel.RoleId.ToString()),
                    new Claim (ClaimTypes.Role ,claimModel.RoleName.ToString()),
                    new Claim (ClaimTypes.Email ,claimModel.EmailAddress) };

                        var grandmaIdentity = new ClaimsIdentity(claim, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        await HttpContext.SignInAsync(userPrincipal);

                        return RedirectToAction("Index", "News");

                       // return await SignInUser(claimModel);
                    }
                    ModelState.AddModelError("Password", "Email Or Password is Not valid or You account is not active.");
                }
            }
            return View(data);
        }

        private async Task<IActionResult> SignInUser(ClaimModel claimModel)
        {
            var Principal = Rules.GenerateCliamOfUser(claimModel);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal, new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                IsPersistent = true,
            });

            return RedirectToAction("Index", "News");

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string Id)
        {
            RequestConfirmPwdDTO requestConfirmPwd = new RequestConfirmPwdDTO();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    string decryptPwd = EncryptSecreteString.Decrypt(Id);
                    requestConfirmPwd = await _userServices.GetUserDetail(decryptPwd);
                }
            }
            catch (Exception ex)

            {
                ModelState.AddModelError("Email", ex.Message);
            }
            return View(requestConfirmPwd);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RequestConfirmPwdDTO requestConfirmPwd)
        {
            if (ModelState.IsValid)
            {
                await _userServices.ConfirmPwdRequest(requestConfirmPwd);
                return RedirectToAction("Index", "Authentication");
            }
            return View(requestConfirmPwd);

        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgetEmailDTO forgetEmailDTO)
        {
            string isEmailSentMessage = "";
            if (ModelState.IsValid)
            {
                string Email = !string.IsNullOrEmpty(forgetEmailDTO.Email) ? forgetEmailDTO.Email.Trim() : "";
                if (!string.IsNullOrEmpty(Email))
                {
                    isEmailSentMessage = await _userServices.ForgotPassword(Email);
                }
                ModelState.AddModelError("Email", isEmailSentMessage);
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return this.RedirectToAction("Login", "Authentication");
        }
    }
}