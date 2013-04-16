using System;
using System.Collections.Generic;

public partial class pcp_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLstPatients();
        }
    }

    private void BindLstPatients()
    {
        string username = Profile.UserName;
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> myPatients = dao.GetParticipantsForPcp(username);

        lstPatients.DataSource = myPatients;
        lstPatients.DataValueField = "ID";
        lstPatients.DataTextField = "FullName";
        lstPatients.DataBind();
    }


    protected void lstPatients_SelectedIndexChanged(object sender, EventArgs e)
    {
        Guid pid = Guid.Parse(lstPatients.SelectedValue);
        Participant participant = (new ParticipantDAO()).FindParticipantById(pid);
        DisplaySelectedParticipant(participant);
    }

    private void DisplaySelectedParticipant(Participant participant)
    {
        lblPatientHeader.Text = participant.FullName;
    }
}