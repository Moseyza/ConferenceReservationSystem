using System.ComponentModel.DataAnnotations;

namespace ConferenceReservationSystem.Models.Admin
{
    public class ParticipationListModel
    {
        [Display(Name = "ردیف")]
        public int Row { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name="نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(Name = "تعداد همراه")]
        public int AccompanyingsCount { get; set; }
        [Display(Name = "تاریخ حضور")]
        public string ShamsiDate { get; set; }

    }
}
