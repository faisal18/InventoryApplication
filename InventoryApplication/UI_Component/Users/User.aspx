<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="InventoryApplication.UI_Component.Users.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron" style="overflow: auto">

        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <link rel="stylesheet" href="/resources/demos/style.css">
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

        <script>
            $(function () {
                $("[id$=txt_LicenseExpiryFooter]").datepicker({
                    //showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
                });
            });
            $(function () {
                $("[id$=txt_EmiratesIdExpiryFooter]").datepicker({
                    //showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
                });
            });
            $(function () {
                $("[id$=txt_VisaExpiryFooter]").datepicker({
                    //showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
                });
            });
            $(function () {
                $("[id$=txt_PassportExpiryFooter]").datepicker({
                    //showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
                });
            });
            $(function () {
                $("[id$=txt_MedicalExpiryFooter]").datepicker({
                    //showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
                });
            });
        </script>



        <h3>Users</h3>

        <asp:GridView runat="server" ID="GV_User"
            CssClass="table table-striped table-bordered table-condensed pagination-ys"
            AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false"
            ShowHeaderWhenEmpty="true"
            ShowFooter="true"
            DataKeyNames="Id"
            OnRowCommand="GV_User_RowCommand"
            OnRowUpdating="GV_User_RowUpdating"
            OnRowDeleting="GV_User_RowDeleting"
            OnRowEditing="GV_User_RowEditing"
            OnRowDataBound="GV_User_RowDataBound"
            OnRowCancelingEdit="GV_User_RowCancelingEdit"
            OnPageIndexChanging="GV_User_PageIndexChanging">

            <Columns>

                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("RoleName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddl_Roles" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Select Role</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList runat="server" ID="ddl_RolesFooter" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Select Role</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

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

                <asp:TemplateField HeaderText="Contact">
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

                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Address") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Address" Text='<%# Eval("Address") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_AddressFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="BasicSalary">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("BasicSalary") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_BasicSalary" Text='<%# Eval("BasicSalary") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_BasicSalaryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="LicenseNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("LicenseNumner") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_LicenseNumber" Text='<%# Eval("LicenseNumner") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_LicenseNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="EmiratesId">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("EmiratesId") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_EmiratesId" Text='<%# Eval("EmiratesId") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_EmiratesIdFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="VisaNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("VisaNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_VisaNumber" Text='<%# Eval("VisaNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_VisaNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PassportNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PassportNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_PassportNumber" Text='<%# Eval("PassportNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PassportNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="MedicalNumber">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("MedicalNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_MedicalNumber" Text='<%# Eval("MedicalNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_MedicalNumberFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="LicenseExpiry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("LicenseExpiry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_LicenseExpiry" Text='<%# Eval("LicenseExpiry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_LicenseExpiryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="EmiratesIdExpiry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("EmiratesIdExpiry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_EmiratesIdExpiry" Text='<%# Eval("EmiratesIdExpiry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_EmiratesIdExpiryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="VisaExpiry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("VisaExpiry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_VisaExpiry" Text='<%# Eval("VisaExpiry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_VisaExpiryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PassportExpiry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PassportExpiry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_PassportExpiry" Text='<%# Eval("PassportExpiry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PassportExpiryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="MedicalExpiry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("MedicalExpiry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_MedicalExpiry" Text='<%# Eval("MedicalExpiry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_MedicalExpiryFooter" runat="server"></asp:TextBox>
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

    </div>

    <asp:Label runat="server" ID="lbl_message"></asp:Label>
</asp:Content>
