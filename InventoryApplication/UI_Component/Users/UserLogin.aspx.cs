using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Users
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void submit_Click(object sender, EventArgs e)
        {
            string txt_username = UserName.Value.ToString();
            string txt_password = Password.Value.ToString();

            try
            {
                Tbl_UserLogin obj_userlogin = loaddata(txt_username, txt_password);
                if (obj_userlogin != null)
                {
                    int userid = obj_userlogin.User_Id;
                    Tbl_User obj_user = DAL.Operations.OpUser.GetRecordbyId(userid);
                    string name = obj_user.Name;
                    string role = DAL.Operations.OpUserRoles.GetRecordbyRank(obj_user.RoleId).RoleDescription;

                    Session["UserId"] = userid;
                    Session["Name"] = name;
                    Session["Role"] = role;
                    FormsAuthentication.SetAuthCookie(Session["Name"].ToString(), true);

                    Response.Redirect("~/UI_Component/Entries/DomainCompany.aspx", false);

                    //if (role == "Admin")
                    //{
                    //    Response.Redirect("~/UI_Component/Main.aspx", false);
                    //}
                    //else if (role == "Finance")
                    //{
                    //    Response.Redirect("~/UI_Component/Reports/Reports.aspx", false);
                    //}
                    //else if (role == "Maintenance")
                    //{
                    //    Response.Redirect("~/UI_Component/Inventory/Maintenance/Maintenance.aspx", false);
                    //}

                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        private Tbl_UserLogin loaddata(string username, string password)
        {
            Tbl_UserLogin yo = null;
            try
            {

                Tbl_UserLogin userlogin = DAL.Operations.OpUserLogin.GetRecordByCreds(username, password);
                if (userlogin != null)
                {
                    yo = userlogin;
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }

            return yo;
        }
    }
}