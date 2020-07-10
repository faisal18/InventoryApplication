using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.IO;

namespace InventoryApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string Page = Path.GetFileName(context.Request.Url.AbsolutePath);
                bool isAccessingInventory = Context.Request.Url.AbsolutePath.ToLower().Contains("inventory");
                

                //string Parent = (Context.Request.Url.Segments[2].Replace("/", ""));
                //    Response.Redirect("~/UIComponents/User/Unauthourized.aspx");


                if (Page != "Userlogin")
                {
                    if (context.Session != null)
                    {
                        if (context.Session.Keys.Count == 0)
                        {
                            FormsAuthentication.RedirectToLoginPage();
                        }
                        else if (context.Session.Keys.Count > 0)
                        {
                            if (Session["Role"].ToString() != "Admin")
                            {
                                if (Session["Role"].ToString() == "Maintenance")
                                {
                                    if (isAccessingInventory)
                                    {

                                    }
                                    else
                                    {
                                        Response.Redirect("~/UIComponents/Users/Unauthourized.aspx");
                                    }
                                }
                                if (Session["Role"].ToString() == "Finance")
                                {
                                    if (Page == "FuelSales" ||
                                        Page == "Reports")
                                    {

                                    }
                                    else
                                    {
                                        Response.Redirect("~/UIComponents/Users/Unauthourized.aspx");
                                    }
                                }

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
    }
}