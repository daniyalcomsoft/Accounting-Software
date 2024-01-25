//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SCGL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web;


namespace SCGL.BAL
{
    
    
    public class InvoiceDesc : DAL_InvoiceDesc
    {
        
        #region Private Properties
        // ======================== Properties ========================
        private int _InvoiceDescID;
        
        private string _InvoiceDescription;
        
        private DateTime _CreatedDate;

        private bool _IsInvoiceDescExist;

        #endregion
        
        #region Constructor
        public InvoiceDesc()
        {
        }
        #endregion
        
        #region Property Method
        // ======================= Property Method ======================
        public int InvoiceDescID
        {
            get
            {
                return this._InvoiceDescID;
            }
            set
            {
                this._InvoiceDescID = value;
            }
        }
        
        public string InvoiceDescription
        {
            get
            {
                return this._InvoiceDescription;
            }
            set
            {
                this._InvoiceDescription = value;
            }
        }
        
        public DateTime CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                this._CreatedDate = value;
            }
        }

        public bool IsInvoiceDescExist
        {
            get
            {
                return this._IsInvoiceDescExist;
            }
            set
            {
                this._IsInvoiceDescExist = value;
            }
        }
        #endregion
        
        #region Public Method
        // =========================== Public Method =========================
        public override int Create(InvoiceDesc p)
        {
            try
            {
                return base.Create(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override InvoiceDesc Read(int p)
        {
            try
            {
                return base.Read(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override DataTable ReadDataTable()
        {
            try
            {
                return base.ReadDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override List<InvoiceDesc> Read()
        {
            try
            {
                return base.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public override bool Update(InvoiceDesc p)
        {
            SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
            if (SBO.Can_Update == true)
            {
                try
                {
                    return base.Update(p);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                JQ.showStatusMsg((Page)(HttpContext.Current.Handler), "3", "User not Allowed to Update Record");
                return false;
            }
        }
        
        public override bool Delete(int p)
        {
            try
            {
                return base.Delete(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(InvoiceDesc p)
        {
             SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
             if (SBO.Can_Delete == true)
             {
                 try
                 {
                     return base.Delete(p.InvoiceDescID);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
             }
             else
             {
                 JQ.showStatusMsg((Page)(HttpContext.Current.Handler), "3", "User not Allowed to Delete Record");
                 return false;
             }
        }
        #endregion
    }
}
