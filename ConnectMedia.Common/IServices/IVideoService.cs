using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.IServices
{
    public interface IVideosService
    {
        bool AddVideo(VideosDTO model);
        List<Videos> GetAllVideos();
        Videos GetVideos(int Id);
        bool DeletVideo(int Id);
    }
}
