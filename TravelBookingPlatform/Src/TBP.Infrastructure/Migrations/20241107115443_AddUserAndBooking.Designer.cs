﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TBP.Infrastructure.Database;

#nullable disable

namespace TBP.Infrastructure.Migrations
{
    [DbContext(typeof(TBPDbContext))]
    [Migration("20241107115443_AddUserAndBooking")]
    partial class AddUserAndBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TBP.Domain.Entites.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TBP.Domain.Entites.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Country 1",
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "City 1",
                            PostOffice = "PostOffice 1",
                            ThumbnailImageUrl = "/images/city1_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Country 2",
                            CreatedAt = new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "City 2",
                            PostOffice = "PostOffice 2",
                            ThumbnailImageUrl = "/images/city2_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Country = "Country 3",
                            CreatedAt = new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "City 3",
                            PostOffice = "PostOffice 3",
                            ThumbnailImageUrl = "/images/city3_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Country = "Country 4",
                            CreatedAt = new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "City 4",
                            PostOffice = "PostOffice 4",
                            ThumbnailImageUrl = "/images/city4_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 5,
                            Country = "Country 5",
                            CreatedAt = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "City 5",
                            PostOffice = "PostOffice 5",
                            ThumbnailImageUrl = "/images/city5_thumbnail.jpg"
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.FeaturedDeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscountedPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("FeaturedDeals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountedPrice = 150m,
                            OriginalPrice = 200m,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountedPrice = 200m,
                            OriginalPrice = 250m,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountedPrice = 150m,
                            OriginalPrice = 180m,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountedPrice = 120m,
                            OriginalPrice = 150m,
                            RoomId = 4
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountedPrice = 250m,
                            OriginalPrice = 300m,
                            RoomId = 5
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StarRating")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location 1",
                            Name = "Hotel 1",
                            Owner = "Owner 1",
                            StarRating = 3,
                            ThumbnailImageUrl = "/images/hotel1_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            CreatedAt = new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location 2",
                            Name = "Hotel 2",
                            Owner = "Owner 2",
                            StarRating = 4,
                            ThumbnailImageUrl = "/images/hotel2_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            CreatedAt = new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location 3",
                            Name = "Hotel 3",
                            Owner = "Owner 3",
                            StarRating = 5,
                            ThumbnailImageUrl = "/images/hotel3_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 4,
                            CreatedAt = new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location 4",
                            Name = "Hotel 4",
                            Owner = "Owner 4",
                            StarRating = 3,
                            ThumbnailImageUrl = "/images/hotel4_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 5,
                            CreatedAt = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location 5",
                            Name = "Hotel 5",
                            Owner = "Owner 5",
                            StarRating = 4,
                            ThumbnailImageUrl = "/images/hotel5_thumbnail.jpg"
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdultCapacity")
                        .HasColumnType("int");

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("ChildCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ThumbnailImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdultCapacity = 2,
                            Availability = true,
                            ChildCapacity = 1,
                            CreatedAt = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HotelId = 1,
                            Number = 101,
                            Price = 200m,
                            ThumbnailImageUrl = "/images/room1_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 2,
                            AdultCapacity = 3,
                            Availability = true,
                            ChildCapacity = 2,
                            CreatedAt = new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HotelId = 1,
                            Number = 102,
                            Price = 150m,
                            ThumbnailImageUrl = "/images/room2_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 3,
                            AdultCapacity = 2,
                            Availability = true,
                            ChildCapacity = 1,
                            CreatedAt = new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HotelId = 2,
                            Number = 201,
                            Price = 180m,
                            ThumbnailImageUrl = "/images/room3_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 4,
                            AdultCapacity = 4,
                            Availability = true,
                            ChildCapacity = 2,
                            CreatedAt = new DateTime(2023, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HotelId = 2,
                            Number = 202,
                            Price = 150m,
                            ThumbnailImageUrl = "/images/room4_thumbnail.jpg"
                        },
                        new
                        {
                            Id = 5,
                            AdultCapacity = 2,
                            Availability = true,
                            ChildCapacity = 1,
                            CreatedAt = new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HotelId = 3,
                            Number = 301,
                            Price = 300m,
                            ThumbnailImageUrl = "/images/room5_thumbnail.jpg"
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user1@example.com",
                            PasswordHash = "hashedPassword1",
                            RoleId = 1,
                            Username = "user1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user2@example.com",
                            PasswordHash = "hashedPassword2",
                            RoleId = 1,
                            Username = "user2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "user3@example.com",
                            PasswordHash = "hashedPassword3",
                            RoleId = 1,
                            Username = "user3"
                        },
                        new
                        {
                            Id = 4,
                            Email = "user4@example.com",
                            PasswordHash = "hashedPassword4",
                            RoleId = 1,
                            Username = "user4"
                        },
                        new
                        {
                            Id = 5,
                            Email = "user5@example.com",
                            PasswordHash = "hashedPassword5",
                            RoleId = 1,
                            Username = "user5"
                        });
                });

            modelBuilder.Entity("TBP.Domain.Entites.Booking", b =>
                {
                    b.HasOne("TBP.Domain.Entites.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TBP.Domain.Entites.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TBP.Domain.Entites.FeaturedDeal", b =>
                {
                    b.HasOne("TBP.Domain.Entites.Room", "Room")
                        .WithOne("FeaturedDeal")
                        .HasForeignKey("TBP.Domain.Entites.FeaturedDeal", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("TBP.Domain.Entites.Hotel", b =>
                {
                    b.HasOne("TBP.Domain.Entites.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("TBP.Domain.Entites.Room", b =>
                {
                    b.HasOne("TBP.Domain.Entites.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TBP.Domain.Entites.User", b =>
                {
                    b.HasOne("TBP.Domain.Entites.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TBP.Domain.Entites.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("TBP.Domain.Entites.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TBP.Domain.Entites.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TBP.Domain.Entites.Room", b =>
                {
                    b.Navigation("FeaturedDeal")
                        .IsRequired();
                });

            modelBuilder.Entity("TBP.Domain.Entites.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}