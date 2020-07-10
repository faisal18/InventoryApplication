using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Inventory.Maintenance
{
    public partial class MaintenanceList : System.Web.UI.Page
    {
        private static int DomainCompanyId = 0;
        private static string DomainCompanyName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
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
                        //LoadData();
                        populate_grid();
                        //LoadAttachment("PH_View");
                    }

                }
            }
            else
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            }
        }


        private void populate_grid()
        {
            try
            {
                DataTable dt_new = GetData();
                if (dt_new.Rows.Count > 0)
                {
                    GV_One.DataSource = dt_new;
                    GV_One.DataBind();
                }
                else
                {
                    dt_new.Rows.Add(dt_new.NewRow());
                    GV_One.DataSource = dt_new;
                    GV_One.DataBind();
                    GV_One.Rows[0].Cells.Clear();
                    GV_One.Rows[0].Cells.Add(new TableCell());
                    GV_One.Rows[0].Cells[0].ColumnSpan = dt_new.Columns.Count;
                    GV_One.Rows[0].Cells[0].Text = "No Data Found !!";
                    GV_One.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt1 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt2 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpMaintenanceType.GetAll());
                dt3 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpMaintenance.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());

                dt = MergeTable(dt3, dt, dt1, dt2);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_Maintenance, DataTable dt_Car, DataTable dt_User, DataTable dt_MaintenanceType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("NumberofHours", typeof(int));
            dt.Columns.Add("DueDate", typeof(DateTime));
            dt.Columns.Add("CarId", typeof(int));
            dt.Columns.Add("CarRegistration", typeof(string));
            dt.Columns.Add("DriverId", typeof(int));
            dt.Columns.Add("DriverName", typeof(string));
            dt.Columns.Add("MaintenanceCategoryId", typeof(int));
            dt.Columns.Add("MaintenanceCategoryDescription", typeof(string));


            try
            {
                var result = from T1 in dt_Maintenance.AsEnumerable()
                             join T2 in dt_Car.AsEnumerable()
                             on T1.Field<int>("CarId") equals T2.Field<int>("Id")

                             join T3 in dt_User.AsEnumerable()
                             on T1.Field<int>("DriverId") equals T3.Field<int>("Id")

                             join T4 in dt_MaintenanceType.AsEnumerable()
                             on T1.Field<int>("MaintenanceCategoryId") equals T4.Field<int>("Id")

                             select dt.LoadDataRow(new object[] {
                                 T1.Field<int>("Id"),
                                 T1.Field<string>("Description"),
                                 T1.Field<string>("Cost"),
                                 T1.Field<int>("NumberofHours"),
                                 T1.Field<DateTime>("DueDate"),

                                 T2.Field<int>("Id"),
                                 T2.Field<string>("PlateNumber"),

                                 T3.Field<int>("Id"),
                                 T3.Field<string>("Name"),

                                 T4.Field<int>("Id"),
                                 T4.Field<string>("Description"),

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


        protected void btn_search_Click(object sender, EventArgs e)
        {

        }

        protected void GV_One_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_One.PageIndex = e.NewPageIndex;
                if (txt_searchbox.Text.Length > 0)
                {
                    //BindGrid();
                }
                else
                {
                    populate_grid();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
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