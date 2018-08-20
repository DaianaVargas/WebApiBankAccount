using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBankAccount.Models
{
    public class BankAccountModel
    {
        #region Constructor

        public BankAccountModel(int number, string cpf, string customerName)
        {
            this._number = number;
            this._cpf = cpf;
            this._customerName = customerName;
        }

        public BankAccountModel(int number, decimal value)
        {
            this._number = number;
            this._value = value;
        }

        #endregion

        #region Properties

        private int _number;
        public int Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        private string _cpf;
        public string CPF
        {
            get
            {
                return this._cpf;
            }
            set
            {
                this._cpf = value;
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                this._customerName = value;
            }
        }

        private decimal _value;
        public decimal Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        private decimal _currentValue;
        public decimal CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
            }
        }

        private string _operation;
        public string Operation
        {
            get
            {
                return this._operation;
            }
            set
            {
                this._operation = value;
            }
        }

        private DateTime _creation;
        public DateTime Creation
        {
            get
            {
                return this._creation;
            }
            set
            {
                this._creation = value;
            }
        }

        #endregion
    }
}