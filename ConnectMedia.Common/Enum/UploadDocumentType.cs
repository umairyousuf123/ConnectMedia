using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConnectMedia.Common.Enum
{
    public enum UploadDocumentType
    {
        Image,
        Video,
        CSV,
        PDF,
        Doc,
    }
    public enum TutorialType
    {
        [Description("Introduction")]
        Introduction,
        [Description("Notice Overview")]
        Notice,
        [Description("Classified Overview")]
        Classified
    }
}
