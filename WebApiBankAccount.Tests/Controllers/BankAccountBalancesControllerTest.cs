﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class BankAccountBalancesControllerTest
    {
        #region Properties

        public BankAccountBalancesController Controller { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        public async Task PostSumAsync()
        {
            // Organizar
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount();

            // Agir
            var resultSum = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 230, "sum") as OkNegotiatedContentResult<string>;

            // Declarar
            Assert.IsNotNull(resultSum);
            Assert.IsTrue(resultSum.Content.Contains("sucesso"));
        }

        [TestMethod]
        public async Task PostDiscountAsync()
        {
            // Organizar
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount();

            // Agir
            var resultDiscount = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 30, "discount") as OkNegotiatedContentResult<string>;

            // Declarar
            Assert.IsNotNull(resultDiscount);
            Assert.IsTrue(resultDiscount.Content.Contains("sucesso"));
        }

        [TestMethod]
        public async Task GetBankAccountBalancesAsync()
        {
            // Organizar
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount();
            var resultSum = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 230, "sum") as OkNegotiatedContentResult<string>;
            var resultDiscount = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 30, "discount") as OkNegotiatedContentResult<string>;

            // Agir
            var result = await this.Controller.GetBankAccountBalances() as OkNegotiatedContentResult<List<BankAccountBalance>>;

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
        }

        [TestMethod]
        public async Task GetBankAccountBalancesCurrentAsync()
        {
            // Organizar
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount();
            var resultSum = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 230, "sum") as OkNegotiatedContentResult<string>;
            var resultDiscount = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 30, "discount") as OkNegotiatedContentResult<string>;


            // Agir
            var result = await this.Controller.GetBankAccountBalance(bankAccountTest.Number, "currentBalance") as OkNegotiatedContentResult<List<BankAccountBalance>>;

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetBankAccountBalancesExtractAsync()
        {
            // Organizar
            this.CreateController();
            var bankAccountTest = await GetDemoBankAccount();
            var resultSum = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 230, "sum") as OkNegotiatedContentResult<string>;
            var resultDiscount = await this.Controller.PostBankAccountBalance(bankAccountTest.Number, 30, "discount") as OkNegotiatedContentResult<string>;


            // Agir
            var result = await this.Controller.GetBankAccountBalance(bankAccountTest.Number, "extract") as OkNegotiatedContentResult<decimal>;

            // Declarar
            Assert.IsNotNull(result);
        }
        
        #endregion
        
        #region Private Methods

        private void CreateController()
        {
            if (this.Controller == null)
                this.Controller = new BankAccountBalancesController(new TestWebApiBankAccountContext());
        }
        
        private async Task<BankAccount> GetDemoBankAccount()
        {
            var bankAccount = new BankAccount()
            {
                Id = Guid.NewGuid(),
                Number = 1234,
                Cpf = 00200200202,
                Creation = DateTime.Now
            };


            BankAccountsController controller = new BankAccountsController(new TestWebApiBankAccountContext());
            var resultBankAccount = await controller.PostBankAccount(bankAccount.Cpf) as OkNegotiatedContentResult<string>;
            var resultBankAccountBalance = await this.Controller.PostBankAccountBalance(bankAccount.Number, 230, "sum") as OkNegotiatedContentResult<string>;
            // pegar o número da conta criado
            //if (result != null)
            //    bankAccount.Number = Convert.ToInt32(result.Content.Replace(" ", ";").Split(';')[2]);

            return bankAccount;

        }

        #endregion
    }
}
