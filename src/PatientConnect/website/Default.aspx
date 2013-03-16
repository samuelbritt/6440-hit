<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div id="authParticipantsList">
        <label for="ListBoxAuthParticipants">Authorized Participants</label>
        <asp:ListBox ID="ListBoxAuthParticipants" runat="server"></asp:ListBox>
        <asp:Button ID="ButtonCheckAuth" runat="server" 
            Text="Check New" onclick="ButtonCheckAuth_Click" />
    </div>
    <asp:Button ID="ButtonEnroll" runat="server" onclick="ButtonEnroll_Click" Text="Enroll Participant" />


</asp:Content>

