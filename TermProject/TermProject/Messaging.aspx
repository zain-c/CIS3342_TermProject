<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Messaging.aspx.cs" Inherits="TermProject.Messaging" %>

<%@ Register Src="~/MessagingDisplay.ascx" TagName="MessagingDisplay" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center" style="margin-bottom: 10%">
        <br />
        <h3>Messages</h3>
        <br />        
        <div id="sendMsgDiv" runat="server" class="row" visible="false">
            <div class="col">                
                <uc2:MessagingDisplay ID="MessagingDisplay1" runat="server" />
            </div>            
            <br />
            <br />
        </div>
        <div class="row">
            <div id="conversationsDiv" runat="server" class="col text-center">
                <asp:Label ID="lblConversations" runat="server" Text="Conversations" ForeColor="Black" Font-Size="24px"></asp:Label>
                <hr style="border-top: 1px solid black; width: 90%" />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
