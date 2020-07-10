<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FuelEntry.aspx.cs" Inherits="InventoryApplication.UI_Component.Entries.FuelEntry" %>

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
            $("[id$=txt_DateofEntry]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });
        //txt_DateofEntryFooter
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

    <script>


        function calculate() {

            var myBox1 = document.getElementById('<%=((TextBox)GV_One.FooterRow.FindControl("txt_PPGFooter")).ClientID %>').value;
            var myBox2 = document.getElementById('<%=((TextBox)GV_One.FooterRow.FindControl("txt_GallonsFooter")).ClientID %>').value;
            var result = document.getElementById('<%=((TextBox)GV_One.FooterRow.FindControl("txt_TotalAmountFooter")).ClientID %>');
            var myResult = myBox1 * myBox2;
            document.getElementById('<%=((TextBox)GV_One.FooterRow.FindControl("txt_TotalAmountFooter")).ClientID %>').value = myResult;

        }

        function calculateEdit() {

            var PPG = document.getElementById("PPGEDIT").firstElementChild.value;
            var GALLONS = document.getElementById('GALLONEDIT').firstElementChild.value;
            var AMOUNT = document.getElementById('AMOUNTEDIT').firstElementChild.value;
            var result = PPG * GALLONS;
            document.getElementById('AMOUNTEDIT').firstElementChild.value = result;
        }

</script>


    <div class="jumbotron" style="overflow: auto">
        <div><h3>Fuel Entry</h3></div>

            <div class="list-group">
            <asp:Button runat="server" ID="btn_report" class="list-group-item" OnClick="btn_report_Click" Text="Generate Report"></asp:Button>
        </div>

        <div>
        <h4 runat="server"  id="Fuel_Gauge"></h4>
            </div>

        <div>
            <asp:Button runat="server" ID="txt_reset" Text="Reset" OnClick="txt_reset_Click" />
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
            OnRowDataBound="GV_One_RowDataBound"
            OnRowCancelingEdit="GV_One_RowCancelingEdit"
            OnPageIndexChanging="GV_One_PageIndexChanging">

            <Columns>

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

                <asp:TemplateField HeaderText="CarId">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PlateNumber") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_CarId"  runat="server">
                        </asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_CarIdFooter" runat="server" >
                        </asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DriverId">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_DriverId" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Select a Driver</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_DriverIdFooter" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Select a Driver</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PPG">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("PPG") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <span id="PPGEDIT">
                        <asp:TextBox ID="txt_PPG" Text='<%# Eval("PPG") %>' runat="server" CssClass="PPGBOX" oninput="calculateEdit();"></asp:TextBox>
                            </span>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_PPGFooter" runat="server" oninput="calculate();"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gallons">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Gallons") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <span id="GALLONEDIT">
                        <asp:TextBox ID="txt_Gallons" Text='<%# Eval("Gallons") %>' runat="server" CssClass="GALLONSBOX" oninput="calculateEdit();"></asp:TextBox>
                            </span>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_GallonsFooter" runat="server" oninput="calculate();"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TotalAmount">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TotalAmount") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <span id="AMOUNTEDIT">
                        <asp:TextBox ID="txt_TotalAmount" Text='<%# Eval("TotalAmount") %>' CssClass="AMOUNTBOX" runat="server"></asp:TextBox>
                            </span>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_TotalAmountFooter" runat="server" ></asp:TextBox>
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
