<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DomainCompany.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.DomainCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="jumbotron" style="overflow: auto">
        <h3>Domain Company Entry</h3>

        <div>
            <div>
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                        <asp:Label runat="server" Text="Select Domain"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList runat="server" ID="ddl_DomainSelection"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>

            <div>
                <asp:Button runat="server" ID="btn_DomainSelect" Text="Submit" OnClick="btn_DomainSelect_Click" />
            </div>
        </div>


        <div>
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
                OnPageIndexChanging="GV_One_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("CompanyName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Description" Text='<%# Eval("CompanyName") %>' runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_DescriptionFooter" runat="server"></asp:TextBox>
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
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_message"></asp:Label>
        </div>
    </div>


</asp:Content>
