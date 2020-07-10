<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="InventoryApplication.UI_Component.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
    <h3>Reports</h3>

       <div class="list-group">

            <a href="Sales/SalesReport.aspx" class="list-group-item" >Sales Report</a>
            <a href="Purchase/PurchaseReport.aspx" class="list-group-item">Purchase Report</a>
            <a href="Salary/DriverSalary.aspx" class="list-group-item">Driver Salary Report</a>
            <a href="Salary/StaffSalaryReport.aspx" class="list-group-item">Staff Salary Report</a>


        </div>
    </div>


</asp:Content>
