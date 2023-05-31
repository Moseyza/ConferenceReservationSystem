using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "کد ملی را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        [MinLength(10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
    }
}
