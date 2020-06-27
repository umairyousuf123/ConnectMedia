using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Services
{
    public class AndroidService : IAndroidService
    {
        IAndroidRepository _androidRepository { get; set; }
        IConfiguration _configuration;
        AppSetting _appSetting;
        readonly ILogger<AndroidService> _logger;

        public AndroidService(ILogger<AndroidService> logger, IAndroidRepository androidRepository, IOptionsSnapshot<AppSetting> appSetting)
        {
            _logger = logger;
            this._androidRepository = androidRepository;
            this._appSetting = appSetting.Value;
        }
        public async Task<DataTransferObject<loginDetails>> UserLogin(LoginRequestDTO data)
        {
            DataTransferObject<loginDetails> Transfer = new DataTransferObject<loginDetails>();
            try
            {
                User user = _androidRepository.UserLogin(data.Email);
                SecureString userPasswordSecure = new NetworkCredential("", data.Password).SecurePassword;
                SecureString userPasswordSecureDb = new NetworkCredential("", user.password).SecurePassword;
                bool isValid = Rules.ComparePassword(userPasswordSecure, userPasswordSecureDb);
                if (isValid)
                {
                    var claim = new Claim[] {
                    new Claim (ClaimTypes.NameIdentifier ,user.Id.ToString()),
                    new Claim (ClaimTypes.Name ,user.firstName.ToString() + " " + user.lastName.ToString()),
                    new Claim ("RoleId" ,user.roleId.ToString()),
                    new Claim (ClaimTypes.Role ,_androidRepository.getRoleName(user.roleId)),
                    new Claim (ClaimTypes.Email ,user.email) };
                    Transfer.Data = new loginDetails
                    {
                        Token = await CreateToken(claim),
                        UserId = user.Id.ToString()
                    }; 
                    Transfer.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                Transfer.SetError(ex.Message);
            }
            return Transfer;
        }
        public List<NoticeDTO> GetPlatlistFromBuilding(string Key)
        {
            List<NoticeDTO> notices = new List<NoticeDTO>();
            try
            {
                notices = _androidRepository.GetPlaylistFromBuilding(Key);
            }
            catch (Exception ex)
            {
                throw;
            }
            return notices;

        }

        public async Task<string> CreateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWTSecurityKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                   issuer: _appSetting.Isuser,
                   audience: _appSetting.Audience,
                   expires: DateTime.Now.AddMinutes(30),
                   claims: claims,
                   signingCredentials: credential
                   );

            var accesstoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return await Task.FromResult(accesstoken);
        }

    }


   
}
