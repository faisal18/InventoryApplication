<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TripOutsource.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.TripOutsource" %>
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
            $("[id$=txt_DateofEntryFooter]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });

        </script>




    <div class="jumbotron" style="overflow: auto">
        <h3>Trip Entry</h3>

              <div class="list-group">
            <asp:Button runat="server" ID="btn_report" class="list-group-item" OnClick="btn_report_Click" Text="Generate Report"></asp:Button>
        </div>

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
            >
            <Columns>

                <asp:TemplateField HeaderText="Client">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("ClientName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ClientId" Text='<%# Eval("ClientName") %>' runat="server" ></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ClientIdFooter" runat="server" ></asp:TextBox>
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

                <asp:TemplateField HeaderText="FromDestination">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("FromDestination") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_FromDestination" Text='<%# Eval("FromDestination") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_FromDestinationFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ToDestination">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("ToDestination") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ToDestination" Text='<%# Eval("ToDestination") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_ToDestinationFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TotalDistance">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TotalDistance") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_TotalDistance" Text='<%# Eval("TotalDistance") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_TotalDistanceFooter" runat="server"></asp:TextBox>
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

                <asp:TemplateField HeaderText="TollCharges">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TollCharges") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_TollCharges" Text='<%# Eval("TollCharges") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_TollChargesFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="WaitingCharges">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("WaitingCharges") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_WaitingCharges" Text='<%# Eval("WaitingCharges") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_WaitingChargesFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TotalCharges">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TotalCharges") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_TotalCharges" Text='<%# Eval("TotalCharges") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_TotalChargesFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DriverCommission">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("DriverCommission") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_DriverCommission" Text='<%# Eval("DriverCommission") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_DriverCommissionFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DateofEntry">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("DateofEntry") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_DateofEntry" Text='<%# Eval("DateofEntry") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_DateofEntryFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Driver">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("DriverName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_DriverId" Text='<%# Eval("DriverName") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_DriverIdFooter" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="Car">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("CarRegistration") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_CarId" Text='<%# Eval("ClientName") %>'  runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_CarIdFooter" runat="server"></asp:TextBox>
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
