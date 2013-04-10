<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="frmAuthParticipantsList" class="form">
        <p>
            Authorized Participants</p>
        <ol>
            <li>
                <asp:ListBox ID="lstAuthParticipants" runat="server" Rows="10"></asp:ListBox>
            </li>
            <li>
                <asp:Button ID="btnCheckAuth" runat="server" Text="Check New Authorizations" CausesValidation="false"
                    OnClick="btnCheckAuth_Click" />
                <p>
                    Last Update:
                    <asp:Label ID="lblLastUpdateDate" runat="server"></asp:Label>
                </p>
            </li>
        </ol>
    </div>
    <div id="frmEnrollNewParticipant" class="form">
        <p>
            Enroll New Participant</p>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="enroll"
            HeaderText="There were errors on the page:" />
        <ol>
            <li>
                <asp:Label ID="lblParticipantFirstName" Text="First Name:" AssociatedControlID="txtParticipantFirstName"
                    runat="server" />
                <asp:TextBox ID="txtParticipantFirstName" runat="server" ValidationGroup="enroll" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtParticipantFirstName"
                    ValidationGroup="enroll" ErrorMessage="First name is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblParticipantLastName" Text="Last Name:" AssociatedControlID="txtParticipantLastName"
                    runat="server" />
                <asp:TextBox ID="txtParticipantLastName" runat="server" ValidationGroup="enroll" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtParticipantLastName"
                    ValidationGroup="enroll" ErrorMessage="Last name is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblParticipantEmail" Text="Email:" AssociatedControlID="txtParticipantEmail"
                    runat="server" />
                <asp:TextBox ID="txtParticipantEmail" runat="server" ValidationGroup="enroll" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtParticipantEmail"
                    ValidationGroup="enroll" ErrorMessage="Email is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblSecurityQuestion" Text="Security Question:" AssociatedControlID="txtSecurityQuestion"
                    runat="server" />
                <asp:TextBox ID="txtSecurityQuestion" runat="server" ValidationGroup="enroll" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSecurityQuestion"
                    ValidationGroup="enroll" ErrorMessage="Security question is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblSecurityAnswer" Text="Security Answer:" AssociatedControlID="txtSecurityAnswer"
                    runat="server" />
                <asp:TextBox ID="txtSecurityAnswer" runat="server" ValidationGroup="enroll" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSecurityAnswer"
                    ValidationGroup="enroll" ErrorMessage="Security answer is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnEnroll" CssClass="submit" runat="server" OnClick="btnEnroll_Click"
                    Text="Enroll Participant" ValidationGroup="enroll" />
            </li>
        </ol>
    </div>
</asp:Content>
