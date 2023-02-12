using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.Persistence
{
    public class AppDbContext : IdentityDbContext, IAppDbContext
    {
        private readonly IWebHostEnvironment _env;
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
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

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Todo onlyLeafRegistrationDelete
            // https://onthedrift.com/posts/efcore-triggered-part1/
            //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())){
            //if(foreignKey.PrincipalEntityType.Name != "Domain.Models."+nameof(Registration))
            //{
            //        foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            //}
            //else
            //{
            //        foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            //}

            modelBuilder.Entity<PEPassport>().HasIndex(pePassport => new { pePassport.TrainingCenterID, pePassport.AVNumber }).IsUnique();
            modelBuilder.Entity<PEWelder>().HasIndex(peWelder => peWelder.NationalNumber).IsUnique();
            modelBuilder.Entity<PEWelder>().HasIndex(peWelder => peWelder.IdNumber).IsUnique();
            modelBuilder.Entity<TrainingCenter>().HasIndex(trainingCenter => trainingCenter.Letter).IsUnique();

            modelBuilder.Seed(_env);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    using (var command = Database.GetDbConnection().CreateCommand())
        //    {
        //        command.CommandText = @"
        //                CREATE TRIGGER tr_OnlyLeafNodesCertificatesDelete
        //                   on dbo."+nameof(Registration)+@"
        //                   INSTEAD OF DELETE
        //                   AS
        //                   BEGIN
        //                        DECLARE @ID INT;
        //                        SELECT @ID = ID
        //                        FROM deleted;
        //                        IF EXIST (SELECT 1 FROM"+nameof(Registration)+@"WHERE PrevID = @ID)
        //                        BEGIN
        //                            ROLLBACK TRANSACTION;
        //                        END;
        //                   END;
        //            ";
        //        command.ExecuteNonQuery();
        //    }
        //}

        override async public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
