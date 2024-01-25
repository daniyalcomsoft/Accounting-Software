using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for CustomerForm_DAL
/// </summary>
public class CustomerForm_DAL
{
	public CustomerForm_DAL()
	{
    }
		public virtual DataTable CreateModifyCustForm(CustomerForm_BAL CustBAL, SqlTransaction Trans)
        {
            SqlParameter[] param = {new SqlParameter("@CustId", CustBAL.CustomerID)
                                   ,new SqlParameter("@title",CustBAL.title)
                                   ,new SqlParameter("@fName",CustBAL.fName)
                                   ,new SqlParameter("@lName",CustBAL.lName)
                                   ,new SqlParameter("@Suffix",CustBAL.Suffix)
                                   ,new SqlParameter("@Email",CustBAL.Email)
                                   ,new SqlParameter("@CompanyName",CustBAL.CompanyName)
                                   ,new SqlParameter("@Phone",CustBAL.Phone)
                                   ,new SqlParameter("@Mobile",CustBAL.Mobile)
                                   ,new SqlParameter("@Fax",CustBAL.Fax)
                                   ,new SqlParameter("@DisplayName",CustBAL.DisplayName)
                                   ,new SqlParameter("@displayNameClick",CustBAL.displayNameClick)
                                   ,new SqlParameter("@NTN",CustBAL.NTN)
                                   ,new SqlParameter("@SalesTaxRegNo",CustBAL.SalesTaxRegNo)
                                   ,new SqlParameter("@BankName",CustBAL.BankName)
                                   ,new SqlParameter("@BranchNo",CustBAL.AccNo)
                                   ,new SqlParameter("@IBAN",CustBAL.IBAN)
                                   ,new SqlParameter("@BillAddressStreet",CustBAL.BillAddressStreet)
                                   ,new SqlParameter("@City",CustBAL.City)
                                   ,new SqlParameter("@State",CustBAL.State)
                                   ,new SqlParameter("@Zip",CustBAL.Zip)
                                   ,new SqlParameter("@Country",CustBAL.Country)
                                   ,new SqlParameter("@ShippingAddressCheck",CustBAL.ShippingAddressCheck)
                                   ,new SqlParameter("@ShippingAddressStreet",CustBAL.ShippingAddressStreet)
                                   ,new SqlParameter("@ShippingCity",CustBAL.ShippingCity)
                                   ,new SqlParameter("@ShippingState",CustBAL.ShippingState)
                                   ,new SqlParameter("@ShippingZip",CustBAL.ShippingZip)
                                   ,new SqlParameter("@ShippingCountry",CustBAL.ShippingCountry)
                                   ,new SqlParameter("@Terms",CustBAL.Terms)
                                   ,new SqlParameter("@FacebookId",CustBAL.FacebookId)
                                   ,new SqlParameter("@MessangerId",CustBAL.MessangerId)
                                   ,new SqlParameter("@SkypeId",CustBAL.SkypeId)
                                   ,new SqlParameter("@GooglePlusId",CustBAL.GooglePlusId)
                                   ,new SqlParameter("@OpeningBalance",CustBAL.OpeningBalance)
                                   ,new SqlParameter("@Date",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@PortofDischarge",CustBAL.PortofDischarge)
                                   ,new SqlParameter("@DestCountry",CustBAL.DestCountry)
                                   ,new SqlParameter("@Consignee",CustBAL.Consignee)
                                   ,new SqlParameter("@Buyer",CustBAL.Buyer)
                                  };
            DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_SCGL_SpCreateModifyCutomerForm", param).Tables[0];
            return dt;
        }
        public virtual DataRow GetCustFormByDispName(string DisplayName)
        {
            SqlParameter[] param = {new SqlParameter("@DisplayName", DisplayName)
                                   };
            DataRow row = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetCutFormbyDispName", param).Tables[0].Rows[0];
            return row;
        
        }
        public virtual DataTable GetCustomerData()
        {
            return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetCustomerRecords").Tables[0];
        }

        public virtual DataTable searchCustomerByCustomerName(string CustomerName)
        {
            SqlParameter[] param = {new SqlParameter("@CustomerName", CustomerName)
                         
                              };

            DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchCustomer_BYCustomerName", param).Tables[0];
            return dt;
        }

        public virtual DataTable searchCustomerByMobileNumber(string MobileNumber)
        {
            SqlParameter[] param = {new SqlParameter("@MobileNumber", MobileNumber)
                         
                              };

            DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchCustomer_BYMobile", param).Tables[0];
            return dt;
        }

        public virtual DataTable searchCustomerByEmail(string Email)
        {
            SqlParameter[] param = {new SqlParameter("@Email", Email)
                         
                              };

            DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchCustomer_BYEmail", param).Tables[0];
            return dt;
        }


        public virtual string DeleteCustomer(int CustomerID, SqlTransaction Trans)
        {
            SqlParameter[] param = { new SqlParameter("@CustId", CustomerID) };
                                    ;
                                    return SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteCustomerRecord", param).ToString();
        }
        public virtual CustomerForm_BAL GetCustomerInfo(int CustomerID)
        {
            CustomerForm_BAL Cust = new CustomerForm_BAL();
            SqlParameter[] param = { new SqlParameter("@CustId", CustomerID) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPGetCustomerRecordByID", param))
            {
                if (dr.Read())
                {
                    Cust.CustomerID = Convert.ToInt32(dr["CustomerID"]);

                    Cust.title = dr["title"].ToString();
                    Cust.fName = dr["fName"].ToString();
                    Cust.lName = dr["lName"].ToString();
                    Cust.Suffix = dr["Suffix"].ToString();
                    Cust.Email = dr["Email"].ToString();
                    Cust.CompanyName = dr["CompanyName"].ToString();
                    Cust.Phone = dr["Phone"].ToString();
                    Cust.Mobile = dr["Mobile"].ToString();
                    Cust.Fax = dr["Fax"].ToString();
                    Cust.DisplayName = dr["DisplayName"].ToString();
                    Cust.displayNameClick = Convert.ToBoolean(dr["displayNameClick"]);
                    Cust.NTN = dr["NTN"].ToString();
                    Cust.SalesTaxRegNo = dr["SalesTaxRegNo"].ToString();
                    Cust.BankName = dr["BankName"].ToString();
                    Cust.AccNo = dr["AccNo"].ToString();
                    Cust.IBAN = dr["IBAN"].ToString();
                    Cust.BillAddressStreet = dr["BillAddressStreet"].ToString();
                    Cust.City = dr["City"].ToString();
                    Cust.State = dr["State"].ToString();
                    Cust.Zip = SCGL_Common.Convert_ToInt(dr["Zip"]);
                    Cust.Country = dr["Country"].ToString();
                    Cust.ShippingAddressCheck = Convert.ToBoolean(dr["ShippingAddressCheck"]);
                    Cust.ShippingAddressStreet = dr["ShippingAddressStreet"].ToString();
                    Cust.ShippingCity = dr["ShippingCity"].ToString();
                    Cust.ShippingState = dr["ShippingState"].ToString();
                    Cust.ShippingZip = Convert.ToInt32(dr["ShippingZip"]);
                    Cust.ShippingCountry = dr["ShippingCountry"].ToString();
                    Cust.Terms = dr["Terms"].ToString();
                    Cust.FacebookId = dr["FacebookId"].ToString();
                    Cust.MessangerId = dr["MessangerId"].ToString();
                    Cust.SkypeId = dr["SkypeId"].ToString();
                    Cust.GooglePlusId = dr["GooglePlusId"].ToString();
                    Cust.OpeningBalance =SCGL_Common.Convert_ToDecimal(dr["OpeningBalance"].ToString());
                    Cust.Date = dr["Date"].ToString();
                    Cust.PortofDischarge = dr["PortOfDischarge"].ToString();
                    Cust.DestCountry = dr["DestCountry"].ToString();
                    Cust.Consignee = dr["Consignee"].ToString();
                    Cust.Buyer = dr["Buyer"].ToString();




                }
            }
            return Cust;
        }


        public virtual DataTable getCustomerByID(int CustomerID)
        {
            SqlParameter param = new SqlParameter("@CustomerID", CustomerID);

            DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetCustomerDetail", param).Tables[0];
            return dt;
        }

        public virtual int SpSubsidiaryCodeCount(CustomerForm_BAL cfBal, SqlTransaction Trans)
        {
            SqlParameter[] param3 = {
                                    new SqlParameter("@maincode",cfBal.MainCode),
                                    new SqlParameter("@controlcode",cfBal.ControlCode)
                                   
                               };
            int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SpSubsidiaryCodeCount", param3));
            return i;
        }

        public virtual bool InsertApartSubsidiary(CustomerForm_BAL cfBal, SqlTransaction Trans)
        {
            SqlParameter[] param11 = {
                                    new SqlParameter("@CustomerID", cfBal.CustomerID), 
                                    new SqlParameter("@MainCode",cfBal.MainCode),
                                    new SqlParameter("@ControlCode", cfBal.ControlCode),
                                    new SqlParameter("@SubsidaryCode", cfBal.SubsidaryCode),
                                    new SqlParameter("@Code", cfBal.Code),
                                    new SqlParameter("@Title",cfBal.Title),
                                    new SqlParameter("@CustID", cfBal.CustID)
                                    
                               
                               };
            int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpInsertApartSubsidiary", param11);
            return i > 0;
        }

        public virtual bool UpdateSubsidiary(CustomerForm_BAL cfBal, SqlTransaction Trans)
        {
            SqlParameter[] param11 = {
                                    new SqlParameter("@CustomerID", cfBal.CustomerID), 
                                    new SqlParameter("@DisplayName",cfBal.DisplayName)
                                                        
                               
                               };
            int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpUpdateSubsidiary", param11);
            return i > 0;
        }

        public virtual bool Delete_Apartsubsidary(int CustomerID, SqlTransaction Trans)
        {
            SqlParameter param = new SqlParameter("@CustomerID", CustomerID);
            int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ApartSubsidary", param);
            return i > 0;
        }

        public virtual int CheckExsistingCust(int CustID)
        {
            SqlParameter[] param = {
                                    new SqlParameter("@CustID", CustID)
                                                                      
                               };
            int i = Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "VT_SP_CheckExsistingCust", param));
            return i;
        }

        public virtual string GetCustomerAccount(SqlTransaction Trans)
        {
            return SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPGetCustomerAccount").ToString();
        }

        // Used in Job Form for Shipping Line purposes
        public virtual string GetShippingAccount(SqlTransaction Trans)
        {
            return SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPGetShippingAccount").ToString();
        }

        public virtual bool InsertShippingLineSubsidiary(CustomerForm_BAL cfBal, SqlTransaction Trans)
        {
            SqlParameter[] param = {
                                    new SqlParameter("@ShippingLineID", cfBal.ShippingLineID), 
                                    new SqlParameter("@MainCode",cfBal.MainCode),
                                    new SqlParameter("@ControlCode", cfBal.ControlCode),
                                    new SqlParameter("@SubsidaryCode", cfBal.SubsidaryCode),
                                    new SqlParameter("@Code", cfBal.Code),
                                    new SqlParameter("@Title",cfBal.Title),
                                  
                                    
                               
                               };
            int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpInsertShippingLineSubsidiary ", param);
            return i > 0;
        }

	}

