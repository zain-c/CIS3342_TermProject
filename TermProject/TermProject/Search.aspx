<%@ Page Title="Search" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="SearchStyle.css" />
    <link href='https://fonts.googleapis.com/css?family=Encode Sans Semi Expanded' rel='stylesheet' />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="mainContainer">
            <div id="search" class="searchContainer">
                <h3>Find your match today!</h3>
                <asp:Table ID="tblPublicSearchFilter" runat="server" HorizontalAlign="Center" Width="100%" CellPadding="15">
                    <asp:TableRow>
                        <asp:TableCell>criteria 1</asp:TableCell>
                        <asp:TableCell>criteria 2</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="tblPrivateSearchFilter" runat="server" HorizontalAlign="Center" Width="100%" CellPadding="15">
                    <asp:TableRow>
                        <asp:TableCell>criteria 3</asp:TableCell>
                        <asp:TableCell>criteria 4</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>criteria 5</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
</asp:Content>

