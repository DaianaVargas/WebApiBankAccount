using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiBankAccount.Models
{
    public class BankAccountBalance
    {
        public Guid id { get; set; }
        [Required]
        public string Operation { get; set; }
        [Required]
        public decimal  Value { get; set; }
        [Required]
        public DateTime Creation { get; set; }
        
        public Guid BankAccountID { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}