using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class ClientInformationEntry : System.Web.UI.Page
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
        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpClientInformation.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
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
                    
                    string CompanyName = (GV_One.FooterRow.FindControl("txt_CompanyNameFooter") as TextBox).Text.Trim();
                    string ContactPerson = (GV_One.FooterRow.FindControl("txt_ContactPersonFooter") as TextBox).Text.Trim();
                    string Number = (GV_One.FooterRow.FindControl("txt_NumberFooter") as TextBox).Text.Trim();
                    string Email = (GV_One.FooterRow.FindControl("txt_EmailFooter") as TextBox).Text.Trim();
                    string Address = (GV_One.FooterRow.FindControl("txt_AddressFooter") as TextBox).Text.Trim();

                    if (CompanyName.Length > 0 && Number.Length > 0)
                    {

                        DAL.Tbl_ClientInformation obj = new DAL.Tbl_ClientInformation
                        {

                            CompanyName = CompanyName,
                            ContactPerson = ContactPerson,
                            Number = Number,
                            Email = Email,
                            Address = Address,
                            DomainCompanyId = DomainCompanyId,

                            CreatedBy = int.Parse(Session["UserId"].ToString()),
                            CreatedDate = DateTime.Now
                        };

                        int result = DAL.Operations.OpClientInformation.InsertRecord(obj);
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
            }
            catch(Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                string CompanyName = (GV_One.Rows[e.RowIndex].FindControl("txt_CompanyName") as TextBox).Text.Trim();
                string ContactPerson = (GV_One.Rows[e.RowIndex].FindControl("txt_ContactPerson") as TextBox).Text.Trim();
                string Number = (GV_One.Rows[e.RowIndex].FindControl("txt_Number") as TextBox).Text.Trim();
                string Email = (GV_One.Rows[e.RowIndex].FindControl("txt_Email") as TextBox).Text.Trim();
                string Address = (GV_One.Rows[e.RowIndex].FindControl("txt_Address") as TextBox).Text.Trim();

                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                if (CompanyName.Length > 0 && Number.Length > 0)
                {

                    DAL.Tbl_ClientInformation obj = new DAL.Tbl_ClientInformation
                    {

                        CompanyName = CompanyName,
                        ContactPerson = ContactPerson,
                        Number = Number,
                        Email = Email,
                        Address = Address,
                        DomainCompanyId = DomainCompanyId,

                        UpdatedBy = int.Parse(Session["UserId"].ToString()),
                        UpdatedDate = DateTime.Now
                    };

                    int result = DAL.Operations.OpClientInformation.UpdateRecord(obj, id);
                    if (result > 0)
                    {
                        lbl_message.Text = "Record updated successfully";
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
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_One_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                if (DAL.Operations.OpClientInformation.DeleteById(id))
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