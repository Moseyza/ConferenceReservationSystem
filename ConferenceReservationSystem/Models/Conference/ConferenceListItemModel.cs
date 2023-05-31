
using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceReservationSystem.Models.Conference
{
    public class ConferenceListItemModel
    {
        [Display(Name = "ردیف")]
        public int Row { get; set; }
        public Guid Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "تاریخ شروع ")]
        public string ShamsiDateFrom { get; set; }
        [Display(Name = "تاریخ پایان ")]
        public string ShamsiDateTo { get; set; }
        [Display(Name = "از ساعت ")]
        public string TimeFrom { get; set; }
        [Display(Name = "تا ساعت ")]
        public string TimeTo { get; set; }
        [Display(Name = "ظرفیت ")]
        public int ParticipantsCount { get; set; }
        [Display(Name = "وضعیت")]
        public string Status { get; set; }
        [Display(Name = "محل برگزاری")]
        public string LocationAddress { get; set; }

    }
}
