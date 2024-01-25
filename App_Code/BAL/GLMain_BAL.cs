using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLMain_BAL
/// </summary>
public class GLMain_BAL:GLMain_DAL
{
    public string MainCode { get; set; }
    //public string MainCodeWhere { get; set; }
    public string Title { get; set; }
    public int Nature { get; set; }
    public Int16 IsActive { get; set; }
    public bool UnDeleteable { get; set; }
    public bool ActiveChild { get; set; }
	public GLMain_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataTable DeleteMainCodeNotInUse(string MainCode)
    {
        return base.DeleteMainCodeNotInUse(MainCode);
    }
    public override System.Data.DataTable GetAllMainAccType()
    {
        return base.GetAllMainAccType();
    }
    public override GLMain_BAL GetMainAccType(short MainCode)
    {
        return base.GetMainAccType(MainCode);
    }
    public override int InsertUpdateGLMain(GLMain_BAL MainBO, SCGL_Session SBO)
    {
        return base.InsertUpdateGLMain(MainBO, SBO);
    }
    public override bool IsMainCodeExists(string MainCode)
    {
        return base.IsMainCodeExists(MainCode);
    }
    
}
