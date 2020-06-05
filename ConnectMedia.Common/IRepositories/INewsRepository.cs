using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.IRepositories
{
    public interface INewsRepository
    {
        List<News> getListOfNews(int Id);
        News getUserDetail(int Id);
        bool deleteNews(int Id, int CurrentUserId);
        bool createUpdate(News news);
    }
}
