﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication
{
    public partial class Default : System.Web.UI.Page
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
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
            }
        }
    }
}