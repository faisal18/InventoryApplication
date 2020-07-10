<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="InventoryApplication.UI_Component.Users.UserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        
           
        <h2>User Login</h2>
        <div class="form-group">
            <label for="email">Email:</label>
            <input runat="server" type="text" class="form-control" id="UserName" placeholder="Enter UserName" name="UserName">
        </div>
        <div class="form-group">
            <label for="pwd">Password:</label>
            <input runat="server" type="password" class="form-control" id="Password" placeholder="Enter password" name="Password" >
        </div>

        <div>
            <%--<button type="submit" runat="server" OnServerClick="Button_Submit()" name="Submit" title="Submit" class="btn btn-default">Submit</button>--%>
            <asp:Button runat="server" ID="submit" Text="Submit" CssClass="btn btn-default" OnClick="submit_Click" />
        </div>

        <div>
            <asp:Label runat="server" ID="lbl_message"></asp:Label>
        </div>
    </div>

</asp:Content>
