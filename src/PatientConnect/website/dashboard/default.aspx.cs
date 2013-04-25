using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using HealthVaultHelper;
using System.Xml.XPath;
using System.Xml;
using System.Collections.ObjectModel;
using Logic;
using System.Web.UI.WebControls;
using System.Web.UI;


public partial class dashboard_default : System.Web.UI.Page
{
    Participant selectedParticipant;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLstPatients();
            frmSelectedPatient.Visible = false;
        }
        selectedParticipant = GetSelectedParticpant();
    }

    private void BindLstPatients()
    {
        lstPatients.DataSource = GetParticipantList();
        lstPatients.DataValueField = "ID";
        lstPatients.DataTextField = "FullName";
        lstPatients.DataBind();
    }

    private ICollection<Participant> GetParticipantList()
    {
        if (User.IsInRole(Roles.ONCOLOGIST))
        {
            return GetOncologistPatients();
        }
        else
        {
            return GetPcpPatients();
        }
    }

    private ICollection<Participant> GetOncologistPatients()
    {
        ParticipantDAO dao = new ParticipantDAO();
        return dao.GetEligibleParticipants();
    }

    private ICollection<Participant> GetPcpPatients()
    {
        string username = Profile.UserName;
        ParticipantDAO dao = new ParticipantDAO();
        return dao.GetParticipantsForPcp(username);
    }

    private Participant GetSelectedParticpant()
    {
        String selection = lstPatients.SelectedValue;
        if (string.IsNullOrEmpty(selection))
            return null;
        Guid pid = Guid.Parse(selection);
        return GetParticipant(pid);
    }

    private Participant GetParticipant(Guid pid)
    {
        ParticipantDAO dao = new ParticipantDAO();
        return dao.FindParticipantById(pid);
    }

    protected void lstPatients_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplaySelectedParticipant(selectedParticipant);
    }

    private void DisplaySelectedParticipant(Participant participant)
    {
        frmSelectedPatient.Visible = true;
        lblPatientHeader.Text = participant.FullName;

        if (participant.HasAuthorized)
        {
            SelectedPatientData.Visible = true;
            lblPatientBasicInfo.Visible = true;
            UnauthPatientMessage.Visible = false;
            GetParticipantData(participant);
        }
        else
        {
            SelectedPatientData.Visible = false;
            lblPatientBasicInfo.Visible = false;
            UnauthPatientMessage.Visible = true;
        }
    }

    private void GetParticipantData(Participant participant)
    {
        try
        {
            HVDataAccessor accessor = new HVDataAccessor(participant);
            AddAllFilters(accessor);

            string nullText = "No information available";
            lblPatientWeight.Text = accessor.GetItemString(Weight.TypeId, nullText);
            lblPatientHeight.Text = accessor.GetItemString(Height.TypeId, nullText);
            lblPatientGlucose.Text = accessor.GetItemString(BloodGlucose.TypeId, nullText);
            lblPatientPressure.Text = accessor.GetItemString(BloodPressure.TypeId, nullText);
            lblPatientCondition.Text = accessor.GetItemString(Condition.TypeId, nullText);
            lblPatientProcedure.Text = accessor.GetItemString(Procedure.TypeId, nullText);

            lstPatientMedication.DataSource = accessor.GetItemCollection(Medication.TypeId);
            lstPatientMedication.DataBind();

            lstPatientAllergy.DataSource = accessor.GetItemCollection(Allergy.TypeId);
            lstPatientAllergy.DataBind();

            lblPatientBasicInfo.Text = accessor.BuildBasicInfoString();
        }
        catch (HealthServiceException ex)
        {
            String msg = String.Empty;
            if (ex is HealthServiceAccessDeniedException)
                msg = "The application may be trying to read user data from an anonymous connection.\n";
            msg += ex.Error.ErrorInfo + "\n" + ex.Message.ToString();
            Debug.WriteLine(msg);
        }
    }

    private static void AddAllFilters(HVDataAccessor accessor)
    {
        accessor.AddFilter(Basic.TypeId);
        accessor.AddFilter(Personal.TypeId);
        accessor.AddFilter(Allergy.TypeId);
        accessor.AddFilter(Height.TypeId);
        accessor.AddFilter(BloodGlucose.TypeId);
        accessor.AddFilter(BloodPressure.TypeId);
        accessor.AddFilter(Condition.TypeId);
        accessor.AddFilter(Procedure.TypeId);
        accessor.AddFilter(Medication.TypeId);
        accessor.AddFilter(Weight.TypeId);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Guid pid = Guid.Parse(lstPatients.SelectedValue);
        Session["selected_patient_pid"] = pid;
        Response.Redirect("~/pcp/edit.aspx");
    }

    protected void btnGetCcd_Click(object sender, EventArgs e)
    {
        HVDataAccessor accessor = new HVDataAccessor(selectedParticipant);
        AddAllFilters(accessor);
        String ccd = accessor.getCcd();
        String filename = String.Format("ccd-{0}.{1}.xml",
            selectedParticipant.LastName, selectedParticipant.FirstName);
        SaveXmlFile(ccd, filename);
    }

    private void SaveXmlFile(string xml, string filename)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition",
            String.Format("attachment; filename={0}", filename));
        doc.Save(Response.OutputStream);
        Response.End();
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        Session["selected_participant"] = selectedParticipant;
        Response.Redirect("~/dashboard/communicate.aspx");
    }
}
