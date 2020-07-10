<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="InventoryApplication.UI_Component.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <div class="jumbotron">
    <h3>Home</h3>

       <div class="list-group">

            <a href="Entries/EntriesNavigation.aspx" class="list-group-item" >Entries</a>
            <a href="Inventory/Inventory.aspx" class="list-group-item">Inventory</a>
            <a href="Reports/Reports.aspx" class="list-group-item">Reports</a>


        </div>
    </div>


</asp:Content>
