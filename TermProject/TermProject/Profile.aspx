<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container"  style="margin-bottom: 10%">        
        <div class="row">
            <div class="col text-center">
                <br />
                <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView><br />--%>
                <asp:Button ID="btnEditProfile" runat="server" class="btn btn-outline-primary" Text="Edit Profile" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMemberView" runat="server" class="btn btn-outline-primary" Text="View profile as other members would see it" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNormalView" runat="server" class="btn btn-outline-primary" Text="Return to normal view" Visible="false" />
                <br />
                <br />
                <asp:Image ID="imgProfilePic" runat="server" Height="300px" Width="300px" />
                <br />
                <asp:Label ID="lblFirstName" runat="server" Text="First" Font-Size="28px"></asp:Label>
                <asp:Label ID="lblLastName" runat="server" Text="Last" Font-Size="28px"></asp:Label>
                <br />
                <br />
                <asp:TextBox ID="txtTitle" runat="server" class="col-5 text-center" MaxLength="50" BackColor="White" BorderStyle="None" ReadOnly="True" Text="This is my title"></asp:TextBox>
                <br />
                <br />
                Age:
                <asp:TextBox ID="txtAge" runat="server" class="col-1 text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                Height:
                <asp:TextBox ID="txtHeightFeet" runat="server" class="col-1 text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>ft
                <asp:TextBox ID="txtHeightIn" runat="server" class="col-1 text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>in.
                Weight:
                <asp:TextBox ID="txtWeight" runat="server" class="col-1 text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>                               
                <br />
                <br />
                Occupation:
                <asp:TextBox ID="txtOccupation" runat="server" MaxLength="50" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                <br />
                <br />                
                Commitment:
                <asp:Label ID="lblCommitment" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="drpCommitment" runat="server" Visible="False">
                    <asp:ListItem Value="Select" Selected="True">Select Commitment</asp:ListItem>
                    <asp:ListItem>Casual</asp:ListItem>
                    <asp:ListItem>Relationship</asp:ListItem>
                    <asp:ListItem>Marriage</asp:ListItem>
                </asp:DropDownList>
                Have Kids:
                <asp:Label ID="lblHaveKids" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="drpHaveKids" runat="server" Visible="False">
                    <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                Want Kids:
                <asp:Label ID="lblWantKids" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="drpWantKids" runat="server" Visible="False">
                    <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Interests: 
                <br />
                <asp:TextBox ID="txtInterests" runat="server" placeholder="Interests" class="col-5 text-center" TextMode="MultiLine" MaxLength="200" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                <br />
                <br />
                Description: 
                <br />
                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" class="col-5 text-center" TextMode="MultiLine" MaxLength="250" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>                
            </div>
        </div>
        <br />
        <div id="contactInfo" runat="server">
            <div class="row">
                <div class="col text-center">
                    <h4>Contact Information</h4>
                    Phone:
                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                    Email:
                    <asp:TextBox ID="txtEmail" runat="server" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                    <br />
                    <br />
                    Address: <br />
                    <asp:TextBox ID="txtAddress" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox><br />
                    <asp:TextBox ID="txtCity" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblState" class="text-center" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="ddState" runat="server" Visible="false">
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
                    <br />
                    <asp:TextBox ID="txtZip" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox><br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
