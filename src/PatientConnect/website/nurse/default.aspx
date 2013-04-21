<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="h1" runat="Server">
    Participant Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="frmAuthParticipantsList" class="form left third">
        <p class="header">
            Authorized Participants</p>
        <ol>
            <li>
                <asp:ListBox ID="lstAuthParticipants" runat="server" CssClass="full" Rows="10"></asp:ListBox>
            </li>
            <li>
                <asp:Button ID="btnCheckAuth" runat="server" Text="Check New Authorizations" CssClass="buttons"
                    CausesValidation="false" OnClick="btnCheckAuth_Click" />
                <p>
                    Last Update:
                    <asp:Label ID="lblLastUpdateDate" runat="server"></asp:Label>
                </p>
            </li>
        </ol>
    </div>
    <div id="frmEnrollNewParticipant" class="form right expand">
        <p class="header">
            Enroll New Participant</p>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="enroll"
            HeaderText="There were errors on the page:" />
        <ol>
            <li>
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
                        <asp:Label ID="lblPcp" Text="Primary Physician:" AssociatedControlID="drpPcpList"
                            runat="server" />
                        <asp:DropDownList ID="drpPcpList" runat="server" OnSelectedIndexChanged="drpPcpList_SelectedIndexChange"
                            AutoPostBack="true" >
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="vldDrpPcpList" runat="server" ControlToValidate="drpPcpList"
                            ValidationGroup="enroll" ErrorMessage="Primary care physician is required">*</asp:RequiredFieldValidator>
                    </li>
                </ol>
            </li>
            <li id="liAddPcp" runat="server">
                <p class="header">
                    New PCP</p>
                <asp:ValidationSummary ID="newPcpValidationSummary" runat="server" ValidationGroup="newPcp"
                    HeaderText="There were errors on the page:" />
                <asp:Label ID="lblUserExists" runat="server" CssClass="error-message" Visible="false">
                    Invalid Username or Password.
                </asp:Label>
                <ol>
                    <li>
                        <asp:Label ID="lblFirstName" Text="First Name:" AssociatedControlID="txtFirstName"
                            runat="server" />
                        <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFirstName"
                            ValidationGroup="newPcp" ErrorMessage="First name is required.">*</asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblLastName" Text="Last Name:" AssociatedControlID="txtLastName" runat="server" />
                        <asp:TextBox ID="txtLastName" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLastName"
                            ValidationGroup="newPcp" ErrorMessage="Last name is required.">*</asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblInstitution" Text="Institution:" AssociatedControlID="txtInstitution"
                            runat="server" />
                        <asp:TextBox ID="txtInstitution" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInstitution"
                            ValidationGroup="newPcp" ErrorMessage="Institution is required.">*</asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblEmail" Text="Direct Email:" AssociatedControlID="txtEmail" runat="server" />
                        <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail"
                            ValidationGroup="newPcp" ErrorMessage="Email is required.">*</asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblPhone" Text="Phone Number:" AssociatedControlID="txtPhone" runat="server" />
                        <asp:TextBox ID="txtPhone" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPhone"
                            ValidationGroup="newPcp" ErrorMessage="Phone number is required.">*</asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Label ID="lblPassword" Text="Password:" AssociatedControlID="txtPassword" runat="server" />
                        <asp:TextBox ID="txtPassword" runat="server" ValidationGroup="newPcp" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPassword"
                            ValidationGroup="newPcp" ErrorMessage="Password is required.">*</asp:RequiredFieldValidator>
                        <asp:Button ID="btnGenPassword" CssClass="buttons" runat="server" OnClick="btnGenPassword_Click"
                            Text="Generate" CausesValidation="false" />
                    </li>
                </ol>
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnEnroll" CssClass="buttons" runat="server" OnClick="btnEnroll_Click"
                    Text="Enroll Participant" ValidationGroup="enroll" />
            </li>
        </ol>
        <p class="form">
        <asp:Label ID="Success" runat="server"></asp:Label>
        </p>
    </div>
</asp:Content>
