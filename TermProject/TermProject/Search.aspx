<%@ Page Title="Search" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TermProject.Search" %>

<%@ Register Src="~/ProfileDisplay.ascx" TagName="ProfileDisplay" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Stylesheets/SearchStyle.css" />
    <link href='https://fonts.googleapis.com/css?family=Encode Sans Semi Expanded' rel='stylesheet' />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="mainContainer">
            <div id="search" class="searchContainer">
                <h3>Find your match today!</h3> <br />

                <asp:Table ID="tblPublicSearchFilter" runat="server" HorizontalAlign="Center" Width="75%" CellPadding="10">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Location</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Gender</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            City &nbsp
                            <asp:TextBox ID="txtLocationFilter" runat="server"></asp:TextBox> <br /><br />
                            State &nbsp
                            <asp:DropDownList ID="ddStateFilter" runat="server">
                                <asp:ListItem Value="Select">Select State</asp:ListItem>
                                <asp:ListItem>AL</asp:ListItem>
                                <asp:ListItem>AK</asp:ListItem>
                                <asp:ListItem>AZ</asp:ListItem>
                                <asp:ListItem>AR</asp:ListItem>
                                <asp:ListItem>CA</asp:ListItem>
                                <asp:ListItem>CO</asp:ListItem>
                                <asp:ListItem>CT</asp:ListItem>
                                <asp:ListItem>DE</asp:ListItem>
                                <asp:ListItem>FL</asp:ListItem>
                                <asp:ListItem>GA</asp:ListItem>
                                <asp:ListItem>HI</asp:ListItem>
                                <asp:ListItem>ID</asp:ListItem>
                                <asp:ListItem>IL</asp:ListItem>
                                <asp:ListItem>IN</asp:ListItem>
                                <asp:ListItem>IA</asp:ListItem>
                                <asp:ListItem>KS</asp:ListItem>
                                <asp:ListItem>KY</asp:ListItem>
                                <asp:ListItem>LA</asp:ListItem>
                                <asp:ListItem>ME</asp:ListItem>
                                <asp:ListItem>MD</asp:ListItem>
                                <asp:ListItem>MA</asp:ListItem>
                                <asp:ListItem>MI</asp:ListItem>
                                <asp:ListItem>MN</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                                <asp:ListItem>MO</asp:ListItem>
                                <asp:ListItem>MT</asp:ListItem>
                                <asp:ListItem>NE</asp:ListItem>
                                <asp:ListItem>NV</asp:ListItem>
                                <asp:ListItem>NH</asp:ListItem>
                                <asp:ListItem>NJ</asp:ListItem>
                                <asp:ListItem>NM</asp:ListItem>
                                <asp:ListItem>NY</asp:ListItem>
                                <asp:ListItem>NC</asp:ListItem>
                                <asp:ListItem>ND</asp:ListItem>
                                <asp:ListItem>OH</asp:ListItem>
                                <asp:ListItem>OK</asp:ListItem>
                                <asp:ListItem>OR</asp:ListItem>
                                <asp:ListItem>PA</asp:ListItem>
                                <asp:ListItem>RI</asp:ListItem>
                                <asp:ListItem>SC</asp:ListItem>
                                <asp:ListItem>SD</asp:ListItem>
                                <asp:ListItem>TN</asp:ListItem>
                                <asp:ListItem>TX</asp:ListItem>
                                <asp:ListItem>UT</asp:ListItem>
                                <asp:ListItem>VT</asp:ListItem>
                                <asp:ListItem>VA</asp:ListItem>
                                <asp:ListItem>WA</asp:ListItem>
                                <asp:ListItem>WV</asp:ListItem>
                                <asp:ListItem>WI</asp:ListItem>
                                <asp:ListItem>WY</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            Gender &nbsp
                            <asp:DropDownList ID="ddGenderFilter" runat="server">
                                <asp:ListItem Value="Select">Select Gender</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                <asp:ListItem Value="N/A">N/A</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="tblPrivateSearchFilter" runat="server" HorizontalAlign="Center" Width="75%" CellPadding="10">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell ToolTip="What level of commitment are you looking for?">Commitment</asp:TableHeaderCell>
                        <asp:TableHeaderCell ToolTip="Would you like your matches to have kids?">Kids</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Commitment &nbsp
                            <asp:DropDownList ID="ddCommitmentFilter" ToolTip="What level of commitment are you looking for?" runat="server">
                                <asp:ListItem Value="Select">Select Commitment</asp:ListItem>
                                <asp:ListItem Value="Casual">Casual</asp:ListItem>
                                <asp:ListItem Value="Relationship">Relationship</asp:ListItem>
                                <asp:ListItem Value="Marriage">Marriage</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            Have Kids &nbsp
                            <asp:DropDownList ID="ddHaveKidsFilter" ToolTip="Would you like your matches to have kids?" runat="server">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="N/A">N/A</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Interests</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Interests &nbsp
                            <asp:TextBox ID="txtInterestsFilter" ToolTip="Enter your desired interests, separated by commas." placeholder="Interests" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />

                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchButton" /> <br /><br />

                <asp:GridView ID="gvSearchResults" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" GridLines="Both" AllowPaging="True" PageSize="6">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <uc1:ProfileDisplay ID="profileDisplay" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
            </div>
        </div>
</asp:Content>

