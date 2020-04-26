<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DateRequests.aspx.cs" Inherits="TermProject.DateRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center">
        <br />
        <h3>Date Requests</h3>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <div class="row">
            <div id="sent" runat="server" class="col">
                <asp:Label ID="lblSent" runat="server" Text="Sent" ForeColor="Black" Font-Size="24px"></asp:Label>
                <hr style="border-top: 1px solid black; width: 100%" />
                <br />
            </div>
           
            <div id="received" runat="server" class="col">
                <asp:Label ID="lblReceived" runat="server" Text="Received" ForeColor="Black" Font-Size="24px"></asp:Label>
                <hr style="border-top: 1px solid black; width: 100%" />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
