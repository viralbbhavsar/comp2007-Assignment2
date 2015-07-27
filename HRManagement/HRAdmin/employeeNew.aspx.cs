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
    public partial class employeeNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartment();                              

                //get the employee information for editing
                if (!String.IsNullOrEmpty(Request.QueryString["EmployeeId"]))
                {                  
                    GetEmployee();

                    txtUsername.Visible = false;
                    txtPassword.Visible = false;
                    txtConfirm.Visible = false;
                }
            }
        }

        protected void GetEmployee()
        {
            //fetch the employee information to edit
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Int32 EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"]);

                Employee emp = (from c in db.Employees
                                      where c.EmployeeId == EmployeeId
                                      select c).FirstOrDefault();

                //fetch data from db to fill out textbox               
                txtFirstname.Text = emp.FirstName;
                txtLastname.Text = emp.LastName;
                txtEmail.Text = emp.Email;
                ddlGender.SelectedValue = emp.Gender;
                txtDob.Text = String.Format("{0:yyyy-MM-dd}", emp.DateofBirth);
                ddlDepartment.SelectedValue = emp.DepartmentId.ToString();
                GetDesignation();
                ddlDesignation.SelectedValue = emp.DesignationId.ToString();



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

                ListItem newItem = new ListItem("-Select-", "0");
                ddlDepartment.Items.Insert(0, newItem);

            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save or update data
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                Employee emp = new Employee();

                if (!String.IsNullOrEmpty(Request.QueryString["EmployeeId"]))
                {
                    Int32 EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"]);

                    emp = (from c in db.Employees
                              where c.EmployeeId == EmployeeId
                              select c).FirstOrDefault();
                }

                //fetch data from input form to fill out textbox 
                emp.FirstName = txtFirstname.Text;
                emp.LastName = txtLastname.Text;
                emp.Email = txtEmail.Text;
                emp.Gender = ddlGender.SelectedValue;
                emp.DateofBirth = Convert.ToDateTime(txtDob.Text);
                emp.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                emp.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

                if (String.IsNullOrEmpty(Request.QueryString["EmployeeId"]))
                {
                    var userStore = new UserStore<IdentityUser>();
                    var manager = new UserManager<IdentityUser>(userStore);

                    var user = new IdentityUser() { UserName = txtUsername.Text };
                    IdentityResult result = manager.Create(user, txtPassword.Text);                    

                    if (result.Succeeded)
                    {

                        emp.UserName = user.Id;
                    }
                    else
                    {
                        lblStatus.Text = result.Errors.FirstOrDefault();
                        lblStatus.CssClass = "label label-danger";
                    }

                    //insert
                    db.Employees.Add(emp);
                }

                //save and redirect user to display page
                db.SaveChanges();
                Response.Redirect("employee.aspx");
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch the designation dropdown
            GetDesignation();
        }

        protected void GetDesignation()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //save the selected DepartmentID
                Int32 DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

                var design = from c in db.Designations
                             where c.DepartmentId == DepartmentId
                             orderby c.DesignationName
                             select c;

                //bind to the designation dropdown
                ddlDesignation.DataSource = design.ToList();
                ddlDesignation.DataBind();

                ListItem newItem = new ListItem("-Select-", "0");
                ddlDesignation.Items.Insert(0, newItem);
            }
        }

    }
}