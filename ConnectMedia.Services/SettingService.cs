using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Enum;
using ConnectMedia.Common.Helper;
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
    public class SettingService : ISettingService
    {
        ISettingRepository _settingRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<SettingService> _logger;
        private readonly DocumentPath _documentPath;

        public SettingService(ILogger<SettingService> logger, ISettingRepository settingRepository, IConfiguration configuration, IOptionsSnapshot<DocumentPath> documentPath)
        {
            _logger = logger;
            this._settingRepository = settingRepository;
            this._configuration = configuration;
            this._documentPath = documentPath.Value;

        }

        #region Admin template creation, Update and Delete
        public List<Template> Templates(int Id)
        {
            List<Template> newsList = _settingRepository.Templates();

            return newsList;
        }
        public Template GetTemplateDetail(int Id)
        {
            Template template = _settingRepository.GetTemplateDetail(Id);
            return template;
        }
        public bool AddEdit(Template template)
        {
            bool IstemplateCreateOrUpdate = _settingRepository.AddEdit(template);
            return IstemplateCreateOrUpdate;
        }
        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = _settingRepository.Delete(Id, CurrentUserId);
            return isDeleted;
        }
        #endregion

        public string RetriveLogo()
        {
            string Imagepath = _settingRepository.RetriveLogo();
            return Imagepath;
        }

        #region Upload All type of Files
        public async Task<string> SaveLogo(ImageDTO imageDTO)
        {
            string ErrorMessage = string.Empty;
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _documentPath.Image, _documentPath.LogoName);
                bool isSaved = await SaveFile(filePath, imageDTO.imgFile);
                if (isSaved)
                {
                    UploadFile _uploadFile = new UploadFile();
                    _uploadFile.Id = 1;
                    _uploadFile.UploadType = UploadDocumentType.Image.ToString();
                    _uploadFile.Title = imageDTO.Title;
                    _uploadFile.Desc = imageDTO.Desc;
                    _uploadFile.FileName = _documentPath.LogoName;
                    _uploadFile.FilePath = filePath;
                    _uploadFile.UpdatedBy = imageDTO.enteryBy;
                    _uploadFile.UpdatedOn = DateTime.Now;
                    ErrorMessage = _settingRepository.UploadFileDetail(_uploadFile);

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;
        }
        public async Task<string> SaveCSV(CSVDTO csv)
        {
            string ErrorMessage = string.Empty;
            try
            {

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _documentPath.CSV, csv.csvFile.FileName);
                bool isSaved = await SaveFile(filePath, csv.csvFile);
                List<ResgisterUser> resgisterUsers = CSVtoModel.CSVFileToModel(filePath, csv.TeamName, csv.enteryBy);
                ErrorMessage = _settingRepository.csvList(resgisterUsers);
                if (ErrorMessage == "Inserted")
                {
                    UploadFile _uploadFile = new UploadFile();
                    _uploadFile.Title = csv.TeamName;
                    _uploadFile.Desc = csv.Desc;
                    _uploadFile.UploadType = UploadDocumentType.CSV.ToString();
                    _uploadFile.FileName = csv.csvFile.FileName;
                    _uploadFile.FilePath = filePath;
                    _uploadFile.CreatedBy = csv.enteryBy;
                    _uploadFile.CreatedOn = DateTime.Now;
                    ErrorMessage = _settingRepository.UploadFileDetail(_uploadFile);
                }
                return ErrorMessage;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;

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
        #endregion

        #region CSV User Add, Update and Delete

        public async Task<List<ResgisterUser>> CSVList(int UserId, string TeamName)
        {
            List<ResgisterUser> resgisters = new List<ResgisterUser>();
            resgisters = _settingRepository.CSVList(UserId, TeamName);
            return resgisters;
        }
        public async Task<ResgisterUser> getEmailUserDetail(int Id)
        {
            ResgisterUser resgister = new ResgisterUser();
            resgister = _settingRepository.getEmailUserDetail(Id);
            return resgister;
        }
        public async Task<bool> createUpdateEmailUser(ResgisterUser resgisterUser)
        {
            bool IsNewsCreateOrUpdate = _settingRepository.createUpdateEmailUser(resgisterUser);
            return IsNewsCreateOrUpdate;
        }
        public async Task<bool> DeleteCSVUser(int Id, int CurrentUserId)
        {
            bool isDeleted = _settingRepository.DeleteCSVUser(Id, CurrentUserId);
            return isDeleted;
        }
        public List<string> TeamList(int UserId)
        {
            List<string> TeamList = _settingRepository.TeamList(UserId);
            return TeamList;
        }
        #endregion
    }
}
