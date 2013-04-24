<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="pcp_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Welcome to the Primary Care Physician Page
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
        <p id="UnauthPatientMessage" runat="server">
            Patient has not yet authorized a HealthVault connection.
        </p>
        <ol id="SelectedPatientData" runat="server">
            <li>
                <asp:Label ID="lblPatientBasic" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientAllergy" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientGlucose" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientPressure" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientHeight" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientCondition" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientProcedure" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientWeight" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblPatientMedication" runat="server"></asp:Label>
            </li>
        </ol>
    </div>
</asp:Content>
