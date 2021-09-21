using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QueryingData.Models;
using System;

namespace QueryingData
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Photo)
                .WithOne(p => p.Person)
                .HasForeignKey<PersonPhoto>(p => p.PersonId);

            // ************************ Data Seeding ***************************
            modelBuilder.Entity<PersonPhoto>().HasData(
                new PersonPhoto { PersonPhotoId = 1, Caption = "PersonPhoto 1", Photo = new byte[] { 65, 66, 67 }, PersonId = 1 },
                new PersonPhoto { PersonPhotoId = 2, Caption = "PersonPhoto 2", Photo = new byte[] { 68, 69, 70 }, PersonId = 2 },
                new PersonPhoto { PersonPhotoId = 3, Caption = "PersonPhoto 3", Photo = new byte[] { 71, 72, 73 }, PersonId = 3 },
                new PersonPhoto { PersonPhotoId = 4, Caption = "PersonPhoto 4", Photo = new byte[] { 74, 75, 76 }, PersonId = 4 },
                new PersonPhoto { PersonPhotoId = 5, Caption = "PersonPhoto 5", Photo = new byte[] { 77, 78, 79 }, PersonId = 5 }
                );

            modelBuilder.Entity<Person>().HasData(
               new Person { PersonId = 1, Name = "Person 1" },
               new Person { PersonId = 2, Name = "Person 2" },
               new Person { PersonId = 3, Name = "Person 3" },
               new Person { PersonId = 4, Name = "Person 4" },
               new Person { PersonId = 5, Name = "Person 5" },
               new Person { PersonId = 6, Name = "Person 6" }
               );

            modelBuilder.Entity<Blog>().HasData(
                new Blog { BlogId = 1, Url = "http://blog1.com", OwnerId = 1 },
                new Blog { BlogId = 2, Url = "http://blog2.com", OwnerId = 2 },
                new Blog { BlogId = 3, Url = "http://blog3.com", OwnerId = 3 },
                new Blog { BlogId = 4, Url = "http://blog5.com" }
                );

            modelBuilder.Entity<Post>().HasData(
                new Post() { PostId = 1, Title = "Post 1", Content = "Dette er Post 1 i Blog 1", Rating = 2, BlogId = 1, AuthorId = 1 },
                new Post() { PostId = 2, Title = "Post 2", Content = "Dette er Post 2 i Blog 1", Rating = 3, BlogId = 1, AuthorId = 4 },
                new Post() { PostId = 3, Title = "Post 3", Content = "Dette er Post 3 i Blog 1", Rating = 4, BlogId = 1, AuthorId = 4 },
                new Post() { PostId = 4, Title = "Post 4", Content = "Dette er post 4 i Blog 2", Rating = 3, BlogId = 2, AuthorId = 5 },
                new Post() { PostId = 5, Title = "Post 5", Content = "Dette er post 5 i Blog 2", Rating = 1, BlogId = 2, AuthorId = 6 },
                new Post() { PostId = 6, Title = "Post 6", Content = "Dette er post 6 i Blog 3", Rating = 2, BlogId = 3 }
                );

            modelBuilder.Entity<Tag>().HasData(
              new Tag { TagId = "Photo" },
              new Tag { TagId = "Sport" },
              new Tag { TagId = "News" },
              new Tag { TagId = "Money" },
              new Tag { TagId = "Living" },
              new Tag { TagId = "Children" }
              );

            modelBuilder.Entity<PostTag>().HasData(
              new PostTag { PostTagId = 1, PostId = 1, TagId = "Sport" },
              new PostTag { PostTagId = 2, PostId = 2, TagId = "Sport" },
              new PostTag { PostTagId = 3, PostId = 2, TagId = "News" },
              new PostTag { PostTagId = 4, PostId = 3, TagId = "News" },
              new PostTag { PostTagId = 5, PostId = 4, TagId = "Living" },
              new PostTag { PostTagId = 6, PostId = 5, TagId = "Photo" },
              new PostTag { PostTagId = 7, PostId = 6, TagId = "News" }
              );
        }

    }
}
