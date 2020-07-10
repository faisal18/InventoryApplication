<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SparePartEdit.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.SparePart.SparePartEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron" style="overflow: auto">
        <h3>Purchase Spare Parts</h3>
        <asp:Table runat="server" CssClass="table" Width="100%">

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Payment"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="txt_MOPId" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Select Mode of Payment</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
        <asp:Label runat="server" Text="Credit Status"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_CreditStaus">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Vendor Name"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_Vendor" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select Vendor</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
     <asp:Label runat="server" Text="Domain Company"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_domainCompany" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Select Company</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="CompanyName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_CompanyName"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="InvoiceNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_InvoiceNumber"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Description"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Description"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Vat"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Vat"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Amount"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Gross"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Gross"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="DateofPurchase"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_DateofPurchase"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Attachment" CssClass="control-label"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:FileUpload runat="server" AllowMultiple="true" ID="file_Attachment"
                        accept=".png,.jpg,.jpeg,.xls,.xlsx,.xml,.txt,.docx,.doc" />
                    <p style="font-size: x-small">The maximum file upload size is 10.00 MB.</p>
                    <asp:PlaceHolder runat="server" ID="Place_Attachment"></asp:PlaceHolder>

                </asp:TableCell>
            </asp:TableRow>


        </asp:Table>
        <asp:Button runat="server" ID="txt_Submit" Text="Submit" OnClick="txt_Submit_Click" />
        <asp:Label runat="server" ID="lbl_message"></asp:Label>
    </div>


</asp:Content>
