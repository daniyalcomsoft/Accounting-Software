using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLSubsidiary_BAL
/// </summary>
public class GLSubsidiary_BAL:GLSubsidiary_DAL
{
    public string MainCode { get; set; }
    public string ControlCode { get; set; }
    public string SubsidaryCode { get; set; }
    public string Title { get; set; }
    public Int16? IsActive { get; set; }
    public bool Deleteable { get; set; }
	public GLSubsidiary_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int DeleteSubsidaryCodeNotInUse(string MainCode, string ControlCode, string SubsidaryCode)
    {
        return base.DeleteSubsidaryCodeNotInUse(MainCode, ControlCode, SubsidaryCode);
    }

    public override System.Data.DataTable GetAllSubsidiaryByMainNControl(string MainCode, string ControlCode)
    {
        return base.GetAllSubsidiaryByMainNControl(MainCode, ControlCode);
    }
    public override System.Data.DataTable GetSubCodeTitleLike(string Match, int YearID, string YearFrom, string YearTo)
    {
        return base.GetSubCodeTitleLike(Match, YearID, YearFrom, YearTo);
    }
    public override System.Data.DataTable GetSubCodeTitleLike2(string Match, int YearID, string YearFrom, string YearTo)
    {
        return base.GetSubCodeTitleLike2(Match, YearID, YearFrom, YearTo);
    }
    public override GLSubsidiary_BAL GetSubsidiaryAccType(string MainCode, string ControlCode, string SubCode)
    {
        return base.GetSubsidiaryAccType(MainCode, ControlCode, SubCode);
    }
    public override System.Data.DataTable GetSubTitle(string Match)
    {
        return base.GetSubTitle(Match);
    }
    public override void InsertUpdateSubsidiary(GLSubsidiary_BAL SubBO, SCGL_Session SBO)
    {
        base.InsertUpdateSubsidiary(SubBO, SBO);
    }
    public override bool IsExistsSubsidiaryAccount(string MainCode, string ControlCode, string SubsidiaryCode)
    {
        return base.IsExistsSubsidiaryAccount(MainCode, ControlCode, SubsidiaryCode);
    }
}
