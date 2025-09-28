using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
//using System.Diagnostics;


//using GMPDemo;
//using BusinessLayer;
//using Factory_DataModel;
using BW_WebApp.Classes;

namespace BW_WebApp.DataManagers
{
    public class DashboardManager
    {


        public DashboardManager()
        {

        }
        #region Numeric Helpers
 
        #endregion
        #region Date Helpers

        public string Get_Repair01_ProcessString
        { get { return "[Bridge Repair],[MSC Repair Handling],[Product Placement]"; } }

        #endregion
        #region Helpers
        #endregion
        #region GetDataHelpers

        public List<Repair_01> Get_Repair01Data(string Today, string wkdaystoreport)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardRepair_01(Today, days).ToList();
                foreach (Repair_01 b in x)
                {
                    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                    b.Product_Placement = b.Product_Placement ?? 0;
                    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                }
                return x;
            }
            //return new List<Repair_01>();
        }
        public List<Repari_01Grid> Get_Repair01DataGrid(string Today, string wkdaystoreport)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardRepair_01_Grid(Today, days).ToList();
                //foreach (Repari_01Grid b in x)
                //{
                //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                //    b.Product_Placement = b.Product_Placement ?? 0;
                //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                //}
                return x;
            }
            //return new List<Repair_01>();
        }
        public List<Repair_01GridValue> Get_Repair01DataGridValues(string Today, string wkdaystoreport)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardRepair_01_GridValue(Today, days).ToList();
                //foreach (Repari_01Grid b in x)
                //{
                //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                //    b.Product_Placement = b.Product_Placement ?? 0;
                //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                //}
                return x;
            }
            //return new List<Repair_01>();
        }
        public List<Repair_01GridValue> Get_Repair01DataGridValuesFiltered(string Today, string wkdaystoreport, string RoleFilter)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardRepair_01_GridValueFiltered(Today, days, RoleFilter).ToList();
                //foreach (Repari_01Grid b in x)
                //{
                //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                //    b.Product_Placement = b.Product_Placement ?? 0;
                //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                //}
                return x;
            }
            //return new List<Repair_01>();
        }


        //public List<Repari_02Grid> Get_Repair02DataGrid(string Today, string wkdaystoreport)
        //{
        //    int days = 0;
        //    if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        var x = ctx.GetDashboardRepair_02_Grid(Today, days).ToList();
        //        //foreach (Repari_01Grid b in x)
        //        //{
        //        //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
        //        //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
        //        //    b.Product_Placement = b.Product_Placement ?? 0;
        //        //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
        //        //}
        //        return x;
        //    }
        //    //return new List<Repair_01>();
        //}
        //public List<Repair_02GridValue> Get_Repair02DataGridValues(string Today, string wkdaystoreport)
        //{
        //    int days = 0;
        //    if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        var x = ctx.GetDashboardRepair_02_GridValue(Today, days).ToList();
        //        //foreach (Repari_01Grid b in x)
        //        //{
        //        //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
        //        //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
        //        //    b.Product_Placement = b.Product_Placement ?? 0;
        //        //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
        //        //}
        //        return x;
        //    }
        //    //return new List<Repair_01>();
        //}
        public List<Repair_02GridValue> Get_Repair02DataGridValuesFiltered(string Today, string wkdaystoreport, string RoleFilter)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardRepair_02_GridValueFiltered(Today, days, RoleFilter).ToList();
                //foreach (Repari_01Grid b in x)
                //{
                //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                //    b.Product_Placement = b.Product_Placement ?? 0;
                //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                //}
                return x;
            }
            //return new List<Repair_01>();
        }







        public List<QC_01GridValue> Get_QC01DataGridValuesFiltered(string Today, string wkdaystoreport, string RoleFilter)
        {
            int days = 0;
            if (int.TryParse(wkdaystoreport, out days) == false) { days = 6; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardQC_01_GridValueFiltered(Today, days, RoleFilter).ToList();
                //foreach (Repari_01Grid b in x)
                //{
                //    b.Bridge_Repair = b.Bridge_Repair ?? 0;
                //    b.MSC_Repair_Handling = b.MSC_Repair_Handling ?? 0;
                //    b.Product_Placement = b.Product_Placement ?? 0;
                //    b.Total = b.Bridge_Repair + b.MSC_Repair_Handling + b.Product_Placement;
                //}
                return x;
            }
            //return new List<Repair_01>();
        }

        public List<DashboardInventoryQTY_01_Grid> GetDashboardInventoryQTY_01_GridValueFiltered(decimal ProjectID, string Product_place, string roles)
        {

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var x = ctx.GetDashboardInventoryQTY_01_GridValueFiltered(ProjectID, Product_place, roles).ToList();
                return x;
            }
            //return new List<Repair_01>();
        }





        #endregion

    }
}