<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VendorInformation.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.VendorInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" style="overflow: auto">
        <h3>Vendor Information</h3>
        <asp:GridView runat="server" ID="GV_One"
            CssClass="table table-striped table-bordered table-condensed pagination-ys"
            AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false"
            ShowHeaderWhenEmpty="true"
            ShowFooter="true"
            DataKeyNames="Id"
            OnRowCommand="GV_One_RowCommand"
            OnRowUpdating="GV_One_RowUpdating"
            OnRowDeleting="GV_One_RowDeleting"
            OnRowEditing="GV_One_RowEditing"
            OnRowCancelingEdit="GV_One_RowCancelingEdit"
            OnPageIndexChanging="GV_One_PageIndexChanging"
            OnRowDataBound="GV_One_RowDataBound">
            <Columns>


                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Name" Text='<%# Eval("Name") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_NameFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Email") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Email" Text='<%# Eval("Email") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_EmailFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact Number">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Contact") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Contact" Text='<%# Eval("Contact") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ContactFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Credit") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_Credit" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_CreditFooter" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

               

                 <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/Edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                        <asp:ImageButton ImageUrl="~/Images/Delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/Update.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                        <asp:ImageButton ImageUrl="~/Images/Cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~/Images/Add.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                    </FooterTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>

        <asp:Label runat="server" ID="lbl_message"></asp:Label>
    </div>


</asp:Content>
