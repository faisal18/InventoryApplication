<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserRegistration.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="overflow: auto">
        <h3>User Registration</h3>
        <asp:GridView runat="server" ID="GV_One"
            CssClass="table table-striped table-bordered table-condensed pagination-ys"
            AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false"
            ShowHeaderWhenEmpty="true"
            ShowFooter="true"
            DataKeyNames="User_Id"
            OnRowCommand="GV_One_RowCommand"
            OnRowUpdating="GV_One_RowUpdating"
            OnRowDeleting="GV_One_RowDeleting"
            OnRowEditing="GV_One_RowEditing"
            OnRowCancelingEdit="GV_One_RowCancelingEdit"
            OnPageIndexChanging="GV_One_PageIndexChanging"
            OnRowDataBound="GV_One_RowDataBound">
            <Columns>


                <asp:TemplateField HeaderText="User">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_User_Id" Text='<%# Eval("Name") %>' runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_User_IdFooter" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Username") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Username" Text='<%# Eval("Username") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_UsernameFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Password">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Password") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Password" Text='<%# Eval("Password") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PasswordFooter" runat="server"></asp:TextBox>
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
