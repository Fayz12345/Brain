using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_DiscrepancyDropDownRelation : System.Web.UI.Page
    {
        private string blank = "&nbsp;";

        protected void Page_Load(object sender, EventArgs e)
        {

            drpType.SelectedIndexChanged += new EventHandler(drpType_SelectedIndexChanged);
            drpDiscrepancy2.SelectedIndexChanged += new EventHandler(drpDiscrepancy2_SelectedIndexChanged);
            btnSaveTypeDisc.Click += new EventHandler(btnSaveTypeDisc_Click);
            btnSaveDiscOut.Click += new EventHandler(btnSaveDiscOut_Click);
            if (IsPostBack == false)
            {
                LoadDropDownValues();
            }
        }



        void btnSaveDiscOut_Click(object sender, EventArgs e)
        {
            decimal MasterID = -1;
            decimal SlaveID = -1;
            List<decimal> Keepers = new List<decimal>();
            if (decimal.TryParse(drpDiscrepancy2.SelectedItem.Value, out MasterID) == false) { MasterID = -1; }

            foreach (ListItem i in chkOutcome.Items)
            {
                if (i.Selected == true)
                {
                    if (decimal.TryParse(i.Value, out SlaveID) == true) { Keepers.Add(SlaveID); }
                }
            }

            if (MasterID > 0)
            {
                DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
                lblSaveDiscOut.Text = dm.SaveRelations(MasterID, Keepers);
            }
            //dm.SaveRelations(
            //throw new NotImplementedException();
            // Save the Discrepancy/Outcome relation
        }

        void btnSaveTypeDisc_Click(object sender, EventArgs e)
        {
            decimal MasterID = -1;
            decimal SlaveID = -1;
            List<decimal> Keepers = new List<decimal>();
            if (decimal.TryParse(drpType.SelectedItem.Value, out MasterID) == false) { MasterID = -1; }

            foreach (ListItem i in chkDiscrepancy1.Items)
            {
                if (i.Selected == true)
                {
                    if (decimal.TryParse(i.Value, out SlaveID) == true) { Keepers.Add(SlaveID); }
                }
            }

            if (MasterID > 0)
            {
                DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
                lblSaveTypeDisc.Text = dm.SaveRelations(MasterID, Keepers);
            }
        }

 
        void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSaveTypeDisc.Text = "";
            UpdateDiscrepancy();
         }
       void drpDiscrepancy2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem i in chkOutcome.Items) { i.Selected = false; }
            lblSaveDiscOut.Text = "";
            UpdateOutCome();
        }

        private void UpdateDiscrepancy()
        {
            decimal MasterID = -1;
            decimal SlaveID = -1;
            List<decimal> Keepers = new List<decimal>();
            if (decimal.TryParse(drpType.SelectedItem.Value, out MasterID) == false) { MasterID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            Keepers = dm.GetSlaves(MasterID);
            foreach (ListItem i in chkDiscrepancy1.Items)
            {
                if (decimal.TryParse(i.Value, out SlaveID) == false) { SlaveID = -1; }
                i.Selected = Keepers.Contains(SlaveID);
            }
        }
        private void UpdateOutCome()
        {
            decimal MasterID = -1;
            decimal SlaveID = -1;
            List<decimal> Keepers = new List<decimal>();
            if (decimal.TryParse(drpDiscrepancy2.SelectedItem.Value, out MasterID) == false) { MasterID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            Keepers = dm.GetSlaves(MasterID);
            foreach (ListItem i in chkOutcome.Items)
            {
                if (decimal.TryParse(i.Value, out SlaveID) == false) { SlaveID = -1; }
                i.Selected = Keepers.Contains(SlaveID);
            }
        }



        void LoadDropDownValues()
        {
            drpType.Items.Clear();
            chkDiscrepancy1.Items.Clear();
            drpDiscrepancy2.Items.Clear();
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();

            LO = qm.GetQuestionOptionList("Discr Type").OrderBy(x=> x.Sequence).ThenBy(x=> x.OptionText).ToList();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                drpType.Items.Add(li);
            }
            drpType.SelectedIndex = 0;


            LO = qm.GetQuestionOptionList("Discr Desc").OrderBy(x => x.Sequence).ThenBy(x => x.OptionText).ToList();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
                chkDiscrepancy1.Items.Add(li);
                drpDiscrepancy2.Items.Add(li2);
            }
            drpDiscrepancy2.SelectedIndex = 0;


            LO = qm.GetQuestionOptionList("Discr OutCome").OrderBy(x => x.Sequence).ThenBy(x => x.OptionText).ToList();
            chkOutcome.Items.Clear();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                chkOutcome.Items.Add(li);
            }

            UpdateDiscrepancy();
            UpdateOutCome();

        }
    }
}