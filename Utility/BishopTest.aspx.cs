using System;
using BW_WebApp.BishopIntegration;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Utility
{
    public partial class BishopTest : System.Web.UI.Page
    {
        clsLog log;
        //List<string> attributeclonelist = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnGetCatalogueJSON.Click += new EventHandler(btnGetCatalogueJSON_Click);
            btnGetCatalogueXML.Click += new EventHandler(btnGetCatalogueXML_Click);
            btnPickListJSON.Click += new EventHandler(btnPickListJSON_Click);
            btnPickListXML.Click += new EventHandler(btnPickListXML_Click);
            btnPickListStatusJSON.Click += new EventHandler(btnPickListStatusJSON_Click);
            btnPickListStatusXML.Click += new EventHandler(btnPickListStatusXML_Click);
            btnGetCatalogueListJSON.Click += new EventHandler(btnGetCatalogueListJSON_Click);
            btnGetCatalogueListXML.Click += new EventHandler(btnGetCatalogueListXML_Click);
            if (!IsPostBack)
            {
                txtSKUList.Text = "ALC-BEL-40A-          -  BLK-  -B -  -           ,ALC-BEL-40A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -W -  -           ,ALC-KOO-170-          -  BLK-  -A -  -           ,ALC-KOO-170-          -  BLK-  -B -  -           ,ALC-KOO-170-          -  BLK-  -C -  -           ,ALC-KOO-170-          -  BLK-  -d -  -           ";
                // Fill any lists etc.
            }
        }

        void btnGetCatalogueListXML_Click(object sender, EventArgs e)
        {
            GetMasterCatalogue c = new GetMasterCatalogue(txtSKUList.Text);
            txtOutput.Text = c.XMLData();
        }

        void btnGetCatalogueListJSON_Click(object sender, EventArgs e)
        {
            GetMasterCatalogue c = new GetMasterCatalogue(txtSKUList.Text);
            txtOutput.Text = c.JSONData();
        }

        void btnPickListStatusXML_Click(object sender, EventArgs e)
        {
            PickListSold c = new PickListSold();
            txtOutput.Text = c.XMLData();
        }

        void btnPickListStatusJSON_Click(object sender, EventArgs e)
        {
            PickListSold c = new PickListSold();
            txtOutput.Text = c.JSONData();
        }

        void btnPickListXML_Click(object sender, EventArgs e)
        {
            PickList c = new PickList();
            txtOutput.Text = c.XMLData();
        }

        void btnPickListJSON_Click(object sender, EventArgs e)
        {
            PickList c = new PickList();
            txtOutput.Text = c.JSONData();
        }

        void btnGetCatalogueXML_Click(object sender, EventArgs e)
        {
            GetMasterCatalogueSlim c = new GetMasterCatalogueSlim();
            txtOutput.Text = c.XMLData();
        }

        void btnGetCatalogueJSON_Click(object sender, EventArgs e)
        {
            GetMasterCatalogueSlim c = new GetMasterCatalogueSlim("SAM-null-null-null-BLK-null-null-null-null-null");
            txtOutput.Text = c.JSONData();
        }







    }
}