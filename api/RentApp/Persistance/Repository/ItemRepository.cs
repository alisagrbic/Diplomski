using RentApp.Models.Entities;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class ItemRepository : Repository<Item, int>, IItemRepository
    {
        protected RADBContext Context { get { return context as RADBContext; } }

        public ItemRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Item> GetItemForVehicle(int vehicleId)
        {
            return Context.Items.Where(p => p.VehicleID == vehicleId);
        }

    }
}