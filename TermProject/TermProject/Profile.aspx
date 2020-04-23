<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TermProject.Profile1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="container" class="container" runat="server" style="margin-bottom: 10%">
        <div class="row">
            <div class="col text-center">
                <br />
                <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView><br />--%>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnEditProfile" runat="server" class="btn btn-outline-primary" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                        <asp:Button ID="btnSaveChanges" runat="server" class="btn btn-outline-primary" Text="Save Changes" Visible="false" OnClick="btnSaveChanges_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-outline-primary" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnMemberView" runat="server" class="btn btn-outline-primary" Text="View profile as other members would see it" OnClick="btnMemberView_Click" />
                        <asp:Button ID="btnNormalView" runat="server" class="btn btn-outline-primary" Text="Return to normal view" Visible="false" OnClick="btnNormalView_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Image ID="imgProfilePic" runat="server" Height="300px" Width="300px" />
                        <br />
                        <asp:FileUpload ID="fileProfilePic" runat="server" accept=".png, .jpeg, .jpg" Visible="false" />
                        <br />
                        <asp:TextBox ID="txtFirstName" ReadOnly="true" runat="server" CssClass="text-center" BackColor="White" BorderStyle="None" Font-Size="28px"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" ReadOnly="true" runat="server" CssClass="text-center" BackColor="White" BorderStyle="None" Font-Size="28px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:TextBox ID="txtTitle" runat="server" class="col-5 text-center" MaxLength="50" BackColor="White" BorderStyle="None" ReadOnly="True" Text="This is my title"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblGender1" runat="server" Text="Gender:"></asp:Label>
                        <asp:Label ID="lblGender" class="col-1 text-center" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="drpGender" runat="server" Visible="false">
                            <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblAge" runat="server" Text="Age:"></asp:Label>
                        <asp:TextBox ID="txtAge" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblHeight" runat="server" Text="Height:"></asp:Label>
                        <asp:TextBox ID="txtHeightFeet" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblFeet" runat="server" Text="ft."></asp:Label>
                        <asp:TextBox ID="txtHeightIn" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblInches" runat="server" Text="in."></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblWeight" runat="server" Text="Weight:"></asp:Label>
                        <asp:TextBox ID="txtWeight" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblOccupation" runat="server" Text="Occupation:"></asp:Label>
                        <asp:TextBox ID="txtOccupation" runat="server" MaxLength="50" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblCommitment1" runat="server" Text="Commitment:"></asp:Label>
                        <asp:Label ID="lblCommitment" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="drpCommitment" runat="server" Visible="False">
                            <asp:ListItem Value="Select" Selected="True">Select Commitment</asp:ListItem>
                            <asp:ListItem>Casual</asp:ListItem>
                            <asp:ListItem>Relationship</asp:ListItem>
                            <asp:ListItem>Marriage</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblHaveKids1" runat="server" Text="Have Kids:"></asp:Label>
                        <asp:Label ID="lblHaveKids" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="drpHaveKids" runat="server" Visible="False">
                            <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblWantKids1" runat="server" Text="Want Kids:"></asp:Label>
                        <asp:Label ID="lblWantKids" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="drpWantKids" runat="server" Visible="False">
                            <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblInterests" runat="server" Text="Interests"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtInterests" runat="server" placeholder="Interests" class="col-5 text-center" TextMode="MultiLine" MaxLength="200" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" class="col-5 text-center" TextMode="MultiLine" MaxLength="250" BackColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveChanges" />
                        <asp:AsyncPostBackTrigger ControlID="btnEditProfile" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnMemberView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNormalView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="contactInfo" runat="server">
                    <div class="row">
                        <div class="col text-center">
                            <h4>Contact Information</h4>

                            Phone:
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
                            &nbsp;&nbsp; 
                            Email:
                            <asp:TextBox ID="txtEmail" runat="server" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
                            <br />
                            <br />
                            <h5>Address</h5>
                            <asp:TextBox ID="txtAddress" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox><br />
                            <asp:TextBox ID="txtCity" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox>
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
                            <asp:TextBox ID="txtZip" runat="server" class="text-center" BackColor="White" BorderStyle="None" ReadOnly="True" Font-Size="16px"></asp:TextBox><br />
                            <br />

                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveChanges" />
                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEditProfile" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnMemberView" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNormalView" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="privacySettings" runat="server" visible="false">
                    <div class="row">
                        <div class="col text-center">
                            <h4>Privacy Settings</h4>
                            <br />
                            Select the visibility of the information on your profile
                            <br />
                            <asp:Label ID="lblPrivacyNotice" runat="server" ForeColor="Red"
                                Text="*Your contact information will always stay hidden from others. It will only be visible to another member once you've accepted their date request.">
                            </asp:Label>
                            <br />
                            <br />
                            Profile Picture:
                            &nbsp;&nbsp;&nbsp; 
                            <asp:DropDownList ID="ddPrivacyProfilePic" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            First name:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyFirstName" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Last name:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyLastName" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Title:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyTitle" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Gender:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyGender" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Age:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyAge" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Height:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyHeight" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Weight:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyWeight" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Occupation:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyOccupation" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Commitment:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyCommitment" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Have kids:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyHaveKids" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Want kids:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyWantKids" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Interests:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyInterests" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Description:
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddPrivacyDescription" runat="server">
                                <asp:ListItem Value="Visible" Selected="True">Visible</asp:ListItem>
                                <asp:ListItem Value="Nonvisible">Not Visible</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveChanges" />
                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEditProfile" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnMemberView" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNormalView" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
