using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.Model
{
    public class UploadFile : BaseModel
    {
        public int Id { get; set; }
        public string UploadType { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Portion { get; set; }
        public int Sequence { get; set; }

    }
}
