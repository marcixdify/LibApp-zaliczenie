using System;
using System.Linq;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);
                else
                    Console.WriteLine("Database already seeded with Customers.");

                if (!context.Genre.Any())
                    SeedGenres(context);
                else
                    Console.WriteLine("Database already seeded with Genres.");

                if (!context.Books.Any())
                    SeedBooks(context);
                else
                    Console.WriteLine("Database already seeded with Books.");

                if (!context.Roles.Any())
                    SeedRoles(context);
                else
                    Console.WriteLine("Database already seeded with roles.");

                if (!context.Customers.Any())
                    SeedCustomers(context, userManager);
                else
                    Console.WriteLine("Database already seeded with Books.");
            }
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
             new MembershipType
             {
                 Id = 1,
                 Name = "Pay as You Go",
                 SignUpFee = 0,
                 DurationInMonths = 0,
                 DiscountRate = 0
             },
             new MembershipType
             {
                 Id = 2,
                 Name = "Monthly",
                 SignUpFee = 30,
                 DurationInMonths = 1,
                 DiscountRate = 10
             },
             new MembershipType
             {
                 Id = 3,
                 Name = "Quaterly",
                 SignUpFee = 90,
                 DurationInMonths = 3,
                 DiscountRate = 15
             },
             new MembershipType
             {
                 Id = 4,
                 Name = "Yearly",
                 SignUpFee = 300,
                 DurationInMonths = 12,
                 DiscountRate = 20
             });

            context.SaveChanges();

        }
        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Name = "Harry Potter i Kamień Filozoficzny",
                    AuthorName = "J.K. Rowling",
                    ReleaseDate = DateTime.Parse("26/06/1997"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 15
                },
                new Book
                {
                    GenreId = 2,
                    Name = "Potop",
                    AuthorName = "Henryk Sienkiewicz",
                    ReleaseDate = DateTime.Parse("11/02/1886"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 50
                },
                new Book
                {
                    GenreId = 3,
                    Name = "Quo vadis",
                    AuthorName = "Henryk Sienkiewicz",
                    ReleaseDate = DateTime.Parse("02/11/1896"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 5
                });

            context.SaveChanges();

        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Sci-Fi"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Criminal"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Horror"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Biography"
                }
            );

            context.SaveChanges();

        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "user"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "StoreManager",
                        NormalizedName = "storemanager"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Owner",
                        NormalizedName = "owner"
                    }
                );

            context.SaveChanges();

        }

        private static void SeedCustomers(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();
            var c1 = new Customer
            {
                Name = "Marcin Lencewicz",
                Email = "marcin@wsei.edu.pl",
                NormalizedEmail = "marcin@wsei.edu.pl",
                UserName = "marcin@wsei.edu.pl",
                NormalizedUserName = "marcin@wsei.edu.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c1).Wait();
            userManager.AddToRoleAsync(c1, "owner").Wait();

            var c2 = new Customer
            {
                Name = "Jan Damian",
                Email = "jan@wp.pl",
                NormalizedEmail = "jan@wp.pl",
                UserName = "jan@wp.pl",
                NormalizedUserName = "jan@kowalski.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c2).Wait();
            userManager.AddToRoleAsync(c2, "user").Wait();

            var c3 = new Customer
            {
                Name = "Zbigniew Jan",
                Email = "zbigniew@wp.pl",
                NormalizedEmail = "zbigniew@wp.pl",
                UserName = "zbigniew@wp.pl",
                NormalizedUserName = "zbigniew@wp.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(c3).Wait();
            userManager.AddToRoleAsync(c3, "storemanager").Wait();


            context.SaveChanges();

        }
    }
}