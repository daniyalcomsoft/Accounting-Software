using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Fish_BAL
/// </summary>
public class Fish_BAL:Fish_DAL
{
    public int FishID { get; set; }
    public string FishName { get; set; }
    public int FishSizeID { get; set; }
    public string FishSize { get; set; }
    public int FishGradeID { get; set; }
    public string FishGrade { get; set; }
    public int SortOrder { get; set; }
    public int IsEnabled { get; set; }
	public Fish_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataTable GetFishName()
    {
        return base.GetFishName();
    }
    public override int CreateModifyFishName(Fish_BAL BO, SCGL_Session SBO)
    {
        return base.CreateModifyFishName(BO, SBO);
    }
    public override Fish_BAL GetFishByID(int FishID)
    {
        return base.GetFishByID(FishID);
    }
    public override string DeleteFish(int FishID)
    {
        return base.DeleteFish(FishID);
    }
    public override System.Data.DataTable GetFishSize()
    {
        return base.GetFishSize();
    }
    public override int CreateModifyFishSize(Fish_BAL BO, SCGL_Session SBO)
    {
        return base.CreateModifyFishSize(BO, SBO);
    }
    public override Fish_BAL GetFishSizeByID(int FishSizeID)
    {
        return base.GetFishSizeByID(FishSizeID);
    }
    public override string DeleteFishSize(int FishSizeID)
    {
        return base.DeleteFishSize(FishSizeID);
    }
    public override int CreateModifyFishGrade(Fish_BAL BO, SCGL_Session SBO)
    {
        return base.CreateModifyFishGrade(BO, SBO);
    }
    public override string DeleteFishGrade(int FishGradeID)
    {
        return base.DeleteFishGrade(FishGradeID);
    }
    public override System.Data.DataTable GetFishGrade()
    {
        return base.GetFishGrade();
    }
    public override Fish_BAL GetFishGradeByID(int FishGradeID)
    {
        return base.GetFishGradeByID(FishGradeID);
    }

    public override System.Data.DataTable searchFishName(string FishName)
    {
        return base.searchFishName(FishName);
    }
    public override System.Data.DataTable searchFishGrade(string FishGrade)
    {
        return base.searchFishGrade(FishGrade);
    }

    public override System.Data.DataTable searchFishSize(string FishSize)
    {
        return base.searchFishSize(FishSize);
    }

    public override int CheckFishSize(string FishSize, int FishSizeID)
    {
        return base.CheckFishSize(FishSize, FishSizeID);
    }
    public override int CheckFishGrade(string FishGrade)
    {
        return base.CheckFishGrade(FishGrade);
    }

    public override int CheckFishName(string FishName)
    {
        return base.CheckFishName(FishName);
    }
}
