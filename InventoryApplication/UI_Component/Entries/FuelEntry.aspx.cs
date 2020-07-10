using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class FuelEntry : System.Web.UI.Page
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
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
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
                Populate_Control();
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


            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpFuel.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt1 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll().Where(x=>x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList().ToList());
                dt2 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());

                dt = MergeTable(dt, dt1,dt2);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_Fuel, DataTable dt_User,DataTable dt_Car)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("DateofEntry", typeof(DateTime));
                dt.Columns.Add("CarId", typeof(int));
                dt.Columns.Add("PlateNumber", typeof(string));
                dt.Columns.Add("DriverId", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("PPG", typeof(string));
                dt.Columns.Add("Gallons", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(string));


                var result = from t1 in dt_Fuel.AsEnumerable()

                             join t2 in dt_Car.AsEnumerable()
                             on t1.Field<int>("CarId") equals t2.Field<int>("Id")

                             join t3 in dt_User.AsEnumerable()
                             on t1.Field<int>("DriverId") equals t3.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                t1.Field<int>("Id"),
                                t1.Field<DateTime>("DateofEntry"),
                                t1.Field<int>("CarId"),
                                t2.Field<string>("PlateNumber"),

                                t1.Field<int>("DriverId"),
                                t3.Field<string>("Name"),

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

        private void Populate_Control()
        {
            try
            {
                Fuel_Gauge.InnerText = DAL.Operations.OpFuelTotal.GetRecordbyId(1).TotalFuel.Value.ToString();
            }
            catch (Exception ex)
            {

                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void GV_One_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddNew")
                {
                    string DateofEntry = (GV_One.FooterRow.FindControl("txt_DateofEntryFooter") as TextBox).Text.Trim();
                    int CarId = int.Parse((GV_One.FooterRow.FindControl("txt_CarIdFooter") as TextBox).Text.Trim());
                    int DriverId = int.Parse((GV_One.FooterRow.FindControl("txt_DriverIdFooter") as DropDownList).SelectedValue.Trim());
                    string PPG = (GV_One.FooterRow.FindControl("txt_PPGFooter") as TextBox).Text.Trim();
                    string Gallons = (GV_One.FooterRow.FindControl("txt_GallonsFooter") as TextBox).Text.Trim();
                    string TotalAmount = (GV_One.FooterRow.FindControl("txt_TotalAmountFooter") as TextBox).Text.Trim();

                    DateTime DDateofEntry = DateTime.ParseExact(DateofEntry, "yyyy-MM-dd", null);

                    DAL.Tbl_Fuel obj = new DAL.Tbl_Fuel
                    {
                        DateofEntry = DDateofEntry,
                        CarId = CarId,
                        DriverId = DriverId,
                        PPG = PPG,
                        Gallons = Gallons,
                        TotalAmount = TotalAmount,
                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreationDate = DateTime.Now,
                        DomainCompanyId = DomainCompanyId,

                    };

                    int result = DAL.Operations.OpFuel.InsertRecord(obj);
                    if (result > 0)
                    {
                        UpdateFuelTotal(Gallons,false);

                        lbl_message.Text = "Record added successfully";
                    }
                    else
                    {
                        lbl_message.Text = "Something went wrong";
                    }

                    Enable_Footer();
                    populate_grid();

                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                string DateofEntry = (GV_One.Rows[e.RowIndex].FindControl("txt_DateofEntry") as TextBox).Text.Trim();
                int CarId = int.Parse((GV_One.Rows[e.RowIndex].FindControl("txt_CarId") as TextBox).Text.Trim());
                int DriverId = int.Parse((GV_One.Rows[e.RowIndex].FindControl("txt_DriverId") as DropDownList).SelectedValue.Trim());
                string PPG = (GV_One.Rows[e.RowIndex].FindControl("txt_PPG") as TextBox).Text.Trim();
                string Gallons = (GV_One.Rows[e.RowIndex].FindControl("txt_Gallons") as TextBox).Text.Trim();
                string TotalAmount = (GV_One.Rows[e.RowIndex].FindControl("txt_TotalAmount") as TextBox).Text.Trim();

                DateTime DDateofEntry = DateTime.ParseExact(DateofEntry, "M/d/yyyy hh:mm:ss tt", null);

                DAL.Tbl_Fuel obj = new DAL.Tbl_Fuel
                {
                    DateofEntry = DDateofEntry,
                    CarId = CarId,
                    DriverId = DriverId,
                    PPG = PPG,
                    Gallons = Gallons,
                    TotalAmount = TotalAmount,
                    DomainCompanyId = DomainCompanyId,
                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreationDate = DateTime.Now,

                };

                int result = DAL.Operations.OpFuel.UpdateRecord(obj, id);
                if (result > 0)
                {
                    UpdateFuelTotal(Gallons,false);
                    lbl_message.Text = "Record updated successfully";
                }
                else
                {
                    lbl_message.Text = "Something went wrong";
                }

                Enable_Footer();
                populate_grid();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
            }
        }
        protected void GV_One_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());

                string gallons = DAL.Operations.OpFuel.GetRecordbyId(id).Gallons;

                if (DAL.Operations.OpFuel.DeleteById(id))
                {

                    UpdateFuelTotal(gallons,true);
                    lbl_message.Text = "Record deleted successfully";
                }
                else
                {
                    lbl_message.Text = "Record deleltion failed";
                }
                populate_grid();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                GV_One.EditIndex = e.NewEditIndex;
                GV_One.FooterRow.Visible = false;
                Disable_Footer();
                populate_grid();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (GV_One.EditIndex == e.Row.RowIndex)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        //DropDownList txt_CarId = (DropDownList)e.Row.FindControl("txt_CarId");
                        //txt_CarId.DataSource = DAL.Operations.OpCar.GetAll();
                        //txt_CarId.DataTextField = "PlateNumber";
                        //txt_CarId.DataValueField = "Id";
                        //txt_CarId.DataBind();

                        DropDownList txt_DriverId = (DropDownList)e.Row.FindControl("txt_DriverId");
                        txt_DriverId.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        txt_DriverId.DataTextField = "Name";
                        txt_DriverId.DataValueField = "Id";
                        txt_DriverId.DataBind();

                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        //DropDownList txt_CarId = (DropDownList)e.Row.FindControl("txt_CarIdFooter");
                        //txt_CarId.DataSource = DAL.Operations.OpCar.GetAll();
                        //txt_CarId.DataTextField = "PlateNumber";
                        //txt_CarId.DataValueField = "Id";
                        //txt_CarId.DataBind();

                        DropDownList txt_DriverId = (DropDownList)e.Row.FindControl("txt_DriverIdFooter");
                        txt_DriverId.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        txt_DriverId.DataTextField = "Name";
                        txt_DriverId.DataValueField = "Id";
                        txt_DriverId.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_One.EditIndex = -1;
                Enable_Footer();
                populate_grid();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_One.PageIndex = e.NewPageIndex;
                populate_grid();
                Enable_Footer();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }

        private void Enable_Footer()
        {
            GV_One.ShowFooter = true;
        }
        private void Disable_Footer()
        {
            GV_One.ShowFooter = false;
        }

        private void UpdateFuelTotal(string Gallons,bool toAdd)
        {
            try
            {

                decimal GallonsStored = DAL.Operations.OpFuelTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().TotalFuel.Value;
                decimal DGallons = 0;

                if(toAdd)
                {
                    DGallons = GallonsStored + decimal.Parse(Gallons);
                }
                else
                {
                    DGallons = GallonsStored - decimal.Parse(Gallons);
                }

                DAL.Tbl_FuelTotal obj = new DAL.Tbl_FuelTotal
                {
                    TotalFuel = DGallons,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                int result = DAL.Operations.OpFuelTotal.UpdateRecord(obj, 1);
                DAL.Operations.OpLogger.Info("FuelTotal has been updated");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void txt_reset_Click(object sender, EventArgs e)
        {
            try
            {

                int RecordID = DAL.Operations.OpVendor.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().Id;

                DAL.Tbl_FuelTotal obj = new DAL.Tbl_FuelTotal {
                    TotalFuel = 0,
                    DomainCompanyId = DomainCompanyId,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now,
                };

                int result = DAL.Operations.OpFuelTotal.UpdateRecord(obj, RecordID);
                Populate_Control();
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
                throw;
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