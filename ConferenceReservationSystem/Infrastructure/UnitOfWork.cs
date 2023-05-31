using ConferenceReservationSystem.Entity;
using ConferenceReservationSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext dbContext) 
        {
            _context = dbContext;
        }


        public AppDbContext Context => _context;

    }
}
