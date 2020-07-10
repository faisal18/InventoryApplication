using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class TripEntry : System.Web.UI.Page
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
            DataTable dt_user = new DataTable();
            DataTable dt_company = new DataTable();
            DataTable dt_car = new DataTable();
            DataTable dt_Trip = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt_contract = new DataTable();


            try
            {
                dt_Trip = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpTrip.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_user = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_company = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpClientInformation.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_car = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_contract = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpClientContract.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());


                dt = MergeTable(dt_Trip,dt_user,dt_company,dt_car, dt_contract);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        public DataTable GetData(int DomainCompanyId)
        {
            DataTable dt_user = new DataTable();
            DataTable dt_company = new DataTable();
            DataTable dt_car = new DataTable();
            DataTable dt_Trip = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt_contract = new DataTable();


            try
            {
                dt_Trip = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpTrip.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_user = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_company = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpClientInformation.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_car = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                dt_contract = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpClientContract.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());


                dt = MergeTable(dt_Trip, dt_user, dt_company, dt_car, dt_contract);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_Trip, DataTable dt_user, DataTable dt_company, DataTable dt_car,DataTable dt_contract)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("FromDestination", typeof(string));
                dt.Columns.Add("ToDestination", typeof(string));
                dt.Columns.Add("TotalDistance", typeof(string));
                dt.Columns.Add("TripRate", typeof(string));
                dt.Columns.Add("TollCharges", typeof(string));
                dt.Columns.Add("WaitingCharges", typeof(string));
                dt.Columns.Add("TotalCharges", typeof(string));
                dt.Columns.Add("DriverCommission", typeof(string));
                dt.Columns.Add("DateofEntry", typeof(DateTime));

                dt.Columns.Add("DriverId", typeof(int));
                dt.Columns.Add("DriverName", typeof(string));

                dt.Columns.Add("ClientId", typeof(int));
                dt.Columns.Add("ClientName", typeof(string));

                dt.Columns.Add("CarId", typeof(int));

                dt.Columns.Add("CarPlateNumber", typeof(string));

                dt.Columns.Add("CreationDate", typeof(DateTime));
                dt.Columns.Add("CreditStatus", typeof(string));

                //dt.Columns.Add("Ports", typeof(string));
                //dt.Columns.Add("RoadPermission", typeof(string));
                //dt.Columns.Add("LoadingPermission", typeof(string));
                dt.Columns.Add("InvoiceNumber", typeof(string));




                var result = from T1 in dt_Trip.AsEnumerable()

                             join T2 in dt_user.AsEnumerable()
                             on T1.Field<int>("DriverId") equals T2.Field<int>("Id")

                             join T3 in dt_company.AsEnumerable()
                             on T1.Field<int>("ClientId") equals T3.Field<int>("Id")

                             join T4 in dt_car.AsEnumerable()
                             on T1.Field<int>("CarId") equals T4.Field<int>("Id")

                             join T5 in dt_contract.AsEnumerable()
                             on T3.Field<int>("Id") equals T5.Field<int>("ClientInformationId")

                             join t6 in DAL.Operations.OpCreditStatus.GetAll().AsEnumerable()
                             on T1.Field<int>("CreditStatusId") equals t6.Id

                             select dt.LoadDataRow(new object[]
                             {
                                T1.Field<int>("Id"),
                                T1.Field<string>("Description"),
                                T1.Field<string>("FromDestination"),
                                T1.Field<string>("ToDestination"),
                                T1.Field<string>("TotalDistance"),
                                T5.Field<string>("TripRate"),
                                T1.Field<string>("TollCharges"),
                                T1.Field<string>("WaitingCharges"),
                                T1.Field<string>("TotalCharges"),
                                T1.Field<string>("DriverCommission"),
                                T1.Field<DateTime>("DateofEntry"),

                                T2.Field<int>("Id"),
                                T2.Field<string>("Name"),

                                T3.Field<int>("Id"),
                                T3.Field<string>("CompanyName"),

                                T4.Field<int>("Id"),
                                T4.Field<string>("PlateNumber"),

                                T1.Field<DateTime>("CreatedDate"),
                                t6.Description,


                                //T1.Field<string>("Ports"),
                                //T1.Field<string>("RoadPermission"),
                                //T1.Field<string>("LoadingPermission"),
                                T1.Field<string>("InvoiceNumber"),


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
                    string TripRate = (GV_One.FooterRow.FindControl("txt_TripRateFooter") as TextBox).Text.Trim();
                    string TollCharges = (GV_One.FooterRow.FindControl("txt_TollChargesFooter") as TextBox).Text.Trim();
                    string WaitingCharges = (GV_One.FooterRow.FindControl("txt_WaitingChargesFooter") as TextBox).Text.Trim();
                    string TotalCharges = (GV_One.FooterRow.FindControl("txt_TotalChargesFooter") as TextBox).Text.Trim();
                    string DriverCommission = (GV_One.FooterRow.FindControl("txt_DriverCommissionFooter") as TextBox).Text.Trim();
                    string DateofEntry = (GV_One.FooterRow.FindControl("txt_DateofEntryFooter") as TextBox).Text.Trim();
                    string DriverId = (GV_One.FooterRow.FindControl("txt_DriverIdFooter") as DropDownList).SelectedValue.Trim();
                    string ClientId = (GV_One.FooterRow.FindControl("txt_ClientIdFooter") as DropDownList).SelectedValue.Trim();
                    string CarId = (GV_One.FooterRow.FindControl("txt_CarIdFooter") as DropDownList).SelectedValue.Trim();
                    string CreditId = (GV_One.FooterRow.FindControl("txt_CreditStatusFooter") as DropDownList).SelectedValue.Trim();
                    //string Ports = (GV_One.FooterRow.FindControl("txt_PortsFooter") as TextBox).Text.Trim();
                    //string RoadPermission = (GV_One.FooterRow.FindControl("txt_RoadPermissionFooter") as TextBox).Text.Trim();
                    //string LoadingPermission = (GV_One.FooterRow.FindControl("txt_LoadingPermissionFooter") as TextBox).Text.Trim();
                    string InvoiceNumber = (GV_One.FooterRow.FindControl("txt_InvoiceNumberFooter") as TextBox).Text.Trim();



                    DateTime DDateofEntry = DateTime.ParseExact(DateofEntry, "yyyy-MM-dd", null);

                    DAL.Tbl_Trip obj = new DAL.Tbl_Trip
                    {

                        InvoiceNumber = InvoiceNumber,
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
                        DriverId = int.Parse(DriverId),
                        ClientId = int.Parse(ClientId),
                        CarId = int.Parse(CarId),
                        CreditStatusId = int.Parse(CreditId),
                        //Ports = Ports,
                        //RoadPermission = RoadPermission,
                        //LoadingPermission = LoadingPermission,
                        DomainCompanyId = DomainCompanyId,

                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    int result = DAL.Operations.OpTrip.InsertRecord(obj);
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
                string TripRate = (GV_One.Rows[e.RowIndex].FindControl("txt_TripRate") as TextBox).Text.Trim();
                string TollCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_TollCharges") as TextBox).Text.Trim();
                string WaitingCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_WaitingCharges") as TextBox).Text.Trim();
                string TotalCharges = (GV_One.Rows[e.RowIndex].FindControl("txt_TotalCharges") as TextBox).Text.Trim();
                string DriverCommission = (GV_One.Rows[e.RowIndex].FindControl("txt_DriverCommission") as TextBox).Text.Trim();
                string DateofEntry = (GV_One.Rows[e.RowIndex].FindControl("txt_DateofEntry") as TextBox).Text.Trim();
                string DriverId = (GV_One.Rows[e.RowIndex].FindControl("txt_DriverId") as DropDownList).SelectedValue.Trim();
                string ClientId = (GV_One.Rows[e.RowIndex].FindControl("txt_ClientId") as DropDownList).SelectedValue.Trim();
                string CarId = (GV_One.Rows[e.RowIndex].FindControl("txt_CarId") as DropDownList).SelectedValue.Trim();
                string CreditId = (GV_One.Rows[e.RowIndex].FindControl("txt_CreditStatus") as DropDownList).SelectedValue.Trim();
                //string Ports = (GV_One.Rows[e.RowIndex].FindControl("txt_Ports") as TextBox).Text.Trim();
                //string RoadPermission = (GV_One.Rows[e.RowIndex].FindControl("txt_RoadPermission") as TextBox).Text.Trim();
                //string LoadingPermission = (GV_One.Rows[e.RowIndex].FindControl("txt_LoadingPermission") as TextBox).Text.Trim();
                string InvoiceNumber = (GV_One.Rows[e.RowIndex].FindControl("txt_InvoiceNumber") as TextBox).Text.Trim();


                DateTime DDateofEntry = DateTime.Parse(DateofEntry);


                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                DAL.Tbl_Trip obj = new DAL.Tbl_Trip
                {
                    InvoiceNumber = InvoiceNumber,
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
                    DriverId = int.Parse(DriverId),
                    ClientId = int.Parse(ClientId),
                    CarId = int.Parse(CarId),
                    CreditStatusId = int.Parse(CreditId),
                    //Ports = Ports,
                    //RoadPermission = RoadPermission,
                    //LoadingPermission = LoadingPermission,
                    DomainCompanyId = DomainCompanyId,


                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };


                int result = DAL.Operations.OpTrip.UpdateRecord(obj, id);
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
                if (DAL.Operations.OpTrip.DeleteById(id))
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
        protected void GV_One_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (GV_One.EditIndex == e.Row.RowIndex)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddl_Car = (DropDownList)e.Row.FindControl("txt_CarId");
                        ddl_Car.DataSource = DAL.Operations.OpCar.GetAll().Where(x=>x.DomainCompanyId == DomainCompanyId).ToList();
                        ddl_Car.DataTextField = "PlateNumber";
                        ddl_Car.DataValueField = "Id";
                        ddl_Car.DataBind();

                        DropDownList ddl_Client = (DropDownList)e.Row.FindControl("txt_ClientId");
                        ddl_Client.DataSource = DAL.Operations.OpClientInformation.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        ddl_Client.DataTextField = "CompanyName";
                        ddl_Client.DataValueField = "Id";
                        ddl_Client.DataBind();


                        DropDownList ddl_User = (DropDownList)e.Row.FindControl("txt_DriverId");
                        ddl_User.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList(); 
                        ddl_User.DataTextField = "Name";
                        ddl_User.DataValueField = "Id";
                        ddl_User.DataBind();

                        DropDownList ddl_CreditStaus = (DropDownList)e.Row.FindControl("txt_CreditStatus");
                        ddl_CreditStaus.DataSource = DAL.Operations.OpCreditStatus.GetAll();
                        ddl_CreditStaus.DataTextField = "Description";
                        ddl_CreditStaus.DataValueField = "Id";
                        ddl_CreditStaus.DataBind();

                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        DropDownList ddl_Car = (DropDownList)e.Row.FindControl("txt_CarIdFooter");
                        ddl_Car.DataSource = DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        ddl_Car.DataTextField = "PlateNumber";
                        ddl_Car.DataValueField = "Id";
                        ddl_Car.DataBind();
                        

                        DropDownList ddl_Client = (DropDownList)e.Row.FindControl("txt_ClientIdFooter");
                        ddl_Client.DataSource = DAL.Operations.OpClientInformation.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        ddl_Client.DataTextField = "CompanyName";
                        ddl_Client.DataValueField = "Id";
                        ddl_Client.DataBind();
                        ddl_Client.Items.Insert(0, new ListItem("Select Company", ""));

                        DropDownList ddl_User = (DropDownList)e.Row.FindControl("txt_DriverIdFooter");
                        ddl_User.DataSource = DAL.Operations.OpUser.GetAll().Where(x=>x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                        ddl_User.DataTextField = "Name";
                        ddl_User.DataValueField = "Id";
                        ddl_User.DataBind();

                        DropDownList ddl_CreditStaus = (DropDownList)e.Row.FindControl("txt_CreditStatusFooter");
                        ddl_CreditStaus.DataSource = DAL.Operations.OpCreditStatus.GetAll();
                        ddl_CreditStaus.DataTextField = "Description";
                        ddl_CreditStaus.DataValueField = "Id";
                        ddl_CreditStaus.DataBind();
                    }
                }
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

        protected void txt_ClientIdFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                    string clientid = (GV_One.FooterRow.FindControl("txt_ClientIdFooter") as DropDownList).SelectedValue.Trim();
                    string rate = DAL.Operations.OpClientContract.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).Where(x => x.ClientInformationId == int.Parse(clientid)).Select(x => x.TripRate).First().ToString();
                    (GV_One.FooterRow.FindControl("txt_TripRateFooter") as TextBox).Text = rate;
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void txt_ClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
                DropDownList list = sender as DropDownList;
                //string ClientId = (grow.FindControl("ddl_ClientId") as DropDownList).SelectedValue.Trim();
                string ClientId = list.SelectedValue;
                if (ClientID.Length>1)
                {
                    string rate = DAL.Operations.OpClientContract.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).Where(x => x.ClientInformationId == int.Parse(ClientId)).Select(x => x.TripRate).First().ToString();
                    var row = (sender as DropDownList).NamingContainer as GridViewRow;
                    row.Cells[5].Text = rate;
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
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
                string FileDir = AppDataDir + "\\TripReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
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