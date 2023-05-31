using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferenceReservationSystem.Enumerations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConferenceReservationSystem.Entity
{
    [Table("Conference")]
    public partial class Conference
    {
        public Conference()
        {
            ConferenceUsers = new HashSet<ConferenceUser>();
        }

        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateTo { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantsCount { get; set; }
        public ConferenceStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public string LocationAddress { get; set; }

        [InverseProperty(nameof(ConferenceUser.Conference))]
        public virtual ICollection<ConferenceUser> ConferenceUsers { get; set; }
    }
}
