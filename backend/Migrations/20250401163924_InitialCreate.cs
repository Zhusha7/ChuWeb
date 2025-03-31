using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChuWeb.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => new { x.CategoriesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PostCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategories_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "ProfilePictureUrl", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, "Experienced tech writer with a passion for web development.", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(4713), "john.doe@example.com", "John", false, "Doe", null, null, "johndoe" },
                    { 2, "Web developer and tech enthusiast.", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(4716), "jane.smith@example.com", "Jane", false, "Smith", null, null, "janesmith" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5058), "Articles related to web development technologies and practices.", false, "Web Development", "web-development", null },
                    { 2, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5060), "Tutorials and articles about ASP.NET Core development.", false, "ASP.NET Core", "aspnet-core", null },
                    { 3, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5062), "Everything about React framework and best practices.", false, "React", "react", null }
                });

            migrationBuilder.InsertData(
                table: "ContactMessages",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "IsRead", "Message", "Name", "PhoneNumber", "ReadAt", "Subject", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5271), "alice@example.com", false, false, "I really enjoyed your blog posts about ASP.NET Core. Looking forward to more content!", "Alice Johnson", "555-123-4567", null, "Website Feedback", null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5109), false, "C#", "csharp", null },
                    { 2, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5111), false, "JavaScript", "javascript", null },
                    { 3, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5113), false, "Entity Framework", "entity-framework", null },
                    { 4, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5114), false, "React", "react", null }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "FeaturedImageUrl", "IsDeleted", "IsPublished", "PublishedAt", "Slug", "Summary", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "This is a comprehensive guide to getting started with ASP.NET Core MVC. ASP.NET Core is a cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected applications.", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5175), null, false, true, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5169), "getting-started-with-aspnet-core-mvc", "Learn the basics of ASP.NET Core MVC in this introductory article.", "Getting Started with ASP.NET Core MVC", null },
                    { 2, 2, "Vite is a modern build tool that significantly improves the frontend development experience. In this post, we'll explore how to build React applications using Vite and why it's a great alternative to Create React App.", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5178), null, false, true, new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5177), "building-react-applications-with-vite", "Learn how to use Vite for faster React development.", "Building React Applications with Vite", null }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "Email", "IsApproved", "IsDeleted", "Name", "ParentCommentId", "PostId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Great article! This helped me understand ASP.NET Core better.", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5222), "mike@example.com", true, false, "Mike Johnson", null, 1, null },
                    { 2, "I've been using Vite for a while and it's fantastic!", new DateTime(2025, 4, 1, 16, 39, 24, 553, DateTimeKind.Utc).AddTicks(5224), "sarah@example.com", true, false, "Sarah Williams", null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "CategoriesId", "PostsId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "PostsId", "TagsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_PostsId",
                table: "PostCategories",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagsId",
                table: "PostTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
