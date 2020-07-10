<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceView.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Maintenance.MaintenanceView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="overflow: auto">
        <h3>Maintenance View</h3>


        <div class="btn-group">
            <asp:Button runat="server" ID="btn_EditTicket" CssClass="btn btn-primary" Text="Edit" OnClick="btn_EditTicket_Click" />
        </div>

        <div class="btn-group">
            <asp:Button runat="server" ID="btn_Delete" CssClass="btn btn-primary" Text="Delete" OnClick="btn_Delete_Click" />
        </div>


        <asp:Table runat="server" CssClass="table" Width="100%">

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="Description"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Description"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="Cost"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Cost"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="NumberofHours"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_NumberofHours"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="DueDate"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_DueDate"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="CarRegistration"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_CarRegistration"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="DriverName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_DriverName"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
<asp:Label runat="server" Text="MaintenanceCategoryDescription"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_MaintenanceCategoryDescription"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

         <asp:TableRow runat="server" ID="div_Brand" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Brand Name"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                                       <asp:Label runat="server" ID="txt_BrandName"></asp:Label>

                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="div_Serial" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Serial Number"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_SerialNumber"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="div_Quantity" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server"  Text="Selected Quantity"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lbl_QuantityAvailable"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

         <asp:TableRow>
                <asp:TableCell>
                <asp:Label runat="server" Text="Attachment" CssClass="control-label"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                   <asp:PlaceHolder runat="server" ID="Place_Attachment"></asp:PlaceHolder>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>


        <%--<asp:Button runat="server" ID="btn_submit" OnClick="btn_submit_Click" />--%>

        <asp:Label runat="server" ID="lbl_message"></asp:Label>

    </div>


</asp:Content>
