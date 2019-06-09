using RentApp.Persistance.Repository;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; set; }
        IAppUserRepository AppUsers { get; set; }
        IBranchOfficeRepository BranchOffices { get; set; }
        ICommentRepository Comments { get; set; }
        IItemRepository Items { get; set; }
        IRateRepository Rates { get; set; }
        IReservationRepository Reservations { get; set; }
        IVehicleRepository Vehicles { get; set; }
        IVehicleTypeRepository VehicleTypes { get; set; }

        int Complete();
    }
}
