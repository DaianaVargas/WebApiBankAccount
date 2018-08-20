using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBankAccount.Migrations;
using WebApiBankAccount.Models;

namespace WebApiBankAccount.Tests
{
    class TestWebApiBankAccountContext: DbContext, IWebApiBankAccountContext
    {
        #region Constructor

        public TestWebApiBankAccountContext()
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

        public void MarkAsModified(BankAccount item) { }
        public void MarkAsModified(BankAccountBalance item) { }

        #endregion

    }
}
