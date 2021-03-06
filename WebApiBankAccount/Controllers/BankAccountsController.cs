﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiBankAccount.Models;

namespace WebApiBankAccount.Controllers
{
    [RoutePrefix("api")]
    public class BankAccountsController : ApiController
    {
        #region Constructors

        public BankAccountsController()
        {
            this._db = new WebApiBankAccountContext();
        }

        public BankAccountsController(IWebApiBankAccountContext context)
        {
            this._db = context;
        }

        #endregion

        #region Private Properties

        private IWebApiBankAccountContext _db;

        #endregion

        #region Public Methods
        
        // GET: api/BankAccounts
        [AcceptVerbs("GET")]
        [Route("BankAccounts")]
        public async Task<IHttpActionResult> GetBankAccounts()
        {
            try
            {
                var accountsBalances = await this._db.BankAccounts.ToListAsync();
                return Ok(accountsBalances);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET: api/BankAccounts/5
        [AcceptVerbs("GET")]
        [Route("BankAccounts/{number}")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> GetBankAccountFromNumber(int number)
        {
            try
            {
                var banksAccounts = await this._db.BankAccounts.Where(a => a.Number.Equals(number)).ToListAsync();
                if (banksAccounts == null || !banksAccounts.Any())
                {
                    return NotFound();
                }

                return Ok(banksAccounts);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }

        // POST: api/BankAccounts
        [AcceptVerbs("POST")]
        [Route("BankAccounts/{cpf}")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(int cpf)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Random random = new Random();
                int numberAccount = random.Next(0, 9999);

                var bankAccount = new BankAccount();
                bankAccount.Id = Guid.NewGuid();
                bankAccount.Number = numberAccount;
                bankAccount.Creation = DateTime.Now;
                bankAccount.Cpf = cpf;
                bankAccount.Creation = DateTime.Now;

                this._db.BankAccounts.Add(bankAccount);
                var save = await this._db.SaveChangesAsync();

                return Ok(string.Format("Conta bancária {0} cadastrada com sucesso para o CPF {1}.", numberAccount, cpf.ToString()));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }

        [AcceptVerbs("POST")]
        [Route("BankAccounts")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(BankAccount bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                this._db.BankAccounts.Add(bankAccount);
                var save = await this._db.SaveChangesAsync();

                return Ok(string.Format("Conta bancária {0} cadastrada com sucesso para o CPF {1}.", bankAccount.Number, bankAccount.Cpf.ToString()));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }

        #endregion

        #region Private Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool BankAccountExists(int number)
        {
            return this._db.BankAccounts.Count(e => e.Number == number) > 0;
        }

        #endregion
    }
}