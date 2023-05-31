using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using ConferenceReservationSystem.Entity;
using ConferenceReservationSystem.Infrastructure;
using ConferenceReservationSystem.Interfaces;
using ConferenceReservationSystem.Models.Conference;
using ConferenceReservationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConferenceReservationSystem.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomUserManager _userManager;
        private readonly IConferenceService _conferenceService;
        #endregion
        #region Constructor
        public ConferenceController(IUnitOfWork unitOfWork, CustomUserManager customUserManager, IConferenceService conferenceService)
        {
            _unitOfWork = unitOfWork;
            _userManager = customUserManager;
            _conferenceService = conferenceService;
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> Participate()
        {
            ViewBag.ConferenceList = await _conferenceService.GetConferenceListAsync();
            ViewBag.AccompanyingsCountOptions = await GetAccompanyingsCountOptionsAsync();
            return View();
        }

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Participate(ConferenceParticipationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conference = await _unitOfWork.Context.Conferences.FirstOrDefaultAsync(x => x.Id == model.ConferenceId);
                    if (conference != null)
                    {
                        var selectedDate = DateTimeMapper.GetDateTimeFromShamsiDate(model.Date);
                        var user = await _userManager.GetUserAsync(User);

                        if (CalcRemainingCapacity(conference, selectedDate) < model.AccompanyingsCount + 1)
                            ViewBag.ErrorMessage = MessageHelper.ThereIsNotEnoughCapacity;
                        else if (conference.DateFrom > selectedDate || conference.DateTo < selectedDate)
                            ViewBag.ErrorMessage = MessageHelper.SelectedDateIsNotBetweenStartAndEndDate;
                        else if ((await _unitOfWork.Context.ConferenceUsers.Where(x => x.UserId == user.Id && x.ConferenceId == model.ConferenceId).FirstOrDefaultAsync()) != null)
                            ViewBag.ErrorMessage = MessageHelper.YouRegisteredBefore;
                        else if (user.Gender == Enumerations.PersonGender.Male && model.AccompanyingsCount > user.ChildCount)
                            ViewBag.ErrorMessage = MessageHelper.AccompanyingsCountIsNotValid;
                        else if (user.Gender == Enumerations.PersonGender.Female && model.AccompanyingsCount > 4)
                            ViewBag.ErrorMessage = MessageHelper.AccompanyingsCountIsNotValid;

                        else 
                        {
                            var conferenceUser = new ConferenceUser()
                            {
                                Id = Guid.NewGuid(),
                                AccompanyingsCount = model.AccompanyingsCount,
                                Date = selectedDate,
                                UserId = user.Id,
                                ConferenceId = model.ConferenceId,
                                Description = model.Description,
                                CreationDate = DateTime.Now

                            };
                            _unitOfWork.Context.Add(conferenceUser);
                            _unitOfWork.Context.SaveChanges();
                            ViewBag.SuccessMessage = MessageHelper.YourRegistrationSaveDone;
                        }
                    }
                }
                catch 
                {
                    ViewBag.ErrorMessage = MessageHelper.InputDataIsNotValid;
                }
            }
            else 
            {
                ViewBag.ErrorMessage = MessageHelper.InputDataIsNotValid;
            }
            ViewBag.ConferenceList = await _conferenceService.GetConferenceListAsync();
            ViewBag.AccompanyingsCountOptions = await GetAccompanyingsCountOptionsAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowDetails(Guid id/*, string date*/)
        {
            try
            {
                var conference = await _unitOfWork.Context.Conferences.Include(x => x.ConferenceUsers).FirstOrDefaultAsync(x => x.Id == id);
                var model = new ConferenceDetailModel();
                //var selectedDate = DateTimeMapper.GetDateTimeFromShamsiDate(date);
                if (conference != null)
                {
                    model.Title = conference.Title;
                    model.Description = conference.Description;
                    model.ShamsiDateFrom = DateTimeMapper.GetShamsiDate(conference.DateFrom);
                    model.ShamsiDateTo = DateTimeMapper.GetShamsiDate(conference.DateTo);
                    model.TimeTo = DateTimeMapper.GetTimeString(conference.TimeTo);
                    model.TimeFrom = DateTimeMapper.GetTimeString(conference.TimeFrom);
                    
                   //model.RemainingCapacity = CalcRemainingCapacity(conference, selectedDate);
                    model.LocationAddress = conference.LocationAddress;
                }
                return PartialView("_ConferenceDetail", model);
            }
            catch { }
            return PartialView("_ConferenceDetail", null);
        }

        private int CalcRemainingCapacity(Conference conference , DateTime date)
        {
            var usedCapacity = conference.ConferenceUsers.Where(x=>x.Date == date).Sum(x => x.AccompanyingsCount) + conference.ConferenceUsers.Count();
            var result = conference.ParticipantsCount - usedCapacity;
            return result;
        }

        private async Task<List<SelectListItem>> GetAccompanyingsCountOptionsAsync()
        {
            var result = new List<SelectListItem>();
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var childCount = 0;
                if (user.Gender == Enumerations.PersonGender.Male)
                    childCount = user.ChildCount;
                else
                    childCount = 3;
                for (int i = 0; i <= childCount; i++)
                {
                    result.Add(new SelectListItem()
                    {
                        Text = i.ToString(),
                        Value = i.ToString(),
                        Selected = i ==0 

                    }) ;
                }
                return result;
            }
            catch 
            {
                return result;
            }
            

        }



    }
}
