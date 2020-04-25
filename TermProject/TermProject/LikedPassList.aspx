<%@ Page Title="Likes and Passed Lists" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LikedPassList.aspx.cs" Inherits="TermProject.LikedPassList" %>

<%@ Register Src="~/ProfileDisplay.ascx" TagName="ProfileDisplay" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center" style="margin-bottom: 10%">
        <br />
        <h3>Manage your Likes and Passed Lists</h3>
        <br />
        <div class="row">
            <div id="likes" class="col" runat="server">
                <asp:Label ID="lblLikes" runat="server" Text="Likes" ForeColor="Black" Font-Size="24px"></asp:Label>
                <hr style="border-top: 1px solid black; width: 90%" />
                <br />
            </div>

            <div id="passes" class="col" runat="server">
                <asp:Label ID="lblPasses" runat="server" Text="Passes" ForeColor="Black" Font-Size="24px"></asp:Label>
                <hr style="border-top: 1px solid black; width: 90%" />
                <br />
            </div>
        </div>
    </div>
</asp:Content>

