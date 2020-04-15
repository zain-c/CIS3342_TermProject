<%@ Page Title="Login" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TermProject.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="LoginStyle.css" />
    <link href='https://fonts.googleapis.com/css?family=Encode Sans Semi Expanded' rel='stylesheet' />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainLogin">
        <h3>Login</h3>
        <div class="login1">
            Username <br />
            <asp:TextBox ID="txtLoginUsername" runat="server" placeholder="Username" Width="100%"></asp:TextBox>
            <br />
            Password <br />
            <asp:TextBox ID="txtLoginPwd" runat="server" placeholder="Password" TextMode="Password" Width="100%"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="loginButton" OnClick="btnLogin_Click" />
            <br />
            <asp:CheckBox ID="chkRememberInfo" runat="server" />Remember Me
            <br />
            <asp:HyperLink ID="linkRetrieveUsernamePassword" NavigateUrl="/login.aspx" Text="Forgot Username/Password" runat="server" Width="100%">Forgot Username/Password</asp:HyperLink>
            <br />
            <asp:HyperLink ID="linkRegister" NavigateUrl="/Registration.aspx" Text="Create an Account" runat="server" Width="100%">Create an Account</asp:HyperLink>
        </div>
    </div>
</asp:Content>
