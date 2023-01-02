using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PaginatedList<sourceType> : List<sourceType>, IPaginatedList<sourceType>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<sourceType> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<sourceType>> CreateAsync(IQueryable<sourceType> source,
            int pageIndex, int pageSize, Func<IQueryable<sourceType>, IQueryable<sourceType>> secondaryQuery = null)
        {
            int count = await source.CountAsync();
            IQueryable<sourceType> itemsQuery = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (secondaryQuery != null)
                itemsQuery = secondaryQuery(itemsQuery);
            List<sourceType> items = await itemsQuery.ToListAsync();

            return new PaginatedList<sourceType>(items, count, pageIndex, pageSize);
        }
    }
}