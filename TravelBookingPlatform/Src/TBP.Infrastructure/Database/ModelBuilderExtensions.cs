using Microsoft.EntityFrameworkCore;
using TBP.Domain.Entites;

namespace TBP.Infrastructure.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "User" },
                new Role { Id = 2, RoleName = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", PasswordHash = "hashedPassword1", RoleId = 1, Email = "user1@example.com" },
                new User { Id = 2, Username = "user2", PasswordHash = "hashedPassword2", RoleId = 1, Email = "user2@example.com" },
                new User { Id = 3, Username = "user3", PasswordHash = "hashedPassword3", RoleId = 1, Email = "user3@example.com" },
                new User { Id = 4, Username = "user4", PasswordHash = "hashedPassword4", RoleId = 1, Email = "user4@example.com" },
                new User { Id = 5, Username = "user5", PasswordHash = "hashedPassword5", RoleId = 1, Email = "user5@example.com" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "City 1", Country = "Country 1", PostOffice = "PostOffice 1", ThumbnailImageUrl = "/images/city1_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 1) },
                new City { Id = 2, Name = "City 2", Country = "Country 2", PostOffice = "PostOffice 2", ThumbnailImageUrl = "/images/city2_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 2) },
                new City { Id = 3, Name = "City 3", Country = "Country 3", PostOffice = "PostOffice 3", ThumbnailImageUrl = "/images/city3_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 3) },
                new City { Id = 4, Name = "City 4", Country = "Country 4", PostOffice = "PostOffice 4", ThumbnailImageUrl = "/images/city4_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 4) },
                new City { Id = 5, Name = "City 5", Country = "Country 5", PostOffice = "PostOffice 5", ThumbnailImageUrl = "/images/city5_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 5) }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hotel 1", StarRating = 3, Location = "Location 1", CityId = 1, Owner = "Owner 1", ThumbnailImageUrl = "/images/hotel1_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 1) },
                new Hotel { Id = 2, Name = "Hotel 2", StarRating = 4, Location = "Location 2", CityId = 2, Owner = "Owner 2", ThumbnailImageUrl = "/images/hotel2_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 2) },
                new Hotel { Id = 3, Name = "Hotel 3", StarRating = 5, Location = "Location 3", CityId = 3, Owner = "Owner 3", ThumbnailImageUrl = "/images/hotel3_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 3) },
                new Hotel { Id = 4, Name = "Hotel 4", StarRating = 3, Location = "Location 4", CityId = 4, Owner = "Owner 4", ThumbnailImageUrl = "/images/hotel4_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 4) },
                new Hotel { Id = 5, Name = "Hotel 5", StarRating = 4, Location = "Location 5", CityId = 5, Owner = "Owner 5", ThumbnailImageUrl = "/images/hotel5_thumbnail.jpg", CreatedAt = new DateTime(2023, 1, 5) }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Number = 101, Price = 200, AdultCapacity = 2, ChildCapacity = 1, HotelId = 1, Availability = true, ThumbnailImageUrl = "/images/room1_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 1) },
                new Room { Id = 2, Number = 102, Price = 150, AdultCapacity = 3, ChildCapacity = 2, HotelId = 1, Availability = true, ThumbnailImageUrl = "/images/room2_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 2) },
                new Room { Id = 3, Number = 201, Price = 180, AdultCapacity = 2, ChildCapacity = 1, HotelId = 2, Availability = true, ThumbnailImageUrl = "/images/room3_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 3) },
                new Room { Id = 4, Number = 202, Price = 150, AdultCapacity = 4, ChildCapacity = 2, HotelId = 2, Availability = true, ThumbnailImageUrl = "/images/room4_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 4) },
                new Room { Id = 5, Number = 301, Price = 300, AdultCapacity = 2, ChildCapacity = 1, HotelId = 3, Availability = true, ThumbnailImageUrl = "/images/room5_thumbnail.jpg", CreatedAt = new DateTime(2023, 2, 5) }
            );

            modelBuilder.Entity<FeaturedDeal>().HasData(
                new FeaturedDeal { Id = 1, RoomId = 1, OriginalPrice = 200, DiscountedPrice = 150, CreatedAt = new DateTime(2023, 8, 1) },
                new FeaturedDeal { Id = 2, RoomId = 2, OriginalPrice = 250, DiscountedPrice = 200, CreatedAt = new DateTime(2023, 8, 2) },
                new FeaturedDeal { Id = 3, RoomId = 3, OriginalPrice = 180, DiscountedPrice = 150, CreatedAt = new DateTime(2023, 8, 3) },
                new FeaturedDeal { Id = 4, RoomId = 4, OriginalPrice = 150, DiscountedPrice = 120, CreatedAt = new DateTime(2023, 8, 4) },
                new FeaturedDeal { Id = 5, RoomId = 5, OriginalPrice = 300, DiscountedPrice = 250, CreatedAt = new DateTime(2023, 8, 5) }
            );


        }
    }
}
