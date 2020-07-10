using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class CreditStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session.Keys.Count > 0)
                {

                }
                else
                {
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }
                if (!IsPostBack)
                {
                    populate_grid();
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
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCreditStatus.GetAll());

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

                    DAL.Tbl_CreditStatus obj = new DAL.Tbl_CreditStatus
                    {
                        Description = Description,
                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now
                    };

                    int result = DAL.Operations.OpCreditStatus.InsertRecord(obj);
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
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void GV_One_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string Description = (GV_One.Rows[e.RowIndex].FindControl("txt_Description") as TextBox).Text.Trim();
                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());

                GV_One.EditIndex = -1;

                DAL.Tbl_CreditStatus obj = new DAL.Tbl_CreditStatus
                {
                    Description = Description,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                int result = DAL.Operations.OpCreditStatus.UpdateRecord(obj, id);
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
                if (DAL.Operations.OpCreditStatus.DeleteById(id))
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
    }
}