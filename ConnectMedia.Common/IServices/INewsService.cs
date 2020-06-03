using ConnectMedia.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface INewsService
    {
        List<News> getListOfNews(int userId);
        Task<News> getUserDetail(int Id);
        Task<bool> createUpdate(News news);
        Task<bool> deleteNews(int Id,int CurrentUserId);

    }
}
