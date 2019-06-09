using RentApp.Models.Entities;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class RateRepository : Repository<Rate, int>, IRateRepository
    {
        protected RADBContext Context { get { return context as RADBContext; } }

        public RateRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Rate> GetRate(int serviceId)
        {
            return Context.Rates.Where(p => p.ServiceID == serviceId);
        }

    }
}