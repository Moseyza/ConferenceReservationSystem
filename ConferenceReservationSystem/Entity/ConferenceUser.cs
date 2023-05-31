using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConferenceReservationSystem.Entity
{
    [Table("ConferenceUser")]
    public partial class ConferenceUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ConferenceId { get; set; }
        public int AccompanyingsCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        
        public string Description { get; set; }

        [ForeignKey(nameof(ConferenceId))]
        [InverseProperty("ConferenceUsers")]
        public virtual Conference Conference { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Person.ConferenceUsers))]
        public virtual Person User { get; set; }

        public DateTime CreationDate { get; set; }


    }
}
