using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JobSheet_BAL
/// </summary>
public class JobSheet_BAL : JobSheet_DAL
{
    public int JobID { get; set; }
    public int JobNo { get; set; }
    public string Description2 { get; set; }
    public string OtherDetails { get; set; }
    public int CustomerID { get; set; }
    public int LoginID { get; set; }
    public decimal Total { get; set; }

    public DateTime Date { get; set; }
    public int PaymentType { get; set; }
    public int InfoType { get; set; }
    public string PONo { get; set; }
    public string ExpenseAcc { get; set; }
    public string PaymentThrough { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

	public JobSheet_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override int CreateModifyJobSheet(JobSheet_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyJobSheet(BALInvoice, Trans);
    }

    public override bool CreateModifyJobSheetDetail(JobSheet_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyJobSheetDetail(BALInvoice, Trans);
    }

    public override bool Delete_JobSheetDetail(int JobSheetID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_JobSheetDetail(JobSheetID, Trans);
    }

    public override bool Delete_JobSheetTrans(int JobSheetID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_JobSheetTrans(JobSheetID, Trans);
    }

    public override int DeleteJobSheet(int JobSheetID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteJobSheet(JobSheetID, Trans);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_JobSheetDetail_byID(int JobSheetID)
    {
        return base.Get_Rows_JobSheetDetail_byID(JobSheetID);
    }

    public override System.Data.DataTable getallJobSheets()
    {
        return base.getallJobSheets();
    }

    public override System.Data.DataTable getCustomerNamebyJobNumber(int JobSheetID)
    {
        return base.getCustomerNamebyJobNumber(JobSheetID);
    }

    public override System.Data.DataTable getjobSheet(int JobSheetID)
    {
        return base.getjobSheet(JobSheetID);
    }

    public override System.Data.DataTable getJobSheetDetail(int JobSheetID)
    {
        return base.getJobSheetDetail(JobSheetID);
    }

    public override System.Data.DataTable GetJobNumber(string JobNo)
    {
        return base.GetJobNumber(JobNo);
    }

}