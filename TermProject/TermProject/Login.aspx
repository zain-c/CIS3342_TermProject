<%@ Page Title="Login" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TermProject.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <asp:TextBox ID="txtLoginUsername" runat="server" placeholder="Login"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtLoginPwd" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
                <br /> 
                <asp:CheckBox ID="chkRememberInfo" runat="server" /> Remember Me
                <br />
                <asp:HyperLink ID="linkRetrieveUsernamePassword" NavigateUrl="/login.aspx" Text="Forgot Username/Password" runat="server">Forgot Username/Password</asp:HyperLink>
                <br />
                <asp:HyperLink ID="linkRegister" NavigateUrl="/Registration.aspx" Text="Create an Account" runat="server">Create an Account</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
