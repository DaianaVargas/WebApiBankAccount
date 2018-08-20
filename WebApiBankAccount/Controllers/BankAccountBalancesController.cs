using System;
using System.Collections.Generic;
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
    public class BankAccountBalancesController : ApiController
    {
        #region Constructors

        public BankAccountBalancesController()
        {
            this._db = new WebApiBankAccountContext();
        }

        public BankAccountBalancesController(IWebApiBankAccountContext context)
        {
            this._db = context;
        }

        #endregion

        #region Fields

        private IWebApiBankAccountContext _db;

        #endregion

        #region Public Methods

        // GET: api/BankAccountBalances
        [AcceptVerbs("GET")]
        [Route("BankAccountBalances")]
        public async Task<IHttpActionResult> GetBankAccountBalances()
        {
            try
            {
                var accountsBalances = await this._db.BankAccountBalances.ToListAsync();
                return Ok(accountsBalances);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }

        // GET: api/BankAccountBalances/5
        [AcceptVerbs("GET")]
        [Route("BankAccountBalances/{number}/{type}")]
        [ResponseType(typeof(BankAccountBalance))]
        public async Task<IHttpActionResult> GetBankAccountBalance(int number, string type)
        {
            try
            {
                var banksAccountsBalances = await this._db.BankAccountBalances.Where(a => a.BankAccount.Number.Equals(number)).ToListAsync();
                if (banksAccountsBalances == null || !banksAccountsBalances.Any())
                {
                    return NotFound();
                }

                if (type.Equals("currentBalance"))
                {
                    return Ok(this.GetCurrentBalance(banksAccountsBalances));
                }
                else
                {
                    return Ok(banksAccountsBalances);
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST: api/BankAccountBalances
        [AcceptVerbs("POST")]
        [Route("BankAccountBalances/{number}/{value}/{operation}")]
        [ResponseType(typeof(BankAccountBalance))]
        public async Task<IHttpActionResult> PostBankAccountBalance(int number, decimal value, string operation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bankAccount = this._db.BankAccounts.FirstOrDefault(a => a.Number.Equals(number));

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

                this._db.BankAccountBalances.Add(bankAccountBalance);
                var save = await this._db.SaveChangesAsync();

                var typeOperation = operation.ToLower().Equals("sum") ? "Depósito" : "Saque";
                return Ok(string.Format("{0} cadastrado com sucesso na conta bancária {1}.", typeOperation, number));
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

        private bool BankAccountBalanceExists(int number)
        {
            return this._db.BankAccountBalances.Count(e => e.BankAccount.Number == number) > 0;
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