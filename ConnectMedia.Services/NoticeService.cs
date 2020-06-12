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
    public class NoticeService : INoticeService
    {
        INoticeRepository _noticeRepository { get; set; }
        readonly IConfiguration _configuration;
        readonly ILogger<NoticeService> _logger;
        private readonly DocumentPath _documentPath;
        readonly EmailSetting _emailSetting;
        ISendSMS _SendSMS { get; set; }


        public NoticeService(ILogger<NoticeService> logger, INoticeRepository noticeRepository, IConfiguration configuration, IOptionsSnapshot<DocumentPath> documentPath, IOptionsSnapshot<EmailSetting> emailSetting, ISendSMS SendSMS)
        {
            _logger = logger;
            this._noticeRepository = noticeRepository;
            this._configuration = configuration;
            this._documentPath = documentPath.Value;
            this._emailSetting = emailSetting.Value;
            _SendSMS = SendSMS;
        }
        public List<NoticeGridView> getNotices(int Userid)
        {
            List<NoticeGridView> notice = _noticeRepository.getNotices(Userid);
            return notice;
        }
        public NoticeDTO getNoticeDetail(int id)
        {
            NoticeDTO noticeDTO = _noticeRepository.getNoticeDetail(id);
            return noticeDTO;
        }

        public NoticeDTO TemplateDetail(int TemplateId)
        {
            NoticeDTO noticeDTO = new NoticeDTO();
            Template template = _noticeRepository.GetTemplate(TemplateId);
            noticeDTO.Name = template.Title;
            noticeDTO.Content = template.Content;
            noticeDTO.CategoryId = template.CategoryId;
            return noticeDTO;
        }
        public bool AddEditNotice(NoticeDTO noticeDTO)
        {
            bool isSaved = _noticeRepository.AddEditNotice(noticeDTO);
            return isSaved;
        }
        public bool Delete(int id, int CurrentUserId)
        {
            bool isDeleted = _noticeRepository.Delete(id, CurrentUserId);
            return isDeleted;
        }
        public bool Active(int id, int CurrentUserId)
        {
            bool isActive = _noticeRepository.Active(id, CurrentUserId);
            return isActive;
        }
        public List<Category> Categories()
        {
            List<Category> noticeList = _noticeRepository.Categories();
            return noticeList;
        }
        public async Task<string> SavePDF(PDFDTO pdfDTO)
        {
            string ErrorMessage = string.Empty;
            string fileName = string.Empty;
            try
            {
                if (pdfDTO.Id > 0)
                {
                    fileName = _noticeRepository.GetGuidUploadFile(pdfDTO.Id);
                }
                else
                {
                    Guid obj = Guid.NewGuid();
                    string extension = Path.GetExtension(pdfDTO.pdfFile.FileName);
                    fileName = obj.ToString() + extension;
                }

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Documents",
                            fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await pdfDTO.pdfFile.CopyToAsync(stream);
                }
               
              //  var filePath = Path.Combine(Directory.GetCurrentDirectory(), _documentPath.Pdf, fileName);
                bool isSaved = await SaveFile(path, pdfDTO.pdfFile);
                if (isSaved)
                {

                    UploadFile _uploadFile = new UploadFile();
                    if (pdfDTO.Id > 0)
                    {
                        _uploadFile.UpdatedOn = DateTime.Now;
                        _uploadFile.UpdatedBy = pdfDTO.enteryBy;
                    }
                    else
                    {
                        _uploadFile.CreatedOn = DateTime.Now;
                        _uploadFile.CreatedBy = pdfDTO.enteryBy;
                    }
                    _uploadFile.Id = pdfDTO.Id;
                    _uploadFile.Title = pdfDTO.Title;
                    _uploadFile.Desc = pdfDTO.Desc;
                    _uploadFile.UploadType = UploadDocumentType.PDF.ToString();
                    _uploadFile.FileName = fileName;
                    _uploadFile.FilePath = path;

                    ErrorMessage = _noticeRepository.UploadFileDetail(_uploadFile);
                }
                else { ErrorMessage = "PDF Not Uploaded Scucessfuly"; }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;

        }
        public async Task<string> SaveDoc(DocDTO docDTO)
        {
            string ErrorMessage = string.Empty;
            string fileName = string.Empty;
            try
            {
                if (docDTO.Id > 0)
                {
                    fileName = _noticeRepository.GetGuidUploadFile(docDTO.Id);
                }
                else
                {
                    Guid obj = Guid.NewGuid();
                    string extension = Path.GetExtension(docDTO.docFile.FileName);
                    fileName = obj.ToString() + extension;
                }
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Documents",
                            fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await docDTO.docFile.CopyToAsync(stream);
                }
               // var filePath = Path.Combine(Directory.GetCurrentDirectory(), _documentPath.WordDocument, fileName);
                bool isSaved = await SaveFile(path, docDTO.docFile);
                if (isSaved)
                {

                    UploadFile _uploadFile = new UploadFile();
                    if (docDTO.Id > 0)
                    {
                        _uploadFile.UpdatedOn = DateTime.Now;
                        _uploadFile.UpdatedBy = docDTO.enteryBy;
                    }
                    else
                    {
                        _uploadFile.CreatedOn = DateTime.Now;
                        _uploadFile.CreatedBy = docDTO.enteryBy;
                    }
                    _uploadFile.Id = docDTO.Id;
                    _uploadFile.Title = docDTO.Title;
                    _uploadFile.Desc = docDTO.Desc;
                    _uploadFile.UploadType = UploadDocumentType.Doc.ToString();
                    _uploadFile.FileName = docDTO.docFile.FileName;
                    _uploadFile.FilePath = path;
                    ErrorMessage = _noticeRepository.UploadFileDetail(_uploadFile);
                }
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
        public List<Playlist> PlaylistDropdown(int UserId)
        {
            List<Playlist> playlist = _noticeRepository.PlaylistDropdown(UserId);
            return playlist;
        }
        public List<UploadFile> GetUserUploadFile(int UserId)
        {
            List<UploadFile> uploadFiles = _noticeRepository.GetUserUploadFile(UserId);
            return uploadFiles;
        }
        public UploadFile getDocDetail(int Id)
        {
            UploadFile uploadFile = _noticeRepository.getDocDetail(Id);
            return uploadFile;
        }
        public string SendTeamEmail(int id, string TeamName, int UserId)
        {

            string Message = string.Empty;
            List<SendEmailList> SenderList = _noticeRepository.SenderList(UserId, TeamName);
            NoticeDTO noticeDTO = _noticeRepository.getNoticeDetail(id);
            if (SenderList != null && noticeDTO != null)
            {
                SendEmail sendEmail = new SendEmail(_configuration, this._emailSetting);
                bool isEmailSent = sendEmail.SendNotice(SenderList, noticeDTO);
                if (isEmailSent)
                    Message = "Email send sucessfully.";
                else
                    Message = "Email server is down due to some maintanance.";
            }
            else
            {
                Message = "Can't find the Sender Email list";
            }
            return Message;
        }
        public string SendTeamSMS(int id, string TeamName, int UserId)
        {
            string Message = string.Empty;
            try
            {
                List<SendSMSList> SenderList = _noticeRepository.SenderSMSList(UserId, TeamName);
                NoticeDTO noticeDTO = _noticeRepository.getNoticeDetail(id);
                List<SMSResponseDetail> SMSResponseDetails = new List<SMSResponseDetail>();
                if (SenderList != null && noticeDTO != null)
                {
                    string SMSMessage = SMSBody.NoticeSMSBody(noticeDTO);
                    foreach (var senderNo in SenderList)
                    {
                        SMSResponseDetail smsDetail = _SendSMS.NexmoSendSMS(new SMSDTO(senderNo.PhoneNo.ToString(), SMSMessage));
                        if (smsDetail.status == "0")
                        {
                            Message += "The SMS send sucessfully to" + smsDetail.to + "/<br>";
                        }
                        else
                        {
                            Message += "The SMS was not send to " + smsDetail.to + " due to" + smsDetail.error_text + " .";
                        }
                        SMSResponseDetails.Add(smsDetail);
                    }
                }
                else
                {
                    Message = "Can't find the Sender SMS list";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Message;
        }
        public NoticeSendEmail TeamList(int Id)
        {
            NoticeSendEmail noticeSendEmail = _noticeRepository.TeamList(Id);
            return noticeSendEmail;
        }

        public PromotionDTO GetPromotionWithNotice(int UserId)
        {
            PromotionDTO promotions = new PromotionDTO();
            promotions.UserNotices = _noticeRepository.getNotices(UserId);
            promotions.AdminNotices = _noticeRepository.getAdminNotices();
            return promotions;

        }
    }
}
