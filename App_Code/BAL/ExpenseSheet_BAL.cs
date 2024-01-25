using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ExpenseSheet_BAL
/// </summary>
public class ExpenseSheet_BAL : ExpenseSheet_DAL
{
    public int ExpenseID { get; set; }
    public int JobID { get; set; }
    public string Description2 { get; set; }
    public string OtherDetails { get; set; }
    public int CustomerID { get; set; }
    public int FinYearID { get; set; }
    public int LoginID { get; set; }
    public decimal Total { get; set; }

    public DateTime Date { get; set; }
    public int PaymentType { get; set; }
    public bool Clearing { get; set; }
    public int InfoType { get; set; }
    public string PONo { get; set; }
    public string ImpressedAcc { get; set; }
    public string ExpenseAcc { get; set; }
    public string PaymentThrough { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

	public ExpenseSheet_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override int CreateModifyExpenseSheet(ExpenseSheet_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyExpenseSheet(BALInvoice, Trans);
    }

    public override bool CreateModifyExpenseSheetDetail(ExpenseSheet_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyExpenseSheetDetail(BALInvoice, Trans);
    }

    public override bool Delete_ExpenseSheetDetail(int ExpenseSheetID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_ExpenseSheetDetail(ExpenseSheetID, Trans);
    }

    public override bool Delete_ExpenseSheetTrans(int ExpenseSheetID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_ExpenseSheetTrans(ExpenseSheetID, Trans);
    }

    public override DataTable getExpenseSheet(int ExpenseSheetID)
    {
        return base.getExpenseSheet(ExpenseSheetID);
    }

    public override DataTable getExpenseSheetDetail(int ExpenseSheetID)
    {
        return base.getExpenseSheetDetail(ExpenseSheetID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_ExpenseSheetDetail_byID(int ExpenseSheetID)
    {
        return base.Get_Rows_ExpenseSheetDetail_byID(ExpenseSheetID);
    }

    public override DataTable GetJobNo(string JobNo)
    {
        return base.GetJobNo(JobNo);
    }
}
