<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="designation.aspx.cs" Inherits="HRManagement.designation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Designation</h1>

    <a href="designationNew.aspx">Add Designation</a>

    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover sort display" 
        DataKeyNames="DesignationId" OnRowDeleting="grdDesignation_RowDeleting" AllowPaging="true"
        OnPageIndexChanging="grdDesignation_PageIndexChanging" PageSize="3" AllowSorting="true"
        OnSorting="grdDesignation_Sorting" OnRowDataBound="grdDesignation_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="DesignationId" HeaderText="Designation Id" SortExpression="DesignationId" />
            <asp:BoundField DataField="DesignationName" HeaderText="Designation Name" SortExpression="DesignationName" />
            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/designationNew.aspx" 
                DataNavigateUrlFields="DesignationId" DataNavigateUrlFormatString="designationNew.aspx?DesignationId={0}" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>


</asp:Content>
