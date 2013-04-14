<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddPcpSuccess.aspx.cs" Inherits="nurse_AddPcpSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Physician added successfully.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div id="frmAddSuccess" class="form">
        <asp:Label ID="lblInvalidUser" runat="server" CssClass="error-message" Visible="false">
            No such user.
        </asp:Label>
        <ol>
            <li>
                <asp:Label ID="lblName" Text="First Name:" AssociatedControlID="lblNameVal" runat="server"
                    CssClass="key" />
                <asp:Label ID="lblNameVal" CssClass="val" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblInstitution" Text="Institution:" AssociatedControlID="lblInstitutionVal"
                    runat="server" CssClass="key" />
                <asp:Label ID="lblInstitutionVal" CssClass="val" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblEmail" Text="Direct Email:" AssociatedControlID="lblEmailVal" runat="server"
                    CssClass="key" />
                <asp:Label ID="lblEmailVal" CssClass="val" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblPhone" Text="Phone Number:" AssociatedControlID="lblPhoneVal" runat="server"
                    CssClass="key" />
                <asp:Label ID="lblPhoneVal" CssClass="val" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblUsername" Text="Username:" AssociatedControlID="lblUsernameVal"
                    runat="server" CssClass="key" />
                <asp:Label ID="lblUsernameVal" CssClass="val" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblPassword" Text="Password:" AssociatedControlID="lblPasswordVal"
                    runat="server" CssClass="key" />
                <asp:Label ID="lblPasswordVal" CssClass="val" runat="server" />
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnBack" CssClass="submit" runat="server" OnClick="btnBack_Click"
                    Text="Back" />
            </li>
        </ol>
    </div>
</asp:Content>
