using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

public class PhysicalStockCountDetail_DAL
{
	public PhysicalStockCountDetail_DAL()
	{
	}
    
    public virtual bool CreateModifyPhysicalStockCountDetail(PhysicalStockCountDetail_BAL PSCD_BAL)
    {
        SqlParameter[] param =
                            {
                                new SqlParameter("@PhysicalStockCountID",PSCD_BAL.PhysicalStockCountID),
                                new SqlParameter("@DayID",PSCD_BAL.DayID),
                                new SqlParameter("@Invontory_Id",PSCD_BAL.Invontory_Id),
                                new SqlParameter("@Quantity",PSCD_BAL.Quantity),
                                new SqlParameter("@Rate",PSCD_BAL.Rate),
                                new SqlParameter("@Amount",PSCD_BAL.Amount)
                            };
        int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyPhysicalStockCountDetail", param);
        return i > 0;
    }
    public virtual DataTable getPhysicalStockCountDetailByDayID(int dayID)
    {
        SqlParameter param = new SqlParameter("@DayID", dayID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPhysicalStockCountDetail", param).Tables[0];
        return dt;
    }
    public virtual int DeletePhysicalStockCountDetail(int dayID)
    {
        SqlParameter[] param = { new SqlParameter("@DayID", dayID) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeletePhysicalStockCountDetail", param));
    }

    public virtual string DeletePhysicalStockCountPernmanent(int PhSCId)
    {
        SqlParameter[] param = { new SqlParameter("@PhSCId", PhSCId) };
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeletePhysicalStockCountPernanent", param).ToString(); ;
    }
}
