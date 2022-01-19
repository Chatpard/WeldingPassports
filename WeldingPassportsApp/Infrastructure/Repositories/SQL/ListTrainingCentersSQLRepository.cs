using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    class ListTrainingCentersSQLRepository : SaveChangesSQL, IListTrainingCentersSQLRepository
    {
        private readonly AppDbContext _context;

        public ListTrainingCentersSQLRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EntityEntry<ListTrainingCenter>> Create(int trainingCenterID, int companyContactID)
        {
            return await Edit(trainingCenterID, companyContactID);
        }

        public async Task<EntityEntry<ListTrainingCenter>> Edit(int trainingCenterID, int companyContactID)
        {
            ListTrainingCenter listTrainingCenter = _context.ListTrainingCenter
                .FirstOrDefault(listTrainingCenter => 
                    listTrainingCenter.TrainingCenterID == trainingCenterID);

            if (listTrainingCenter == null && companyContactID == 0)
            {
                return null;
            }

            if (listTrainingCenter != null && companyContactID == 0)
            {
                return _context.ListTrainingCenter.Remove(listTrainingCenter);
            }

            if(listTrainingCenter == null)
                listTrainingCenter = new ListTrainingCenter();

            if (trainingCenterID != 0)
                listTrainingCenter.TrainingCenter = _context.TrainingCenters.Find(trainingCenterID);

            if (companyContactID != 0)
                listTrainingCenter.CompanyContact = _context.CompanyContacts.Find(companyContactID);

            return _context.ListTrainingCenter.Update(listTrainingCenter);
        }

            //if(companyContactID == 0)
            //{
            //    return await PostTrainingCenterCreate(listTrainingCenter.ID);
            //}

            //listTrainingCenter.CompanyContactID = companyContactID;
            //listTrainingCenter.CompanyContact = null;

            //return _context.ListTrainingCenter.Update(listTrainingCenter);
            ////EntityEntry<ListTrainingCenter> entityEntry = _context.ListTrainingCenter.Attach(listTrainingCenterUpdate);
            //EntityEntry<ListTrainingCenter> entityEntry = _context.Entry(listTrainingCenter);
            //entityEntry.State = EntityState.Modified;
            //entityEntry.Property(listTrainingCenter => listTrainingCenter.CompanyContactID).IsModified = true;
            //return _context.ListTrainingCenter.Update(listTrainingCenter);
            //return entityEntry;
        //}

        private async Task<EntityEntry<ListTrainingCenter>> PostTrainingCenterCreate(int listTrainingCenterID)
        {
            try
            {
                ListTrainingCenter listTrainingCenter = _context.ListTrainingCenter.First(listTrainingCenter => listTrainingCenter.ID == listTrainingCenterID);
                EntityEntry<ListTrainingCenter> entityEntry = _context.ListTrainingCenter.Remove(listTrainingCenter);
                _context.SaveChanges();
                return entityEntry;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
