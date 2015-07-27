<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="designationNew.aspx.cs" Inherits="HRManagement.designationNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Designation Details</h1>

    <h5>All fields are required</h5>

    <div class="form-group">
            <label for="ddlDepartment" class="col-sm-2">Department Name:</label>
            <asp:DropDownList ID="ddlDepartment" runat="server" 
                 DataTextField="DepartmentName" DataValueField="DepartmentId"></asp:DropDownList>
    </div>

    <div class="form-group">
        <label for="txtName" class="col-sm-2">Designation Name:</label>
        <asp:TextBox ID="txtName" runat="server" required="true" MaxLength="50" />
    </div>
    
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn breadcrumb"
             OnClick="btnSave_Click"/>
    </div>

</asp:Content>
