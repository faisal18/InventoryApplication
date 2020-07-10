<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarEntry.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.CarEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


  <%--  <%@ MasterType VirtualPath="~/Site.Master" %>

    <asp:Label ID="lbl_DomainCompany" runat="server"></asp:Label>--%>


    <div class="jumbotron" style="overflow: auto">
        <h3>Car Entry</h3>
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


                <asp:TemplateField HeaderText="PlateNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PlateNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_PlateNumber" Text='<%# Eval("PlateNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PlateNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ChasisNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("ChasisNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ChasisNumber" Text='<%# Eval("ChasisNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ChasisNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RegistrationNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("RegistrationNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_RegistrationNumber" Text='<%# Eval("RegistrationNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_RegistrationNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Model">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Model") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Model" Text='<%# Eval("Model") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ModelFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Description" Text='<%# Eval("Description") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_DescriptionFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PortTags">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PortTags") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_PortTags" Text='<%# Eval("PortTags") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PortTagsFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Active") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_Active" runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_ActiveFooter" runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="RoadPermission">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("RoadPermission") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_RoadPermission" runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_RoadPermissionFooter" runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="LoadingPermission">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("LoadingPermission") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_LoadingPermission"  runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_LoadingPermissionFooter" runat="server">
                            <asp:ListItem Value="true">True</asp:ListItem>
                            <asp:ListItem Value="false">False</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CarTypeId">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("CarTypeId") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_CarTypeId" Text='<%# Eval("CarTypeId") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_CarTypeIdFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="File">
                    <ItemTemplate>
                        <asp:PlaceHolder runat="server" ID="PH_View"></asp:PlaceHolder>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:PlaceHolder runat="server" ID="PH_Edit"></asp:PlaceHolder> 
                        <asp:FileUpload runat="server" ID="FU_Edit" AllowMultiple="true" />                       
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:FileUpload runat="server" ID="FU_Foot" AllowMultiple="true" />        
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
