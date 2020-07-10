<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceEdit.aspx.cs" Inherits="InventoryApplication.UI_Component.Inventory.Maintenance.MaintenanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron" style="overflow: auto">
        <h3>Maintenance Edit</h3>
        <asp:Table runat="server" CssClass="table" Width="100%">
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
                    <asp:Label runat="server" Text="Cost"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Cost"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="NumberofHours"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_NumberofHours"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="DueDate"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_DueDate"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="CarRegistration"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_CarRegistration" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Select Car</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="DriverName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_DriverName" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Select Driver</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="MaintenanceCategoryDescription"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_MaintenanceCategoryDescription"  AutoPostBack="true" OnSelectedIndexChanged="ddl_MaintenanceCategoryDescription_SelectedIndexChanged" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Select Category</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="div_Brand" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Brand Name"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_BrandName" AppendDataBoundItems="false" AutoPostBack="true"  OnSelectedIndexChanged="ddl_BrandName_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Brand</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="div_Serial" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server" Text="Serial Number"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_SerialNumber" AppendDataBoundItems="false" AutoPostBack="true"  OnSelectedIndexChanged="ddl_SerialNumber_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Serial Number</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="div_Quantity" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server"  Text="Available Quantity"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lbl_QuantityAvailable"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

              <asp:TableRow runat="server" ID="div_SelectedQuanity" Visible="false">
                <asp:TableCell>
                    <asp:Label runat="server"  Text="Tyre Quantity"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_SelectedQuantity"></asp:TextBox>
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


        <asp:Button runat="server" ID="btn_Submit" Text="Submit" OnClick="btn_Submit_Click" />

        <asp:Label runat="server" ID="lbl_message"></asp:Label>
    </div>

</asp:Content>
