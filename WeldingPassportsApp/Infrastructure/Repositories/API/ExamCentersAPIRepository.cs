using Application.Interfaces.Repositories.API;
using Domain.Models;
using Infrastructure.Services.Persistence;
using System.Linq;

namespace Infrastructure.Repositories.API
{

    public class ExamCentersAPIRepository : IExamCentersAPIRepository
    {
        private readonly AppDbContext _context;

        public ExamCentersAPIRepository(AppDbContext context)
        {
            _context=context;
        }

        public bool IsCompanyIDInUse(int companyID)
        {
            ExamCenter examCenter = _context.ExamCenters.FirstOrDefault(examCenter => examCenter.CompanyID == companyID);
            if (examCenter == null)
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
