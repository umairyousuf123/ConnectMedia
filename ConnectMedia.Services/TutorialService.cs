using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Enum;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConnectMedia.Services
{
    public class TutorialService : ITutorialService
    {
        ITutorialRepository _tutorialRepository;
        IConfiguration _configuration;
        readonly ILogger<TutorialService> _logger;
        readonly DocumentPath _documentPath;


        public TutorialService(ILogger<TutorialService> logger, ITutorialRepository tutorialRepository, IConfiguration configuration, IOptionsSnapshot<DocumentPath> documentPath)
        {
            _logger = logger;
            this._tutorialRepository = tutorialRepository;
            this._configuration = configuration;
            this._documentPath = documentPath.Value;

        }
        public List<UploadFile> GetAllTutorial(int UserId)
        {
            List<UploadFile> Tutorials = _tutorialRepository.GetAllTutorial(UserId);
            return Tutorials;
        }
        public UploadFile GetTutorialById(int Id)
        {
            UploadFile uploadFile = _tutorialRepository.GetTutorialById(Id);
            return uploadFile;
        }
        public async Task<string> AddEditTutorial(VideoDTO videoDTO)
        {
            string ErrorMessage = string.Empty;
            string fileName = string.Empty;
            try
            {
                if (videoDTO.Id > 0)
                {
                    fileName = _tutorialRepository.GetGuidUploadFile(videoDTO.Id);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = GenerateGUID(videoDTO.videoFile.FileName);
                    }
                }
                else
                {
                    fileName = GenerateGUID(videoDTO.videoFile.FileName);
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _documentPath.Video, fileName);
                bool isSaved = await SaveFile(filePath, videoDTO.videoFile);
                if (isSaved)
                {

                    UploadFile _uploadFile = new UploadFile();
                    if (videoDTO.Id > 0)
                    {
                        _uploadFile.UpdatedOn = DateTime.Now;
                        _uploadFile.UpdatedBy = videoDTO.enteryBy;
                    }
                    else
                    {
                        _uploadFile.CreatedOn = DateTime.Now;
                        _uploadFile.CreatedBy = videoDTO.enteryBy;
                    }
                    _uploadFile.Id = videoDTO.Id;
                    _uploadFile.Title = videoDTO.Title;
                    _uploadFile.Desc = videoDTO.Desc;
                    _uploadFile.Portion = videoDTO.Portion;
                    _uploadFile.Sequence = videoDTO.Sequence;
                    _uploadFile.UploadType = UploadDocumentType.Video.ToString();
                    _uploadFile.FileName = fileName;
                    _uploadFile.FilePath = filePath;

                    ErrorMessage = _tutorialRepository.UploadFileDetail(_uploadFile);
                }
                else
                {
                    ErrorMessage = "Video Not Uploaded Scucessfuly";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;


        }
        public bool Delete(int id, int CurrentUserId)
        {
            bool isDeleted = _tutorialRepository.Delete(id, CurrentUserId);
            return isDeleted;
        }
        public bool Active(int id, int CurrentUserId)
        {
            bool isActive = _tutorialRepository.Active(id, CurrentUserId);
            return isActive;
        }
        private string GenerateGUID(string filename)
        {
            Guid obj = Guid.NewGuid();
            string extension = Path.GetExtension(filename);
            return obj.ToString() + extension;
        }
        private async Task<bool> SaveFile(string filepath, IFormFile formFile)
        {
            try
            {
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}