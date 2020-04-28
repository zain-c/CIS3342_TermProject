<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessagingDisplay.ascx.cs" Inherits="TermProject.MessagingDisplay" %>

<style>
    .msgStyle{
        background-color:whitesmoke;
        width: 50%;
    }

    .msgTextbox{
        width: 80%
    }

    .messages{
        border-style: solid;
        border-color: black;
        width: 80%;
        padding: 10px;
    }
</style>

<div id="messageDisplay" class="container text-center msgStyle">
    <div class="row">        
        <div class="col">
            <br />
            <asp:Label ID="lblTo" CssClass="col-2" runat="server" Text="To: "></asp:Label>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="5000"></asp:Timer>
            <br />
            <div class="row justify-content-center">
                <div class="text-left messages">                    
                    <asp:Label ID="lblMessages" CssClass="msgTextbox" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col">
                    <asp:TextBox ID="txtSendMessage" CssClass="msgTextbox" TextMode="MultiLine" runat="server" OnTextChanged="txtSendMessage_TextChanged"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnSendMessage" CssClass="btn btn-outline-success" runat="server" Text="Send" OnClick="btnSendMessage_Click" />
                    <br />
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSendMessage" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="txtSendMessage" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</div>
