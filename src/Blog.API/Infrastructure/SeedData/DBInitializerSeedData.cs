using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class DBInitializerSeedData
    {
        public static void InitializeDatabase(AppContext appContext)
        {
            if(!appContext.Blogs.Any())
            {
                var blogs = new Blog[]
                {
                    new Blog
                    {
                        Name = "Architecture",
                        Description = "the Microsoft Architecture"
                    },
                     new Blog
                    {
                        Name = "Blazor",
                        Description = "the Microsoft Blazor"
                    },
                    new Blog
                    {
                        Name = "net core",
                        Description = "the Microsoft net core"
                    },
                    new Blog
                    {
                        Name = "c sharp",
                        Description = "the Microsoft c sharp"
                    }

                };
                appContext.Blogs.AddRangeAsync(blogs);
                appContext.SaveChanges();
            }

            if(!appContext.Author.Any())
            {
                var author = new Author[]
                {
                    new Author
                    {
                        Name= "Sami",
                        Email = "sami@gmail.com"
                    },
                    new Author
                    {
                        Name= "shubham",
                        Email = "shubham@gmail.com"
                    },
                    new Author
                    {
                        Name= "jhon",
                        Email = "jhon@gmail.com"
                    },
                    new Author
                    {
                        Name= "jack",
                        Email = "jack@gmail.com"
                    }

                };
                appContext.Author.AddRangeAsync(author);
                appContext.SaveChanges();
            }
        }
    }
}
