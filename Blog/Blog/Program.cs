using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog
{
    public class Program
    {
        private static BlogDataContext _context;

        static void Main(string[] args)
        {
            _context = new BlogDataContext();

            ReadTags();
            Console.ReadKey();
        }

        static void CreateTag()
        {
            var tag = new Tag { Name = "ASP.NET", Slug = "aspnet" };

            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        static void CreateCategory()
        {
            var category = new Category { Name = "NET", Slug = "net" };

            _context.Categories.Add(category);
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
                .Include(x => x.Roles)
                .ToList();

            foreach (var item in users)
            {
                Console.WriteLine($"-> {item.Id} - {item.Name} - {item.Slug}");

                foreach (var role in item?.Roles)
                {
                    Console.WriteLine($"--> {role.Id} - {role.Name} - {role.Slug}");
                }
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
