using ConnectMedia.Common;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Services
{
    public class VideosService: IVideosService
    {
        private IVideosRepository _videoRepository;
        public VideosService(IVideosRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public bool AddVideo(VideosDTO model)
        {
            Videos obj = new Videos();
            DataCopier.Copy(model, obj);
            return _videoRepository.AddVideo(obj);

        }

        public List<Videos> GetAllVideos()
        {
           
            return _videoRepository.GetAllVideos();

        }

        public Videos GetVideos(int Id)
        {

            return _videoRepository.GetVideobyId(Id);

        }

        public bool DeletVideo(int Id)
        {

            return _videoRepository.DeleteVideo(Id);

        }
    }

}
