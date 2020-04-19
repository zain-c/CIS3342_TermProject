<%@ Page Title="" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="LoginRecovery.aspx.cs" Inherits="TermProject.LoginRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Stylesheets/LoginRecoveryStyle.css" />
    <link href='https://fonts.googleapis.com/css?family=Encode Sans Semi Expanded' rel='stylesheet' />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br />--%>
    <div class="mainRecovery">
        <h3>Account Recovery</h3>
        <div id="recoveryDiv" runat="server" class="recovery1">
            <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label> <br />
            Please enter the email on your account <br />
            <asp:TextBox ID="txtRecoveryEmail" runat="server" Width="50%"></asp:TextBox>
            <br /><br />
            Answer the following security questions to verify your account. 
            <br /><br />
            What is your mother's maiden name?
            <br />
            <asp:TextBox ID="txtSQ1" runat="server" Width="50%"></asp:TextBox><br />
            In what city were you born?
            <br />
            <asp:TextBox ID="txtSQ2" runat="server" Width="50%"></asp:TextBox><br />
            What is the name the first school you attended?<br />
            <asp:TextBox ID="txtSQ3" runat="server" Width="50%"></asp:TextBox>
            <br /><br />

            <asp:Button ID="btnRecover" runat="server" Text="Recover Account" CssClass="recoveryButton" OnClick="btnRecover_Click" />
        </div>
        <br />
        <div id="successDiv" runat="server" visible="false" class="recovery1">
            <h4>Recovery Successful!</h4>
            Here are your login credentials: 
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Label ID="lblUsername" runat="server" Text="Username: " Font-Bold="true"></asp:Label>
            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Label ID="lblPassword" runat="server" Text="Password: " Font-Bold="true"></asp:Label>
        </div>
        <br />
    </div>
</asp:Content>
