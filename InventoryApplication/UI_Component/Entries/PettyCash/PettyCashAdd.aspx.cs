using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries.PettyCash
{
    public partial class PettyCashAdd : System.Web.UI.Page
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
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpPettyCash_Add.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
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

                string Description = (GV_One.FooterRow.FindControl("txt_DescriptionFooter") as TextBox).Text.Trim();
                string Amount = (GV_One.FooterRow.FindControl("txt_AmountFooter") as TextBox).Text.Trim();



                DAL.Tbl_PettyCash_Add obj = new DAL.Tbl_PettyCash_Add
                {
                    Amount = Amount,
                    Description = Description,
                    DomainCompanyId = DomainCompanyId,

                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreatedDate = DateTime.Now
                };


                int result = DAL.Operations.OpPettyCash_Add.InsertRecord(obj);
                if (result > 0)
                {
                    InsertPettyCash(Amount, true);
                    lbl_message.Text = "Record added successfully";
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
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void GV_One_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string Description = (GV_One.Rows[e.RowIndex].FindControl("txt_Description") as TextBox).Text.Trim();
                string Amount = (GV_One.Rows[e.RowIndex].FindControl("txt_Amount") as TextBox).Text.Trim();
                


                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());

                string PrevAmount = DAL.Operations.OpPettyCash_Add.GetRecordbyId(id).Amount;


                GV_One.EditIndex = -1;

                DAL.Tbl_PettyCash_Add obj = new DAL.Tbl_PettyCash_Add
                {
                    Amount = Amount,
                    Description = Description,
                    DomainCompanyId = DomainCompanyId,

                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                int result = DAL.Operations.OpPettyCash_Add.UpdateRecord(obj, id);
                if (result > 0)
                {
                    InsertPettyCash(PrevAmount, false);
                    InsertPettyCash(Amount, true);

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
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void GV_One_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                string Amount = DAL.Operations.OpPettyCash_Add.GetRecordbyId(id).Amount;

                if (DAL.Operations.OpPettyCash_Add.DeleteById(id))
                {
                    InsertPettyCash(Amount, false);

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


        private void InsertPettyCash(string Amount, bool toAdd)
        {
            try
            {
                string amount = DAL.Operations.OpPettyCash_Total.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().Amount;
                string total = string.Empty;
                

                if (toAdd)
                {
                    total = (decimal.Parse(Amount) + decimal.Parse(amount)).ToString();
                }
                else
                {
                    total = (decimal.Parse(Amount) - decimal.Parse(amount)).ToString();
                }

                DAL.Tbl_PettyCash_Total obj = new DAL.Tbl_PettyCash_Total
                {
                    Amount = total,
                    DomainCompanyId = DomainCompanyId,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                int result = DAL.Operations.OpPettyCash_Total.UpdateRecord(obj, 1);
                DAL.Operations.OpLogger.Info("FuelTotal has been updated");

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