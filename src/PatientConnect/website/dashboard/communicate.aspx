<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="communicate.aspx.cs" Inherits="dashboard_communicate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Patient Connect
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div id="frmCommunicate" class="form">
        <div class="header">
            <asp:Label ID="lblPatientHeader" runat="server" />
            <asp:Label ID="lblPatientBasicInfo" CssClass="right" runat="server" />
        </div>
        <ol>
            <li>
                <label>
                    Send DIRECT Email:</label>
                <asp:TextBox ID="txtEmailSubject" Text="Subject" runat="server" />
            </li>
            <li class="long">
                <label>
                    &nbsp;</label>
                <asp:TextBox ID="txtEmailBody" TextMode="multiline" Rows="10" 
                    runat="server" />
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnSend" CssClass="buttons" Text="Send" runat="server" OnClick="btnSend_Click" />
                <asp:Button ID="btnAttach" CssClass="buttons" Text="Attach File" runat="server" OnClick="btnAttach_Click" />
                <asp:FileUpload ID="btnUpload" runat="server" ></asp:FileUpload>
            </li>
        </ol>
    </div>
</asp:Content>
