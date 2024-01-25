using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SW.SW_Common;

/// <summary>
/// Summary description for InvoiceStatus_DAL
/// </summary>
public class InvoiceStatus_DAL
{
	public InvoiceStatus_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual DataTable getallInvoice()
    {
        //SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID),
        //                        new SqlParameter("@FinYearID", FinYearID) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallInvoiceStatus").Tables[0];
        return dt;
    }

    public virtual bool Update(InvoiceStatus_BAL p)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                        new SqlParameter("InvoiceID", p.InvoiceID),
                        new SqlParameter("ChequeNo", p.ChequeNo)};
            int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "VT_SP_InvoiceStatus_Update", SqlParam);
            return i >= 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public virtual bool UpdateDetails(InvoiceStatus_BAL InvStatus)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@InvoiceID", InvStatus.InvoiceID),
                             new SqlParameter("@CheqNo", InvStatus.CheqNo),
                             new SqlParameter("@Status", InvStatus.Status),
                             new SqlParameter("@RecDate", InvStatus.RecDate)
                              };
        int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "VT_SP_InvoiceStatus_Update", param);
        return i > 0;
    }
}

