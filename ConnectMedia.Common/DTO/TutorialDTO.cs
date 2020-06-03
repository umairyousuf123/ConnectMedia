using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class TutorialDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FilePath { get; set; }
        public int Sequence { get; set; }
        public int Portion { get; set; }
    }
}
