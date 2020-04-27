<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PlanDate.aspx.cs" Inherits="TermProject.PlanDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center">
        <br />
        <h3>Plan Your Date</h3>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <div class="row">
            <div class="col">
                Date:
                <asp:TextBox ID="txtDate" TextMode="" runat="server"></asp:TextBox>

                Description
                <br />
                <asp:TextBox ID="txtDescription" TextMode="MultiLine" CssClass="col-5" MaxLength="150" placeholder="Description" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
