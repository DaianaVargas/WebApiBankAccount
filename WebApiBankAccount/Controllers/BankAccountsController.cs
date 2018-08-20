using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        private IWebApiBankAccountContext _db;// = new WebApiBankAccountContext();

        #endregion

        #region Public Methods
        
        // GET: api/BankAccounts
        [AcceptVerbs("GET")]
        [Route("BankAccounts")]
        public List<BankAccount> GetBankAccounts()
        {
            return this._db.BankAccounts.ToList();
        }

        // GET: api/BankAccounts/5
        [AcceptVerbs("GET")]
        [Route("BankAccounts/{number}")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> GetBankAccountFromNumber(int number)
        {
            var banksAccounts = await this._db.BankAccounts.Where(a => a.Number.Equals(number)).ToListAsync();
            if (banksAccounts == null || !banksAccounts.Any())
            {
                return NotFound();
            }

            return Ok(banksAccounts);
        }

        // POST: api/BankAccounts
        [AcceptVerbs("POST")]
        [Route("BankAccounts/{cpf}")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(int cpf)
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

        [AcceptVerbs("POST")]
        [Route("BankAccounts")]
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._db.BankAccounts.Add(bankAccount);
            var save = await this._db.SaveChangesAsync();

            return Ok(string.Format("Conta bancária {0} cadastrada com sucesso para o CPF {1}.", bankAccount.Number, bankAccount.Cpf.ToString()));
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