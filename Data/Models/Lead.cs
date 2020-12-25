using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCRM.Data.Models
{
    public class Lead
    {
        [Key, Required, MaxLength(9)]
        public string ID { get; set; }

        [Required, MinLength(2)]
        public string Name { get; set; }

        [Required, MinLength(4)]
        public string Address { get; set; }

        [Required, MinLength(10), MaxLength(13)]
        public string Phonenumber { get; set; }

        [MinLength(10), MaxLength(13)]
        public string SecondaryPhonenumber { get; set; }

        [Required, MinLength(5)]
        public string Email { get; set; }

        public bool? Male { get; set; }

        [Required]
        public StatusEnum Status { get; set; }
        public bool SafetyCheck { get; set; }
        public bool PaidRegistrationFee { get; set; }
        public bool SignedContract { get; set; }
        public bool AssignedToWork { get; set; }
        public bool PaymentTransfered { get; set; }
        public string DesiredCourse { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }
        public virtual ICollection<FreeText> FreeTexts { get; set; } 

        public Lead()
        {
            FreeTexts = new List<FreeText>();
        }


        public enum StatusEnum
        {
            Unspoken,
            Spoken,
            Signed
        }
    }
}
