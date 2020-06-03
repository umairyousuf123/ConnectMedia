using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.IRepositories
{
   public interface IVideosRepository
    {
        bool AddVideo(Videos model);
        List<Videos> GetAllVideos();
        bool DeleteVideo(int Id);
        Videos GetVideobyId(int Id);
    }
}
