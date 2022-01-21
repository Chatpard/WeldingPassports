using Domain.Models;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder, IWebHostEnvironment env)
        {
            modelBuilder.Entity<AppSettings>().HasData(SeedDataStore.GetSeedData<AppSettings>());

            modelBuilder.Entity<Process>().HasData(SeedDataStore.GetSeedData<Process>());

            modelBuilder.Entity<RegistrationType>().HasData(SeedDataStore.GetSeedData<RegistrationType>());

            modelBuilder.Entity<AllowedRegistrationType>().HasData(SeedDataStore.GetSeedData<AllowedRegistrationType>());

            modelBuilder.Entity<UIColor>().HasData(SeedDataStore.GetSeedData<UIColor>());

            if (env.IsDevelopment() || env.IsStaging())
            {
                modelBuilder.Entity<UserToApprove>().HasData(SeedDataStore.GetSeedData<UserToApprove>());

                modelBuilder.Entity<PEWelder>().HasData(SeedDataStore.GetSeedData<PEWelder>());

                modelBuilder.Entity<Contact>().HasData(SeedDataStore.GetSeedData<Contact>());

                modelBuilder.Entity<Address>().HasData(SeedDataStore.GetSeedData<Address>());

                modelBuilder.Entity<Company>().HasData(SeedDataStore.GetSeedData<Company>());

                modelBuilder.Entity<TrainingCenter>().HasData(SeedDataStore.GetSeedData<TrainingCenter>());

                modelBuilder.Entity<PEPassport>().HasData(SeedDataStore.GetSeedData<PEPassport>());

                modelBuilder.Entity<CompanyContact>().HasData(SeedDataStore.GetSeedData<CompanyContact>());

                modelBuilder.Entity<ExamCenter>().HasData(SeedDataStore.GetSeedData<ExamCenter>());

                modelBuilder.Entity<Examination>().HasData(SeedDataStore.GetSeedData<Examination>());

                modelBuilder.Entity<Registration>().HasData(SeedDataStore.GetSeedData<Registration>());

                modelBuilder.Entity<Revoke>().HasData(SeedDataStore.GetSeedData<Revoke>());

                modelBuilder.Entity<ListTrainingCenter>().HasData(SeedDataStore.GetSeedData<ListTrainingCenter>());

                modelBuilder.Entity<ListExamCenter>().HasData(SeedDataStore.GetSeedData<ListExamCenter>());
            }
        }
    }
}