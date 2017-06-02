using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BaseSupperDetailPage : BaseSupperPage
{
    public int CurrentID
    {
        get
        {
            if (this.ViewState["CurrentID"] == null)
            {
                int currentID = 0;
                int.TryParse(Request.QueryString["ID"], out currentID);
                this.ViewState["CurrentID"] = currentID;
            }
            if (this.ViewState["CurrentID"] == null) return 0;
            return (int)this.ViewState["CurrentID"];
        }
        set
        {
            this.ViewState["CurrentID"] = value;
        }
    }
}