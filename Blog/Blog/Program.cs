using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog
{
    public class Program
    {
        private static string CONNECTION_STRING_ID = Environment.GetEnvironmentVariable("CONNECTION_STRING_SQL_SERVER_ID", EnvironmentVariableTarget.Machine);
        private static string CONNECTION_STRING_PASSWORD = Environment.GetEnvironmentVariable("CONNECTION_STRING_SQL_SERVER_PASSWORD", EnvironmentVariableTarget.Machine);
        private static string connectionString = @$"Server=localhost, 1433;Database=Blog;User ID={CONNECTION_STRING_ID};Password={CONNECTION_STRING_PASSWORD};TrustServerCertificate=True";

        private static BlogDataContext _context;

        static void Main(string[] args)
        {
            _context = new BlogDataContext(connectionString);

            ReadUsers();
            Console.ReadKey();
        }

        static void CreateTag()
        {
            var tag = new Tag { Name = "ASP.NET", Slug = "aspnet" };

            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        static void UpdateTag(int id, Tag alterTag)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            tag.Name = alterTag.Name;
            tag.Slug = alterTag.Slug;
            _context.Tags.Update(tag);
            _context.SaveChanges();
        }

        static void ReadTags()
        {
            var tags = _context.Tags.AsNoTracking().ToList();

            foreach (var item in tags)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Slug}");
            }
        }

        static void DeleteTag(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            _context.Remove(tag);
            _context.SaveChanges();
        }

        static void ReadUsers()
        {
            var users = _context.Users
                .AsNoTracking()
                .ToList();

            foreach (var item in users)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Slug}");
            }
        }

        static void ReadPosts()
        {
            var posts = _context.Posts
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderBy(x => x.LastUpdateDate)
                .ToList();

            foreach (var item in posts)
            {
                Console.WriteLine($"{item.Id} - {item.Title} - {item.Author?.Name} - {item.Category?.Name}");
            }
        }
    }
}
