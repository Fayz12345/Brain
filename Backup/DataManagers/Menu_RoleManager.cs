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
using System.Xml.Linq;

using BW_WebApp.Classes;


namespace BW_WebApp.DataManagers
{
    #region MenuManager
    public class MenuManager
    {
        Menu _menu { get; set; }
        public List<string> restrictedList { get; set; }
        List<MenuItem> RemoveItems = new List<MenuItem>();
        CleanURL Clean = new CleanURL();
        public MenuManager(Menu menu)
        {
            _menu = menu;
            restrictedList = new List<string>();
        }
        public void RestrictMenu()
        {
            foreach (MenuItem i in _menu.Items)
            {
                RemoveUnwantedb(i);
            }
            foreach (MenuItem i in RemoveItems)
            {
                i.Parent.ChildItems.Remove(i);
            }
        }
        void RemoveUnwanted(MenuItem item)
        {
            if (restrictedList.Contains(item.Text.ToUpper() + "," + Clean.Clean(item.NavigateUrl.ToUpper())) == false && item.Text.ToUpper() != "HOME")
            {
                RemoveItems.Add(item);
                //item.Parent.ChildItems.Remove(item);
            }
            else
            {
                foreach (MenuItem i in item.ChildItems)
                {
                    RemoveUnwanted(i);
                }
            }
        }
        void RemoveUnwantedb(MenuItem item)
        {
            if (restrictedList.Contains(item.Text.ToUpper() + "," + Clean.Clean(item.NavigateUrl.ToUpper())) == false && item.Text.ToUpper() != "HOME")
            {
                RemoveItems.Add(item);
                //item.Parent.ChildItems.Remove(item);
            }
            foreach (MenuItem i in item.ChildItems)
            {
                RemoveUnwantedb(i);
            }
        }

        MenuItem GetMenuItem(MenuItem item, string Searchfor)
        {
            // Display the menu item's text value.
            //Message.Text += item.Text + "<br>";
            if (item.Text.ToUpper() + "," + item.NavigateUrl.ToUpper() == Searchfor.ToUpper())
            {
                return item;
            }
            // Iterate through the child menu items of the parent menu item 
            // passed into this method, and display their values.
            foreach (MenuItem childItem in item.ChildItems)
            {

                // Recursively call the DisplayChildMenuText method to
                // traverse the tree and display all child menu items.
                item = GetMenuItem(childItem, Searchfor);
                if (item != null) { return item; }
            }
            return null;
        }
        public void LoadAllMenuItems()
        {
            if (_menu.Items.Count > 0)
            {
                // Iterate through the root menu items in the Items collection.
                foreach (MenuItem i in _menu.Items)
                {
                    GetChildMenuText(i);
                }

            }
        }
        void GetChildMenuText(MenuItem item)
        {
            CleanURL Clean = new CleanURL();
            // Display the menu item's text value.
            restrictedList.Add(item.Text + "," + Clean.Clean(item.NavigateUrl));
            // Iterate through the child menu items of the parent menu item 
            // passed into this method, and display their values.
            foreach (MenuItem childItem in item.ChildItems)
            {
                // Recursively call the DisplayChildMenuText method to
                // traverse the tree and display all child menu items.
                GetChildMenuText(childItem);

            }

        }

        public bool IsValidMenuOption(string LookForKey)
        {

            foreach (MenuItem item in _menu.Items)
            {
                if (IsValidMenuOptionThere(item, LookForKey) == true) { return true; }
            }
            return false;
        }

        public bool IsValidMenuOptionThere(MenuItem item, string LookForKey)
        {
             string mKey = "";
             mKey = item.Text.ToUpper() + "," + Clean.Clean(item.NavigateUrl.ToUpper());
             if (mKey == LookForKey) { return true; }
             else
             {
                 foreach (MenuItem i in item.ChildItems)
                 {
                     if (IsValidMenuOptionThere(item, LookForKey) == true) { return true; }
                 }
             }
             return false;
        }

    }
    public class RoleMenuAccessManager : DataManagers
    {
        List<string> ValidRoles = new List<string>();
        CleanURL Clean = new CleanURL();

        public RoleMenuAccessManager(string Username) :base(Username)
        {
        }
        public List<string> GetData(string role, bool isAdmin)
        {
            string[] r = role.Split(',');
            return GetData(r, isAdmin);
        }
        public List<string> GetData(string[] r, bool isAdmin)
        {
            List<string> Rol = r.ToList();
            for (int a = 0; a < Rol.Count; a = a + 1) { Rol[a] = Rol[a].ToUpper(); }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var data = from d in ctx.RoleMenuAccesses
                           where (Rol.Contains(d.Role.ToUpper()) && d.Active == true)     
                           select d.MenuTitle.ToUpper() + "," + d.MenuURL.ToUpper();
                return data.Distinct().ToList();
            }
        }
        public void SaveData(string role, List<string> keep, List<string> drop)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                // Loop through the drop list and get rid of them.
                string[] keys = new string[] { };
                foreach (string s in drop)
                {
                    keys = s.Split(',');
                    var data = ctx.RoleMenuAccesses.FirstOrDefault(x => x.Role == role && x.MenuTitle == keys[0] && x.MenuURL == keys[1]);
                    if (data != null)
                    {
                        data.Active = false;
                    }

                }

                foreach (string s in keep)
                {
                    keys = s.Split(',');
                    var data = ctx.RoleMenuAccesses.FirstOrDefault(x => x.Role == role && x.MenuTitle == keys[0] && x.MenuURL == keys[1]);
                    if (data != null)
                    {
                        data.Active = true;
                        data.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        RoleMenuAccess ra = new RoleMenuAccess();
                        ra.Active = true;
                        ra.CreateDate = DateTime.Now;
                        ra.CreateUser = UserName;
                        ra.MenuTitle = keys[0];
                        ra.MenuURL = keys[1];
                        ra.Role = role;
                        ctx.RoleMenuAccesses.InsertOnSubmit(ra);
                    }

                }

                ctx.SubmitChanges();

                // loop through the keep list and make sure they are there.
            }
        }
        public string CleanRoleMenuAccessTable()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                // Set all menu options to Inactive.
                foreach (RoleMenuAccess d in ctx.RoleMenuAccesses) { d.Directive = 0; }
                // We will activate those that require it.
                ValidRoles = Roles.GetAllRoles().ToList();
                for (int a = 0; a < ValidRoles.Count; a = a + 1) { ValidRoles[a] = ValidRoles[a].ToUpper(); }
                CleanMenuData(ctx);
                ctx.SubmitChanges();
                string msg = ctx.RoleMenuAccesses.Count(x => x.Directive == 1).ToString() + " Active - " +  ctx.RoleMenuAccesses.Count(x => x.Directive != 1).ToString() + " can be Removed";
                return msg;
            }
        }
        public string CleanRoleMenuAccessTable_Delete()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                int Count = 0;
                foreach (RoleMenuAccess d in ctx.RoleMenuAccesses.Where(x => x.Directive != 1 || x.Active == false)) {
                    ctx.RoleMenuAccesses.DeleteOnSubmit(d);
                    Count++;
                }
                ctx.SubmitChanges();
                string msg = Count.ToString() + " have been Removed";
                return msg;
            }
        }
        private void CleanMenuData(clsLinqDataContext ctx)
        {
            // Open the File and read in the elements.
            XElement xelement1 = XElement.Load(HttpContext.Current.Server.MapPath("~/web.sitemap"));
            XElement axis = (from element in xelement1.Elements("siteMapNode")
                             where element.Attribute("title").Value == "Home"
                             select element).Single();
            // move through each element and work down the nodes.
            foreach (XElement x in axis.Elements())
            {
                CleanMenuData(x, ctx);
            }
        }
        private void CleanMenuData(XElement element, clsLinqDataContext ctx)
        {
            // This will only set those with child elements.
            if (element.HasElements == true)
            {
                foreach (XElement x in element.Elements())
                {
                    if (x.HasElements == true) { CleanMenuData(x, ctx); }
                    else { FlagMenuOptionActive(ctx, Clean, x); }
                }
            }
            // This is required to flag the final element in a chain.
            FlagMenuOptionActive(ctx, Clean, element);

        }
        private void FlagMenuOptionActive(clsLinqDataContext ctx, CleanURL Clean, XElement x)
        {
            string Title = x.Attribute("title").Value.ToUpper();
            string URL = Clean.Clean(x.Attribute("url").Value.ToUpper());
            var data = from y in ctx.RoleMenuAccesses where y.MenuTitle.ToUpper() == Title && y.MenuURL.ToUpper() == URL && ValidRoles.Contains(y.Role.ToUpper()) == true select y;
            foreach (RoleMenuAccess d in data)
            {
                d.Directive = 1; 
            }
        }
    }
    public class CleanURL
    {
        public CleanURL()
        {
        }

        public string Clean(string Data)
        {
            int i = Data.LastIndexOf('/');
            if (i > -1)
            {
                Data = Data.Substring(i + 1);
            }
            return Data.Trim();
        }
    }
    #endregion
}