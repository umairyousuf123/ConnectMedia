using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
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
    public class SettingRepository : ISettingRepository
    {
        readonly IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<SettingRepository> _logger;
        readonly int AllowRole = 2;
        public SettingRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<SettingRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }

        #region Admin template creation, Update and Delete
        public List<Template> Templates()
        {
            List<Template> templates = new List<Template>();
            templates = _db.Template.Where(x => x.IsDel == false).OrderByDescending(x => x.Id).ToList();

            //if (roleid == 1 || roleid == 2)
            //    newsList = _db.Template.Where(b => b.IsDel == false).ToList();
            //else
            //    newsList = _db.Template.Where(b => b.IsDel == false && b.CreatedBy == Id && b.IssueDate < DateTime.Now).ToList();
            return templates;
        }
        public Template GetTemplateDetail(int Id)
        {
            Template template = new Template();
            try
            {
                template = _db.Template.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return template;
        }
        public bool AddEdit(Template template)
        {
            try
            {
                if (template.Id > 0)
                {
                    Template Oldtemplate = _db.Template.SingleOrDefault(b => b.Id == template.Id && b.IsDel == false);
                    Oldtemplate.Title = template.Title;
                    Oldtemplate.Description = template.Description;
                    Oldtemplate.CategoryId = template.CategoryId;
                    Oldtemplate.Content = template.Content;
                    Oldtemplate.UpdatedOn = DateTime.UtcNow;
                    Oldtemplate.UpdatedBy = template.UpdatedBy;
                    _db.Template.Attach(Oldtemplate);
                    _db.Entry(Oldtemplate).State = EntityState.Modified;
                }
                else
                {
                    _db.Template.Add(template);
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
        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            try
            {
                Template template = _db.Template.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                template.IsDel = true;
                template.UpdatedOn = DateTime.UtcNow;
                template.UpdatedBy = CurrentUserId;
                _db.Template.Attach(template);
                _db.Entry(template).State = EntityState.Modified;
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
        #endregion

        #region Upload All type of Files
        public string RetriveLogo()

        {
            UploadFile upload = _db.UploadFile.SingleOrDefault(x => x.Id == 1 && x.IsDel == false);
            return upload.FilePath;
        }

        public string UploadFileDetail(UploadFile uploadFile)
        {
            string ErrorMessage = string.Empty;
            try
            {
                if (uploadFile.Id > 0)
                {
                    _db.UploadFile.Attach(uploadFile);
                    _db.Entry(uploadFile).State = EntityState.Modified;
                }
                else
                {
                    _db.UploadFile.Add(uploadFile);
                }
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Upload sucessfully " : "Upload not ";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ex.ToString());
            }
            return ErrorMessage;
        }
        public string csvList(List<ResgisterUser> resgisterUsers)
        {
            string ErrorMessage = string.Empty;
            try
            {
                _db.Database.EnsureCreated();
                _db.ResgisterUser.AddRange(resgisterUsers);
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "user added sucessfully " : "user not added";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ex.ToString());

            }
            return ErrorMessage;
        }
        #endregion

        #region CSV User Add, Update and Delete
        public List<ResgisterUser> CSVList(int Id, string TeamName)
        {
            int roleid = _db.Users.Where(x => x.Id == Id).FirstOrDefault().roleId;
            List<ResgisterUser> ResgisterUserList;
            if (AllowRole >= roleid)
                ResgisterUserList = _db.ResgisterUser.Where(b => b.IsDel == false && b.TeamName== TeamName).OrderByDescending(x => x.Id).ToList();
            else
                ResgisterUserList = _db.ResgisterUser.Where(b => b.IsDel == false && b.CreatedBy == Id && b.TeamName == TeamName).OrderByDescending(x => x.Id).ToList();
            return ResgisterUserList;
        }
        public ResgisterUser getEmailUserDetail(int Id)
        {
            ResgisterUser resgister = new ResgisterUser();
            try
            {
                resgister = _db.ResgisterUser.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return resgister;
        }
        public bool createUpdateEmailUser(ResgisterUser resgisterUser)
        {
            try
            {
                if (resgisterUser.Id > 0)
                {
                    _db.ResgisterUser.Attach(resgisterUser);
                    _db.Entry(resgisterUser).State = EntityState.Modified;
                }
                else
                {
                    _db.ResgisterUser.Add(resgisterUser);
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
        public bool DeleteCSVUser(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            ResgisterUser resgister = new ResgisterUser();
            try
            {
                resgister = _db.ResgisterUser.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                resgister.IsDel = true;
                resgister.UpdatedOn = DateTime.UtcNow;
                resgister.UpdatedBy = CurrentUserId;
                _db.ResgisterUser.Attach(resgister);
                _db.Entry(resgister).State = EntityState.Modified;
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
        public List<string> TeamList(int Id)
        {
            int roleid = _db.Users.Where(x => x.Id == Id).FirstOrDefault().roleId;
            List<string> teamLists;
            if (AllowRole >= roleid)
                teamLists = _db.ResgisterUser.Where(b => b.IsDel == false).Select(o => o.TeamName).Distinct().ToList();
            else
                teamLists = _db.ResgisterUser.Where(b => b.IsDel == false && b.CreatedBy == Id).Select(o => o.TeamName).Distinct().ToList();
            return teamLists;
        }
        #endregion

    }
}
