using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppContext :DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public virtual DbSet<Blog> Blogs { get; set; }  

        public virtual DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
