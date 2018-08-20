using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiBankAccount.Models
{
    public class BankAccount
    {
        #region Properties

        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public DateTime Creation { get; set; }
        [Required]
        public int Cpf { get; set; }

        #endregion
    }
}