<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Inventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="jumbotron">
    <h3>Inventory Menu</h3>

       <div class="list-group">

            <a href="Fuel/Fuel.aspx" class="list-group-item" >Fuel</a>
            <a href="Insurance/Insurance.aspx" class="list-group-item">Insurance</a>
            <a href="SparePart/SparePart.aspx" class="list-group-item">SparePart</a>
            <a href="Tyre/Tyre.aspx" class="list-group-item">Tyre</a>
           <a href="Maintenance/Maintenance.aspx"class="list-group-item">Maintenance</a>


        </div>
    </div>


</asp:Content>
