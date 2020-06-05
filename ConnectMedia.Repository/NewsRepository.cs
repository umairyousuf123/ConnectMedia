using ConnectMedia.Common.Database;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Repository
{

    public class NewsRepository : INewsRepository
    {
        readonly IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<NewsRepository> _logger;
        public NewsRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<NewsRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }
        public List<News> getListOfNews(int Id)
        {
            int roleid = _db.Users.Where(x => x.Id == Id).FirstOrDefault().roleId;
            List<News> newsList;
            if (roleid == 1 || roleid == 2)
                newsList = _db.News.Where(b => b.IsDel == false).OrderBy(x=>x.Id).ToList();
            else
                newsList = _db.News.Where(b => b.IsDel == false && b.CreatedBy == Id && b.IssueDate < DateTime.Now).OrderByDescending(x => x.Id).ToList();
            return newsList;
        }
        public News getUserDetail(int Id)
        {
            News news = new News();
            try
            {
                news = _db.News.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return news;
        }
        public bool createUpdate(News news)
        {
            try
            {
                if (news.Id > 0)
                {
                    News Oldnews = _db.News.SingleOrDefault(b => b.Id == news.Id && b.IsDel == false);
                    Oldnews.Type = news.Type;
                    Oldnews.Heading = news.Heading;
                    Oldnews.Description = news.Description;
                    Oldnews.UpdatedOn = DateTime.UtcNow;
                    Oldnews.UpdatedBy = news.UpdatedBy;
                    _db.News.Attach(Oldnews);
                    _db.Entry(Oldnews).State = EntityState.Modified;
                }
                else
                {
                    news.CreatedOn = DateTime.UtcNow;
                    news.CreatedBy = news.CreatedBy;
                    _db.News.Add(news);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }
        public bool deleteNews(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            News news = new News();
            try
            {
                news = _db.News.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                news.IsDel = true;
                news.UpdatedOn = DateTime.UtcNow;
                news.UpdatedBy = CurrentUserId;
                _db.News.Attach(news);
                _db.Entry(news).State = EntityState.Modified;
                _db.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                isDeleted = false;

            }
            return isDeleted;
        }
    }
}
