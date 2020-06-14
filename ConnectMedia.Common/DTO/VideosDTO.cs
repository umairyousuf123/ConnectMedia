using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{

    public class VideosDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public IFormFile file { get; set; }

        public DateTime Date { get; set; }
    }
}
