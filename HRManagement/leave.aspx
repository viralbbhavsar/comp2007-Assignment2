<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="leave.aspx.cs" Inherits="HRManagement.leave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Employee Leave</h1>

    <a href="leaveNew.aspx">Add Leave</a>
    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdLeave" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover sort display" 
        DataKeyNames="LeaveId" OnRowDeleting="grdLeave_RowDeleting" AllowPaging="true"
        OnPageIndexChanging="grdLeave_PageIndexChanging" PageSize="3" AllowSorting="true"
        OnSorting="grdLeave_Sorting" OnRowDataBound="grdLeave_RowDataBound" >
        <Columns>            
            <asp:BoundField DataField="LeaveId" HeaderText="Leave Id" SortExpression="LeaveId" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="FromDate" DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="ToDate" HeaderText="To Name" SortExpression="ToDate" DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="LeaveMessage" HeaderText="Leave Message" SortExpression="LeaveMessage" />
            <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" SortExpression="LeaveStatus" />            
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/leaveNew.aspx" 
                DataNavigateUrlFields="LeaveId" DataNavigateUrlFormatString="leaveNew.aspx?LeaveId={0}" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>


</asp:Content>
