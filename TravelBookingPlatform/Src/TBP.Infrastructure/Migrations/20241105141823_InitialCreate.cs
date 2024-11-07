using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TBP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdultCapacity = table.Column<int>(type: "int", nullable: false),
                    ChildCapacity = table.Column<int>(type: "int", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeaturedDeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedDeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedDeals_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "CreatedAt", "Name", "PostOffice", "ThumbnailImageUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Country 1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "City 1", "PostOffice 1", "/images/city1_thumbnail.jpg", null },
                    { 2, "Country 2", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "City 2", "PostOffice 2", "/images/city2_thumbnail.jpg", null },
                    { 3, "Country 3", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "City 3", "PostOffice 3", "/images/city3_thumbnail.jpg", null },
                    { 4, "Country 4", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "City 4", "PostOffice 4", "/images/city4_thumbnail.jpg", null },
                    { 5, "Country 5", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "City 5", "PostOffice 5", "/images/city5_thumbnail.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "CreatedAt", "Location", "Name", "Owner", "StarRating", "ThumbnailImageUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Location 1", "Hotel 1", "Owner 1", 3, "/images/hotel1_thumbnail.jpg", null },
                    { 2, 2, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Location 2", "Hotel 2", "Owner 2", 4, "/images/hotel2_thumbnail.jpg", null },
                    { 3, 3, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Location 3", "Hotel 3", "Owner 3", 5, "/images/hotel3_thumbnail.jpg", null },
                    { 4, 4, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Location 4", "Hotel 4", "Owner 4", 3, "/images/hotel4_thumbnail.jpg", null },
                    { 5, 5, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Location 5", "Hotel 5", "Owner 5", 4, "/images/hotel5_thumbnail.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "user1@example.com", "hashedPassword1", 1, "user1" },
                    { 2, "user2@example.com", "hashedPassword2", 1, "user2" },
                    { 3, "user3@example.com", "hashedPassword3", 1, "user3" },
                    { 4, "user4@example.com", "hashedPassword4", 1, "user4" },
                    { 5, "user5@example.com", "hashedPassword5", 1, "user5" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultCapacity", "Availability", "ChildCapacity", "CreatedAt", "HotelId", "Number", "Price", "ThumbnailImageUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, true, 1, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 101, 200m, "/images/room1_thumbnail.jpg", null },
                    { 2, 3, true, 2, new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 102, 150m, "/images/room2_thumbnail.jpg", null },
                    { 3, 2, true, 1, new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 201, 180m, "/images/room3_thumbnail.jpg", null },
                    { 4, 4, true, 2, new DateTime(2023, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 202, 150m, "/images/room4_thumbnail.jpg", null },
                    { 5, 2, true, 1, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 301, 300m, "/images/room5_thumbnail.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "FeaturedDeals",
                columns: new[] { "Id", "CreatedAt", "DiscountedPrice", "OriginalPrice", "RoomId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, 200m, 1, null },
                    { 2, new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m, 250m, 2, null },
                    { 3, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, 180m, 3, null },
                    { 4, new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 150m, 4, null },
                    { 5, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m, 300m, 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedDeals_RoomId",
                table: "FeaturedDeals",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedDeals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
