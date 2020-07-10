<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntriesNavigation.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.EntriesNavigation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
    <h3>Admin Control Utility</h3>

        <div class="list-group">

            <a href="PettyCash/PettyCash.aspx" class="list-group-item">Petty Cash</a>
            <a href="../Users/User.aspx" class="list-group-item">User</a>
            <a href="../Users/UserRegistration.aspx" class="list-group-item">User Registration</a>

            <a href="CreditStatus.aspx" class="list-group-item">Credit Status</a>

            <a href="DomainCompany.aspx" class="list-group-item">Domain Company</a>
            <a href="CarEntry.aspx" class="list-group-item">Car Entry</a>
            <a href="ClientInformationEntry.aspx" class="list-group-item">Client Information</a>

            <a href="ClientContractEntry.aspx" class="list-group-item">Client Contract</a>
            <a href="VendorInformation.aspx" class="list-group-item">Vendor Information</a>

            <a href="FuelEntry.aspx" class="list-group-item">FuelEntry</a>
       
            <a href="Template.aspx" class="list-group-item">Template</a>
            <a href="TripOutsource.aspx" class="list-group-item">TripEntry Outsource</a>

            <a href="TripEntry.aspx" class="list-group-item">TripEntry</a>

        </div>
    </div>
</asp:Content>
