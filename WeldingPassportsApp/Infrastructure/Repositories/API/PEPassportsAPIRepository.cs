using Application.Interfaces.Repositories.API;
using Infrastructure.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories.API
{
    class PEPassportsAPIRepository : IPEPassportsAPIRepository
    {
        private readonly AppDbContext _context;

        public PEPassportsAPIRepository(AppDbContext context)
        {
            _context = context;
        }

        public int? GetMaxAVNumber(int trainingCenterID)
        {
            try
            {
                if(! _context.TrainingCenters.Where(trainingCenter => trainingCenter.ID == trainingCenterID).Any())
                {
                    return null;
                }

                if( _context.PEPassports.Where(pePassport => pePassport.TrainingCenterID == trainingCenterID).Any())
                {
                    return _context.PEPassports
                        .Where(pePassport => pePassport.TrainingCenterID == trainingCenterID)
                        .Max(pePassport => pePassport.AVNumber);
                }

                return 0;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
