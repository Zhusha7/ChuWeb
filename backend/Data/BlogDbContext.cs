using ChuWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChuWeb.API.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Post and Category
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Posts)
                .UsingEntity(j => j.ToTable("PostCategories"));

            // Configure many-to-many relationship between Post and Tag
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity(j => j.ToTable("PostTags"));

            // Configure Comment self-referencing relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed some initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Bio = "Experienced tech writer with a passion for web development.",
                    Username = "johndoe",
                    CreatedAt = DateTime.UtcNow
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Bio = "Web developer and tech enthusiast.",
                    Username = "janesmith",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Web Development",
                    Slug = "web-development",
                    Description = "Articles related to web development technologies and practices.",
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = 2,
                    Name = "ASP.NET Core",
                    Slug = "aspnet-core",
                    Description = "Tutorials and articles about ASP.NET Core development.",
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = 3,
                    Name = "React",
                    Slug = "react",
                    Description = "Everything about React framework and best practices.",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 1,
                    Name = "C#",
                    Slug = "csharp",
                    CreatedAt = DateTime.UtcNow
                },
                new Tag
                {
                    Id = 2,
                    Name = "JavaScript",
                    Slug = "javascript",
                    CreatedAt = DateTime.UtcNow
                },
                new Tag
                {
                    Id = 3,
                    Name = "Entity Framework",
                    Slug = "entity-framework",
                    CreatedAt = DateTime.UtcNow
                },
                new Tag
                {
                    Id = 4,
                    Name = "React",
                    Slug = "react",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Getting Started with ASP.NET Core MVC",
                    Content = "This is a comprehensive guide to getting started with ASP.NET Core MVC. ASP.NET Core is a cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected applications.",
                    Summary = "Learn the basics of ASP.NET Core MVC in this introductory article.",
                    Slug = "getting-started-with-aspnet-core-mvc",
                    AuthorId = 1,
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                },
                new Post
                {
                    Id = 2,
                    Title = "Building React Applications with Vite",
                    Content = "Vite is a modern build tool that significantly improves the frontend development experience. In this post, we'll explore how to build React applications using Vite and why it's a great alternative to Create React App.",
                    Summary = "Learn how to use Vite for faster React development.",
                    Slug = "building-react-applications-with-vite",
                    AuthorId = 2,
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Content = "Great article! This helped me understand ASP.NET Core better.",
                    Name = "Mike Johnson",
                    Email = "mike@example.com",
                    PostId = 1,
                    IsApproved = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 2,
                    Content = "I've been using Vite for a while and it's fantastic!",
                    Name = "Sarah Williams",
                    Email = "sarah@example.com",
                    PostId = 2,
                    IsApproved = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed sample contact message
            modelBuilder.Entity<ContactMessage>().HasData(
                new ContactMessage
                {
                    Id = 1,
                    Name = "Alice Johnson",
                    Email = "alice@example.com",
                    PhoneNumber = "555-123-4567",
                    Subject = "Website Feedback",
                    Message = "I really enjoyed your blog posts about ASP.NET Core. Looking forward to more content!",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Set up post-category relationships
            modelBuilder.Entity("CategoryPost").HasData(
                new { PostsId = 1, CategoriesId = 2 },  // Post 1 is in ASP.NET Core category
                new { PostsId = 2, CategoriesId = 3 }   // Post 2 is in React category
            );

            // Set up post-tag relationships
            modelBuilder.Entity("PostTag").HasData(
                new { PostsId = 1, TagsId = 1 },  // Post 1 has C# tag
                new { PostsId = 1, TagsId = 3 },  // Post 1 has Entity Framework tag
                new { PostsId = 2, TagsId = 2 },  // Post 2 has JavaScript tag
                new { PostsId = 2, TagsId = 4 }   // Post 2 has React tag
            );
        }
    }
} 