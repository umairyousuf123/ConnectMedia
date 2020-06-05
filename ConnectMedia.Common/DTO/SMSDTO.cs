namespace ConnectMedia.Common.DTO
{
    public class SMSDTO
    {
        public SMSDTO(string to, string message)
        {
            To = to;
            Message = message;
        }
        public string To { get; }
        public string Message { get; }
    }
    public class NexmoDTO
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string FromNumber { get; set; }
    }
}
