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
    public partial class employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "EmployeeId";
                GetEmployee();
            }
        }

        protected void GetEmployee()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var emp = (from c in db.Employees
                              select new { c.EmployeeId, c.AspNetUser.UserName, c.FirstName, c.LastName, c.Email, c.Gender, c.DateofBirth, c.Department.DepartmentName ,c.Designation.DesignationName});

                //append the current direction to the Sort Column
                String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                grdEmployee.DataSource = emp.AsQueryable().OrderBy(Sort).ToList();
                grdEmployee.DataBind();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdEmployee.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetEmployee();
        }

        protected void grdEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 EmployeeId = Convert.ToInt32(grdEmployee.DataKeys[e.RowIndex].Values["EmployeeId"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Employee emp = (from c in db.Employees
                                      where c.EmployeeId  == EmployeeId
                                      select c).FirstOrDefault();

                db.Employees.Remove(emp);
                db.SaveChanges();
            }

            GetEmployee();
        }

        protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdEmployee.PageIndex = e.NewPageIndex;
            GetEmployee();
        }

        protected void grdEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetEmployee();

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

        protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdEmployee.Columns.Count - 1; i++)
                    {
                        if (grdEmployee.Columns[i].SortExpression == Session["SortColumn"].ToString())
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