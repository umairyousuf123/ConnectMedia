using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class SMSResponseDetail
    {
        public string status { get; set; }
        [JsonProperty("message-id")]
        public string message_id { get; set; }
        public string to { get; set; }
        [JsonProperty("client-ref")]
        public string client_ref { get; set; }
        [JsonProperty("remaining-balance")]
        public string remaining_balance { get; set; }
        [JsonProperty("message-price")]
        public string message_price { get; set; }
        public string network { get; set; }
        [JsonProperty("error-text")]
        public string error_text { get; set; }
    }
}
