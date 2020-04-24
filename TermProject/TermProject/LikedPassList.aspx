<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LikedPassList.aspx.cs" Inherits="TermProject.LikedPassList" %>

<%@ Register Src="~/ProfileDisplay.ascx" TagName="ProfileDisplay" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
                <br />
                <h3>Manage your Likes and Passed Lists</h3>
                <br />
                <br />
                <asp:LinkButton ID="lbLiked" runat="server" ForeColor="Black" Font-Size="24px" OnClick="lbLiked_Click">Likes</asp:LinkButton>
                <asp:Label ID="lblVertBreak" runat="server" Text="|" Font-Size="24px"></asp:Label>
                <asp:LinkButton ID="lbPassed" runat="server" ForeColor="Black" Font-Size="24px" OnClick="lbPassed_Click">Passed</asp:LinkButton>
                <hr style="border-top: 1px solid black; width: 90%" />
                <br />
                <asp:DataList ID="DataList1" runat="server">
                    <ItemTemplate>
                        <uc1:ProfileDisplay ID="pdLikedPass" runat="server" />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
