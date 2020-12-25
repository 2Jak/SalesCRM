using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCRM.Data.Models
{
    public class FreeText
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        [MaxLength(9)]
        public string LeadID { get; set; }
        public Lead Lead { get; set; }

        public FreeText()
        {
            this.Text = "";
        }
    }
}