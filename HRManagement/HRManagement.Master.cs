using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference owin security references
using Microsoft.Owin.Security;

namespace HRManagement
{
    public partial class HRManagement : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //determine role
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!HttpContext.Current.User.IsInRole("HRAdmin"))
                {
                    plhEmployee.Visible = true;
                    plhHRAdmin.Visible = false;
                    plhPublic.Visible = false;
                }
                else
                {
                    plhEmployee.Visible = true;
                    plhHRAdmin.Visible = true;
                    plhPublic.Visible = false;
                }
            }
            else
            {
                plhEmployee.Visible = false;
                plhHRAdmin.Visible = false;
                plhPublic.Visible = true;
            }
        }
    }
}