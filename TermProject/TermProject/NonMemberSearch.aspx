<%@ Page Title="" Language="C#" MasterPageFile="~/Access.Master" AutoEventWireup="true" CodeBehind="NonMemberSearch.aspx.cs" Inherits="TermProject.NonMemberSearch" %>

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
                            <asp:TextBox ID="txtLocationFilter" MaxLength="50" runat="server"></asp:TextBox> <br /><br />
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
                                <asp:ListItem Value="Both">Both</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>

                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
                <br />
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="searchButton" /> <br /><br />
             </div>
             <div id="ResultsContainer" runat="server">
                <Table id="ResultsTable">
                    <tr>
                        <th>UserName</th>                       
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Gender</th>                        
                        <th>City</th>
                        <th>State</th>
                    </tr>
                
                    <asp:Repeater ID="rptSearchResults" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "username") %>'></asp:Label>
                                </td>                             
                                <td>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "firstName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "gender") %>'></asp:Label>
                                </td>                              
                                <td>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "city") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "state") %>'></asp:Label>
                                </td>  
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
        </div>

</asp:Content>
