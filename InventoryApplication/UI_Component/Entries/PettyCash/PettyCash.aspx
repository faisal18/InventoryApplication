<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PettyCash.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.PettyCash.PettyCash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Petty Cash</h3>

        <div class="list-group">

            <a href="PettyCashAdd.aspx" class="list-group-item">PettyCashAdd</a>
            <a href="PettyCashWithDrawl.aspx" class="list-group-item">PettyCashWithDrawl</a>


        </div>
    </div>




</asp:Content>
