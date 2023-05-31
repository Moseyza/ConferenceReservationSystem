using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConferenceReservationSystem.Entity.TotalSystem
{
    [Table("SMSDATA")]
    [Index(nameof(Perscode), Name = "RELATIONSHIP_696_FK")]
    [Index(nameof(PerCompanycode), nameof(Personcode), Name = "RELATIONSHIP_697_FK")]
    [Index(nameof(Departcode), Name = "RELATIONSHIP_698_FK")]
    [Index(nameof(Companycode), Name = "RELATIONSHIP_726_FK")]
    public partial class SmsData
    {
        [Key]
        [Column("SERIALCREATESMS")]
        [StringLength(10)]
        public string Serialcreatesms { get; set; }
        [Column("PER_COMPANYCODE")]
        [StringLength(5)]
        public string PerCompanycode { get; set; }
        [Column("PERSONCODE")]
        [StringLength(5)]
        public string Personcode { get; set; }
        [Column("DEPARTCODE")]
        [StringLength(5)]
        public string Departcode { get; set; }
        [Column("PERSCODE", TypeName = "numeric(4, 0)")]
        public decimal? Perscode { get; set; }
        [Column("COMPANYCODE")]
        [StringLength(5)]
        public string Companycode { get; set; }
        [Required]
        [Column("RECEIVERNUMBER")]
        [StringLength(20)]
        public string Receivernumber { get; set; }
        [Column("DELIVERYTIME", TypeName = "datetime")]
        public DateTime? Deliverytime { get; set; }
        [Column("REFRENCENUMBER")]
        public long? Refrencenumber { get; set; }
        [Column("SENDTIMESMS", TypeName = "datetime")]
        public DateTime? Sendtimesms { get; set; }
        [Required]
        [Column("SMSTEXT", TypeName = "text")]
        public string Smstext { get; set; }
        [Column("STARTSENDTIME", TypeName = "datetime")]
        public DateTime Startsendtime { get; set; }
        [Column("STATUSSENDSMS")]
        [StringLength(1)]
        public string Statussendsms { get; set; }
        [Column("RETRYCOUNT")]
        public int? Retrycount { get; set; }
        [Column("SENDER")]
        [StringLength(20)]
        public string Sender { get; set; }
        [Column("CREATEDATE", TypeName = "datetime")]
        public DateTime? Createdate { get; set; }
        [Column("CREATEUSERID")]
        [StringLength(20)]
        public string Createuserid { get; set; }
        [Column("UPDATEDATE", TypeName = "datetime")]
        public DateTime? Updatedate { get; set; }
        [Column("UPDATEUSERID")]
        [StringLength(20)]
        public string Updateuserid { get; set; }
    }
}
