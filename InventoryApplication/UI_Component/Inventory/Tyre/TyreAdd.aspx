<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TyreAdd.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.PurchaseTyreAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        $(function () {
            $("[id$=txt_DateOfPurchase]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });

        </script>

    <div class="jumbotron" style="overflow: auto">
        <h3>Purchase Tyre</h3>
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
                    <asp:Label runat="server" Text="Company"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Company"></asp:TextBox>
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
                    <asp:Label runat="server" Text="SerialNumber"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_SerialNumber"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="NumberOfTyres"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_NumberOfTyres"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="BrandName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_BrandName"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="DateOfPurchase"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_DateOfPurchase"></asp:TextBox>
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
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>

        <asp:Button runat="server" ID="btn_Submit" Text="Submit" OnClick="btn_Submit_Click" />

        <asp:Label runat="server" ID="lbl_message"></asp:Label>
    </div>

</asp:Content>
