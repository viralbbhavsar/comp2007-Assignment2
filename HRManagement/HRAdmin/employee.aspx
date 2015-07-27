<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="employee.aspx.cs" Inherits="HRManagement.employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Employee</h1>

    <a href="employeeNew.aspx">Add Employee</a>

    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdEmployee" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover sort display" 
        DataKeyNames="EmployeeId" OnRowDeleting="grdEmployee_RowDeleting" AllowPaging="true"
        OnPageIndexChanging="grdEmployee_PageIndexChanging" PageSize="3" AllowSorting="true"
        OnSorting="grdEmployee_Sorting" OnRowDataBound="grdEmployee_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId Id" SortExpression="EmployeeId" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="DateofBirth" HeaderText="Date of Birth" SortExpression="DateofBirth" DataFormatString="{0:yyyy-MM-dd}" />            
            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
            <asp:BoundField DataField="DesignationName" HeaderText="Designation Name" SortExpression="DesignationName" />
            
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/employeeNew.aspx" 
                DataNavigateUrlFields="EmployeeId" DataNavigateUrlFormatString="employeeNew.aspx?EmployeeId={0}" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
