using RentApp.Models.Entities;using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class ServiceRepository : Repository<Service, int>, IServiceRepository
    {
        protected RADBContext Context { get { return context as RADBContext; } }

        public ServiceRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Service> GetAllServices()
        {
            return Context.Services.Where(p => p.Deleted != true).ToList();
        }

    }
}