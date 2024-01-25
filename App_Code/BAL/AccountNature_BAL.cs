using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccountNature_BAL
/// </summary>
public class AccountNature_BAL : AccountNature_DAL
{
    public enum AccountNatureBase
    {
        Current_Assets = 1,
        Non_Current_Assets = 2,
        Expense = 3,
        Current_Liabilities = 4,
        Non_Current_Liabilities = 5,
        Income = 6,
        Capital = 7,
        Cost_Of_Sales=8,
        Other_Income=9,
        Operating_Expenses=10,
        Financial_Expenses=11,
        Other_Expenses=12,
        Taxation=13

    };
	public AccountNature_BAL()
	{
        
		//
		// TODO: Add constructor logic here
		//
	}

   

}
