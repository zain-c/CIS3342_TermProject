<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" type="text/css" href="SearchStyle.css" />
    <link href='https://fonts.googleapis.com/css?family=Encode Sans Semi Expanded' rel='stylesheet' />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />

    <title>Search for potential candidates</title>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div id="container" runat="server" class="mainContainer">
            <h3>Search for potential candidates</h3>
            <div id="search" class="searchContainer">
                <asp:Table ID="tblPublicFilters" runat="server" HorizontalAlign="Center" CellPadding="10" CellSpacing="5" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>criteria 1 - Location</asp:TableCell>
                        <asp:TableCell>criteria 2 - Gender</asp:TableCell>
                    </asp:TableRow>                    
                </asp:Table>
                <asp:Table ID="tblPrivateFilters" runat="server" HorizontalAlign="Center" CellPadding="10" CellSpacing="5" GridLines="None" Width="100%" Visible="false">
                    <asp:TableRow>
                        <asp:TableCell>criteria 3 - Age Range</asp:TableCell>
                        <asp:TableCell>criteria 4 - Commitment Level</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>citeria 6</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Button ID="btnSearch" runat="server" Text="Search" />
                <br /><br />
            </div>
        </div>
    </form>
</body>
</html>
