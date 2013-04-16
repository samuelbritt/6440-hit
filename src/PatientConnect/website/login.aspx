<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="h1" runat="server">
Log In
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="login" class="form center half">
        <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="login"
            HeaderText="There were errors on the page:" />
        <ol>
            <li>
                <asp:Label ID="lblUsername" Text="Username:" AssociatedControlID="txtUsername" runat="server" />
                <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="login" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                    ValidationGroup="login" ErrorMessage="Username is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblPassword" Text="Password:" AssociatedControlID="txtPassword" runat="server" />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="login" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                    ValidationGroup="login" ErrorMessage="Password is required.">*</asp:RequiredFieldValidator>
            </li>
            <li>
                <label>&nbsp;</label>
                <asp:Button ID="btnLogin" CssClass="buttons" runat="server" OnClick="btnLogin_Click"
                    Text="Log In" ValidationGroup="login" />
            </li>
        </ol>
    </div>
</asp:Content>
