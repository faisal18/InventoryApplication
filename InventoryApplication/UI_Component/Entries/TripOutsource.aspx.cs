using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class TripOutsource : System.Web.UI.Page
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
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        public DataTable GetData()
        {
            DataTable dt_Trip = new DataTable();


            try
            {
                dt_Trip = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpTripOutsource.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt_Trip;
        }

        protected void GV_One_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddNew")
                {
                    string Description = (GV_One.FooterRow.FindControl("txt_DescriptionFooter") as TextBox).Text.Trim();
                    string FromDestination = (GV_One.FooterRow.FindControl("txt_FromDestinationFooter") as TextBox).Text.Trim();
                    string ToDestination = (GV_One.FooterRow.FindControl("txt_ToDestinationFooter") as TextBox).Text.Trim();
                    string TotalDistance = (GV_One.FooterRow.FindControl("txt_TotalDistanceFooter") as TextBox).Text.Trim();
                    //string TripRate = (GV_One.FooterRow.FindControl("txt_TripRateFooter") as TextBox).Text.Trim();
                    string TollCharges = (GV_One.FooterRow.FindControl("txt_TollChargesFooter") as TextBox).Text.Trim();
                    string WaitingCharges = (GV_One.FooterRow.FindControl("txt_WaitingChargesFooter") as TextBox).Text.Trim();
                    string TotalCharges = (GV_One.FooterRow.FindControl("txt_TotalChargesFooter") as TextBox).Text.Trim();
                    string DriverCommission = (GV_One.FooterRow.FindControl("txt_DriverCommissionFooter") as TextBox).Text.Trim();
                    string DateofEntry = (GV_One.FooterRow.FindControl("txt_DateofEntryFooter") as TextBox).Text.Trim();
                    string DriverId = (GV_One.FooterRow.FindControl("txt_DriverIdFooter") as TextBox).Text.Trim();
                    string ClientId = (GV_One.FooterRow.FindControl("txt_ClientIdFooter") as TextBox).Text.Trim();
                    string CarId = (GV_One.FooterRow.FindControl("txt_CarIdFooter") as TextBox).Text.Trim();


                    DateTime DDateofEntry = DateTime.ParseExact(DateofEntry, "yyyy-MM-dd", null);

                    DAL.Tbl_TripOutsource obj = new DAL.Tbl_TripOutsource
                    {

                        Description = Description,
                        FromDestination = FromDestination,
                        ToDestination = ToDestination,
                        TotalDistance = TotalDistance,
                        //TripRate = TripRate,
                        TollCharges = TollCharges,
                        WaitingCharges = WaitingCharges,
                        TotalCharges = TotalCharges,
                        DriverCommission = DriverCommission,
                        DateofEntry = DDateofEntry,
                        DriverName = DriverId,
                        ClientName = ClientId,
                        CarRegistration = CarId,
                        DomainCompanyId = DomainCompanyId,

                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    int result = DAL.Operations.OpTripOutsource.InsertRecord(obj);
                    if (result > 0)
                    {
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

                string Description = (GV_One.Rows[e.RowIndex].FindControl("txt_Description") as TextBox).Text.Trim();
                string FromDestination = (GV_One.Rows[e.RowIndex].FindControl("txt_FromDestination") as TextBox).Text.Trim();
                string ToDestination = (GV_One.Rows[e.RowIndex].FindControl("txt_ToDestination") as TextBox).Text.Trim();
                string TotalDistance = (GV_One.Rows[e.RowIndex].FindControl("txt_TotalDistance") as TextBox).Text.Trim();
                //string TripRate = (GV_One.Rows[e.RowIndex].FindControl("ddl_TripRate") as TextBox).Text.Trim();
                string TollCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_TollCharges") as TextBox).Text.Trim();
                string WaitingCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_WaitingCharges") as TextBox).Text.Trim();
                string TotalCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_TotalCharges") as TextBox).Text.Trim();
                string DriverCommission = (GV_One.Rows[e.RowIndex].FindControl("txt_DriverCommission") as TextBox).Text.Trim();
                string DateofEntry = (GV_One.Rows[e.RowIndex].FindControl("txt_DateofEntry") as TextBox).Text.Trim();
                string DriverId = (GV_One.Rows[e.RowIndex].FindControl("txt_DriverId") as TextBox).Text.Trim();
                string ClientId = (GV_One.Rows[e.RowIndex].FindControl("txt_ClientId") as TextBox).Text.Trim();
                string CarId = (GV_One.Rows[e.RowIndex].FindControl("txt_CarId") as TextBox).Text.Trim();
                DateTime DDateofEntry = DateTime.Parse(DateofEntry);


                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                DAL.Tbl_TripOutsource obj = new DAL.Tbl_TripOutsource
                {
                    Description = Description,
                    FromDestination = FromDestination,
                    ToDestination = ToDestination,
                    TotalDistance = TotalDistance,
                    //TripRate = TripRate,
                    TollCharges = TollCharges,
                    WaitingCharges = WaitingCharges,
                    TotalCharges = TotalCharges,
                    DriverCommission = DriverCommission,
                    DateofEntry = DDateofEntry,
                    DriverName = DriverId,
                    ClientName = ClientId,
                    CarRegistration = CarId,
                    DomainCompanyId = DomainCompanyId ,

                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdateDate = DateTime.Now
                };


                int result = DAL.Operations.OpTripOutsource.UpdateRecord(obj, id);
                if (result > 0)
                {
                    lbl_message.Text = "Record updated successfully";
                }
                else
                {
                    lbl_message.Text = "Something went wrong";
                }


                populate_grid();
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void GV_One_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                if (DAL.Operations.OpTripOutsource.DeleteById(id))
                {
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
                string FileDir = AppDataDir + "\\TripOutsourceReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
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