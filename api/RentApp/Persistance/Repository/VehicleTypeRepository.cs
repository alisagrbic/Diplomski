using RentApp.Models.Entities;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class VehicleTypeRepository : Repository<VehicleType, int>, IVehicleTypeRepository
    {
        protected RADBContext Context { get { return context as RADBContext; } }

        public VehicleTypeRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            return Context.VehicleTypes.Where(p => p.Deleted != true).ToList();
        }

    }
}