using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace ConnectMedia.Common.Database
{
    public class ConnectMediaDB : DbContext
    {
        public ConnectMediaDB()
        {
        }
        public ConnectMediaDB(DbContextOptions<ConnectMediaDB> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //.HasOne<UserRole>(s => s.UserRole)
            //.WithOne(ad => ad.Users)
            //.HasForeignKey<UserRole>(ad => ad.UserId);
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Classified> Classified { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<UploadFile> UploadFile { get; set; }
        public DbSet<ResgisterUser> ResgisterUser { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<PlaylistBuilding> PlaylistBuilding { get; set; }
        public DbSet<NoticePlaylist> NoticePlaylist { get; set; }

        public DbSet<PlayListRunningSlots> playlistrunningslots { get; set; }

        public DbSet<RunningNoticeClassified> RunningNoticeClassified { get; set; }
        public DbSet<Videos> Videos { get; set; }
    }
}
