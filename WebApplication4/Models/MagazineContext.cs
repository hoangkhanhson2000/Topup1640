using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MagazineContext : DbContext
    {
        public MagazineContext()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<ArticlesComment> ArticlesComments { get; set; }


    }
}