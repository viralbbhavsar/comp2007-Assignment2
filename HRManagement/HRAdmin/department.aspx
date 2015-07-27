<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="HRManagement.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h1>Department</h1>

    <a href="departmentNew.aspx">Add Department</a>

    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover sort display" 
        DataKeyNames="DepartmentId" OnRowDeleting="grdDepartment_RowDeleting" AllowPaging="true"
        OnPageIndexChanging="grdDepartment_PageIndexChanging" PageSize="3" AllowSorting="true"
        OnSorting="grdDepartment_Sorting" OnRowDataBound="grdDepartment_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="DepartmentId" HeaderText="Department Id" SortExpression="DepartmentId" />
            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/departmentNew.aspx" 
                DataNavigateUrlFields="DepartmentId" DataNavigateUrlFormatString="departmentNew.aspx?DepartmentId={0}" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

</asp:Content>
