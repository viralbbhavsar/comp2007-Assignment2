<%@ Page Title="" Language="C#" MasterPageFile="~/HRManagement.Master" AutoEventWireup="true" CodeBehind="leaveNew.aspx.cs" Inherits="HRManagement.leaveNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Leave Details</h1>
    <h6>All fields are required</h6>

    <div class="form-group">
        <label for="txtFromDate" class="col-sm-2">Leave FromDate:</label>
        <asp:TextBox ID="txtFromDate" runat="server" required="true" TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a valid Date"
            ControlToValidate="txtFromDate" CssClass="label label-danger"
            Type="Date" MinimumValue="1900-01-01" MaximumValue="2999-12-31">
            </asp:RangeValidator>
    </div>

    <div class="form-group">
        <label for="txtToDate" class="col-sm-2">Leave ToDate:</label>
        <asp:TextBox ID="txtToDate" runat="server" required="true" TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Must be a valid Date"
            ControlToValidate="txtToDate" CssClass="label label-danger"
            Type="Date" MinimumValue="1900-01-01" MaximumValue="2999-12-31">
            </asp:RangeValidator>
    </div>

    <div class="form-group">
        <label for="txtMessage" class="col-sm-2">Leave Message:</label>
        <asp:textbox id="txtMessage" runat="server" TextMode="MultiLine" />
    </div>
   
    <div class="form-group">
        <label for="ddlStatus" class="col-sm-2">Leave Status:</label>        
        <asp:DropDownList ID="ddlStatus" runat="server" Enabled ="false">
            <asp:ListItem Text="-Select-"/>
            <asp:ListItem Text="Submitted" Value="Submitted" />
            <asp:ListItem Text="Approved" Value="Approved" />
            <asp:ListItem Text="Declined" Value="Declined" />
        </asp:DropDownList>
    </div>

    <div class="col-sm-offset-2">
        <asp:button id="btnAdd" runat="server" text="Save" cssclass="btn breadcrumb" 
            OnClick="btnAdd_Click" />
    </div>

    
</asp:Content>
