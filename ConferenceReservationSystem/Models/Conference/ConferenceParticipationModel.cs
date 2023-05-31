using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Models.Conference
{
    public class ConferenceParticipationModel
    {
        public ConferenceParticipationModel()
        {
            AccompanyingsCount = 0;
        }
        [Display(Name = "همایش")]
        [Required(ErrorMessage = "همایش مورد نظر را انتخاب نمایید")]
        public Guid ConferenceId { get; set; }

        [Display(Name = "تاریخ حضور")]
        [Required(ErrorMessage = "تاریخ حضور را وارد کنید")]
        public string Date { get; set; }
        [Display(Name = "تعداد همراهان")]
        [Required(ErrorMessage = "تعداد همراهان را وارد کنید")]
        public int AccompanyingsCount { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
  
    }
}
