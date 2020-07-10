﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FuelList.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Fuel.FuelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron">
        <h2>List of Fuel Purchasing</h2>


        <div>
            <asp:Label runat="server" Text="Search"></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txt_searchbox"></asp:TextBox>

            <asp:DropDownList runat="server" ID="ddl_search">

                <asp:ListItem Value="InvoiceNumber">InvoiceNumber</asp:ListItem>

            </asp:DropDownList>
            <asp:Button runat="server" CssClass="btn-primary" Text="Search" ID="btn_search" OnClick="btn_search_Click" />
        </div>

        <div class="list-group">
            <asp:Button runat="server" ID="btn_report" class="list-group-item" OnClick="btn_report_Click" Text="Generate Report"></asp:Button>
        </div>

        <div id="grid" style="overflow: auto">
            <asp:GridView ID="GV_ListTickets" CssClass="table  table-hover table-bordered  pagination-ys" runat="server" CellPadding="10" CellSpacing="5"
                AllowPaging="true" PageSize="10"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                ShowFooter="true"
                DataKeyNames="Id"
                OnPageIndexChanging="GV_ListTickets_PageIndexChanging">

                <Columns>
                    <asp:TemplateField HeaderText="Invoice Number">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hyp_TicketNumber" Text='<%# Eval("InvoiceNumber") %>' NavigateUrl='<%# Eval("Id", "~/UI_Component/Inventory/Fuel/FuelView.aspx?RecordID={0}")%>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DomainCompany">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VendorName">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CreditStatus">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("CreditStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gallons">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("GallonsPurchased") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gross">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Gross") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Purchased">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DateofPurchase") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>


        <asp:Label runat="server" ID="lbl_message"></asp:Label>

    </div>

</asp:Content>
