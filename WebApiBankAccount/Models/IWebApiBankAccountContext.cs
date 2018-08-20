using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApiBankAccount.Models
{
    public interface IWebApiBankAccountContext: IDisposable
    {
        DbSet<BankAccount> BankAccounts { get; }
        DbSet<BankAccountBalance> BankAccountBalances { get; }
        int SaveChanges();
        void MarkAsModified(BankAccount item);
        void MarkAsModified(BankAccountBalance item);
        Task<int> SaveChangesAsync();        
    }
}