using RentApp.Models.Entities;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class ReservationRepository : Repository<Reservation, int>, IReservationRepository
    {

        protected RADBContext Context { get { return context as RADBContext; } }

        public ReservationRepository(DbContext context) : base(context)
        {

        }
    }
}