using ConnectMedia.Common.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nexmo.Api;
using System;
using System.Collections.Generic;

namespace ConnectMedia.Common.Helper
{
    public class SendSMS : ISendSMS
    {
        IConfiguration _configuration;
        readonly ILogger<SendSMS> _logger;
        readonly NexmoDTO _nexmoDTO;
        public SendSMS(ILogger<SendSMS> logger, IConfiguration configuration, IOptionsSnapshot<NexmoDTO> nexmoDTO)
        {
            _logger = logger;
            this._configuration = configuration;
            this._nexmoDTO = nexmoDTO.Value;
        }
        public SMSResponseDetail NexmoSendSMS(SMSDTO SMSBody)
        {
            SMSResponseDetail SMSResponseDetail = new SMSResponseDetail();
            try
            {
                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = _nexmoDTO.ApiKey,
                    ApiSecret = _nexmoDTO.ApiSecret
                });
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = _nexmoDTO.FromNumber,
                    to = SMSBody.To,
                    text = SMSBody.Message
                });
                if (Convert.ToInt32(results.message_count) > 0)
                {
                    var response = results.messages[0];
                    DataCopier.Copy(response, SMSResponseDetail);
                }
               
                return SMSResponseDetail;
            }
            catch (Exception ex)
            {
                return SMSResponseDetail;
            }
        }
    }
}
