<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileDisplay.ascx.cs" Inherits="TermProject.ProfileDisplay" %>


<asp:Table ID="tblProfile" runat="server" CellPadding="5">
    <asp:TableRow>
        <asp:TableCell RowSpan="4" VerticalAlign="Top">
            <asp:Image ID="imgProfilePic" runat="server" Height="140px" Width="120px" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">
            <asp:Label ID="lblAge" runat="server" Text="Age: "></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
