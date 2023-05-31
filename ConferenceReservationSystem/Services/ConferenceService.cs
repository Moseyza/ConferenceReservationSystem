using ConferenceReservationSystem.Infrastructure;
using ConferenceReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Services
{
    public interface IConferenceService 
    {
        Task<List<SelectListItem>> GetConferenceListAsync();
    }
    public class ConferenceService : IConferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConferenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<SelectListItem>> GetConferenceListAsync()
        {
            var activeConfrences = await _unitOfWork.Context.Conferences.Where(x => x.Status == Enumerations.ConferenceStatus.Enabled && x.DateTo >= DateTime.Now).ToListAsync();
            var conferenceList = new List<SelectListItem>() { new SelectListItem() { Text = "انتخاب کنید", Value = "" } };
            foreach (var conference in activeConfrences)
            {
                conferenceList.Add(new SelectListItem()
                {
                    Text = conference.Title,
                    Value = conference.Id.ToString()
                });
            }
            return conferenceList;
        }
    }
}
