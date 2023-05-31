using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Interfaces
{
    public interface ISmsSender
    {
        Task SendSecurityCodeAsync(string phoneNumber, string code);
    }
}
