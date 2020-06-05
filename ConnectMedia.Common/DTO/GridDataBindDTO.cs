using System.Collections.Generic;

namespace ConnectMedia.Common.DTO
{
    public class GridDataBindDTO

    {

    }
    public class UserGridView
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string BuildingName { get; set; }
        public string RoleName { get; set; }
        public List<string> emailList { get; set; }
    }
    public class ClassifiedGridView
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContactNumber { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Status { get; set; }
        public string Playlist { get; set; }
        public string PostedBy { get; set; }

    }
    public class NoticeGridView
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Duration { get; set; }
        public string Playlist { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
    }
    public class PlaylistGridView
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public string BuildingName { get; set; }
    }
    public class NoticeSendEmail
    {
        public int NoticeId { get; set; }
        public List<string> EmailTeam { get; set; }
    }
}
