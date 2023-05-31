using ConferenceReservationSystem.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceReservationSystem.Models.Conference
{
    public class ConferenceEditModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "تاریخ شروع برگزاری را وارد کنید")]
        [Display(Name = "تاریخ شروع برگزاری")]
        public string ShamsiDateFrom { get; set; }
        [Display(Name = "تاریخ اتمام برگزاری")]
        [Required(ErrorMessage = "تاریخ اتمام برگزاری")]
        public string ShamsiDateTo { get; set; }
        [Display(Name = "از ساعت")]
        public string TimeFrom { get; set; }
        [Display(Name = "تا ساعت")]
        public string TimeTo { get; set; }
        [Display(Name = "تعداد شرکت کنندگان")]
        [Required(ErrorMessage = "تعداد شرکت کنندگان را وارد کنید")]
        public int ParticipantsCount { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "وضعیت")]
        public ConferenceStatus Status { get; set; }
        [Display(Name = "محل برگزاری")]
        public string LocationAddress { get; set; }
    }
}
