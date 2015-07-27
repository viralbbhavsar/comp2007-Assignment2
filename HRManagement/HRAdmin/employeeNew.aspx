<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" 
    CodeBehind="employeeNew.aspx.cs" Inherits="HRManagement.employeeNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Employee Details</h1>
    <h6>All fields are required</h6>

    <div class="form-group-lg">
        <asp:label id="lblStatus" runat="server" cssclass="label label-info" />
    </div>

    <div class="form-group">
        <label for="txtFirstname" class="col-sm-2">Firstname:</label>
        <asp:textbox id="txtFirstname" runat="server" required="true" />
    </div>

    <div class="form-group">
        <label for="txtLastname" class="col-sm-2">Lastname:</label>
        <asp:textbox id="txtLastname" runat="server" required="true" />
    </div>

    <div class="form-group">
        <label for="txtEmail" class="col-sm-2">Email:</label>
        <asp:textbox id="txtEmail" runat="server" required="true" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
         ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  
            ErrorMessage="Please enter valid email"></asp:RegularExpressionValidator>
    </div>
    <div class="form-group">
        <label for="ddlGender" class="col-sm-2">Gender:</label>        
        <asp:DropDownList ID="ddlGender" runat="server" required="true">
            <asp:ListItem Text="-Select-"/>
            <asp:ListItem Text="Male" Value="Male" />
            <asp:ListItem Text="Female" Value="Male" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label for="txtDob" class="col-sm-2">Date of Birth:</label>
        <asp:TextBox ID="txtDob" runat="server" required="true" TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a valid Date"
            ControlToValidate="txtDob" CssClass="label label-danger"
            Type="Date" MinimumValue="1900-01-01" MaximumValue="2999-12-31">
            </asp:RangeValidator>
    </div>

    <div class="form-group">
        <label for="ddlDepartment" class="col-sm-2">Department Name:</label>
        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true"
              DataTextField="DepartmentName" DataValueField="DepartmentId" 
            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" required="true">
        </asp:DropDownList>
    </div>
    <div class="form-group">
       <label for="ddlDesignation" class="col-sm-2">Designation Name:</label>
       <asp:DropDownList ID="ddlDesignation" runat="server" 
                 DataTextField="DesignationName" DataValueField="DesignationId" 
           required="true"></asp:DropDownList>
    </div>

    <div class="form-group">
        <label for="txtUsername" class="col-sm-2" id="lblUsername">Username:</label>
        <asp:textbox id="txtUsername" runat="server" required="true" />
    </div>

    <div class="form-group">
        <label for="txtPassword" class="col-sm-2" id="lblPassword">Password:</label>
        <asp:textbox id="txtPassword" runat="server" textmode="password" required="true"/>
    </div>
    <div class="form-group">
        <label for="txtConfirm" class="col-sm-2" id="lblConfirm">Confirm:</label>
        <asp:textbox id="txtConfirm" runat="server" textmode="password" required="true"></asp:textbox>
        <asp:comparevalidator runat="server" controltovalidate="txtPassword" 
            controltocompare="txtConfirm"
            operator="Equal" errormessage="Passwords must match" cssclass="label label-danger" />
    </div>

    <div class="col-sm-offset-2">
        <asp:button id="btnSave" runat="server" text="Save" cssclass="btn breadcrumb" 
            OnClick="btnSave_Click" />
    </div>


</asp:Content>
