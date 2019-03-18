// Install-Package Microsoft.EntityFrameworkCore.SqlServer
// Install-Package Microsoft.Extensions.Logging.Console
// Install-Package Microsoft.EntityFrameworkCore.Tools

using Microsoft.EntityFrameworkCore;
using QueryingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryingData
{
    class Program
    {
        static void Main(string[] args)
        {
            EagerLoading_PostsBlogs();
            //EagerLoading_BlogsOwner_BlogsPosts();
            //EagerLoading_BlogsPostsAuthor();
            //EagerLoading_BlogsPostsAuthorPhoto();
            //EagerLoding_BlogsPostsAuthorPhoto_BlogsOwnerPhoto();
            //EagerLoding_BlogsPostsAuthor_BlogsPostsTags();

            //ExplicitLoading_BlogPostOwner();
        }

        private static void ExplicitLoading_BlogPostOwner()
        {
            using (var context = new BloggingContext())
            {
                var blog = context.Blogs
                    .Single(b => b.BlogId == 1);

                context.Entry(blog)
                    .Collection(b => b.Posts)
                    .Load();

                context.Entry(blog)
                    .Reference(b => b.Owner)
                    .Load();

                Console.WriteLine($"BlogId: {blog.BlogId} - Url: {blog.Url} - Owner: {blog.Owner.Name}");
                foreach (var post in blog.Posts)
                {
                    Console.WriteLine($"\tTitle: {post.Title} - Content: {post.Content}");
                }

            }
        }

        static void EagerLoading_PostsBlogs()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        static void EagerLoading_BlogsOwner_BlogsPosts()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                    .Include(blog => blog.Owner)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        private static void EagerLoading_BlogsPostsAuthor()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                     .ThenInclude(post => post.Author)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        private static void EagerLoading_BlogsPostsAuthorPhoto()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                        .ThenInclude(post => post.Author)
                            .ThenInclude(author => author.Photo)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        private static void EagerLoding_BlogsPostsAuthorPhoto_BlogsOwnerPhoto()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                        .ThenInclude(post => post.Author)
                            .ThenInclude(author => author.Photo)
                    .Include(blog => blog.Owner)
                        .ThenInclude(owner => owner.Photo)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        private static void EagerLoding_BlogsPostsAuthor_BlogsPostsTags()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .Include(blog => blog.Posts)
                        .ThenInclude(post => post.Author)
                    .Include(blog => blog.Posts)
                        .ThenInclude(post => post.Tags)
                    .ToList();
                DisplayGraph(blogs);
            }
        }

        static void DisplayGraph(IEnumerable<Blog> blogs)
        {
            foreach (var blog in blogs)
            {
                Console.WriteLine($"BlogId: {blog.BlogId} - Url: {blog.Url} - Owner: {blog?.Owner?.Name} - PhotoCaption: {blog.Owner?.Photo?.Caption}  ");
                foreach (var post in blog.Posts)
                {
                    Console.WriteLine($"\tTitle: {post.Title} - Content: {post.Content} - PersonName: {post.Author?.Name} - PhotoCaption: {post.Author?.Photo?.Caption} ");
                    if (post.Tags != null)
                    {
                        foreach (var tag in post?.Tags)
                        {
                            Console.WriteLine($"\t\tTag: {tag?.TagId}");
                        }
                    }
                }
            }
        }
    }
}