using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBankAccount.Models;

namespace WebApiBankAccount.Tests
{
    public class TestBankAccountDbSet : TestDbSet<BankAccount>
    {
        public override BankAccount Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Id == (Guid)keyValues.Single());
        }
    }
}
