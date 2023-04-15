using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {
        private string _connectionString;
        public BlogDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        // public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        // public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
            // Mostra a query executada pelo EF
            //options.LogTo(Console.WriteLine);
        }
    }
}
