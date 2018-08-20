using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebApiBankAccount.Controllers;
using WebApiBankAccount.Models;

namespace WebApiBankAccount.Tests.Controllers
{
    [TestClass]
    public class BankAccountsControllerTest
    {
        #region Properties

        public BankAccountsController Controller { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        public async Task PostAsync()
        {
            // Organizar
            //BankAccountsController controller = new BankAccountsController(new TestWebApiBankAccountContext());
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount(false);

            // Agir
            var result = await this.Controller.PostBankAccount(bankAccountTest.Cpf) as OkNegotiatedContentResult<string>;
            
            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Contains("sucesso"));
        }

        [TestMethod]
        public async Task GetBankAccountsAsync()
        {
            // Organizar
            //BankAccountsController controller = new BankAccountsController(new TestWebApiBankAccountContext());
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount(true);

            // Agir
            var result = this.Controller.GetBankAccounts();

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task GetBankAccountFromNumber()
        {
            // Organizar
            //BankAccountsController controller = new BankAccountsController(new TestWebApiBankAccountContext());
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount(true);
            Task<IHttpActionResult> result = null;

            //var result1 = await this.Controller.PostBankAccount(bankAccountTest.Cpf) as OkNegotiatedContentResult<string>;

            // Agir
            if (bankAccountTest != null)
            {
                //var number = Convert.ToInt32(bankAccountTest.Content.Replace(" ", ";").Split(';')[2]);
                result = this.Controller.GetBankAccountFromNumber(bankAccountTest.Number);
            }

            // Declarar
            Assert.IsNotNull(result);
            
        }

        #endregion


        #region Private Methods

        private void CreateController()
        {
            if (this.Controller == null)
                this.Controller = new BankAccountsController(new TestWebApiBankAccountContext());
        }

        //private bool MoreThanZero(IQueryable<BankAccount> collection)
        //{
        //    return collection.Count() > 0;
        //}

        private async Task<BankAccount> GetDemoBankAccount(bool isInsert)
        {
            var bankAccount = new BankAccount()
            {
                Id = Guid.NewGuid(),
                Number = 1234,
                Cpf = 00200200202,
                Creation = DateTime.Now
            };

            if (isInsert)
            {
                //BankAccountsController controller = new BankAccountsController(new TestWebApiBankAccountContext());
                var result = await this.Controller.PostBankAccount(bankAccount.Cpf)as OkNegotiatedContentResult<string>;
                // pegar o número da conta criado
                if (result != null)
                    bankAccount.Number = Convert.ToInt32(result.Content.Replace(" ", ";").Split(';')[2]);

                return bankAccount;
            }
            else
                return bankAccount;
        }

        #endregion
    }
}
