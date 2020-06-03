using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConnectMedia.Services
{
    public class ClassifiedService : IClassifiedService
    {

        IClassifiedRepository _classifiedRepository { get; set; }
        ISendSMS _SendSMS { get; set; }
        IConfiguration _configuration;
        readonly ILogger<ClassifiedService> _logger;

        public ClassifiedService(ILogger<ClassifiedService> logger, IClassifiedRepository classifiedRepository, IConfiguration configuration, ISendSMS SendSMS)
        {
            _logger = logger;
            this._classifiedRepository = classifiedRepository;
            this._configuration = configuration;
            _SendSMS = SendSMS;
        }
        public List<ClassifiedGridView> getClassifiedList(int id)
        {
            List<ClassifiedGridView> classifiedList = _classifiedRepository.getClassifiedList(id);

            return classifiedList;
        }
        public ClassifiedDTO getClassifiedDetail(int id)
        {
            ClassifiedDTO classifiedDTO = _classifiedRepository.getClassifiedDetail(id);
            return classifiedDTO;
        }
        public bool AddEditClassified(ClassifiedDTO classifiedDTO)
        {
            bool isSaved = false;
            isSaved = _classifiedRepository.AddEditClassified(classifiedDTO);
            if (isSaved)
            {
                string Message = SMSBody.ClassifiedSMSBody(classifiedDTO);
                _SendSMS.NexmoSendSMS(new SMSDTO(classifiedDTO.ContactNumber.ToString(), Message));
            }

            return isSaved;
        }
        public bool Delete(int id, int CurrentUserId)
        {
            bool isDeleted = false;
            isDeleted = _classifiedRepository.Delete(id, CurrentUserId);
            return isDeleted;
        }
        public bool Active(int id, int CurrentUserId)
        {
            bool isActive = false;
            isActive = _classifiedRepository.Active(id, CurrentUserId);
            return isActive;
        }
        public List<Playlist> PlaylistDropdown(int UserId)
        {
            List<Playlist> playlist = _classifiedRepository.PlaylistDropdown(UserId);
            return playlist;
        }
    }
}
