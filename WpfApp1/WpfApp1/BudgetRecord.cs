using System;

namespace WpfApp1
{
    [Serializable]
    public class BudgetRecord
    {
        private string _name;
        private RecordType _type;
        private decimal _amount;
        private bool _isDeduction;
        private DateTime _date;

        public BudgetRecord(string name, RecordType type, decimal amount, bool isDeduction, DateTime date)
        {
            _name = name;
            _type = type;
            _amount = amount;
            _isDeduction = isDeduction;
            _date = date;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public RecordType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value < 0)
                {
                    _isDeduction = true;
                    _amount = Math.Abs(value);
                }
                else
                {
                    _isDeduction = false;
                    _amount = value;
                }
            }
        }

        public bool IsDeduction
        {
            get
            {
                return _isDeduction;
            }
            set
            {
                _isDeduction = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
    }

    public enum RecordType
    {
        Income,
        Expense
    }
}