using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.Repository.Interfaces
{
    public interface IItemRepository : IRepository<Item, int>
    {

        IEnumerable<Item> GetItemForVehicle(int vehicleId);
    }
}
