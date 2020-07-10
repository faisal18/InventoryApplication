<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientContractEntry.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.ClientContractEntry" %>

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
            $("[id$=txt_ContractStartFooter]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });

        $(function () {
            $("[id$=txt_ContractStart]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });

         $(function () {
             $("[id$=txt_ContractTermination]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd 00:00",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
         });

         $(function () {
             $("[id$=txt_ContractTerminationFooter]").datepicker({
                 //showOn: 'button',
                 buttonImageOnly: true,
                 changeMonth: true,
                 dateFormat: "yy-mm-dd 00:00",
                 buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
             });
         });

    </script>


    <div class="jumbotron" style="overflow: auto">
        <h3>Client Contract Entry</h3>
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

                 <%--<asp:TemplateField HeaderText="TripRate">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TripRate") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_TripRate" Text='<%# Eval("TripRate") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_TripRateFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="ContractStart">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("ContractStart") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ContractStart" Text='<%# Eval("ContractStart") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ContractStartFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ContractTermination">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("ContractTermination") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ContractTermination" Text='<%# Eval("ContractTermination") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ContractTerminationFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ClientInformationId">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("CompanyName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_ClientInformationId" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="">Select a client</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_ClientInformationIdFooter" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Select a client</asp:ListItem>
                        </asp:DropDownList>
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
