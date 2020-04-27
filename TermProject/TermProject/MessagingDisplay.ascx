<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessagingDisplay.ascx.cs" Inherits="TermProject.MessagingDisplay" %>

<div id="messageDisplay" class="container">
    <div class = "row">
        <div class="col">
            <asp:Label ID="lblFrom" runat="server" Text="From: "></asp:Label>
        </div>
        <div class="col">
            <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <asp:Label ID="lblMessageText" runat="server"></asp:Label>
        </div>
    </div>

</div>