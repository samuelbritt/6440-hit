<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="authParticipantsList">
        <label for="ListBoxAuthParticipants">
            Authorized Participants</label>
        <asp:ListBox ID="AuthParticipantsListBox" runat="server" Rows="10"></asp:ListBox>
        <asp:Button ID="CheckAuthButton" runat="server" Text="Check New Authorizations" CausesValidation="false"
            OnClick="btnCheckAuth_Click" />
        <span id="lastUpdate"><span>Last Update: </span>
            <asp:Label ID="LastUpdateLabel" CssClass="LastUpdateLabel" runat="server" Text="-"></asp:Label>
        </span>
    </div>
    <div id="enrollNewParticipant">
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="enroll" HeaderText="There were errors on the page:" />
        <label id="enrollLabel">
            Enroll New Participant</label>
        <asp:Label ID="PatientFirstNameLabel" Text="First Name:" AssociatedControlID="PatientFirstNameTextBox"
            runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PatientFirstNameTextBox"
                ValidationGroup="enroll" ErrorMessage="First name is required."> *
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="PatientFirstNameTextBox" runat="server" ValidationGroup="enroll" />
        </asp:Label>
        <asp:Label ID="PatientLastNameLabel" Text="Last Name:" AssociatedControlID="PatientLastNameTextBox"
            runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PatientLastNameTextBox"
                ValidationGroup="enroll" ErrorMessage="Last name is required."> *
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="PatientLastNameTextBox" runat="server" ValidationGroup="enroll" />
        </asp:Label>
        <asp:Label ID="PatientEmailLabel" Text="Email:" AssociatedControlID="PatientEmailTextBox"
            runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PatientEmailTextBox"
                ValidationGroup="enroll" ErrorMessage="Email is required."> *
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="PatientEmailTextBox" runat="server" ValidationGroup="enroll" />
        </asp:Label>
        <asp:Label ID="SecurityQuestionLabel" Text="Security Question:" AssociatedControlID="SecurityQuestionTextBox"
            runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="SecurityQuestionTextBox"
                ValidationGroup="enroll" ErrorMessage="Security question is required."> *
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="SecurityQuestionTextBox" runat="server" ValidationGroup="enroll" />
        </asp:Label>
        <asp:Label ID="SecurityAnswerLabel" Text="Security Answer:" AssociatedControlID="SecurityAnswerTextBox"
            runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="SecurityAnswerTextBox"
                ValidationGroup="enroll" ErrorMessage="Security answer is required."> *
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="SecurityAnswerTextBox" runat="server" ValidationGroup="enroll" />
        </asp:Label>
        <asp:Button ID="ButtonEnroll" runat="server" OnClick="btnEnroll_Click" Text="Enroll Participant"
            ValidationGroup="enroll" />
    </div>
</asp:Content>
