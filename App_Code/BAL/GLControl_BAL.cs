using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLControl_BAL
/// </summary>
public class GLControl_BAL:GLControl_DAL
{
    public string MainCode { get; set; }
    public string ControlCode { get; set; }
    //public string ControlCodeWhere { get; set; }
    public string Title { get; set; }
    public Int16? IsActive { get; set; }
    public bool Deleteable { get; set; }
	public GLControl_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override int DeleteControlCodeNotInUse(string MainCode, string ControlCode)
    {
        return base.DeleteControlCodeNotInUse(MainCode, ControlCode);
    }
    public override System.Data.DataTable GetAllControlMainAccType(string MainCode)
    {
        return base.GetAllControlMainAccType(MainCode);
    }
    public override GLControl_BAL GetControlAccType(string MainCode, string ControlCode)
    {
        return base.GetControlAccType(MainCode, ControlCode);
    }
    public override int InsertUpdateGLControl(GLControl_BAL ControlBO, SCGL_Session SBO)
    {
        return base.InsertUpdateGLControl(ControlBO, SBO);
    }
    public override bool IsControlCodeExists(string MainCode, string ControlCode)
    {
        return base.IsControlCodeExists(MainCode, ControlCode);
    }
}
