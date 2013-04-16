<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="pcp_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Welcome to the Primary Care Physician Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div class="form left quarter">
        <p class="header">
            Your Patients
        </p>
        <asp:ListBox ID="lstPatients" CssClass="full" runat="server" />
    </div>
    <div class="form expand">
        <p class="header">
            Your Patients
        </p>
        <asp:ListBox ID="ListBox1" CssClass="full" runat="server" />
    </div>
</asp:Content>
