<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="statistician_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Welcome to the Statistician Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
<div id="header">
        <p class="header" id="form left quarter">
            Group A - Average Weight
            </p>
            <p>
            <asp:Label ID="lblWtGroupA" runat="server"></asp:Label>
        </p>
        <p class="header" id="form right quarter">
            Group B - Average Weight
            </p>
            <p>
            <asp:Label ID="lblWtGroupB" runat="server"></asp:Label>
        </p>
                <p class="header" id="P1">
            Group A - Average Age
            </p>
            <p>
            <asp:Label ID="lblAgeGroupA" runat="server"></asp:Label>
        </p>
        <p class="header" id="P2">
            Group B - Average Age
            </p>
            <p>
            <asp:Label ID="lblAgeGroupB" runat="server"></asp:Label>
        </p>
</div>
</asp:Content>
