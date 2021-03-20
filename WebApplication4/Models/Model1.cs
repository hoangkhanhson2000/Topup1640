using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication4.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

       
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Magazine> Magazines { get; set; }

        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Magazines)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID);
        }
    }
}
