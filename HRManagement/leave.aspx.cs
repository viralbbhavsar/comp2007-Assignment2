using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Add Entity Model References
using HRManagement.Models;
using System.Linq.Dynamic;

//Add Authorization references
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace HRManagement
{
    public partial class leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "LeaveId";
                GetLeave();
            }
        }

        protected void GetLeave()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Determined the Employee or HRAdmin
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.IsInRole("HRAdmin"))
                    {
                        var leave = (from c in db.Employee_Leave
                                     select new { c.LeaveId, c.AspNetUser.UserName, c.FromDate, c.ToDate, c.LeaveMessage, c.LeaveStatus });

                        //append the current direction to the Sort Column
                        String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                        grdLeave.DataSource = leave.AsQueryable().OrderBy(Sort).ToList();
                        grdLeave.DataBind();


                    }
                    else
                    {
                        String  UserName = HttpContext.Current.User.Identity.GetUserId();

                        var leave = (from c in db.Employee_Leave
                                     where c.UserName == UserName
                                     select new { c.LeaveId, c.AspNetUser.UserName, c.FromDate, c.ToDate, c.LeaveMessage, c.LeaveStatus });

                        //append the current direction to the Sort Column
                        String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                        grdLeave.DataSource = leave.AsQueryable().OrderBy(Sort).ToList();
                        grdLeave.DataBind();

                    }
                }

                
                
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdLeave.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetLeave();
        }

        protected void grdLeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 LeaveId = Convert.ToInt32(grdLeave.DataKeys[e.RowIndex].Values["LeaveId"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Employee_Leave leave = (from c in db.Employee_Leave
                                      where c.LeaveId == LeaveId
                                      select c).FirstOrDefault();

                db.Employee_Leave.Remove(leave);
                db.SaveChanges();
            }

            GetLeave();
        }

        protected void grdLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdLeave.PageIndex = e.NewPageIndex;
            GetLeave();
        }

        protected void grdLeave_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetLeave();

            //change the direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdLeave.Columns.Count - 1; i++)
                    {
                        if (grdLeave.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "/images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "/images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }

            }
        }
    }
}