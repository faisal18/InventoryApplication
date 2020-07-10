using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Reports.Purchase
{
    public partial class PurchaseReport : System.Web.UI.Page
    {
        private static int DomainCompanyId = 0;
        private static string DomainCompanyName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session.Keys.Count > 0)
                {
                    if (Session["DomainCompanyId"] == null)
                    {
                        Response.Redirect("~/UI_Component/Entries/DomainCompany.aspx", false);
                    }
                    else
                    {
                        DomainCompanyId = int.Parse(Session["DomainCompanyId"].ToString());
                        DomainCompanyName = Session["DomainCompanyName"].ToString();

                        if (!IsPostBack)
                        {
                            LoadData();
                            //populate_grid();
                            //LoadAttachment("PH_View");
                        }

                    }
                }
                else
                {
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }

        private void LoadData()
        {
            try
            {
                ddl_domaincompany.DataSource = DAL.Operations.OpDomainCompany.GetAll();
                ddl_domaincompany.DataTextField = "CompanyName";
                ddl_domaincompany.DataValueField = "Id";
                ddl_domaincompany.DataBind();
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string report = ddl_reporttype.SelectedValue;


                DataTable dt = new DataTable();

                switch (report)
                {
                    case "Fuel":
                        Inventory.Fuel.FuelList obj = new Inventory.Fuel.FuelList();
                        dt = obj.GetData(DomainCompanyId);
                        break;

                    case "Insurance":
                        Inventory.Insurance.InsuranceList obj2 = new Inventory.Insurance.InsuranceList();
                        dt = obj2.GetData(DomainCompanyId);
                        break;

                    case "SpareParts":
                        Inventory.SparePart.SparePartList obj3 = new Inventory.SparePart.SparePartList();
                        dt = obj3.GetData(DomainCompanyId);
                        break;

                    case "Tyre":
                        Inventory.Tyre.TyreList obj4 = new Inventory.Tyre.TyreList();
                        dt = obj4.GetData(DomainCompanyId);
                        break;
                }

                if (rdl_option.SelectedValue == "domain")
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Columns["Company"] == null)
                        {
                            dt = dt.AsEnumerable().Where(x => x.Field<string>("DomainCompany") == ddl_domaincompany.SelectedItem.Text).ToList().CopyToDataTable();
                        }
                        else if (dt.Columns["CompanyName"] == null) 
                        {
                            dt = dt.AsEnumerable().Where(x => x.Field<string>("DomainCompany") == ddl_domaincompany.SelectedItem.Text).ToList().CopyToDataTable();
                        }
                        ProcessData(dt);
                    }
                    else
                    {
                        lbl_message.Text = "No records founds";
                    }

                }
                else if(rdl_option.SelectedValue == "date")
                {
                    DateTime from = Convert.ToDateTime(txt_FromDate.Text);
                    DateTime to = Convert.ToDateTime(txt_ToDate.Text);

                    dt = dt.AsEnumerable().Where(x => x.Field<DateTime>("CreationDate") >= from && x.Field<DateTime>("CreationDate") <= to).ToList().CopyToDataTable();
                    ProcessData(dt);
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }

        protected void rdl_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdl_option.SelectedValue == "domain")
                {
                    div_dateTo.Visible = false;
                    div_dateFrom.Visible = false;
                    div_domain.Visible = true;
                }
                else if (rdl_option.SelectedValue == "date")
                {
                    div_dateTo.Visible = true;
                    div_dateFrom.Visible = true;
                    div_domain.Visible = false;
                }

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