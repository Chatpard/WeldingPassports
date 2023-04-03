using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IAppDbContext
    {
        private readonly IWebHostEnvironment _env;       
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRole { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ExamCenter> ExamCenters { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ListExamCenter> ListExamCenter { get; set; }
        public DbSet<ListTrainingCenter> ListTrainingCenter { get; set; }
        public DbSet<PEPassport> PEPassports { get; set; }
        public DbSet<PEWelder> PEWelders { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<RegistrationType> RegistrationTypes { get; set; }
        public DbSet<UIColor> UIColors { get; set; }
        public DbSet<Revoke> Revokes { get; set; }
        public DbSet<TrainingCenter> TrainingCenters { get; set; }       
        public DbSet<UserToApprove> UsersToApprove { get; set; }
        public DbSet<AllowedRegistrationType> AllowedRegistrationTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IWebHostEnvironment env) : base(options)
        {
            _env = env;
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.AddRowNumberSupport();

            modelBuilder.Entity<AppUser>(b => 
            {
                b.HasMany(appUser => appUser.AppUserRoles)
                .WithOne(appUserRole => appUserRole.AppUser)
                .HasForeignKey(appUserRole => appUserRole.UserId);
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.HasMany(appRole => appRole.AppUserRoles)
                .WithOne(appUserRole => appUserRole.AppRole)
                .HasForeignKey(appUserRole => appUserRole.RoleId);
            });

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<PEPassport>().HasIndex(pePassport => new { pePassport.TrainingCenterID, pePassport.AVNumber }).IsUnique();
            modelBuilder.Entity<PEWelder>().HasIndex(peWelder => peWelder.NationalNumber).IsUnique();
            modelBuilder.Entity<PEWelder>().HasIndex(peWelder => peWelder.IdNumber).IsUnique();
            modelBuilder.Entity<TrainingCenter>().HasIndex(trainingCenter => trainingCenter.Letter).IsUnique();

            modelBuilder.Seed(_env);
        }

        override async public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
