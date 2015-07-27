using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Add Entity Model References
using HRManagement.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;
//Add Authorization references
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace HRManagement
{
    public partial class leaveNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                //get the leave infromation for editing
                if (!String.IsNullOrEmpty(Request.QueryString["LeaveId"]))
                {
                    ddlStatus.Enabled = true;
                    GetLeave();
                }
            }

        }

        protected void GetLeave()
        {
            //fetch the information to edit
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 LeaveId = Convert.ToInt32(Request.QueryString["LeaveId"]);

                Employee_Leave leave = (from c in db.Employee_Leave
                                      where c.LeaveId == LeaveId
                                      select c).FirstOrDefault();

                //fetch data from db
                txtFromDate.Text = String.Format("{0:yyyy-MM-dd}", leave.FromDate);
                txtToDate.Text = String.Format("{0:yyyy-MM-dd}", leave.ToDate);
                txtMessage.Text = leave.LeaveMessage;
                ddlStatus.SelectedValue = leave.LeaveStatus;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //save or update
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Employee_Leave leave = new Employee_Leave();

                if (!String.IsNullOrEmpty(Request.QueryString["LeaveId"]))
                {
                    Int32 LeaveId = Convert.ToInt32(Request.QueryString["LeaveId"]);

                    leave = (from c in db.Employee_Leave
                              where c.LeaveId == LeaveId
                              select c).FirstOrDefault();
                }

                //fetch infromation from input form 
                leave.FromDate = Convert.ToDateTime(txtFromDate.Text); ;
                leave.ToDate = Convert.ToDateTime(txtToDate.Text);                
                leave.LeaveMessage = txtMessage.Text;
                leave.LeaveStatus = ddlStatus.SelectedValue;

                if (String.IsNullOrEmpty(Request.QueryString["LeaveId"]))
                {
                    leave.LeaveStatus = "Submitted";
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        leave.UserName = HttpContext.Current.User.Identity.GetUserId();
                    }

                    //insert
                    db.Employee_Leave.Add(leave);
                }

                //save and redirect user to diaply page
                db.SaveChanges();
                Response.Redirect("leave.aspx");

            }
        }
    }
}