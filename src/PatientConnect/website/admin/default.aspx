<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="h1" runat="Server">
    Edit Profiles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="frmUserList" class="form left third">
        <p class="header">
            Users</p>
        <ol>
            <li>
                <asp:ListBox ID="lstUsers" runat="server" CssClass="full" Rows="10" AutoPostBack="true"
                    OnSelectedIndexChanged="lstUsers_SelectedIndexChanged" />
            </li>
        </ol>
    </div>
    <div id="frmEditProfile" class="form right expand">
        <p class="header">
            Edit Profile</p>
        <ol>
            <li>
                <asp:Label ID="lblFirstName" Text="First Name:" AssociatedControlID="txtFirstName"
                    runat="server" />
                <asp:TextBox ID="txtFirstName" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblLastName" Text="Last Name:" AssociatedControlID="txtLastName" runat="server" />
                <asp:TextBox ID="txtLastName" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblEmail" Text="Email:" AssociatedControlID="txtEmail" runat="server" />
                <asp:TextBox ID="txtEmail" runat="server" />
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnSubmit" CssClass="buttons" runat="server" OnClick="btnSubmit_Click"
                    Text="Submit" />
            </li>
        </ol>
    </div>
</asp:Content>
