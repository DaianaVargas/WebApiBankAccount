using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiBankAccount.Migrations;

namespace WebApiBankAccount.Models
{
    public class WebApiBankAccountContext : DbContext, IWebApiBankAccountContext
    {
        #region Constructor

        public WebApiBankAccountContext() : base("name=WebApiBankAccountContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebApiBankAccountContext, Configuration>());
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        #endregion

        #region Public Properties

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountBalance> BankAccountBalances { get; set; }

        #endregion

        #region Public Methods
        
        public void MarkAsModified(BankAccount item)
        {
            Entry(item).State = EntityState.Modified;
        }
        
        public void MarkAsModified(BankAccountBalance item)
        {
            Entry(item).State = EntityState.Modified;
        }

        #endregion
    }
}
