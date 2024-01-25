using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

public class PhysicalStockCount_DAL
{
	public PhysicalStockCount_DAL()
	{
    }

    //////public virtual int CreateModifyPhysicalStockCount(PhysicalStockCount_BAL PSC_BAL)
    //////{
    //////    SqlParameter[] param =
    //////                        {
    //////                            new SqlParameter("@DayID",PSC_BAL.DayID),
    //////                            new SqlParameter("@date",PSC_BAL.date)                                
    //////                        };
    //////    return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyPhysicalStockCount", param));
    //////}
    public virtual DataTable getPhysicalStockCountByID(int dayID)
    {
        SqlParameter param = new SqlParameter("@DayID", dayID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPhysicalStockCount", param).Tables[0];
        return dt;
    }

    public virtual DataTable getValuationByDate(int FinYearID,int CostCenterID,string Date)
    {
        SqlParameter[] param = {new SqlParameter("@FinYearID", FinYearID)
                               ,new SqlParameter("@CostCenterID", CostCenterID)
                               ,new SqlParameter("@Date", Date)
        
        };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalEvaluation_ByDate2", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetAlreadyPresentPhysicalStock(int FinYearID, int CostCenterID, string Date)
    {
        SqlParameter[] param = {new SqlParameter("@FinYearID", FinYearID)
                               ,new SqlParameter("@CostCenterID", CostCenterID)
                               ,new SqlParameter("@Date", Date)
        
        };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SP_GetAlreadyPresentPhysicalStock_ByDate", param).Tables[0];
        return dt;
    }


    public virtual int CheckValuationByDate(DateTime Date, int CostCenterID)
    {
        SqlParameter[] param = { new SqlParameter("@Date", Date)
                                ,new SqlParameter("@CostCenterID", CostCenterID)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CheckTotalEvaluation_ByDate", param));
    }

    public virtual SqlDataReader getValuationByDate1(int FinYearID, int CostCenterID, string Date)
    {
        SqlParameter[] param = {new SqlParameter("@FinYearID", FinYearID)
                               ,new SqlParameter("@CostCenterID", CostCenterID)
                               ,new SqlParameter("@Date", Date)
        
        };
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "GetTotalEvaluation_ByDate2", param);
    }

    public virtual DataTable getPhysicalStockCountByDate(string date)
    {
        SqlParameter param = new SqlParameter("@date", date);

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPhysicalStockCountByDate", param).Tables[0];
    }
    //public virtual int DeletePhysicalStockCountandExcess(DateTime Date,int CostCenterID)
    //{
    //    SqlParameter[] param = { new SqlParameter("@Date", Date)
    //                           ,new SqlParameter("@CostCenterID", CostCenterID)};
    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeletePhysicalStockCount", param));
    //}

    public virtual int DeletePhysicalStockCountandExcess(PhysicalStockCount_BAL PSC_BAL, SqlTransaction Trans)
    {
        SqlParameter[] param = { new SqlParameter("@Date", PSC_BAL.date)
                               ,new SqlParameter("@CostCenterID", PSC_BAL.CostCenterID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeletePhysicalStockCount", param));
    }

    //public virtual int DeleteExcessShortfromGL(DateTime Date,int CostCenterID)
    //{
    //    SqlParameter[] param = { new SqlParameter("@Date", Date)
    //                           ,new SqlParameter("@CostCenterID", CostCenterID)};
    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteExcessShortfromGL", param));
    //}


    public virtual int DeleteExcessShortfromGL(PhysicalStockCount_BAL PSC_BAL, SqlTransaction Trans)
    {
        SqlParameter[] param = { new SqlParameter("@Date", PSC_BAL.date)
                               //,new SqlParameter("@CostCenterID", PSC_BAL.CostCenterID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteExcessShortfromGL", param));
    }

    public virtual bool CreateModifyPhysicalStock(PhysicalStockCount_BAL PSC_BAL, SqlTransaction Trans)
    {
        SqlParameter[] param =
                            {
                                new SqlParameter("@Inventory_Id",PSC_BAL.Inventory_Id),
                                new SqlParameter("@OnHand",PSC_BAL.OnHand),
                                new SqlParameter("@PhysicalStock",PSC_BAL.PhysicalStockCount),
                                new SqlParameter("@Difference",PSC_BAL.Difference),
                                new SqlParameter("@Date",PSC_BAL.date),
                                new SqlParameter("@CostCenterID",PSC_BAL.CostCenterID),
                                new SqlParameter("@FinYearID",PSC_BAL.FinYearID)
                                
                            };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreatePhysicalStockDetail", param);
        return i > 0;
        //return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_sp_CreatePhysicalStockDetail", param));
    }

    public virtual int CreateModifyExcessShort(PhysicalStockCount_BAL PSC_BAL, SqlTransaction Trans)
    {
        SqlParameter[] param =
                            {   new SqlParameter("@ExcessShortID",PSC_BAL.ExcessShortID),
                                new SqlParameter("@CostCenterID",PSC_BAL.CostCenterID),
                                new SqlParameter("@Date",PSC_BAL.date),
                                new SqlParameter("@Inventory_Id",PSC_BAL.Inventory_Id),
                                new SqlParameter("@Difference",PSC_BAL.Difference),
                                new SqlParameter("@FinYearID",PSC_BAL.FinYearID)
                                
                            };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_sp_CreateExcessShortDetail", param));
    }

    public virtual int CountTotalPurchasesandSalesInvoices(DateTime Date)
    {
        SqlParameter[] param = { new SqlParameter("@Date", Date)
                                };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCountTotalPurchasesandSalesInvoices", param));
    }

    public virtual DataTable GetLastExcessShortID()
    {

        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_sp_GetLastExcessShortID").Tables[0];
        return dt;
    }


    public virtual DataTable GetUpdateExcessShortID(DateTime Date, int CostCenterID)
    {
        SqlParameter[] param = {new SqlParameter("@date", Date)
                               ,new SqlParameter("@CostCenterID",CostCenterID)};

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_GetUpdateExcessShortID", param).Tables[0];
    }
}
