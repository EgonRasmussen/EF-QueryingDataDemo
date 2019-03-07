using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QueryingData.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PhotoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_Person_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoto",
                columns: table => new
                {
                    PersonPhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    PersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoto", x => x.PersonPhotoId);
                    table.ForeignKey(
                        name: "FK_PersonPhoto_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    BlogId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Person_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostTagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostId = table.Column<int>(nullable: false),
                    TagId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => x.PostTagId);
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "OwnerId", "Rating", "Url" },
                values: new object[] { 4, null, null, "http://blog5.com" });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonId", "Name", "PhotoId" },
                values: new object[,]
                {
                    { 1, "Person 1", null },
                    { 2, "Person 2", null },
                    { 3, "Person 3", null },
                    { 4, "Person 4", null },
                    { 5, "Person 5", null },
                    { 6, "Person 6", null }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                column: "TagId",
                values: new object[]
                {
                    "Photo",
                    "Sport",
                    "News",
                    "Money",
                    "Living",
                    "Children"
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "OwnerId", "Rating", "Url" },
                values: new object[,]
                {
                    { 1, 1, null, "http://blog1.com" },
                    { 2, 2, null, "http://blog2.com" },
                    { 3, 3, null, "http://blog3.com" }
                });

            migrationBuilder.InsertData(
                table: "PersonPhoto",
                columns: new[] { "PersonPhotoId", "Caption", "PersonId", "Photo" },
                values: new object[,]
                {
                    { 1, "PersonPhoto 1", 1, new byte[] { 65, 66, 67 } },
                    { 2, "PersonPhoto 2", 2, new byte[] { 68, 69, 70 } },
                    { 3, "PersonPhoto 3", 3, new byte[] { 71, 72, 73 } },
                    { 4, "PersonPhoto 4", 4, new byte[] { 74, 75, 76 } },
                    { 5, "PersonPhoto 5", 5, new byte[] { 77, 78, 79 } }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "AuthorId", "BlogId", "Content", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "Dette er Post 1 i Blog 1", 2, "Post 1" },
                    { 2, 4, 1, "Dette er Post 2 i Blog 1", 3, "Post 2" },
                    { 3, 4, 1, "Dette er Post 3 i Blog 1", 4, "Post 3" },
                    { 4, 5, 2, "Dette er post 1 i Blog 2", 0, "Post 1" },
                    { 5, 6, 2, "Dette er post 2 i Blog 2", 0, "Post 2" },
                    { 6, null, 3, "Dette er post 1 i Blog 3", 0, "Post 1" }
                });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostTagId", "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, "Sport" },
                    { 2, 2, "Sport" },
                    { 3, 2, "News" },
                    { 4, 3, "News" },
                    { 5, 4, "Living" },
                    { 6, 5, "Photo" },
                    { 7, 6, "News" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoto_PersonId",
                table: "PersonPhoto",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPhoto");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
