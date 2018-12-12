using System;
using System.Collections.Generic;
using System.Text;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PetTreatment> PetTreatments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                StreetAddress = "123 Infinity Way",
                IsVet = true,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            ApplicationUser user2 = new ApplicationUser
            {
                FirstName = "aaron",
                LastName = "aaron",
                StreetAddress = "123 Infinity Way",
                IsVet = null,
                UserName = "aaron@aaron.com",
                NormalizedUserName = "AARON@AARON.COM",
                Email = "aaron@aaron.com",
                NormalizedEmail = "AARON@AARON.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash2 = new PasswordHasher<ApplicationUser>();
            user2.PasswordHash = passwordHash2.HashPassword(user2, "Aaron8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user2);

            modelBuilder.Entity<Message>().HasData(
                new Message()
                {
                    MessageId = 1,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Messages = "Hi"
                },
                new Message()
                {
                    MessageId = 2,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Messages = "Hello"
                },
                new Message()
                {
                    MessageId = 3,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Messages = "How are you"
                },
                new Message()
                {
                    MessageId = 4,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Messages = "I'm good"
                }
            );

            modelBuilder.Entity<Note>().HasData(
                new Note()
                {
                    NoteId = 1,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Message = "Get shots"
                },
                new Note()
                {
                    NoteId = 2,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Message = "Happy pupper"
                }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet()
                {
                    PetId = 1,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Name = "Gunner",
                    Age = 1,
                    Status = "Healthy"
                },
                new Pet()
                {
                    PetId = 2,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Name = "Marley",
                    Age = 1,
                    Status = "Sick"
                },
                new Pet()
                {
                    PetId = 3,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Name = "Whitley",
                    Age = 1,
                    Status = "Beat up"
                },
                new Pet()
                {
                    PetId = 4,
                    UserId = user2.Id,
                    VetId = user.Id,
                    Name = "Rocky",
                    Age = 1,
                    Status = "Healthy"
                }
            );

            modelBuilder.Entity<Treatment>().HasData(
                new Treatment()
                {
                    TreatmentId = 1,
                    Name = "Parvovirus Vaccine",
                    Description = "Attacks the lining of the intestinal tract and damages the heart of very young puppies; disease can be fatal",
                    Price = 20
                }
            );

            modelBuilder.Entity<Treatment>().HasData(
                new Treatment()
                {
                    TreatmentId = 2,
                    Name = "Canine Distemper Virus Vaccine",
                    Description = "Attacks the lungs and affects the function of the brain and spinal cord; disease can be fatal",
                    Price = 20
                }
            );

            modelBuilder.Entity<Treatment>().HasData(
                new Treatment()
                {
                    TreatmentId = 3,
                    Name = "Canine adenovirus",
                    Description = "Affects the liver and can cause loss of vision",
                    Price = 15
                }
            );

            modelBuilder.Entity<Treatment>().HasData(
                new Treatment()
                {
                    TreatmentId = 4,
                    Name = "Rabies",
                    Description = "This is a fatal viral disease that can infect all warm-blooded animals, including dogs and humans",
                    Price = 20
                }
            );

            modelBuilder.Entity<PetTreatment>().HasData(
                new PetTreatment()
                {
                    PetTreatmentId = 1,
                    PetId = 1,
                    TreatmentId = 1
                },
                new PetTreatment()
                {
                    PetTreatmentId = 2,
                    PetId = 2,
                    TreatmentId = 2
                }
            );

        }
    }
}
