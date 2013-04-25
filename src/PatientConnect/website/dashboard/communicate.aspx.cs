using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthVaultHelper;
using System.IO;
using System.Diagnostics;
using Microsoft.Health;
using mysendemail;

public partial class dashboard_communicate : System.Web.UI.Page
{
    Participant selectedParticipant;
    protected void Page_Load(object sender, EventArgs e)
    {
        selectedParticipant = (Participant)Session["selected_participant"];
        if (selectedParticipant == null)
        {
            lblPatientHeader.Text = "No selected patient";
            return;
        }
        HVDataAccessor accessor = new HVDataAccessor(selectedParticipant);
        lblPatientHeader.Text = selectedParticipant.FullName;
        lblPatientBasicInfo.Text = accessor.BuildBasicInfoString();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        String msg = txtEmailBody.Text;
        string subject = txtEmailSubject.Text;

        String filename = "";
        if (Session["filename"] != null)
        {
            filename = Server.MapPath("~/") + Session["filename"].ToString();
        }

        new HotmailEmail().Sender(msg, subject, filename, selectedParticipant.Email);
        Session["filename"] = String.Empty;
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {
        if (btnUpload.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(btnUpload.FileName);
                btnUpload.SaveAs(Server.MapPath("~/") + filename);
                Session["filename"] = filename;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
            }
        }
    }
}