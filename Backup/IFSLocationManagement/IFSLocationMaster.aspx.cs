using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BW_WebApp.DataManagers;

namespace BW_WebApp.IFSLocationManagement
{
    public partial class IFSLocationMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //btnViewDetail.Click += new EventHandler(btnViewDetail_Click);
            //btnCloseDetail.Click += new EventHandler(btnCloseDetail_Click);
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            btnUnFreeze.Click +=new EventHandler(btnUnFreeze_Click);
            MainGrid.RowDataBound+=new GridViewRowEventHandler(MainGrid_RowDataBound);
            MainGrid.RowCommand +=new GridViewCommandEventHandler(MainGrid_RowCommand);
            if (!IsPostBack)
            {
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;

                drpAddStatus.DataValueField = "MasterIFSLocationStatusID";
                drpAddStatus.DataTextField = "Status";
                drpAddPurpose.DataValueField = "MasterIFSLocationPurposeID";
                drpAddPurpose.DataTextField = "Purpose";
                drpEditStatus.DataValueField = "MasterIFSLocationStatusID";
                drpEditStatus.DataTextField = "Status";
                drpEditPurpose.DataValueField = "MasterIFSLocationPurposeID";
                drpEditPurpose.DataTextField = "Purpose";
                txtSEG1.Focus();

                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    var Status = from x in ctx.MasterIFSLocationStatus.OrderBy(y => y.Status) select x;
                    var Purpose = from x in ctx.MasterIFSLocationPurposes.OrderBy(y => y.Purpose) select x;

                    drpAddStatus.DataSource = Status.ToList();
                    drpEditStatus.DataSource = Status.ToList();
                    drpAddPurpose.DataSource = Purpose.ToList();
                    drpEditPurpose.DataSource = Purpose.ToList();
                    drpAddStatus.DataBind();
                    drpEditStatus.DataBind();
                    drpAddPurpose.DataBind();
                    drpEditPurpose.DataBind();
                }


                
            }
        }


        void MainGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
            }
            else if (e.CommandName != "Page")
            {
                ImageButton bbutton = (ImageButton)e.CommandSource;

                if (bbutton.ID.ToUpper() == "IMGFILTERONLOCATION")
                {
                    txtSEG1.Text = bbutton.CommandArgument.Substring(0, 3);
                    txtSEG2.Text = bbutton.CommandArgument.Substring(4, 3);
                    txtSEG3.Text = bbutton.CommandArgument.Substring(8, 3);
                    txtSEG4.Text = bbutton.CommandArgument.Substring(12, 3);
                }

                #region Download
                string script = "";
                if (bbutton.ID.ToUpper() == "IMGDOWNLOADDEVICES")
                {
                    script = "PrintReport('IFSLOCATIONDEVICES','" + bbutton.CommandArgument + "');";
                    //script = "alert('IFSLOCATIONDEVICES:'" + bbutton.CommandArgument + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", script, true);
                }
                if (bbutton.ID.ToUpper() == "IMGDOWNLOADPARTS")
                {
                    script = "PrintReport('IFSLOCATIONPARTS','" + bbutton.CommandArgument + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", script, true);
                }
                #endregion
            }
        }
        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterIFSLocation data = (MasterIFSLocation)e.Row.DataItem;
                ImageButton bbutton = (ImageButton)e.Row.FindControl("imgFilterOnLocation");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }

                bbutton = (ImageButton)e.Row.FindControl("imgDownloadDevices");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }
                bbutton = (ImageButton)e.Row.FindControl("imgDownloadParts");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }

                bbutton = (ImageButton)e.Row.FindControl("imgViewDevices");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.Visible = false; }
                }

                bbutton = (ImageButton)e.Row.FindControl("imgViewParts");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.Visible = false; }
                }

                


            }
        }

        //void btnCloseDetail_Click(object sender, EventArgs e)
        //{
        //    pndDetail.Visible = false;
        //    pnlMasterList.Visible = true;
        //}

        //void btnViewDetail_Click(object sender, EventArgs e)
        //{
        //    DetailMessage.Text = "";
        //    if (btnViewDetail.Text == "Detail")
        //    {
        //        btnViewDetail.Text = "List";
        //        if (txtSEG1.Text.Length == 0 || txtSEG2.Text.Length == 0 || txtSEG3.Text.Length == 0 || txtSEG4.Text.Length == 0)
        //        {
        //            DetailMessage.Text = "You must supply all sigments to view the detail in that location.";
        //            grdViewDevices.DataSource = null;
        //            grdViewParts.DataSource = null;
        //        }
        //        else
        //        {
        //            IFS_LocationManager lm = new IFS_LocationManager(User.Identity.Name);
        //            grdViewDevices.DataSource = lm.GetDevicesInThisIFSLocation(txtSEG1.Text + "-" + txtSEG2.Text + "-" + txtSEG3.Text + "-" + txtSEG4.Text);
        //            grdViewParts.DataSource = lm.GetPartsInThisIFSLocation(txtSEG1.Text + "-" + txtSEG2.Text + "-" + txtSEG3.Text + "-" + txtSEG4.Text);

        //        }
        //        grdViewDevices.DataBind();
        //        grdViewParts.DataBind();
        //        pndDetail.Visible = true;
        //        pnlMasterList.Visible = false;
        //    }
        //    else
        //    {
        //        btnViewDetail.Text = "Detail";
        //        pndDetail.Visible = false;
        //        pnlMasterList.Visible = true;
        //    }
        //}

        void btnRefresh_Click(object sender, EventArgs e)
        {
            DetailMessage.Text = "";
            UpdateMainGrid();
        }

        protected void UpdateMainGrid()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                UpdateMainGrid(ctx);
            }
        }
        protected void UpdateMainGrid(clsLinqDataContext ctx)
        {
            decimal count = 0;
            DetailMessage.Text = "";
            if (chktxtSEG1.Checked == true && txtSEG1.Text.Length > 0) { count++; }
            if (chktxtSEG2.Checked == true && txtSEG2.Text.Length > 0) { count++; }
            if (chktxtSEG3.Checked == true && txtSEG3.Text.Length > 0) { count++; }
            if (chktxtSEG4.Checked == true && txtSEG4.Text.Length > 0) { count++; }
            if (count < 2) { DetailMessage.Text = "Two or more segments manditory."; return; }

            string SEG1 = txtSEG1.Text;
            string SEG2 = txtSEG2.Text;
            string SEG3 = txtSEG3.Text;
            string SEG4 = txtSEG4.Text;
            if (chktxtSEG1.Checked == false) { SEG1 = ""; }
            if (chktxtSEG2.Checked == false) { SEG2 = ""; }
            if (chktxtSEG3.Checked == false) { SEG3 = ""; }
            if (chktxtSEG4.Checked == false) { SEG4 = ""; }
            MainGrid.DataSource = (from x in ctx.MasterIFSLocations.OrderBy(y => y.IFSLocation)
                                   where (SEG1.Length == 0 || x.SEG1 == SEG1)
                                      && (SEG2.Length == 0 || x.SEG2 == SEG2)
                                      && (SEG3.Length == 0 || x.SEG3 == SEG3)
                                      && (SEG4.Length == 0 || x.SEG4 == SEG4) 
                                   select x
            //new {
            //                           x.MasterIFSLocationID
            //                           , x.MasterIFSLocationPurpose.Purpose
            //                           , x.MasterIFSLocationStatus.Status
            //                           , x.IsWip
            //                           , x.IFSLocation
            //                           , x.Description
            //                           , x.DeviceRollup
            //                           , x.PartRollup
            //                           , x.PickLevel
            //                           , x.IsFrozen
            //                           , x.IFSLocationALT
            //                       }
            );
            MainGrid.DataBind();
        }

        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    MasterIFSLocation qu = ctx.MasterIFSLocations.FirstOrDefault(x => x.MasterIFSLocationID == KeyID);
                    if (qu != null)
                    {
                        hdnEditKeyID.Value = qu.MasterIFSLocationID.ToString();
                        ListItem _ListItem = drpEditPurpose.Items.FindByValue(qu.PurposeID.ToString());
                        if (_ListItem == null) { drpEditPurpose.SelectedIndex = 0; }
                        else { drpEditPurpose.SelectedIndex = drpEditPurpose.Items.IndexOf(_ListItem); }

                        _ListItem = drpEditStatus.Items.FindByValue(qu.StatusID.ToString());
                        if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                        else { drpEditStatus.SelectedIndex = drpEditStatus.Items.IndexOf(_ListItem); }
                        lblSelectedText.Text = string.Format("{0}, ({1}-{2}-{3}-{4} {5})", qu.Description.Trim(), qu.SEG1, qu.SEG2, qu.SEG3, qu.SEG4, qu.SEG5);
                        drpEditPurpose.SelectedIndex = 0;
                        drpEditStatus.SelectedIndex = 0;
                        EditisWhip.Checked = qu.IsWip;
                        EditIFSLocation.Text = qu.IFSLocation;
                        EditDescription.Text = qu.Description;
                        EditDeviceRollup.Text = qu.DeviceRollup;
                        EditPartRollup.Text = qu.PartRollup;
                        EditPickLevel.Text = qu.PickLevel;
                        EditIsFrozen.Checked = false;
                        EditAltIFSLocation.Text = "";
                        btnUnFreeze.Visible = false;
                        EditIsFrozen.Enabled = true; 
                        EditAltIFSLocation.Enabled = true; 
                        if (qu.IsFrozen != null && qu.IsFrozen == true) { EditIsFrozen.Checked = true; btnUnFreeze.Visible = true; EditIsFrozen.Enabled = false; EditAltIFSLocation.Enabled = false; }
                        if (qu.IFSLocationALT != null) { EditAltIFSLocation.Text = qu.IFSLocationALT; }
                    }

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        #region QuesionDetail

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            drpAddPurpose.SelectedIndex = 0;
            drpAddStatus.SelectedIndex = 0;
            AddisWhip.Checked = false;
            AddIFSLocation.Text = "";
            AddDescription.Text = "";
            AddDeviceRollup.Text = "";
            AddPartRollup.Text = "";
            AddPickLevel.Text = "";
            AddIsFrozen.Checked = false;
            AddAltIFSLocation.Text = "";
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            //if (MainGrid.SelectedIndex >= 0)
            //{
            //    decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //    using (clsLinqDataContext ctx = new clsLinqDataContext())
            //    {
            //        MasterIFSLocation qu = ctx.MasterIFSLocations.FirstOrDefault(x => x.MasterIFSLocationID == KeyID);
            //        if (qu != null)
            //        {
            //            ctx.MasterIFSLocations.DeleteOnSubmit(qu);
            //            ctx.SubmitChanges();
            //            UpdateMainGrid(ctx);
            //        }
            //    }
            //}
        }

        void btnUnFreeze_Click(object sender, EventArgs e)
        {
            //IFSLocation ToLocation = new IFSLocation(EditIFSLocation.Text);
            //IFSLocation FromLocation = new IFSLocation(EditAltIFSLocation.Text);
            //if (ToLocation.isFrozen == true)
            //{
            //    using (clsLinqDataContext ctx = new clsLinqDataContext())
            //    {
            //        ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            //        string IP = rm.GetUserIPAddress();
            //        string ErrorMessage = "";
            //        int count = 0;
            //        int Errorcount = 0;
            //        string rMessage = "";
            //        EditIsFrozen.Checked = false;
            //        EditAltIFSLocation.Text = "";
            //        SaveLocation(ctx, ToLocation.ID);
            //        List<ReceiveDetail> RDList = (ctx.ReceiveDetails.Where(x => x.Version == "000" && x.IFSLocation == FromLocation.Text && x.ReceiveDetailStatus.Status.ToUpper() == "ACTIVE")).ToList();
            //        foreach (ReceiveDetail r in RDList)
            //        {
            //            rMessage = rm.IFSLocationUpdate_ESN(r.ESN, ToLocation.Text, IP);
            //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
            //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }

            //        }
            //        string FullMessage = count.ToString() + " Devices Moved";
            //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Moved"; }
            //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
            //        EditMessage.Text = FullMessage;
            //    }
            //}
            //else
            //{
            //    EditIsFrozen.Checked = false;
            //    EditAltIFSLocation.Text = "";
            //    SaveLocation(ToLocation.ID);
            //}
            //pnlMainView.Visible = true;
            //pnlEdit.Visible = false;
        }



        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (hdnEditKeyID.Value.Length == 0) { return; }
            EditMessage.Text = "";
            if (EditIsFrozen.Checked == true && EditAltIFSLocation.Text.Length == 0)
            {
                EditMessage.Text = "If the Location IS FROZEN, you need to enter an Alternate IFS Location";
                return;
            }
            decimal KeyID = decimal.Parse(hdnEditKeyID.Value);
            if (EditAltIFSLocation.Text.Length > 0)
            {
                IFSLocation l = new IFSLocation(EditAltIFSLocation.Text);
                if (l.isValid == false)
                {
                    EditIsFrozen.Enabled = true;
                    EditAltIFSLocation.Enabled = true; 
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Alternate Location is Invalid');", true);
                    return;
                }
            }
            SaveLocation(KeyID);
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        private void SaveLocation(decimal KeyID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                SaveLocation(ctx, KeyID);
            }
        }
        private void SaveLocation(clsLinqDataContext ctx, decimal KeyID)
        {
            if (EditAltIFSLocation.Text.Length > 0)
            {
                IFSLocation l = new IFSLocation(EditAltIFSLocation.Text);
                if (l.isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Alternate Location is Invalid');", true);
                    return;
                }
            }
            try
            {
                MasterIFSLocation qu = ctx.MasterIFSLocations.FirstOrDefault(x => x.MasterIFSLocationID == KeyID);
                if (qu != null)
                {
                    qu.StatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
                    qu.PurposeID = decimal.Parse(drpEditPurpose.SelectedItem.Value);
                    qu.CreateUser = User.Identity.Name;
                    qu.Description = EditDescription.Text;
                    qu.DeviceRollup = EditDeviceRollup.Text;
                    qu.IFSLocation = EditIFSLocation.Text;
                    qu.IsWip = EditisWhip.Checked;
                    qu.LastUpdateDate = DateTime.Now;
                    qu.LastUpdateUser = User.Identity.Name;
                    qu.PartRollup = EditPartRollup.Text;
                    qu.PickLevel = EditPickLevel.Text;
                    qu.IsFrozen = EditIsFrozen.Checked;
                    qu.IFSLocationALT = EditAltIFSLocation.Text;

                    qu.SEG1 = (EditIFSLocation.Text.Length < 3) ? "" : EditIFSLocation.Text.Substring(0, 3);
                    qu.SEG2 = (EditIFSLocation.Text.Length < 7) ? "" : EditIFSLocation.Text.Substring(4, 3);
                    qu.SEG3 = (EditIFSLocation.Text.Length < 11) ? "" : EditIFSLocation.Text.Substring(8, 3);
                    qu.SEG4 = (EditIFSLocation.Text.Length < 15) ? "" : EditIFSLocation.Text.Substring(12, 3);
                    ctx.SubmitChanges();
                    UpdateMainGrid(ctx);
                }
            }
            catch (FormatException ex)
            {
                EditMessage.Text = ex.Message;
                // some code that handles the FormatException
            }
            catch (InvalidOperationException ex)
            {
                EditMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                EditMessage.Text = ex.Message;
            }
            finally
            {
                //any clean up code. This portion is executed regardless an exception is thrown on not
            }
        }

        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }

        #endregion


        protected void AddOK_Click(object sender, EventArgs e)
        {
            AddMessage.Text = "";

            if (AddIFSLocation.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No Location Given!');", true);
                AddMessage.Text = "No Location Given!";
                return;
            }
            if (AddIFSLocation.Text.Length > 0)
            {
                IFSLocation l = new IFSLocation(AddIFSLocation.Text);
                if (l.isValid == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Location already on file!');", true);
                    AddMessage.Text = "Location already on file!";
                    return;
                }
            }
            if (AddIsFrozen.Checked == true && AddAltIFSLocation.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('If the Location IS FROZEN, you need to enter an Alternate IFS Location.);", true);
                AddMessage.Text = "If the Location IS FROZEN, you need to enter an Alternate IFS Location";
                return;
            }
            if (AddAltIFSLocation.Text.Length > 0)
            {
                IFSLocation l = new IFSLocation(AddAltIFSLocation.Text);
                if (l.isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Alternate Location is Invalid');", true);
                    AddMessage.Text = "Alternate Location is Invalid";
                    return;
                }
            }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                try
                {
                    MasterIFSLocation qu = new MasterIFSLocation();
                    qu.StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
                    qu.PurposeID = decimal.Parse(drpAddPurpose.SelectedItem.Value);
                    qu.CreateUser = User.Identity.Name;
                    qu.Description = AddDescription.Text;
                    qu.DeviceRollup = AddDeviceRollup.Text;
                    qu.IFSLocation = AddIFSLocation.Text;
                    qu.IsWip = AddisWhip.Checked;
                    qu.IsDevice = false;
                    qu.IsPart = false;
                    qu.LastUpdateDate = DateTime.Now;
                    qu.LastUpdateUser = User.Identity.Name;
                    qu.PartRollup = AddPartRollup.Text;
                    qu.PickLevel = AddPickLevel.Text;
                    qu.IFSLocationALT = AddAltIFSLocation.Text;
                    qu.IsFrozen = AddIsFrozen.Checked;
                    qu.SEG1 = (AddIFSLocation.Text.Length < 3) ? "" : AddIFSLocation.Text.Substring(0, 3);
                    qu.SEG2 = (AddIFSLocation.Text.Length < 7) ? "" : AddIFSLocation.Text.Substring(4, 3);
                    qu.SEG3 = (AddIFSLocation.Text.Length < 11) ? "" : AddIFSLocation.Text.Substring(8, 3);
                    qu.SEG4 = (AddIFSLocation.Text.Length < 15) ? "" : AddIFSLocation.Text.Substring(12, 3);
                    ctx.SubmitChanges();
                    ctx.MasterIFSLocations.InsertOnSubmit(qu);
                    ctx.SubmitChanges();
                    UpdateMainGrid(ctx);
                    pnlMainView.Visible = true;
                    pnlAdd.Visible = false;
                }
                catch (FormatException ex)
                {
                    AddMessage.Text = ex.Message;
                    // some code that handles the FormatException
                }
                catch (InvalidOperationException ex)
                {
                    AddMessage.Text = ex.Message;
                }
                catch (Exception ex)
                {
                    AddMessage.Text = ex.Message;
                }
                finally
                {
                    //any clean up code. This portion is executed regardless an exception is thrown on not
                }
            }
        }

        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }



    }
}