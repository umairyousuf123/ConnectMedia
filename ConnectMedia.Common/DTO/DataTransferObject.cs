namespace ConnectMedia.Common.DTO
{
    public class DataTransferObject<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public string[] Error { get; set; }


        public void SetError(string message)
        {
            IsSuccess = false;
            Message = message;
            Error = new string[] { message };
        }
    }
}
