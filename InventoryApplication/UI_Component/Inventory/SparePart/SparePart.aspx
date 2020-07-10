<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SparePart.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.SparePart.SparePart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="jumbotron">
        <h3>Inventory SparePart</h3>

        <div class="btn-group">
            <asp:Button runat="server" Text="Add Record" ID="btn_Add" CssClass="btn btn-primary" OnClick="btn_Add_Click" />
        </div>

        <div class="btn-group">
            <asp:Button runat="server" Text="List all Record" ID="btn_List" CssClass="btn btn-primary" OnClick="btn_List_Click" />
        </div>

        <asp:Label runat="server" ID="lbl_message"></asp:Label>


    </div>

</asp:Content>
