using IS220M11.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Data
{
    public class FindFoundContext : DbContext
    {
        public FindFoundContext(DbContextOptions<FindFoundContext> options) : base(options)
        { }
        public DbSet<accountModel> accounts { get; set; }
        public DbSet<banModel> bans { get; set; }
        public DbSet<chatModel> chats { get; set; }
        public DbSet<commentModel> comments { get; set; }
        public DbSet<interestModel> interests { get; set; }
        public DbSet<notiModel> notis { get; set; }
        public DbSet<pictureModel> pictures { get; set; }
        public DbSet<postModel> posts { get; set; }
        public DbSet<reportModel> reports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accountModel>().ToTable("account");
            modelBuilder.Entity<banModel>().ToTable("ban");
            modelBuilder.Entity<chatModel>().ToTable("chat");
            modelBuilder.Entity<commentModel>().ToTable("comment");
            modelBuilder.Entity<interestModel>().ToTable("interest").
            HasKey(c => new { c.InPostID, c.InUserID });
            modelBuilder.Entity<notiModel>().ToTable("noti");
            modelBuilder.Entity<pictureModel>().ToTable("picture");
            modelBuilder.Entity<postModel>().ToTable("post");
            modelBuilder.Entity<reportModel>().ToTable("report");
        }

        private void HasKey(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
