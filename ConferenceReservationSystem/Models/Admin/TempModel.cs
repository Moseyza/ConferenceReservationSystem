using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceReservationSystem.Models.Admin
{
    public class SelectConferenceModel

    {
        [Display(Name = "همایش")]
        public Guid ConferenceId { get; set; }
    }
}
