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
    public class BankAccountBalancesController : ApiController
    {
        #region Constructors

        public BankAccountBalancesController()
        {
            this.db = new WebApiBankAccountContext();
        }

        public BankAccountBalancesController(IWebApiBankAccountContext context)
        {
            this.db = context;
        }

        #endregion

        #region Private Properties

        private IWebApiBankAccountContext db;

        #endregion

        #region Public Methods

        // GET: api/BankAccountBalances
        [AcceptVerbs("GET")]
        [Route("BankAccountBalances")]
        public List<BankAccountBalance> GetBankAccountBalances()
        {
            return db.BankAccountBalances.ToList();
        }

        // GET: api/BankAccountBalances/5
        [AcceptVerbs("GET")]
        [Route("BankAccountBalances/{number}/{type}")]
        [ResponseType(typeof(BankAccountBalance))]
        public async Task<IHttpActionResult> GetBankAccountBalance(int number, string type)
        {
            var banksAccountsBalances = db.BankAccountBalances.Where(a => a.BankAccount.Number.Equals(number)).ToList();
            if (banksAccountsBalances == null || !banksAccountsBalances.Any())
            {
                return NotFound();
            }

            if (type.Equals("currentBalance"))
                return Ok(this.GetCurrentBalance(banksAccountsBalances));
            else 
                return Ok(banksAccountsBalances);
        }

        // POST: api/BankAccountBalances
        [AcceptVerbs("POST")]
        [Route("BankAccountBalances/{number}/{value}/{operation}")]
        [ResponseType(typeof(BankAccountBalance))]
        public async Task<IHttpActionResult> PostBankAccountBalance(int number, decimal value, string operation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankAccount = db.BankAccounts.FirstOrDefault(a => a.Number.Equals(number));

            if (bankAccount == null)
            {
                return NotFound();
            }

            var bankAccountBalance = new BankAccountBalance();
            bankAccountBalance.id = Guid.NewGuid();
            bankAccountBalance.BankAccountID = bankAccount.Id;
            bankAccountBalance.Value = value;
            bankAccountBalance.Operation = operation.ToLower();
            bankAccountBalance.Creation = DateTime.Now;

            db.BankAccountBalances.Add(bankAccountBalance);
            var save = await db.SaveChangesAsync();

            var typeOperation = operation.ToLower().Equals("sum") ? "Depósito" : "Saque";
            return Ok(string.Format("{0} cadastrado com sucesso na conta bancária {1}.", typeOperation, number));
        }

        #endregion

        #region Private Methods
                
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankAccountBalanceExists(int number)
        {
            return db.BankAccountBalances.Count(e => e.BankAccount.Number == number) > 0;
        }

        private decimal GetCurrentBalance(List<BankAccountBalance> collection)
        {
            decimal total = 0;

            foreach (var item in collection.OrderBy(a => a.Creation))
            {
                total += item.Operation.ToLower().Equals("sum") ? item.Value : (item.Value * -1);
            }

            return total;
        }

        #endregion
    }
}