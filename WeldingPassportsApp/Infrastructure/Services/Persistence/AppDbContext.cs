using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Process = Domain.Models.Process;

namespace Infrastructure.Services.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IAppDbContext
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

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

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config, IWebHostEnvironment env) : base(options)
        {
            _config=config ?? throw new ArgumentNullException(nameof(config));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("WeldingPassportsDBConnection"));

            if (_env.IsDevelopment())
                optionsBuilder
                    .LogTo(message => Debug.WriteLine(message))
                    .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        //https://learn.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
        public static string Format(int number, string format)
        {
            throw new NotImplementedException("This method is for us with Entity Framework Core only and has no in-memory implementation.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDbFunction(typeof(AppDbContext).GetMethod(nameof(AppDbContext.Format)))
                .HasTranslation(args =>
                    new SqlFunctionExpression(
                        functionName: "FORMAT",
                        arguments: args,
                        nullable: true,
                        argumentsPropagateNullability: new[] {true, true},
                        type: typeof(string),
                        typeMapping: null));

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

            base.OnModelCreating(modelBuilder);
        }

        override async public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
