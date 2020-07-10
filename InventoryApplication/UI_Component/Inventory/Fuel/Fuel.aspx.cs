using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Inventory.Fuel
{
    public partial class Fuel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session.Keys.Count > 0)
                {

                }
                else
                {
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }

                if (!IsPostBack)
                {
                    //LoadData();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("FuelAdd.aspx", false);
        }

        protected void btn_List_Click(object sender, EventArgs e)
        {
            Response.Redirect("FuelList.aspx", false);

        }
    }
}