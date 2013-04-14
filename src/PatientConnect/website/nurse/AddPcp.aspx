<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddPcp.aspx.cs" Inherits="nurse_AddPcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Add new Primary Care Physician
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div id="frmAddNewPcp" class="form">
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="newPcp"
            HeaderText="There were errors on the page:" />
        <asp:Label ID="lblUserExists" runat="server" CssClass="error-message" Visible="false">
            User exists. Please choose another username.
        </asp:Label>
        <ol>
            <li>
                <asp:Label ID="lblFirstName" Text="First Name:" AssociatedControlID="txtFirstName"
                    runat="server" />
                <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="newPcp" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                    ValidationGroup="newPcp" ErrorMessage="First name is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblLastName" Text="Last Name:" AssociatedControlID="txtLastName" runat="server" />
                <asp:TextBox ID="txtLastName" runat="server" ValidationGroup="newPcp" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                    ValidationGroup="newPcp" ErrorMessage="Last name is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblInstitution" Text="Institution:" AssociatedControlID="txtInstitution"
                    runat="server" />
                <asp:TextBox ID="txtInstitution" runat="server" ValidationGroup="newPcp" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInstitution"
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhone"
                    ValidationGroup="newPcp" ErrorMessage="Phone number is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnNewPcp" CssClass="submit" runat="server" OnClick="btnNewPcp_Click"
                    Text="Create" ValidationGroup="enroll" />
            </li>
        </ol>
    </div>
</asp:Content>
