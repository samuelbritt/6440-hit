using System;
using System.Collections.Generic;

public partial class pcp_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = Profile.UserName;
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> myPatients = dao.GetParticipantsForPcp(username);

        lstPatients.DataSource = myPatients;
        lstPatients.DataValueField = "ID";
        lstPatients.DataTextField = "FullName";
        lstPatients.DataBind();
    }
}