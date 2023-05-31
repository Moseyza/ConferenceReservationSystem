using ConferenceReservationSystem.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceReservationSystem.Entity
{
    /// <summary>
    /// In this system people are also users!
    /// </summary>
    public class Person : IdentityUser<Guid>
    {
       
        [Column("FirtName")]
        public string FirtName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("FatherName")]
        public string FatherName { get; set; }
        [MaxLength(10)]
        [Column("ShamsiBirthDate")]
        public string ShamsiBirthDate { get; set; }
        [MaxLength(10)]
        [Column("NationalCode")]
        public string NationalCode { get; set; }
        [Column("Gender")]
        public PersonGender Gender { get; set; }
        [Column("ChildCount")]
        public int ChildCount { get; set; }


        [InverseProperty(nameof(ConferenceUser.User))]
        public virtual ICollection<ConferenceUser> ConferenceUsers { get; set; }
    }



}
