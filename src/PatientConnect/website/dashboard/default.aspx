<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="dashboard_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="h1" runat="Server">
    Patient Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <div id="frmMyPatients" class="form left quarter">
        <div class="header">
            <span>Your Patients</span>
        </div>
        <asp:ListBox ID="lstPatients" CssClass="full" OnSelectedIndexChanged="lstPatients_SelectedIndexChanged"
            AutoPostBack="true" Rows="10" runat="server" />
    </div>
    <div id="frmSelectedPatient" class="form expand" runat="server">
        <div class="header">
            <asp:Label ID="lblPatientHeader" runat="server" />
            <asp:Label ID="lblPatientBasicInfo" CssClass="right" runat="server" />
        </div>
        <p id="UnauthPatientMessage" runat="server">
            Patient has not yet authorized a HealthVault connection.
        </p>
        <ol id="SelectedPatientData" runat="server">
            <li>
                <label>
                    Height:</label>
                <asp:Label ID="lblPatientHeight" runat="server"></asp:Label>
            </li>
            <li>
                <label>
                    Weight:</label>
                <asp:Label ID="lblPatientWeight" runat="server"></asp:Label>
            </li>
            <li class="long">
                <label>
                    Allergies:</label>
                <asp:ListBox ID="lstPatientAllergy" runat="server"></asp:ListBox>
            </li>
            <li>
                <label>
                    Latest Blood Glucose:</label>
                <asp:Label ID="lblPatientGlucose" runat="server"></asp:Label>
            </li>
            <li>
                <label>
                    Latest Blood Pressure:</label>
                <asp:Label ID="lblPatientPressure" runat="server"></asp:Label>
            </li>
            <li>
                <label>
                    Condition:</label>
                <asp:Label ID="lblPatientCondition" runat="server"></asp:Label>
            </li>
            <li>
                <label>
                    Latest Procedure:</label>
                <asp:Label ID="lblPatientProcedure" runat="server"></asp:Label>
            </li>
            <li class="long">
                <label>
                    Medications:</label>
                <asp:ListBox ID="lstPatientMedication" ReadOnly="true" runat="server"></asp:ListBox>
            </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button ID="btnGetCcd" OnClick="btnGetCcd_Click" Text="Get CCD" CssClass="buttons"
                    runat="server" />
            </li>
        </ol>
    </div>
</asp:Content>
