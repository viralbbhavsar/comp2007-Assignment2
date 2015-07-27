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
    public partial class departmentAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                //get the department for editing
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentId"]))
                {
                    GetDepartment();
                }
            }
        }

        protected void GetDepartment()
        {
            //fetch department information to edit
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 DepartmentId = Convert.ToInt32(Request.QueryString["DepartmentId"]);

                Department dept = (from c in db.Departments
                               where c.DepartmentId == DepartmentId
                               select c).FirstOrDefault();

                //fills the textbox
                txtName.Text = dept.DepartmentName;                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save or update data
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Department dept = new Department();

                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentId"]))
                {
                    Int32 DepartmentId = Convert.ToInt32(Request.QueryString["DepartmentId"]);
                    dept = (from c in db.Departments
                            where c.DepartmentId == DepartmentId
                            select c).FirstOrDefault();
                }

                //fetch the information to edit
                dept.DepartmentName = txtName.Text;
                
                if (String.IsNullOrEmpty(Request.QueryString["DepartmentId"]))
                {
                    //insert
                    db.Departments.Add(dept);
                }

                //save and redirect user to display page
                db.SaveChanges();
                Response.Redirect("department.aspx");
            }
        }
    }
}