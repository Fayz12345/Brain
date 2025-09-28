using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
// using DAL;
using Syncfusion.Web.UI.WebControls.Shared;
using Syncfusion.XlsIO;

using System.Text;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterSKU : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        //ClientManager CM = null;
        //clsLinqDataContext ctx = null;
        decimal KeyID = -1;
        MasterCarrierManufacturerModelColourManager cm = null;
        vwMasterSKUTable cl = null;



        protected void Page_Load(object sender, EventArgs e)
        {

            //CM = new ClientManager(User.Identity.Name);
            cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);

            KeyID = -1;
            cl = null;
            if (MainGrid.SelectedValue != null)
            {
                KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                cl = cm.GetMasterSKUView(KeyID);
            }

            //btnMoveClientLocation.Click += new EventHandler(btnMoveClientLocation_Click);

            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            //ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);
            drpEditCarrier.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);
            drpEditManufacturer.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);
            drpEditModel.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);

            drpAddCarrier.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);
            drpAddManufacturer.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);
            drpAddModel.SelectedIndexChanged += new EventHandler(drpEdit_SelectedIndexChanged);
            if (!IsPostBack)
            {

                //MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                drpAddStatus.DataValueField = "MasterSKUStatusID";
                drpAddStatus.DataTextField = "Status";
                drpEditStatus.DataValueField = "MasterSKUStatusID";
                drpEditStatus.DataTextField = "Status";
                drpAddStatus.DataSource = cm.GetSKUStatusList();
                drpEditStatus.DataSource = cm.GetSKUStatusList();
                drpAddStatus.DataBind();
                drpEditStatus.DataBind();

                drpAddManufacturer.DataValueField = "ID";
                drpAddManufacturer.DataTextField = "Desc";
                drpAddManufacturer.DataSource = cm.GetMasterManufacturerList();
                drpAddManufacturer.DataBind();
                drpAddModel.DataValueField = "ID";
                drpAddModel.DataTextField = "Desc";
                drpAddModel.DataSource = cm.GetMasterModelList();
                drpAddModel.DataBind();
                drpAddCarrier.DataValueField = "ID";
                drpAddCarrier.DataTextField = "Desc";
                drpAddCarrier.DataSource = cm.GetMasterCarrierList();
                drpAddCarrier.DataBind();
                drpAddColour.DataValueField = "ID";
                drpAddColour.DataTextField = "Desc";
                drpAddColour.DataSource = cm.GetMasterColourList();
                drpAddColour.DataBind();

                drpEditManufacturer.DataValueField = "ID";
                drpEditManufacturer.DataTextField = "Desc";
                drpEditManufacturer.DataSource = cm.GetMasterManufacturerList();
                drpEditManufacturer.DataBind();
                drpEditModel.DataValueField = "ID";
                drpEditModel.DataTextField = "Desc";
                drpEditModel.DataSource = cm.GetMasterModelList();
                drpEditModel.DataBind();
                drpEditCarrier.DataValueField = "ID";
                drpEditCarrier.DataTextField = "Desc";
                drpEditCarrier.DataSource = cm.GetMasterCarrierList();
                drpEditCarrier.DataBind();
                drpEditColour.DataValueField = "ID";
                drpEditColour.DataTextField = "Desc";
                drpEditColour.DataSource = cm.GetMasterColourList();
                drpEditColour.DataBind();


                UpdateMainGrid();
            }
        }

        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void UpdateMainGrid()
        {
            //MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            MainGrid.DataSource = cm.GetSKUDataList();
            MainGrid.DataBind();
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                //hdnSelectedClientID.Value = MainGrid.SelectedValue.ToString();
                if (MainGrid.SelectedValue != null)
                {
                    KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                    cl = cm.GetMasterSKUView(KeyID);
                    lblRecordSubtitle.Text = cl.Status + " / " + cl.SKU + " / " + cl.Carrier + " / " + cl.Manufacturer + " / " + cl.Model + " / " + cl.Colour; 
                }
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }
        //protected void ChildGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ChildGrid.SelectedIndex >= 0)
        //    {
        //        decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //        btnEditLocation.Visible = btnEdit.Visible;
        //        btnDeleteLocation.Visible = btnDelete.Visible;
        //        btnAddLocation.Visible = btnAdd.Visible;
        //        btnMoveLocation.Visible = btnDelete.Visible;
        //    }
        //    else
        //    {
        //        btnAddLocation.Visible = btnAdd.Visible;
        //        btnEditLocation.Visible = false;
        //        btnDeleteLocation.Visible = false;
        //        btnMoveLocation.Visible = false;
        //    }
        //}
        //protected void UpdateChildGrid(decimal KeyID)
        //{
        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    ChildGrid.DataSource = cm.GetClientLocationssDefered(KeyID);
        //    ChildGrid.DataBind();
        //    ChildGrid.SelectedIndex = -1;
        //    btnEditLocation.Visible = false;
        //    btnDeleteLocation.Visible = false;
        //    btnMoveLocation.Visible = false;
        //    #region QuestionAnswerRestrictions
        //    //UpdateClientQuestionRestrictionGrid();
        //    //UpdateClientAnswerRestrictionGrid();
        //    #endregion

        //}

        #region SKU
        protected void ReadyAdd()
        {
            AddOK.Enabled = false;
            AddSKU.Text = "";
            AddMessage.Text = "";
            drpAddStatus.SelectedIndex = 0;
            lblRecordTitle.Text = "Maintenance Master SKU"; 
            //drpAddManufacturer.SelectedIndex = 0;
            //drpAddModel.SelectedIndex = 0;
            //drpAddCarrier.SelectedIndex = 0;
            //drpAddColour.SelectedIndex = 0;
            drpEdit_SelectedIndexChanged(drpAddCarrier, new EventArgs());
        }
        protected void ReadyEdit()
        {
            //decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            //MasterSKU cl = cm.GetMasterSKU(KeyID);
            if (cl != null)
            {
                EditOK.Enabled = true;
                EditVerify.Enabled = false;
                EditSKU.Text = cl.SKU;
                EditNewSKU.Text = "";
                EditMessage.Text = "";
                lblRecordSubtitle.Text = cl.Status + " / " + cl.SKU + " / " + cl.Carrier + " / " + cl.Manufacturer + " / " + cl.Model + " / " + cl.Colour;
                EditOriginal.Text = cl.Status + " / " + cl.SKU + " / " + cl.Carrier + " / " + cl.Manufacturer + " / " + cl.Model + " / " + cl.Colour;

                ListItem _ListItem = drpEditStatus.Items.FindByValue(cl.StatusID.ToString());
                if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                else { drpEditStatus.SelectedIndex = drpEditStatus.Items.IndexOf(_ListItem); }

                _ListItem = drpEditManufacturer.Items.FindByValue(cl.ManufacturerID.ToString());
                if (_ListItem == null) { drpEditManufacturer.SelectedIndex = 0; }
                else { drpEditManufacturer.SelectedIndex = drpEditManufacturer.Items.IndexOf(_ListItem); }

                _ListItem = drpEditModel.Items.FindByValue(cl.ModelID.ToString());
                if (_ListItem == null) { drpEditModel.SelectedIndex = 0; }
                else { drpEditModel.SelectedIndex = drpEditModel.Items.IndexOf(_ListItem); }

                _ListItem = drpEditCarrier.Items.FindByValue(cl.CarrierID.ToString());
                if (_ListItem == null) { drpEditCarrier.SelectedIndex = 0; }
                else { drpEditCarrier.SelectedIndex = drpEditCarrier.Items.IndexOf(_ListItem); }

                _ListItem = drpEditColour.Items.FindByValue(cl.ColourID.ToString());
                if (_ListItem == null) { drpEditColour.SelectedIndex = 0; }
                else { drpEditColour.SelectedIndex = drpEditColour.Items.IndexOf(_ListItem); }

                drpEdit_SelectedIndexChanged(drpEditCarrier, new EventArgs());
            }
        }

        void drpEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            decimal ColourID = -1;

            if (list.ID.Contains("Edit") == true)
            {
                EditOK.Enabled = false;
                EditVerify.Enabled = false;
                EditMessage.Text = "";
                EditNewSKU.Text = "";
                if (list.ID == "drpEditCarrier")
                {
                    if (drpEditCarrier.SelectedItem != null && decimal.TryParse(drpEditCarrier.SelectedItem.Value, out CarrierID) == true)
                    {
                        LoadValidValue(drpEditManufacturer, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpEditManufacturer, e);
                    }
                    else
                    {
                        drpEditManufacturer.Items.Clear();
                        drpEditModel.Items.Clear();
                        drpEditColour.Items.Clear();
                    }
                }

                if (list.ID == "drpEditManufacturer")
                {
                    if (drpEditCarrier.SelectedItem != null && decimal.TryParse(drpEditCarrier.SelectedItem.Value, out CarrierID) == true
                     && drpEditManufacturer.SelectedItem != null && decimal.TryParse(drpEditManufacturer.SelectedItem.Value, out ManufacturerID) == true)
                    {
                        LoadValidValue(drpEditModel, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpEditModel, e);
                    }
                    else
                    {
                        drpEditModel.Items.Clear();
                        drpEditColour.Items.Clear();
                    }
                }
                if (list.ID == "drpEditModel")
                {
                    if (drpEditCarrier.SelectedItem != null && decimal.TryParse(drpEditCarrier.SelectedItem.Value, out CarrierID) == true
                     && drpEditManufacturer.SelectedItem != null && decimal.TryParse(drpEditManufacturer.SelectedItem.Value, out ManufacturerID) == true
                     && drpEditModel.SelectedItem != null && decimal.TryParse(drpEditModel.SelectedItem.Value, out ModelID) == true)
                    {
                        LoadValidValue(drpEditColour, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpEditColour, e);
                    }
                    else
                    {
                        drpEditColour.Items.Clear();
                    }
                }

                if (list.ID == "drpEditColour")
                {
                    EditVerify.Enabled = true;
                }
            }
            if (list.ID.Contains("Add") == true)
            {
                AddOK.Enabled = false;
                AddVerify.Enabled = false;
                AddMessage.Text = "";
                AddSKU.Text = "";
                if (list.ID == "drpAddCarrier")
                {
                    if (drpAddCarrier.SelectedItem != null && decimal.TryParse(drpAddCarrier.SelectedItem.Value, out CarrierID) == true)
                    {
                        LoadValidValue(drpAddManufacturer, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpAddManufacturer, e);
                    }
                    else
                    {
                        drpAddManufacturer.Items.Clear();
                        drpAddModel.Items.Clear();
                        drpAddColour.Items.Clear();
                    }
                }

                if (list.ID == "drpAddManufacturer")
                {
                    if (drpAddCarrier.SelectedItem != null && decimal.TryParse(drpAddCarrier.SelectedItem.Value, out CarrierID) == true
                     && drpAddManufacturer.SelectedItem != null && decimal.TryParse(drpAddManufacturer.SelectedItem.Value, out ManufacturerID) == true)
                    {
                        LoadValidValue(drpAddModel, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpAddModel, e);
                    }
                    else
                    {
                        drpAddModel.Items.Clear();
                        drpAddColour.Items.Clear();
                    }
                }
                if (list.ID == "drpAddModel")
                {
                    if (drpAddCarrier.SelectedItem != null && decimal.TryParse(drpAddCarrier.SelectedItem.Value, out CarrierID) == true
                     && drpAddManufacturer.SelectedItem != null && decimal.TryParse(drpAddManufacturer.SelectedItem.Value, out ManufacturerID) == true
                     && drpAddModel.SelectedItem != null && decimal.TryParse(drpAddModel.SelectedItem.Value, out ModelID) == true)
                    {
                        LoadValidValue(drpAddColour, CarrierID, ManufacturerID, ModelID, ColourID);
                        drpEdit_SelectedIndexChanged(drpAddColour, e);
                    }
                    else
                    {
                        drpAddColour.Items.Clear();
                    }
                }

                if (list.ID == "drpAddColour")
                {
                    AddVerify.Enabled = true;
                }
            }


        }

        void LoadValidValue(DropDownList Target,decimal CarrierID, decimal ManufacturerID, decimal ModelID,  decimal ColourID)
        {
            //MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            Target.Items.Clear();
            Target.SelectedIndex = -1;
            if (Target.ID == "drpEditManufacturer" || Target.ID == "drpAddManufacturer")
            {
                List<PairIDValue> data = cm.GetManufacturerListRestricted(CarrierID);
                Target.DataSource = data;
                Target.DataBind();
                if (data.Count > 0) { Target.SelectedIndex = 0; }
            }
            if (Target.ID == "drpEditModel" || Target.ID == "drpAddModel")
            {
                List<PairIDValue> data = cm.GetModelListRestricted(CarrierID, ManufacturerID);
                Target.DataSource = data;
                Target.DataBind();
                if (data.Count > 0) { Target.SelectedIndex = 0; }
            }
            if (Target.ID == "drpEditColour" || Target.ID == "drpAddColour")
            {
                List<PairIDValue> data = cm.GetColourListRestricted(CarrierID, ManufacturerID, ModelID);
                Target.DataSource = data;
                Target.DataBind();
                if (data.Count > 0) { Target.SelectedIndex = 0; }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ////AddStatus.Text = "";
            ReadyAdd();
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //return;
            ReadyEdit();
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                //decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //ClientManager cm = new ClientManager(User.Identity.Name);
                cm.DeleteMasterSku(KeyID);
                UpdateMainGrid();
                //KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //UpdateChildGrid(KeyID);
            }
        }

        protected void AddVerify_Click(object sender, EventArgs e)
        {
            decimal KeyID = -1;                // decimal.Parse(MainGrid.SelectedValue.ToString());
            MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            decimal StatusID = 0;
            decimal ManufacturerID = 0;
            decimal ModelID = 0;
            decimal CarrierID = 0;
            decimal ColourID = 0;
            StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            ManufacturerID = decimal.Parse(drpAddManufacturer.SelectedItem.Value);
            ModelID = decimal.Parse(drpAddModel.SelectedItem.Value);
            CarrierID = decimal.Parse(drpAddCarrier.SelectedItem.Value);
            ColourID = decimal.Parse(drpAddColour.SelectedItem.Value);

            if (cm.IsMasterSKUUnique(KeyID, ManufacturerID, ModelID, CarrierID, ColourID) == true)
            {
                AddMessage.Text = "SKU FOUND, already on file";
                AddOK.Enabled = false;
                return;
            }

            AddSKU.Text = cm.BuildSKU(ManufacturerID, ModelID, CarrierID, ColourID);
            AddMessage.Text = "SKU Not found, OK to Add";
            AddOK.Enabled = true;

        }
        protected void AddOK_Click(object sender, EventArgs e)
        {
            EditOK.Enabled = false;
            EditVerify.Enabled = false;

            MasterSKU sku = new MasterSKU();
            decimal StatusID = 0;
            decimal ManufacturerID = 0;
            decimal ModelID = 0;
            decimal CarrierID = 0;
            decimal ColourID = 0;
            string SKU = "";
            StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            ManufacturerID = decimal.Parse(drpAddManufacturer.SelectedItem.Value);
            ModelID = decimal.Parse(drpAddModel.SelectedItem.Value);
            CarrierID = decimal.Parse(drpAddCarrier.SelectedItem.Value);
            ColourID = decimal.Parse(drpAddColour.SelectedItem.Value);
            SKU = cm.BuildSKU(ManufacturerID, ModelID, CarrierID, ColourID);

            sku.MasterSKUID = KeyID;
            sku.StatusID = StatusID;
            sku.ManufacturerID = ManufacturerID;
            sku.ModelID = ModelID;
            sku.CarrierID = CarrierID;
            sku.ColourID = ColourID;
            sku.SKU = SKU;

            if (cm.AddMasterSku(sku) == true)
            {
                AddMessage.Text = "SKU Saved";
            }
            else
            {
                AddMessage.Text = "SKU Not Saved";
            }
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }

        protected void EditVerify_Click(object sender, EventArgs e)
        {
            //decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            decimal StatusID = 0;
            decimal ManufacturerID = 0;
            decimal ModelID = 0;
            decimal CarrierID = 0;
            decimal ColourID = 0;
            StatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
            ManufacturerID = decimal.Parse(drpEditManufacturer.SelectedItem.Value);
            ModelID = decimal.Parse(drpEditModel.SelectedItem.Value);
            CarrierID = decimal.Parse(drpEditCarrier.SelectedItem.Value);
            ColourID = decimal.Parse(drpEditColour.SelectedItem.Value);

            if (cm.IsMasterSKUUnique(KeyID, ManufacturerID, ModelID, CarrierID, ColourID) == true)
            {
                EditMessage.Text = "SKU FOUND, already on file";
                EditOK.Enabled = false;
                return;
            }

            EditNewSKU.Text = cm.BuildSKU(ManufacturerID, ModelID, CarrierID, ColourID);
            EditMessage.Text = "SKU Not found, OK to Add";
            EditOK.Enabled = true;

        }
        protected void EditOK_Click(object sender, EventArgs e)
        {
            EditOK.Enabled = false;
            EditVerify.Enabled = false;

            MasterSKU sku = new MasterSKU();
            decimal StatusID = 0;
            decimal ManufacturerID = 0;
            decimal ModelID = 0;
            decimal CarrierID = 0;
            decimal ColourID = 0;
            string SKU = "";
            StatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
            ManufacturerID = decimal.Parse(drpEditManufacturer.SelectedItem.Value);
            ModelID = decimal.Parse(drpEditModel.SelectedItem.Value);
            CarrierID = decimal.Parse(drpEditCarrier.SelectedItem.Value);
            ColourID = decimal.Parse(drpEditColour.SelectedItem.Value);
            SKU = cm.BuildSKU(ManufacturerID, ModelID, CarrierID, ColourID);

            sku.MasterSKUID = KeyID;
            sku.StatusID = StatusID;
            sku.ManufacturerID = ManufacturerID;
            sku.ModelID = ModelID;
            sku.CarrierID = CarrierID;
            sku.ColourID = ColourID;
            sku.SKU = SKU;

            if (cm.UpdateMasterSku(sku) == true)
            {
                EditMessage.Text = "SKU Saved";
            }
            else
            {
                EditMessage.Text = "SKU Not Saved";
            }
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;


        }
        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        #endregion

    }
}