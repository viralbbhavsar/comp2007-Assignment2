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
    public partial class designation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "DesignationId";
                GetDesignation();
            }
        }

        protected void GetDesignation()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var design = (from c in db.Designations
                               select new { c.DesignationId, c.DesignationName, c.Department.DepartmentName});

                //append the current direction to the Sort Column
                String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                grdDesignation.DataSource = design.AsQueryable().OrderBy(Sort).ToList();
                grdDesignation.DataBind();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdDesignation.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetDesignation();
        }

        protected void grdDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 DesignationId = Convert.ToInt32(grdDesignation.DataKeys[e.RowIndex].Values["DesignationId"].ToString());

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Designation design = (from c in db.Designations
                               where c.DesignationId == DesignationId
                               select c).FirstOrDefault();

                db.Designations.Remove(design);
                db.SaveChanges();
            }

            GetDesignation();
        }

        protected void grdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdDesignation.PageIndex = e.NewPageIndex;
            GetDesignation();
        }

        protected void grdDesignation_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetDesignation();

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

        protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdDesignation.Columns.Count - 1; i++)
                    {
                        if (grdDesignation.Columns[i].SortExpression == Session["SortColumn"].ToString())
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