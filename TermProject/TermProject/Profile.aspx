<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        
        <div class="row" style="margin-bottom: 10%">
            <div class="col text-center">
                <br />
                <br />
                <asp:Image ID="imgProfilePic" runat="server" /><br />
                <asp:Label ID="lblName" runat="server" Text="Name" Font-Size="28px"></asp:Label>
                <br />
                <br />
                <asp:TextBox ID="txtTitle" runat="server" placeholder="Title" class="col-5" MaxLength="50"></asp:TextBox>
                <br />
                <br />
                Phone:
                <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone Number" TextMode="Phone"></asp:TextBox>
                Occupation:
                <asp:TextBox ID="txtOccupation" runat="server" placeholder="Occupation" MaxLength="50"></asp:TextBox>
                <br />
                <br />
                Age:
                <asp:TextBox ID="txtAge" runat="server" placeholder="Age" class="col-1"></asp:TextBox>
                Height:
                <asp:TextBox ID="txtHeightFeet" runat="server" placeholder="Ft" class="col-1"></asp:TextBox>
                <asp:TextBox ID="txtHeightIn" runat="server" placeholder="In" class="col-1"></asp:TextBox>
                Weight:
                <asp:TextBox ID="txtWeight" runat="server" placeholder="Weight" class="col-1"></asp:TextBox>                               
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
                &nbsp;
                Have Kids:
                <asp:Label ID="lblHaveKids" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="drpHaveKids" runat="server" Visible="False">
                    <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
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
                <asp:TextBox ID="txtInterests" runat="server" placeholder="Interests" class="col-5" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                <br />
                <br />
                Description: 
                <br />
                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" class="col-5" TextMode="MultiLine" MaxLength="250"></asp:TextBox>
                
            </div>
        </div>
    </div>
</asp:Content>
