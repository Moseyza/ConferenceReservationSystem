using ConferenceReservationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Interfaces
{
    public interface IUnitOfWork
    {
        AppDbContext Context { get; }
    }
}
