<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="departmentNew.aspx.cs" Inherits="HRManagement.departmentAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Department Details</h1>

    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtName" class="col-sm-3">DepartmentName:</label>
        <asp:TextBox ID="txtName" runat="server" required="true" MaxLength="50" />
    </div>
    
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn breadcrumb"
             OnClick="btnSave_Click" />
    </div>
</asp:Content>
