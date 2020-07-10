<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TyreView.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Tyre.TyreView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron" style="overflow: auto">
        <h3>Purchase Tyre</h3>


        <div class="btn-group">
            <asp:Button runat="server" ID="btn_EditTicket" CssClass="btn btn-primary" Text="Edit" OnClick="btn_EditTicket_Click" />
        </div>

        <div class="btn-group">
            <asp:Button runat="server" ID="btn_Delete" CssClass="btn btn-primary" Text="Delete" OnClick="btn_Delete_Click" />
        </div>

        <asp:Table runat="server" CssClass="table" Width="100%">

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Payment"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_MOPId">
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
    <asp:TableCell>
        <asp:Label runat="server" Text="Credit Status"></asp:Label>
    </asp:TableCell>
    <asp:TableCell>
        <asp:Label runat="server" ID="txt_CreditStatus">
        </asp:Label>
    </asp:TableCell>
</asp:TableRow>

            <asp:TableRow>
<asp:TableCell>
<asp:Label runat="server" Text="Vendor Name"></asp:Label>
</asp:TableCell>
<asp:TableCell>
<asp:Label runat="server" ID="txt_VendorName">
</asp:Label>
</asp:TableCell>
</asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Domain Company"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_DomainCompany">
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Company"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Company"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="InvoiceNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InvoiceNumber"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

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
                    <asp:Label runat="server" Text="Vat"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Vat"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Amount"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Gross"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Gross"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="SerialNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_SerialNumber"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="NumberOfTyres"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_NumberOfTyres"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="BrandName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_BrandName"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="DateOfPurchase"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_DateOfPurchase"></asp:Label>
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


        <asp:Label runat="server" ID="lbl_message"></asp:Label>

    </div>


</asp:Content>
