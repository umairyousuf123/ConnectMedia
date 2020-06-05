using ConnectMedia.Common.Model;

namespace ConnectMedia.Common.IRepositories
{
    public interface IUserRepository
    {
        User UserLogin(string email);
        User GetUserDetail(string hashKey);
        bool ConfirmPwdRequest(User user);
        string getRoleName(int RoleId);
    }
}
