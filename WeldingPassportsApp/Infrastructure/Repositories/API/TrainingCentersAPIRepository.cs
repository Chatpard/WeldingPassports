using Application.Interfaces.Repositories.API;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.API
{
    class TrainingCentersAPIRepository : ITrainingCentersAPIRepository
    {
        private readonly AppDbContext _context;

        public TrainingCentersAPIRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<char?> GetLetterByCompanyID(int id)
        {
            TrainingCenter trainingCenter = await _context.TrainingCenters
                .Where(trainingCenter => trainingCenter.CompanyID == id).SingleOrDefaultAsync();

            if (trainingCenter == null)
            {
                return null;
            }

            return trainingCenter.Letter;
        }

        public bool IsLetterInUse(char letter)
        {
            TrainingCenter trainingCenter = _context.TrainingCenters.FirstOrDefault (trainingCenter => trainingCenter.Letter == letter);
            if (trainingCenter == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
