using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Reports
{
    public partial class FuelSales : System.Web.UI.Page
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
                            //LoadData();
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetData(Convert.ToDateTime(txt_FromDate.Text), Convert.ToDateTime(txt_ToDate.Text));

                if (dt.Rows.Count > 0)
                {
                    ProcessData(dt);
                }

            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }

        private DataTable GetData(DateTime From,DateTime To)
        {
            DataTable dt_FuelSale = new DataTable();
            DataTable dt_Car = new DataTable();
            DataTable dt_Driver = new DataTable();
            DataTable dt = new DataTable();


            try
            {
                dt_FuelSale = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpFuel.GetAll().Where(x => x.CreationDate >= From && x.CreationDate <= To).Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_Car = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_Driver = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList());

                dt = MergeTable(dt_FuelSale, dt_Car, dt_Driver);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_FuelSale, DataTable dt_Car,DataTable dt_Driver)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("DateofEntry", typeof(DateTime));

                dt.Columns.Add("PlateNumber", typeof(string));
                dt.Columns.Add("RegistrationNumber", typeof(string));
                dt.Columns.Add("Model", typeof(string));


                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Contact", typeof(string));

                dt.Columns.Add("PPG", typeof(string));
                dt.Columns.Add("Gallons", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(string));


                var result = from t1 in dt_FuelSale.AsEnumerable()

                             join t2 in dt_Car.AsEnumerable()
                             on t1.Field<int>("CarId") equals t2.Field<int>("Id")

                             join t3 in dt_Driver.AsEnumerable()
                             on t1.Field<int>("DriverId") equals t3.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                t1.Field<DateTime>("DateofEntry"),

                                t2.Field<string>("PlateNumber"),
                                t2.Field<string>("RegistrationNumber"),
                                t2.Field<string>("Model"),

                                t3.Field<string>("Name"),
                                t3.Field<string>("Contact"),

                                t1.Field<string>("PPG"),
                                t1.Field<string>("Gallons"),
                                t1.Field<string>("TotalAmount"),




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