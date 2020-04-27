<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DateRequests.aspx.cs" Inherits="TermProject.DateRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center" style="margin-bottom: 10%">
        <br />
        <h3>Date Requests</h3>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <div id="planDate" runat="server" visible="false" class="row">
            <div class="col text-center">
                Date (ex. 1/1/20):
                <asp:TextBox ID="txtDate" ReadOnly="false" Enabled="true" runat="server"></asp:TextBox>
                <br />
                <br />
                Time (ex. 2:00 PM):
                <asp:TextBox ID="txtTime" ReadOnly="false" Enabled="true" runat="server"></asp:TextBox>
                <br />
                <br />
                Description
                <br />
                <asp:TextBox ID="txtDescription" TextMode="MultiLine" CssClass="col-5" MaxLength="150" placeholder="Description" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSavePlan" runat="server" CssClass="btn btn-outline-success" Text="Save Plan" OnClick="btnSavePlan_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" OnClick="btnClose_Click" />
                <br />
                <br />
            </div>
        </div>
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
