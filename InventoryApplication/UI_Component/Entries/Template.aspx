<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.Template" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="overflow: auto">
        <div>
            <h3>Template</h3>
        </div>


        <div>
            <asp:Table runat="server" CssClass="table table-bordered">

                <asp:TableRow>
                    <asp:TableCell>
                    <asp:Label runat="server" Text="Template Category"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddl_templatecategory" AutoPostBack="true" OnSelectedIndexChanged="ddl_templatecategory_SelectedIndexChanged">
                            <asp:ListItem Value="10">Bank</asp:ListItem>
                            <asp:ListItem Value="11">Port</asp:ListItem>
                            <asp:ListItem Value="12">Labor/Immigration</asp:ListItem>
                            <asp:ListItem Value="13">Client</asp:ListItem>
                            <asp:ListItem Value="14">Other</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                  <asp:TableRow>
                    <asp:TableCell>
                    <asp:Label runat="server" Text="Template File"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddl_templateFile">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text ="Delete File "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btn_delete" Text="Delete File" OnClick="btn_delete_Click" />
                    </asp:TableCell>
                </asp:TableRow>



                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text ="Donwload File "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btn_download" Text="Download" OnClick="btn_download_Click" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>

                    <asp:TableCell>
                        <asp:Label runat="server" Text="Upload"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                         <asp:FileUpload runat="server" ID="file_upload" AllowMultiple="true" />
                        <asp:Button runat="server" ID="btn_submit" Text="Upload File" OnClick="btn_submit_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <asp:Label runat="server" ID="lbl_message"></asp:Label>
    </div>

</asp:Content>
