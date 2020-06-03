using ConnectMedia.Common.Database;
using ConnectMedia.Common.Enum;
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
    public class TutorialRepository : ITutorialRepository
    {
        readonly IConfiguration _configuration;
        ConnectMediaDB _db;
        readonly ILogger<TutorialRepository> _logger;
        public TutorialRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<TutorialRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }

        public List<UploadFile> GetAllTutorial(int UserId)
        {

            List<UploadFile> tutorials = new List<UploadFile>();
            tutorials = _db.UploadFile.Where(x => x.IsDel == false && x.UploadType == UploadDocumentType.Video.ToString()).OrderBy(x => x.Sequence).ToList();
            return tutorials;
        }
        public UploadFile GetTutorialById(int Id)
        {
            UploadFile uploadFile = new UploadFile();
            try
            {
                uploadFile = _db.UploadFile.Where(x => x.Id == Id && x.IsDel == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return uploadFile;
        }

        public string UploadFileDetail(UploadFile uploadFile)
        {
            string ErrorMessage = string.Empty;
            try
            {
                if (uploadFile.Id > 0)
                {

                    UploadFile uploadFileObj = _db.UploadFile.SingleOrDefault(x => x.Id == uploadFile.Id);
                    uploadFileObj.Title = uploadFile.Title;
                    uploadFileObj.Desc = uploadFile.Desc;
                    uploadFileObj.Portion = uploadFile.Portion;
                    uploadFileObj.Sequence = uploadFile.Sequence;
                    uploadFileObj.UploadType = uploadFile.UploadType;
                    uploadFileObj.FileName = uploadFile.FileName;
                    uploadFileObj.FilePath = uploadFile.FilePath;
                    _db.UploadFile.Attach(uploadFileObj);
                    _db.Entry(uploadFileObj).State = EntityState.Modified;
                }
                else
                {
                    _db.UploadFile.Add(uploadFile);
                }
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Video Upload sucessfully " : "Video Upload not ";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ex.ToString());
            }
            return ErrorMessage;
        }

        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            UploadFile upload = new UploadFile();
            try
            {
                upload = _db.UploadFile.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                upload.IsDel = true;
                upload.UpdatedOn = DateTime.UtcNow;
                upload.UpdatedBy = CurrentUserId;
                _db.UploadFile.Attach(upload);
                _db.Entry(upload).State = EntityState.Modified;
                _db.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                isDeleted = false;

            }
            return isDeleted;
        }

        public bool Active(int Id, int CurrentUserId)
        {
            bool isActive = false;
            UploadFile upload = new UploadFile();
            try
            {
                upload = _db.UploadFile.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                upload.IsActive = !upload.IsActive;
                upload.UpdatedOn = DateTime.UtcNow;
                upload.UpdatedBy = CurrentUserId;
                _db.UploadFile.Attach(upload);
                _db.Entry(upload).State = EntityState.Modified;
                _db.SaveChanges();
                isActive = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                isActive = false;
            }
            return isActive;
        }

        public string GetGuidUploadFile(int Id)
        {
            {
                string fileName = string.Empty;
                try
                {
                    fileName = _db.UploadFile.SingleOrDefault(x => x.Id == Id).FileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return fileName;
            }
        }
    }
}
