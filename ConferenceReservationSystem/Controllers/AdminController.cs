using ConferenceReservationSystem.Entity;
using ConferenceReservationSystem.Enumerations;
using ConferenceReservationSystem.Infrastructure;
using ConferenceReservationSystem.Interfaces;
using ConferenceReservationSystem.Models.Admin;
using ConferenceReservationSystem.Models.Conference;
using ConferenceReservationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConferenceService _conferenceService;
        private readonly IWebHostEnvironment _webhostEnviroment;
        #endregion

        public AdminController(IUnitOfWork unitOfWork, IConferenceService conferenceService, IWebHostEnvironment webhostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _conferenceService = conferenceService;
            _webhostEnviroment = webhostEnviroment;
        }

        [HttpGet]
        public IActionResult CreateConference()
        {
            ViewBag.StatusList = EnumHelper.GetEnumValueAsSelectList<ConferenceStatus>();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConference(ConferenceEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conference = new Conference()
                    {
                        Id = Guid.NewGuid(),
                        DateFrom = DateTimeMapper.GetDateTimeFromShamsiDate(model.ShamsiDateFrom),
                        DateTo = DateTimeMapper.GetDateTimeFromShamsiDate(model.ShamsiDateTo),
                        TimeFrom = DateTimeMapper.GetTime(model.TimeFrom),
                        TimeTo = DateTimeMapper.GetTime(model.TimeTo),
                        ParticipantsCount = model.ParticipantsCount,
                        Title = model.Title,
                        Description = model.Description,
                        Status = model.Status,
                        LocationAddress = model.LocationAddress,
                        CreationDate = DateTime.Now


                    };
                    _unitOfWork.Context.Conferences.Add(conference);
                    await _unitOfWork.Context.SaveChangesAsync();
                    return RedirectToAction("ConferenceList");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = MessageHelper.SaveErrorMessage;

                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteConference(Guid id)
        {
            try
            {
                var conference = await _unitOfWork.Context.Conferences.FindAsync(id);
                _unitOfWork.Context.Conferences.Remove(conference);
                await _unitOfWork.Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: add suitable message

            }


            return RedirectToAction("ConferenceList");

        }

        public async Task<IActionResult> EditConference(Guid id)
        {
            var model = new ConferenceEditModel();
            try
            {
                var conference = await _unitOfWork.Context.Conferences.FindAsync(id);
                if (conference != null)
                {
                    model.Title = conference.Title;
                    model.Description = conference.Description;
                    model.ShamsiDateFrom = $"{conference.DateFrom.Year}/{conference.DateFrom.Month}/{conference.DateFrom.Day}";
                    model.ShamsiDateTo = $"{conference.DateTo.Year}/{conference.DateTo.Month}/{conference.DateTo.Day}";
                    model.TimeTo = DateTimeMapper.GetTimeString(conference.TimeTo);
                    model.TimeFrom = DateTimeMapper.GetTimeString(conference.TimeFrom);
                    model.Status = conference.Status;
                    model.LocationAddress = conference.LocationAddress;
                    model.ParticipantsCount = conference.ParticipantsCount;
                    model.Id = id;


                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.StatusList = EnumHelper.GetEnumValueAsSelectList<ConferenceStatus>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConference(ConferenceEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conference = await _unitOfWork.Context.Conferences.FindAsync(model.Id);
                    if (conference != null)
                    {
                        conference.DateFrom = DateTimeMapper.GetDateTimeFromShamsiDate(model.ShamsiDateFrom);
                        conference.DateTo = DateTimeMapper.GetDateTimeFromShamsiDate(model.ShamsiDateTo);
                        conference.TimeFrom = DateTimeMapper.GetTime(model.TimeFrom);
                        conference.TimeTo = DateTimeMapper.GetTime(model.TimeTo);
                        conference.ParticipantsCount = model.ParticipantsCount;
                        conference.Title = model.Title;
                        conference.Description = model.Description;
                        conference.Status = model.Status;
                        conference.LocationAddress = model.LocationAddress;
                        conference.CreationDate = DateTime.Now;


                    };
                    await _unitOfWork.Context.SaveChangesAsync();
                    return RedirectToAction("ConferenceList");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = MessageHelper.SaveErrorMessage;

                }
                return RedirectToAction("ConferenceList");
            }
            else
            {
                return View(model);
            }

        }

        public async Task<IActionResult> ConferenceList()
        {
            var conferenceList = await _unitOfWork.Context.Conferences.OrderByDescending(x => x.CreationDate).ToListAsync();
            var conferenceModelList = new List<ConferenceListItemModel>();
            var index = 1;
            foreach (var conference in conferenceList)
            {
                var model = new ConferenceListItemModel()
                {
                    Id = conference.Id,
                    ParticipantsCount = conference.ParticipantsCount,
                    ShamsiDateFrom = DateTimeMapper.GetShamsiDate(conference.DateFrom),
                    ShamsiDateTo = DateTimeMapper.GetShamsiDate(conference.DateTo),
                    Title = conference.Title,
                    TimeFrom = DateTimeMapper.GetTimeString(conference.TimeFrom),
                    TimeTo = DateTimeMapper.GetTimeString(conference.TimeTo),
                    Status = EnumHelper.GetEnumDescription<ConferenceStatus>(conference.Status),
                    Row = index++
                };
                conferenceModelList.Add(model);
            }

            return View(conferenceModelList);


        }

        [HttpGet]
        public async Task<IActionResult> ParticipationList()
        {
            ViewBag.ConferenceList = await _conferenceService.GetConferenceListAsync();
            return View();
        }

        public async Task<IActionResult> ShowParticipationList(Guid conferenceId)
        {
            var conferenceUsers = await _unitOfWork.Context.ConferenceUsers.Where(x => x.ConferenceId == conferenceId).Include(x => x.User).OrderBy(x => x.Date).ThenBy(x => x.CreationDate).ToListAsync();
            var participationList = new List<ParticipationListModel>();
            var index = 1;
            foreach (var item in conferenceUsers)
            {
                var participationItem = new ParticipationListModel()
                {
                    Row = index++,
                    AccompanyingsCount = item.AccompanyingsCount,
                    FirstName = item.User.FirtName,
                    LastName = item.User.LastName,
                    NationalCode = item.User.NationalCode,
                    ShamsiDate = DateTimeMapper.GetShamsiDate(item.Date)
                };
                participationList.Add(participationItem);
            }
            ViewBag.IsConferenceSelected = conferenceId != Guid.Empty;
            return PartialView("_ParticipationList", participationList);
        }

        public async Task<HttpStatusCode> CreateExcelReport(Guid conferenceId)
        {
            try
            {
                var conference = await _unitOfWork.Context.Conferences.FindAsync(conferenceId);
                var conferenceUsers = await _unitOfWork.Context.ConferenceUsers.Where(x => x.ConferenceId == conferenceId).Include(x => x.User).OrderBy(x => x.Date).ThenBy(x => x.CreationDate).ToListAsync();
                string excelpath = $"{_webhostEnviroment.WebRootPath}/reports/report.xlsx";
                FileInfo finame = new FileInfo(excelpath);
                if (System.IO.File.Exists(excelpath))
                {
                    System.IO.File.Delete(excelpath);
                }
                if (!System.IO.File.Exists(excelpath))
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;
                    var excel = new ExcelPackage(finame);
                    var workSheet = excel.Workbook.Worksheets.Add(DateTime.UtcNow.Date.ToString());
                    workSheet.Cells[1, 1, 1, 8].Merge = true;
                    workSheet.View.RightToLeft = true;
                    var mergedId = workSheet.MergedCells[1, 1];
                    var sheetTitle = workSheet.Cells[mergedId].RichText.Add($"گزارش شرکت افراد در  «{conference.Title}»");
                    sheetTitle.Size = 14;
                    sheetTitle.Bold = true;
                    workSheet.Cells[mergedId].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    for (int i = 1; i <= 8; i++)
                    {
                        workSheet.Column(i).Width = 15;
                    }

                    workSheet.Cells[2, 1].RichText.Add("ردیف").Bold = true;
                    workSheet.Cells[2, 2].RichText.Add("نام").Bold = true;
                    workSheet.Cells[2, 3].RichText.Add("نام خانوادگی").Bold = true;
                    workSheet.Cells[2, 4].RichText.Add("نام پدر").Bold = true;
                    workSheet.Cells[2, 5].RichText.Add("کد ملی").Bold = true;
                    workSheet.Cells[2, 6].RichText.Add("تاریخ حضور").Bold = true;
                    workSheet.Cells[2, 7].RichText.Add("تعداد همراهان").Bold = true;
                    workSheet.Cells[2, 8].RichText.Add("توضیحات").Bold = true;
                    for (int i = 1; i <= conferenceUsers.Count; i++)
                    {
                        workSheet.Cells[i + 2, 1].Value = i;
                        workSheet.Cells[i + 2, 2].Value = conferenceUsers[i - 1].User.FirtName;
                        workSheet.Cells[i + 2, 3].Value = conferenceUsers[i - 1].User.LastName;
                        workSheet.Cells[i + 2, 4].Value = conferenceUsers[i - 1].User.FatherName;
                        workSheet.Cells[i + 2, 5].Value = conferenceUsers[i - 1].User.NationalCode;
                        workSheet.Cells[i + 2, 6].Value = DateTimeMapper.GetShamsiDate(conferenceUsers[i - 1].Date);
                        workSheet.Cells[i + 2, 7].Value = conferenceUsers[i - 1].AccompanyingsCount;
                        workSheet.Cells[i + 2, 8].Value = conferenceUsers[i - 1].Description;



                    }
                    var totalCount = conferenceUsers.Sum(x => x.AccompanyingsCount) + conferenceUsers.Count;
                    var lastRowIndex = conferenceUsers.Count + 3;
                    workSheet.Cells[lastRowIndex, 1, lastRowIndex, 8].Merge = true;
                    workSheet.View.RightToLeft = true;
                    var mergedIdLastRow = workSheet.MergedCells[lastRowIndex, 1];
                    var sheetTitleLastRow = workSheet.Cells[mergedIdLastRow].RichText.Add($"مجموع کل افراد شرکت کننده : {totalCount}");
                    sheetTitleLastRow.Size = 11;
                    sheetTitleLastRow.Bold = true;

                    workSheet.Cells[mergedIdLastRow].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;



                    System.IO.File.WriteAllBytes(excelpath, excel.GetAsByteArray());
                }
                return HttpStatusCode.OK;
            }
            catch 
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public IActionResult DownloadFile()

        {

            var memory = DownloadSinghFile("report.xlsx");

            return File(memory.ToArray(), "application/vnd.ms-excel", "report.xlsx");

        }

        private MemoryStream DownloadSinghFile(string filename)

        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{_webhostEnviroment.WebRootPath}/reports/", filename);

            var memory = new MemoryStream();

            if (System.IO.File.Exists(path))

            {

                var net = new WebClient();

                var data = net.DownloadData(path);

                var content = new System.IO.MemoryStream(data);

                memory = content;

            }

            memory.Position = 0;

            return memory;

        }
    }




}

