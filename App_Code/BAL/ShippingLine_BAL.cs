using SCGL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;


namespace SCGL.BAL
{

    
    public class ShippingLine_BAL : ShippingLine_DAL
    {
        
        #region Private Properties
        // ======================== Properties ========================
        private int _ShippingLineID;

        private string _ShippingLine;

        private DateTime _CreatedDate;

        //public string _MainCode;
        //public string _ControlCode;
        //public string _SubsidaryCode;
        //public string _Code;
        //public string _Title;

        private bool _IsShippingExist;
        #endregion

        #region Constructor
        public ShippingLine_BAL()
        {
        }
        #endregion

        #region Property Method
        // ======================= Property Method ======================
        public int ShippingLineID
        {
            get
            {
                return this._ShippingLineID;
            }
            set
            {
                this._ShippingLineID = value;
            }
        }

        public string ShippingLine
        {
            get
            {
                return this._ShippingLine;
            }
            set
            {
                this._ShippingLine = value;
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

        //public string MainCode
        //{
        //    get
        //    {
        //        return this._MainCode;
        //    }
        //    set
        //    {
        //        this._MainCode = value;
        //    }
        //}

        //public string ControlCode
        //{
        //    get
        //    {
        //        return this._ControlCode;
        //    }
        //    set
        //    {
        //        this._ControlCode = value;
        //    }
        //}

        //public string SubsidaryCode
        //{
        //    get
        //    {
        //        return this._SubsidaryCode;
        //    }
        //    set
        //    {
        //        this._SubsidaryCode = value;
        //    }
        //}

        //public string Code
        //{
        //    get
        //    {
        //        return this._Code;
        //    }
        //    set
        //    {
        //        this._Code = value;
        //    }
        //}

        //public string Title
        //{
        //    get
        //    {
        //        return this._Title;
        //    }
        //    set
        //    {
        //        this._Title = value;
        //    }
        //}

        public bool IsShippingExist
        {
            get
            {
                return this._IsShippingExist;
            }
            set
            {
                this._IsShippingExist = value;
            }
        }
        #endregion

        #region Public Method
        // =========================== Public Method =========================
        public override int Create(ShippingLine_BAL p, SqlTransaction trans)
        {
            try
            {
                return base.Create(p,trans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ShippingLine_BAL Read(int p)
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

        public override List<ShippingLine_BAL> Read()
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

        public override bool Update(ShippingLine_BAL p)
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

        //public bool Delete(ShippingLine_BAL p)
        //{
        //    SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
        //    if (SBO.Can_Update == true)
        //    {
        //        try
        //        {
        //            return base.Delete(p.ShippingLineID);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        JQ.showStatusMsg((Page)(HttpContext.Current.Handler), "3", "User not Allowed to Update Record");
                
        //    }
        //}
        public bool Delete(ShippingLine_BAL p)
        {
            SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
            if (SBO.Can_Delete == true)
            {
                try
                {
                    return base.Delete(p.ShippingLineID);
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
