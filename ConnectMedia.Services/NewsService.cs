using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectMedia.Services
{
    public class NewsService : INewsService
    {
        INewsRepository _newsRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<NewsService> _logger;

        public NewsService(ILogger<NewsService> logger, INewsRepository newsRepository, IConfiguration configuration)
        {
            _logger = logger;
            this._newsRepository = newsRepository;
            this._configuration = configuration;
        }

        public List<News> getListOfNews(int Id)
        {
            List<News> newsList = new List<News>();
            newsList = _newsRepository.getListOfNews(Id);

            return newsList;
        }
        public async Task<News> getUserDetail(int Id)
        {
            News news = new News();
            news = _newsRepository.getUserDetail(Id);
            return news;
        }
        public async Task<bool> createUpdate(News news)
        {
            bool IsNewsCreateOrUpdate = false;
            IsNewsCreateOrUpdate = _newsRepository.createUpdate(news);
            return IsNewsCreateOrUpdate;
        }
        public async Task<bool> deleteNews(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            isDeleted = _newsRepository.deleteNews(Id, CurrentUserId);
            return isDeleted;
        }

    }
}
