<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="h1" runat="server">
Log In
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="login" class="form">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="pcpLogin"
            HeaderText="There were errors on the page:" />
        <ol>
            <li>
                <asp:Label ID="lblUsername" Text="Email:" AssociatedControlID="txtUsername" runat="server" />
                <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="pcpLogin" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                    ValidationGroup="pcpLogin" ErrorMessage="Email is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblPassword" Text="Password:" AssociatedControlID="txtPassword" runat="server" />
                <asp:TextBox ID="txtPassword" runat="server" ValidationGroup="pcpLogin" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                    ValidationGroup="pcpLogin" ErrorMessage="Password is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <label>&nbsp;</label>
                <asp:Button ID="btnLogin" CssClass="submit" runat="server" OnClick="btnLogin_Click"
                    Text="Log In" ValidationGroup="pcpLogin" />
            </li>
        </ol>
    </div>
</asp:Content>
