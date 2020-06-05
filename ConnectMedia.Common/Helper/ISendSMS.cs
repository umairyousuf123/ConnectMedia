using ConnectMedia.Common.DTO;
using System.Collections.Generic;

namespace ConnectMedia.Common.Helper
{
    public interface ISendSMS
    {
        SMSResponseDetail NexmoSendSMS(SMSDTO SMSBody);
    }
}
