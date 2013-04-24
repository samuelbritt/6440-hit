<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="edit.aspx.cs" Inherits="pcp_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Edit Patient:
    <br />
    <asp:Label ID="lblPatientHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div class="form">
        <ol>
            <li>
                <asp:Label ID="lblBasic" runat="server" />
                <asp:TextBox ID="tbBasic" runat="server" />
            </li>
            <li>
                <asp:Label ID="lblAllergies" runat="server" />
                <asp:TextBox ID="tbAllergies" runat="server" />
            </li>
            <li>
                <asp:Button ID="btnSubmit" CssClass="buttons" onClick="btnSubmit_Click" Text="Submit" runat="server" />
            </li>
        </ol>
    </div>
</asp:Content>
