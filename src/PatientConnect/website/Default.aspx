<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="authParticipantsList">
        <label for="ListBoxAuthParticipants">
            Authorized Participants</label>
        <asp:ListBox ID="AuthParticipantsListBox" runat="server" OnSelectedIndexChanged="AuthParticipantsListBox_SelectedIndexChanged">
        </asp:ListBox>
        <asp:Button ID="CheckAuthButton" runat="server" Text="Check New Authorizations" OnClick="btnCheckAuth_Click" />
        <span id="lastUpdate"><span>Last Update: </span>
            <asp:Label ID="LastUpdateLabel" CssClass="LastUpdateLabel" runat="server" Text="-"></asp:Label>
        </span>
    </div>
    <div id="enrollNewParticipant">
        <label id="enrollLabel">
            Enroll New Participant</label>
        <asp:Label ID="PatientFirstNameLabel" Text="First Name:" AssociatedControlID="PatientFirstNameTextBox"
            runat="server">
            <asp:TextBox ID="PatientFirstNameTextBox" runat="server" />
        </asp:Label>
        <asp:Label ID="PatientLastNameLabel" Text="Last Name:" AssociatedControlID="PatientLastNameTextBox"
            runat="server">
            <asp:TextBox ID="PatientLastNameTextBox" runat="server" />
        </asp:Label>
        <asp:Label ID="PatientEmailLabel" Text="Email:" AssociatedControlID="PatientEmailTextBox"
            runat="server">
            <asp:TextBox ID="PatientEmailTextBox" runat="server" />
        </asp:Label>
        <asp:Label ID="SecurityQuestionLabel" Text="Security Question:" AssociatedControlID="SecurityQuestionTextBox"
            runat="server">
            <asp:TextBox ID="SecurityQuestionTextBox" runat="server" />
        </asp:Label>
        <asp:Label ID="SecurityAnswerLabel" Text="Security Answer:" AssociatedControlID="SecurityAnswerTextBox"
            runat="server">
            <asp:TextBox ID="SecurityAnswerTextBox" runat="server" />
        </asp:Label>
        <asp:Button ID="ButtonEnroll" runat="server" OnClick="btnEnroll_Click" Text="Enroll Participant" />
    </div>
</asp:Content>
