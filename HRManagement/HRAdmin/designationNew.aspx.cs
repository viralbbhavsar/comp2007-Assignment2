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
    public partial class designationNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartment();

                ListItem newItem = new ListItem("-Select-", "0");
                ddlDepartment.Items.Insert(0, newItem);
                
                //get the designation for editing
                if (!String.IsNullOrEmpty(Request.QueryString["DesignationId"]))
                {
                    GetDesignation();
                }
            }
        }

        protected void GetDesignation()
        {
            //fetch designation information to edit
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 DesignationId = Convert.ToInt32(Request.QueryString["DesignationId"]);

                Designation design = (from c in db.Designations
                               where c.DesignationId == DesignationId
                               select c).FirstOrDefault();

                //populate the form
                txtName.Text = design.DesignationName;                
                ddlDepartment.SelectedValue = design.DepartmentId.ToString();
            }
        }

        protected void GetDepartment()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var deps = (from d in db.Departments
                            orderby d.DepartmentName
                            select d);

                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save or update data
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Designation design = new Designation();

                if (!String.IsNullOrEmpty(Request.QueryString["DesignationId"]))
                {
                    Int32 DesignationId = Convert.ToInt32(Request.QueryString["DesignationId"]);
                    design = (from c in db.Designations
                            where c.DesignationId == DesignationId
                            select c).FirstOrDefault();
                }

                //fetch the information to edit
                design.DesignationName = txtName.Text;                
                design.DepartmentId= Convert.ToInt32(ddlDepartment.SelectedValue);

                if (String.IsNullOrEmpty(Request.QueryString["DesignationId"]))
                {
                    //insert
                    db.Designations.Add(design);
                }

                //save and redirect user to display page
                db.SaveChanges();
                Response.Redirect("designation.aspx");
            }
        }
    }
}