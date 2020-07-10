using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class VendorInformation : System.Web.UI.Page
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
            DataTable dt1 = new DataTable();

            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpVendor.GetAll().Where(x => x.DomainCompanyId == int.Parse(Session["DomainCompanyId"].ToString())).ToList());
                dt1 = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCreditStatus.GetAll());

                dt = MergeTable(dt, dt1);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_vendor, DataTable dt_credit)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("Id", typeof(int));

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Contact", typeof(string));
                dt.Columns.Add("Credit", typeof(string));



                var result = from t1 in dt_vendor.AsEnumerable()
                             join t2 in dt_credit.AsEnumerable()
                             on  t1.Field<int>("CreditStatusId") equals t2.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                t1.Field<int>("Id"),
                                t1.Field<string>("Name"),
                                t1.Field<string>("Email"),
                                t1.Field<string>("Contact"),
                                t2.Field<string>("Description"),

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


                    string Name = (GV_One.FooterRow.FindControl("txt_NameFooter") as TextBox).Text.Trim();
                    string Email = (GV_One.FooterRow.FindControl("txt_EmailFooter") as TextBox).Text.Trim();
                    string Contact = (GV_One.FooterRow.FindControl("txt_ContactFooter") as TextBox).Text.Trim();
                    string Credit = (GV_One.FooterRow.FindControl("txt_CreditFooter") as DropDownList).SelectedValue.Trim();



                    if (Name.Length > 0 && Email.Length > 0)
                    {
                        DAL.Tbl_Vendor obj = new DAL.Tbl_Vendor
                        {

                            Name = Name,
                            Email = Email,
                            Contact = Contact,
                            //CreditStatusId = int.Parse(Credit),
                            CreditStatusId = int.Parse(Credit),
                            DomainCompanyId = DomainCompanyId,
                            CreatedBy = int.Parse(Session["UserId"].ToString()),
                            CreatedDate = DateTime.Now

                        };

                        int result = DAL.Operations.OpVendor.InsertRecord(obj);
                        if (result > 0)
                        {
                            lbl_message.Text = "Record added successfully";
                            //foreach (HttpPostedFile file in FU_Foot.PostedFiles)
                            //{
                            //    DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                            //    {
                            //        filename = file.FileName,
                            //        ItemId = result,
                            //        AttachmentCategoryId = 9,
                            //        DomainCompanyId = DomainCompanyId,
                            //        fileinByte = ConvetStreamToByte(file.InputStream),
                            //        CreatedBy = int.Parse(Session["UserId"].ToString()),
                            //        CreationDate = DateTime.Now
                            //    };
                            //    int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                            //}
                        }
                        else
                        {
                            lbl_message.Text = "Something went wrong";
                        }

                        Enable_Footer();
                        populate_grid();
                        //LoadAttachment("PH_View");

                    }
                    else
                    {
                        lbl_message.Text = "Please enter data in mandatory fields";
                    }

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
                string Name = (GV_One.Rows[e.RowIndex].FindControl("txt_Name") as TextBox).Text.Trim();
                string Email = (GV_One.Rows[e.RowIndex].FindControl("txt_Email") as TextBox).Text.Trim();
                string Contact = (GV_One.Rows[e.RowIndex].FindControl("txt_Contact") as TextBox).Text.Trim();
                string Credit = (GV_One.Rows[e.RowIndex].FindControl("txt_Credit") as DropDownList).SelectedValue.Trim();


                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                if (Name.Length > 0 && Email.Length > 0)
                {

                    DAL.Tbl_Vendor obj = new DAL.Tbl_Vendor
                    {

                        Name= Name,
                        Email = Email,
                        Contact = Contact,
                        CreditStatusId = int.Parse(Credit),
                        DomainCompanyId = DomainCompanyId,

                        UpdatedBy = int.Parse(Session["UserId"].ToString()),
                        UpdatedDate = DateTime.Now
                    };


                    int result = DAL.Operations.OpVendor.UpdateRecord(obj, id);
                    if (result > 0)
                    {
                        //foreach (HttpPostedFile file in FU_Edit.PostedFiles)
                        //{
                        //    DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                        //    {
                        //        filename = file.FileName,
                        //        ItemId = id,
                        //        AttachmentCategoryId = 9,
                        //        DomainCompanyId = DomainCompanyId,
                        //        fileinByte = ConvetStreamToByte(file.InputStream),
                        //        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        //        CreationDate = DateTime.Now
                        //    };
                        //    int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                        //}
                        lbl_message.Text = "Record updated successfully";
                    }
                    else
                    {
                        lbl_message.Text = "Something went wrong";
                    }

                    Enable_Footer();
                    populate_grid();
                    //LoadAttachment("PH_View");

                }
                else
                {
                    lbl_message.Text = "Please enter data in mandatory fields";
                }
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
                if (DAL.Operations.OpVendor.DeleteById(id))
                {
                    lbl_message.Text = "Record deleted successfully";
                }
                else
                {
                    lbl_message.Text = "Record deleltion failed";
                }
                populate_grid();
                //LoadAttachment("PH_View");

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
                //LoadAttachment("PH_Edit");

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
                //LoadAttachment("PH_View");

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
                //LoadAttachment("PH_View");

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
                        DropDownList ddl_CreditStaus = (DropDownList)e.Row.FindControl("txt_Credit");
                        ddl_CreditStaus.DataSource = DAL.Operations.OpCreditStatus.GetAll();
                        ddl_CreditStaus.DataTextField = "Description";
                        ddl_CreditStaus.DataValueField = "Id";
                        ddl_CreditStaus.DataBind();
                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        DropDownList ddl_CreditStaus = (DropDownList)e.Row.FindControl("txt_CreditFooter");
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
    }
}