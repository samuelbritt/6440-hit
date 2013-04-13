<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UnauthorizedAccess.aspx.cs" Inherits="UnauthorizedAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Unauthorized Access
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <p>
        Sorry, you are trying to access a page that you are not authorized to view.
    </p>
    <p>
        Please log out and log back in as an authorized user.
    </p>
</asp:Content>
