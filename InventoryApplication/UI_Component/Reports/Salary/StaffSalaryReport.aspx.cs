using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Reports.Salary
{
    public partial class StaffSalaryReport : System.Web.UI.Page
    {
        private static int DomainCompanyId = 0;
        private static string DomainCompanyName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session.Keys.Count > 0)
                {

                    if (String.IsNullOrEmpty(Session["DomainCompanyId"].ToString()))
                    {
                        Response.Redirect("~/UI_Component/Entries/DomainCompany.aspx", false);
                    }
                    else
                    {
                        DomainCompanyId = int.Parse(Session["DomainCompanyId"].ToString());
                        DomainCompanyName = Session["DomainCompanyName"].ToString();
                    }
                }
                else
                {
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }
                if (!IsPostBack)
                {
                    LoadDrivers();
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        private void LoadDrivers()
        {
            try
            {
                ddl_staff.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId != 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                ddl_staff.DataTextField = "Name";
                ddl_staff.DataValueField = "Id";
                ddl_staff.DataBind();
                if (ddl_staff.Items.Count > 0)
                {
                    ddl_staff.Items.Insert(0, new ListItem("All", "0"));
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                if(ddl_staff.SelectedValue == "0")
                {
                    dt =  DAL.Helper.ListToDatatable.ToDataTable<DAL.Tbl_User>(DAL.Operations.OpUser.GetAll()
                        .Where(x => x.RoleId != 5)
                        .Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                }
                else
                {
                    dt = DAL.Helper.ListToDatatable.ToDataTable<DAL.Tbl_User>(DAL.Operations.OpUser.GetAll()
                        .Where(x => x.RoleId != 5 && x.Id == int.Parse(ddl_staff.SelectedValue))
                        .Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                }

                ProcessData(dt);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }

        public void ProcessData(DataTable dt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));
                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
                string AppDataDir = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
                string FileDir = AppDataDir + "\\FuelReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                System.IO.File.WriteAllBytes(FileDir, Encoding.UTF8.GetBytes(sb.ToString()));
                download_string(FileDir);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        public void download_string(string path)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
    }
}