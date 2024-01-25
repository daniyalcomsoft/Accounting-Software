using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

/// <summary>
/// Summary description for InvoiceDetail_DAL
/// </summary>
public class InvoiceDetail_DAL
{
	public InvoiceDetail_DAL()
	{
	}
    
    public virtual DataTable CreateModifyInvoiceDetailForm(InvoiceDetail_BAL InvoiceDetailBAL)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@DetailID", InvoiceDetailBAL.InvoiceDetailID)
                                   ,new SqlParameter("@ProductServiceID",InvoiceDetailBAL.ProductServiceID)
                                   ,new SqlParameter("@Description",InvoiceDetailBAL.Description)
                                   ,new SqlParameter("@Quantity",InvoiceDetailBAL.Quantity)
                                   ,new SqlParameter("@Rate",InvoiceDetailBAL.Rate)
                                   ,new SqlParameter("@Amount",InvoiceDetailBAL.Amount)
                                   ,new SqlParameter("@InvoiceID",InvoiceDetailBAL.InvoiceID)
                               };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyInvoiceDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID(int invoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", invoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable getProformaInvoiceDetailByInvoiceID(int invoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", invoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetProformaInvoiceDetail", param).Tables[0];
        return dt;
    }



    public virtual DataTable getInvoiceDetailByInvoiceID1(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail1", param).Tables[0];
        return dt;
    }



    public virtual DataTable getProformaInvoiceDetailByInvoiceID1(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetProformaInvoiceDetail1", param).Tables[0];
        return dt;
    }
   
    public virtual int DeleteInvoiceDetail(int InvoiceID)
    {
        SqlParameter[] param = { new SqlParameter("@InvoiceID", InvoiceID) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteInvoiceDetail", param));
    }

    // BAL Temp COde

    public virtual DataTable getInvoiceDetailByInvoiceID3(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail3", param).Tables[0];
        return dt;
    }

    public virtual DataTable getProformaInvoiceDetailByInvoiceID3(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetProformaInvoiceDetail3", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID4(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail4", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID4(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetProformaInvoiceDetail4", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID5(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail5", param).Tables[0];
        return dt;
    }

    public virtual DataTable getProformaInvoiceDetailByInvoiceID5(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail5", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID6(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail6", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID6(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail6", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID7(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail7", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID8(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail8", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID9(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail9", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID10(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail10", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID11(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail11", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID14(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail14", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID15(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail15", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID13(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail13", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceDetailByInvoiceID12(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetprformaInvoiceDetail12", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID7(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail7", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID8(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail8", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID9(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail9", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID10(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail10", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID11(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail11", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID12(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail12", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID13(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail13", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID14(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail14", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceDetailByInvoiceID15(int ID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", ID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceDetail15", param).Tables[0];
        return dt;
    }
}
