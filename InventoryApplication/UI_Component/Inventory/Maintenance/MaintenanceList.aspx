<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceList.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Maintenance.MaintenanceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>List of Maintenance</h2>


        <div>
            <asp:Label runat="server"  Text="Search"></asp:Label>
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
            <asp:GridView ID="GV_One" CssClass="table  table-hover table-bordered  pagination-ys" runat="server" CellPadding="10" CellSpacing="5"
                AllowPaging="true" PageSize="10"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                ShowFooter="true"
                DataKeyNames="Id"
                OnPageIndexChanging="GV_One_PageIndexChanging">

                <Columns>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hyp_TicketNumber" Text='<%# Eval("Description") %>' NavigateUrl='<%# Eval("Id", "~/UI_Component/Inventory/Maintenance/MaintenanceView.aspx?RecordID={0}")%>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Cost") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="NumberofHours">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("NumberofHours") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="CarRegistration">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("CarRegistration") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="DriverName">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DriverName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="MaintenanceCategoryDescription">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("MaintenanceCategoryDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>


        <asp:Label runat="server" ID="lbl_message"></asp:Label>

    </div>

</asp:Content>
