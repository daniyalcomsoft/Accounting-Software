using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

        public enum AccountNatureBase
        {
        Current_Assets = 1,
        NonCurrent_Assets = 2,
        Expense = 3,
        Current_Liabilities = 4,
        NonCurrent_Liabilities = 5,
        Income = 6,
        Capital = 7,
        CostOfSales=8,
        OtherIncome=9,
        Operating_Expenses=10,
        Financial_Expenses=11,
        Other_Expenses=12,
        Taxation=13
        }

    public class NatureOfAccountBO
    {
        public enum AccountNatureBase { Current_Assets = 1, NonCurrent_Assets, Expense, Current_Liabilities, NonCurrent_Liabilities, Income, Capital };
    }

