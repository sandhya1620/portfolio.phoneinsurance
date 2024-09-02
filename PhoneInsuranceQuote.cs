csharp
using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneInsurance.Models
{
    public class PhoneInsuranceQuote
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneBrand { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneModel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string Condition { get; set; } // New, Like New, Used, etc.

        public decimal QuoteAmount { get; set; }
    }
}