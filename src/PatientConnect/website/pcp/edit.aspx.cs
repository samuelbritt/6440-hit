using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Health.Web;
using Microsoft.Health;
using HealthVaultHelper;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;

public partial class pcp_edit : System.Web.UI.Page
{
    Participant selectedParticipant;
    Dictionary<Guid, String> lblTextDict;
    Dictionary<Guid, Label> lblLabelDict;
    Dictionary<Guid, TextBox> tbDict;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPatient();
        }
        catch (Exception)
        {
            lblPatientHeader.Text = "Error: could not find patient";
            return;
        }

        LoadDictionaries();
        LoadUI();
    }

    private void GetPatient()
    {

        Guid pid = (Guid)Session["selected_patient_pid"];
        ParticipantDAO dao = new ParticipantDAO();
        selectedParticipant = dao.FindParticipantById(pid);
    }

    private void LoadDictionaries()
    {
        LoadTextDict();
        LoadLabelDict();
        LoadTextBoxDict();
    }

    private void LoadTextDict()
    {
        lblTextDict = new Dictionary<Guid, string>();
        lblTextDict.Add(Basic.TypeId, "Gender, Birth Year");
        //lblTextDict.Add(Allergy.TypeId, "Allergy");
        //lblTextDict.Add(Height.TypeId, "Height");
        //lblTextDict.Add(BloodGlucose.TypeId, "Blood Glucose");
        //lblTextDict.Add(BloodPressure.TypeId, "Blood Pressure");
        //lblTextDict.Add(Condition.TypeId, "Condition");
        //lblTextDict.Add(Procedure.TypeId, "Procedure");
        //lblTextDict.Add(Medication.TypeId, "Medication");
        //lblTextDict.Add(Weight.TypeId, "Weight");
    }

    private void LoadLabelDict()
    {

        lblLabelDict = new Dictionary<Guid, Label>();
        lblLabelDict.Add(Basic.TypeId, lblBasic);
        //lblLabelDict.Add(Allergy.TypeId, "Allergy");
        //lblLabelDict.Add(Height.TypeId, "Height");
        //lblLabelDict.Add(BloodGlucose.TypeId, "Blood Glucose");
        //lblLabelDict.Add(BloodPressure.TypeId, "Blood Pressure");
        //lblLabelDict.Add(Condition.TypeId, "Condition");
        //lblLabelDict.Add(Procedure.TypeId, "Procedure");
        //lblLabelDict.Add(Medication.TypeId, "Medication");
        //lblLabelDict.Add(Weight.TypeId, "Weight");
    }

    private void LoadTextBoxDict()
    {
        tbDict = new Dictionary<Guid, TextBox>();
        tbDict.Add(Basic.TypeId, tbBasic);

        //tbDict.Add(Allergy.TypeId, "Allergy");
        //tbDict.Add(Height.TypeId, "Height");
        //tbDict.Add(BloodGlucose.TypeId, "Blood Glucose");
        //tbDict.Add(BloodPressure.TypeId, "Blood Pressure");
        //tbDict.Add(Condition.TypeId, "Condition");
        //tbDict.Add(Procedure.TypeId, "Procedure");
        //tbDict.Add(Medication.TypeId, "Medication");
        //tbDict.Add(Weight.TypeId, "Weight");
    }

    private void LoadUI()
    {

        lblPatientHeader.Text = selectedParticipant.FullName;

        OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(selectedParticipant.HVPersonID);
        HealthRecordAccessor accessor = new HealthRecordAccessor(conn, selectedParticipant.HVRecordID);

        HealthRecordSearcher search = accessor.CreateSearcher();
        search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
        search.Filters.Add(new HealthRecordFilter(Allergy.TypeId));
        HealthRecordItemCollection items = null;


        lblBasic.Text = "Gender, Birth Year: ";
        items = search.GetMatchingItems()[0];
        if (items.Count > 0)
            tbBasic.Text = items[0].ToString();
        else
            tbBasic.Text = "No Basic Info Available";


        lblAllergies.Text = "Allergies:";
        items = search.GetMatchingItems()[1];
        if (items.Count > 0)
            tbAllergies.Text = items[0].ToString();
        else
            tbAllergies.Text = "No Allergies Info Available";



        //foreach (Guid typeId in lblLabelDict.Keys)
        //{
        //    search.Filters.Add(new HealthRecordFilter(typeId));
        //}

        //foreach (Guid TypeId in lblLabelDict.Keys)
        //{
        //    Label lbl = lblLabelDict[TypeId];
        //    lbl.Text = lblTextDict[TypeId];
        //    TextBox tb = tbDict[TypeId];
        //    try
        //    {
        //        HealthRecordItem item = search.GetSingleItem(TypeId, HealthRecordItemSections.All);
        //        tb.Text = item.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //        tb.Text = "No info available";
        //    }

        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(selectedParticipant.HVPersonID);
        HealthRecordAccessor accessor = new HealthRecordAccessor(conn, selectedParticipant.HVRecordID);

        double weight = 300.0;
        Weight w = new Weight(
            new HealthServiceDateTime(DateTime.Now),
            new WeightValue(weight * 1.6, new DisplayValue(weight, "lbs", "lbs")));
        accessor.NewItem(w);
        //Allergy allergy = new Allergy(
        //CodableValue val = new CodableValue(tbAllergies.Text)
        //Allergy allergy = new Allergy(val);
        //accessor.NewItem(allergy);
    }
}