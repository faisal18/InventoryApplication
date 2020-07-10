<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsuranceView.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Insurance.InsuranceView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron" style="overflow: auto">
        <h3>Insurance View</h3>


        <div class="btn-group">
            <asp:Button runat="server" ID="btn_EditTicket" CssClass="btn btn-primary" Text="Edit" OnClick="btn_EditTicket_Click" />
        </div>

        <div class="btn-group">
            <asp:Button runat="server" ID="btn_Delete" CssClass="btn btn-primary" Text="Delete" OnClick="btn_Delete_Click" />
        </div>

        <asp:Table runat="server" CssClass="table" Width="100%">


            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="MOPId"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_MOPId" >
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
                    <asp:Label runat="server" Text="Vendor Name"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_VendorName">
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
                    <asp:Label runat="server" Text="InsuranceTypeId"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InsuranceTypeId" >
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Insurer"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_Insurer"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="PolicyNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_PolicyNumber"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="InsuranceCompany"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InsuranceCompany"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="InsuranceStartDate"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InsuranceStartDate"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="InsuranceExpiryDate"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InsuranceExpiryDate"></asp:Label>
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
                    <asp:Label runat="server" Text="InvoiceNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="txt_InvoiceNumber"></asp:Label>
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
