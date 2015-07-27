using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Add Entity Model References
using HRManagement.Models;
using System.Linq.Dynamic;

namespace HRManagement
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "DepartmentId";
                GetDepartment();
            }
        }

        protected void GetDepartment()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var dep = (from c in db.Departments
                               select new { c.DepartmentId, c.DepartmentName});

                //append the current direction to the Sort Column
                String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                grdDepartment.DataSource = dep.AsQueryable().OrderBy(Sort).ToList();
                grdDepartment.DataBind();
            }
        }

        protected void grdDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 DepartmentId = Convert.ToInt32(grdDepartment.DataKeys[e.RowIndex].Values["DepartmentId"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Department objC = (from c in db.Departments
                               where c.DepartmentId == DepartmentId
                               select c).FirstOrDefault();

                db.Departments.Remove(objC);
                db.SaveChanges();
            }

            GetDepartment();
        }

        protected void grdDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdDepartment.PageIndex = e.NewPageIndex;
            GetDepartment();
        }

        protected void grdDepartment_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetDepartment();

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

        protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdDepartment.Columns.Count - 1; i++)
                    {
                        if (grdDepartment.Columns[i].SortExpression == Session["SortColumn"].ToString())
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

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdDepartment.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetDepartment();
        }   
      
    }
}