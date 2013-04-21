<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="oncologist_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Welcome to the Oncologist Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div id="frmMyPatients" class="form left quarter">
        <p class="header">
            Your Patients
        </p>
        <asp:ListBox ID="lstPatients" CssClass="full" OnSelectedIndexChanged="lstPatients_SelectedIndexChanged"
           AutoPostBack="true" runat="server" />
    </div>
    <div id="frmSelectedPatient" class="form expand" runat="server">
        <p class="header">
            <asp:Label ID="lblPatientHeader" runat="server"></asp:Label>
            
        </p>
        <p class="header">
        <asp:Label ID="lblPatientInfo" runat="server"></asp:Label>
        </p>
        <p class="header">
        <asp:Label ID="lblPatientBasic" runat="server"></asp:Label>
        </p>
        <p class="header">
            <asp:Label ID="lblPatientCondition" runat="server"></asp:Label>
        </p>
        <p class="header">
            <asp:Label ID="lblPatientProcedure" runat="server"></asp:Label>
        </p>
        <p class="header">
            <asp:Label ID="lblPatientMedication" runat="server"></asp:Label>
        </p>
    </div>
</asp:Content>
