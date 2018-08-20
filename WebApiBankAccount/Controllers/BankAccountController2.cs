using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApiBankAccount.Models;

namespace WebApiBankAccount.Controllers
{    
    [RoutePrefix("api")]
    public class BankAccountController2: ApiController
    {
        private static List<BankAccountModel> _banksAccountsList = new List<BankAccountModel>();
        
        [AcceptVerbs("POST")]
        [Route("BankAccount/{cpf}")]
        public string CreateBankAccount(int cpf)
        {
            Random random = new Random();
            int numberAccount = random.Next(0, 9999);

            var bankAccount = new BankAccountModel(numberAccount, cpf.ToString(), "Cliente padrão");
            bankAccount.Creation = DateTime.Now;
            _banksAccountsList.Add(bankAccount);
            // inserir conta no banco de dados e retornar o número da conta criado
            return string.Format("Conta bancária {0} cadastrada com sucesso para Cliente padrão.", numberAccount);
        }

        [AcceptVerbs("POST")]
        [Route("BankAccount/{name}/{cpf}")]
        public string CreateBankAccount(string name, int cpf)
        {
            Random random = new Random();
            int numberAccount = random.Next(0, 9999);

            var bankAccount = new BankAccountModel(numberAccount, cpf.ToString(), name);
            bankAccount.Creation = DateTime.Now;
            _banksAccountsList.Add(bankAccount);
            // inserir conta no banco de dados e retornar o número da conta criado
            return string.Format("Conta bancária {0} cadastrada com sucesso para {1}.", numberAccount, name);
        }

        [AcceptVerbs("POST")]
        [Route("BankAccount/{number}/{value}/{operation}")]
        public string InsertValueToBankAccount(int number, decimal value, string operation)
        {
            var bankAccount = new BankAccountModel(number, value);
            bankAccount.Operation = operation.ToLower();
            bankAccount.Creation = DateTime.Now;
            _banksAccountsList.Add(bankAccount);
            // inserir depósito ou saque de valor na conta
            var typeOperation = operation.ToLower().Equals("sum") ? "Depósito" : "Saque";
            return string.Format("{0} cadastrado com sucesso na conta bancária {1}.", typeOperation, number);
        }

        [AcceptVerbs("GET")]
        [Route("BankAccount/{number}/{type}")]
        public List<BankAccountModel> GetCurrentBalance(int number, string type)
        {
            var collection = _banksAccountsList.Where(a => a.Number.Equals(number) && a.Value > 0).OrderBy(b => b.Number).ThenBy(c => c.Creation);
            
            return collection.ToList();

            //if (type.ToLower().Equals("currentbalance"))
            //    return _banksAccountsList.Where(n => n.Number == number).Select(n => n.Value).FirstOrDefault();
            //else // extract
            //    return _banksAccountsList.Where(n => n.Number == number).Select(n => n.Value).FirstOrDefault();
            // busca todas as operações daquela conta e calcula o saldo atual
            //return currentBalance;
        }

    }
}