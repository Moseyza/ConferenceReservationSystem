using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Models.Account
{
    public class VerifyCodeModel
    {
        [Required(ErrorMessage = "کد امنیتی را وارد کنید")]
        [Display(Name = "کد امنیتی")]
        public string Code { get; set; }
        public string NationalCode { get; set; }
    }
}
