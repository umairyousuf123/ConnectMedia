using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface IUserServices
    {
        Task<ClaimModel> UserLogin(string email, string password);
        Task<RequestConfirmPwdDTO> GetUserDetail(string hashKey);
        Task<bool> ConfirmPwdRequest(RequestConfirmPwdDTO requestConfirmPwd);
        Task<string> ForgotPassword(string Email);
    }
}
