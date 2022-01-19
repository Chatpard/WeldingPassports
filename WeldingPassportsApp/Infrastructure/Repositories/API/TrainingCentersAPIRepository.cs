using Application.Interfaces.Repositories.API;
using Domain.Models;
using Infrastructure.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<char?> GetLetterById(int id)
        {
            TrainingCenter trainingCenter = new TrainingCenter();
            trainingCenter = await _context.TrainingCenters.FindAsync(id);
            
            if (trainingCenter == null)
            {
                return null;
            }

            return trainingCenter.Letter;
        }
    }
}
