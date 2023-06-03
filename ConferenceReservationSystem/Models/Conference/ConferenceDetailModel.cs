using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Models.Conference
{
    public class ConferenceDetailModel
    {
        [Display(Name = "عنوان:")]
        public string Title { get; set; }
        [Display(Name = "تاریخ شروع برگزاری:")]
        public string ShamsiDateFrom { get; set; }
        [Display(Name = "تاریخ اتمام برگزاری")]
        public string ShamsiDateTo { get; set; }
        [Display(Name = "از ساعت:")]
        public string TimeFrom { get; set; }
        [Display(Name = "تا ساعت:")]
        public string TimeTo { get; set; }
        [Display(Name = "ظرفیت در هر روز:")]
        public int DailyCapacity { get; set; }
        [Display(Name = "محل برگزاری:")]
        public string LocationAddress { get; set; }
        [Display(Name = "توضیحات:")]
        public string Description { get; set; }

    }
}
