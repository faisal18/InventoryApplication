using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if(Session.Keys.Count>0)
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
            DataTable dt1 = new DataTable();
            
            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll());
                dt1 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUserLogin.GetAll());

                dt = MergeTable(dt, dt1);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_User,DataTable dt_UserLogin)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("User_Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("Password", typeof(string));


                var result = from t1 in dt_UserLogin.AsEnumerable()
                             join t2 in dt_User.AsEnumerable()
                             on t1.Field<int>("User_Id") equals t2.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                 t2.Field<int>("Id"),
                                 t2.Field<string>("Name"),
                                 t1.Field<string>("Username"),
                                 t1.Field<string>("Password"),
                             }, false);

                if(result.Count()>0)
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
                    string User_Id = (GV_One.FooterRow.FindControl("ddl_User_IdFooter") as DropDownList).SelectedValue.Trim();
                    string Username = (GV_One.FooterRow.FindControl("txt_UsernameFooter") as TextBox).Text.Trim();
                    string Password = (GV_One.FooterRow.FindControl("txt_PasswordFooter") as TextBox).Text.Trim();

                    DAL.Tbl_UserLogin obj_userlogin = new DAL.Tbl_UserLogin
                    {
                        User_Id = int.Parse(User_Id),
                        Username = Username,
                        Password = Password,


                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    int result = DAL.Operations.OpUserLogin.InsertRecord(obj_userlogin);
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

                string User_Id = (GV_One.Rows[e.RowIndex].FindControl("ddl_User_Id") as DropDownList).SelectedValue.Trim();
                string Username = (GV_One.Rows[e.RowIndex].FindControl("txt_Username") as TextBox).Text.Trim();
                string Password = (GV_One.Rows[e.RowIndex].FindControl("txt_Password") as TextBox).Text.Trim();

                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                DAL.Tbl_UserLogin obj_userlogin = new DAL.Tbl_UserLogin
                {
                    User_Id = int.Parse(User_Id.ToString()),
                    Username = Username,
                    Password = Password,

                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };


                int result = DAL.Operations.OpUserLogin.UpdateRecord(obj_userlogin, id);
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
                if (DAL.Operations.OpUser.DeleteById(id))
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
                        DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddl_User_Id");
                        ddlRole.DataSource = DAL.Operations.OpUser.GetAll();
                        ddlRole.DataTextField = "Name";
                        ddlRole.DataValueField = "Id";
                        ddlRole.DataBind();
                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        DropDownList ddlFRRole = (DropDownList)e.Row.FindControl("ddl_User_IdFooter");
                        ddlFRRole.DataSource = DAL.Operations.OpUser.GetAll();
                        ddlFRRole.DataTextField = "Name";
                        ddlFRRole.DataValueField = "Id";
                        ddlFRRole.DataBind();
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

    }
}