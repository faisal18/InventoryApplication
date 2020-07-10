using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Inventory.SparePart
{
    public partial class SparePartList : System.Web.UI.Page
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
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LoadData()
        {
            try
            {
                DataTable dt_Data = GetData();

                if (dt_Data.Rows.Count > 0)
                {
                    GV_ListTickets.DataSource = dt_Data;
                    GV_ListTickets.DataBind();
                }
                else
                {
                    dt_Data.Rows.Add(dt_Data.NewRow());
                    GV_ListTickets.DataSource = dt_Data;
                    GV_ListTickets.DataBind();
                    GV_ListTickets.Rows[0].Cells.Clear();
                    GV_ListTickets.Rows[0].Cells.Add(new TableCell());
                    GV_ListTickets.Rows[0].Cells[0].ColumnSpan = dt_Data.Columns.Count;
                    GV_ListTickets.Rows[0].Cells[0].Text = "No Data Found !!";
                    GV_ListTickets.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {

                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }


        }
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpPurchase_SparePart.GetAll().Where(x => x.DomainCompanyId == int.Parse(Session["DomainCompanyId"].ToString())).ToList());
                DataTable dt_domain = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpDomainCompany.GetAll().Where(x => x.Id == DomainCompanyId).ToList());
                dt = MergeTable(dt, dt_domain);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        public DataTable GetData(int DomainCompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpPurchase_SparePart.GetAll().Where(x => x.DomainCompanyId == int.Parse(Session["DomainCompanyId"].ToString())).ToList());
                DataTable dt_domain = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpDomainCompany.GetAll().Where(x => x.Id == DomainCompanyId).ToList());
                dt = MergeTable(dt, dt_domain);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_Main, DataTable dt_Domain)
        {
            DataTable dt = new DataTable();
            try
            {

                dt.Columns.Add("Id", typeof(int));

                dt.Columns.Add("InvoiceNumber", typeof(string));

                dt.Columns.Add("CompanyName", typeof(string));

                dt.Columns.Add("DomainCompany", typeof(string));

                dt.Columns.Add("Amount", typeof(string));

                dt.Columns.Add("Gross", typeof(string));

                dt.Columns.Add("DateofPurchase", typeof(DateTime));
                dt.Columns.Add("CreationDate", typeof(DateTime));
                dt.Columns.Add("CreditStatus", typeof(string));
                dt.Columns.Add("VendorName", typeof(string));



                var result = from t1 in dt_Main.AsEnumerable()
                             join t2 in dt_Domain.AsEnumerable()
                             on t1.Field<int>("DomainCompanyId") equals t2.Field<int>("Id")
                             join t3 in DAL.Operations.OpCreditStatus.GetAll().AsEnumerable()
                        on t1.Field<int>("CreditStatusId") equals t3.Id
                             join t4 in DAL.Operations.OpVendor.GetAll().AsEnumerable()
                                  on t1.Field<int>("VendorId") equals t4.Id

                             select dt.LoadDataRow(new object[]
                             {

                                t1.Field<int>("Id"),
                                t1.Field<string>("InvoiceNumber"),
                                t1.Field<string>("CompanyName"),
                                t2.Field<string>("CompanyName"),
                                t1.Field<string>("Amount"),
                                t1.Field<string>("Gross"),
                                t1.Field<DateTime>("DateofPurchase"),
                                t1.Field<DateTime>("CreationDate"),
                                t3.Description,
                                t4.Name,

                             }, false);

                if (result.Count() > 0)
                {
                    dt = result.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }

        protected void GV_ListTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_ListTickets.PageIndex = e.NewPageIndex;
                if (txt_searchbox.Text.Length > 0)
                {
                    BindGrid();
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_searchbox.Text.Length > 1)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        private DataTable GetSearchResults(string category, string search_query)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpPurchase_SparePart.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                var result = dt.AsEnumerable().Where(x => x.Field<string>(category).ToLower().Contains(search_query.ToLower())).ToList();
                if (result.Count() > 0)
                {
                    dt = result.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }

            return dt;
        }
        private void BindGrid()
        {
            try
            {
                DataTable dataTable = GetSearchResults(ddl_search.SelectedValue, txt_searchbox.Text);
                GV_ListTickets.DataSource = dataTable;
                GV_ListTickets.DataBind();
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }


        protected void btn_report_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessData(GetData());
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        public void ProcessData(DataTable dt)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
                System.IO.File.WriteAllBytes(FileDir, System.Text.Encoding.UTF8.GetBytes(sb.ToString()));
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