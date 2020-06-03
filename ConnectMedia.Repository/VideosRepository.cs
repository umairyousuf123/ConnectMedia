using ConnectMedia.Common.Database;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectMedia.Repository
{
    public class VideosRepository: IVideosRepository
    {
        readonly IConfiguration _configuration;
        private ConnectMediaDB _db;

        public VideosRepository(IConfiguration configuration, ConnectMediaDB db)
        {
            this._configuration = configuration;
            this._db = db;

        }
        public bool AddVideo(Videos model)
        {
            try
            {
                _db.Videos.Add(model);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;

        
            }
        }
        public List<Videos> GetAllVideos()
        {
            try
            {
               return _db.Videos.ToList();
               
            }
            catch (Exception)
            {

                return null;

            }
        }
        public Videos GetVideobyId(int Id)
        {
            try
            {
                return _db.Videos.Where(x=>x.Id==Id)?.FirstOrDefault();

            }
            catch (Exception)
            {

                return null;

            }
        }
        public bool DeleteVideo(int Id)
        {
            try
            {
                var model=_db.Videos.Where(x => x.Id == Id)?.FirstOrDefault();
               
                _db.Videos.Remove(model);
                _db.SaveChanges();

                 return true;
            }
            catch (Exception)
            {
                return false;


            }
        }
    }
}
