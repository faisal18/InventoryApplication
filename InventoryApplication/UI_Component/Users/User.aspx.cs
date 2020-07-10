using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Users
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                LoadAttachment("PH_View");
            }
            
        }

        private void populate_grid()
        {
            try
            {
                DataTable dt_new = GetData();
                if (dt_new.Rows.Count > 0)
                {
                    GV_User.DataSource = dt_new;
                    GV_User.DataBind();
                }
                else
                {
                    dt_new.Rows.Add(dt_new.NewRow());
                    GV_User.DataSource = dt_new;
                    GV_User.DataBind();
                    GV_User.Rows[0].Cells.Clear();
                    GV_User.Rows[0].Cells.Add(new TableCell());
                    GV_User.Rows[0].Cells[0].ColumnSpan = dt_new.Columns.Count;
                    GV_User.Rows[0].Cells[0].Text = "No Data Found !!";
                    GV_User.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
            DataTable dt_Roles = new DataTable();
            try
            {
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll());
                dt_Roles = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUserRoles.GetAll());

                dt = MergeTable(dt, dt_Roles);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_User, DataTable dt_Roles)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("RoleName", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Contact", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("BasicSalary", typeof(string));
                dt.Columns.Add("LicenseNumner", typeof(string));
                dt.Columns.Add("EmiratesId", typeof(string));
                dt.Columns.Add("VisaNumber", typeof(string));
                dt.Columns.Add("PassportNumber", typeof(string));
                dt.Columns.Add("MedicalNumber", typeof(string));

                dt.Columns.Add("LicenseExpiry", typeof(DateTime));
                dt.Columns.Add("EmiratesIdExpiry", typeof(DateTime));
                dt.Columns.Add("VisaExpiry", typeof(DateTime));
                dt.Columns.Add("PassportExpiry", typeof(DateTime));
                dt.Columns.Add("MedicalExpiry", typeof(DateTime));


                var result = from t1 in dt_User.AsEnumerable()
                             join t2 in dt_Roles.AsEnumerable()
                             on t1.Field<int>("RoleId") equals t2.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                t1.Field<int>("Id"),
                                t2.Field<string>("RoleDescription"),
                                t1.Field<string>("Name"),
                                t1.Field<string>("Contact"),
                                t1.Field<string>("Email"),
                                t1.Field<string>("Address"),
                                t1.Field<string>("BasicSalary"),
                                t1.Field<string>("LicenseNumner"),
                                t1.Field<string>("EmiratesId"),
                                t1.Field<string>("VisaNumber"),
                                t1.Field<string>("PassportNumber"),
                                t1.Field<string>("MedicalNumber"),

                                t1.Field<DateTime>("LicenseExpiry"),
                                t1.Field<DateTime>("EmiratesIdExpiry"),
                                t1.Field<DateTime>("VisaExpiry"),
                                t1.Field<DateTime>("PassportExpiry"),
                                t1.Field<DateTime>("MedicalExpiry"),

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

        protected void GV_User_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "AddNew")
                {

                    FileUpload FU_Foot = (GV_User.FooterRow.FindControl("FU_Foot") as FileUpload);

                    if (FU_Foot.HasFile)
                    {



                        int RoleId = int.Parse((GV_User.FooterRow.FindControl("ddl_RolesFooter") as DropDownList).SelectedValue.Trim());
                        string Name = (GV_User.FooterRow.FindControl("txt_NameFooter") as TextBox).Text.Trim();
                        string Contact = (GV_User.FooterRow.FindControl("txt_ContactFooter") as TextBox).Text.Trim();
                        string Email = (GV_User.FooterRow.FindControl("txt_EmailFooter") as TextBox).Text.Trim();
                        string Address = (GV_User.FooterRow.FindControl("txt_AddressFooter") as TextBox).Text.Trim();
                        string BasicSalary = (GV_User.FooterRow.FindControl("txt_BasicSalaryFooter") as TextBox).Text.Trim();
                        string LicenseNumber = (GV_User.FooterRow.FindControl("txt_LicenseNumberFooter") as TextBox).Text.Trim();
                        string EmiratesId = (GV_User.FooterRow.FindControl("txt_EmiratesIdFooter") as TextBox).Text.Trim();
                        string VisaNumber = (GV_User.FooterRow.FindControl("txt_VisaNumberFooter") as TextBox).Text.Trim();
                        string PassportNumber = (GV_User.FooterRow.FindControl("txt_PassportNumberFooter") as TextBox).Text.Trim();
                        string MedicalNumber = (GV_User.FooterRow.FindControl("txt_MedicalNumberFooter") as TextBox).Text.Trim();
                        string LicenseExpiry = (GV_User.FooterRow.FindControl("txt_LicenseExpiryFooter") as TextBox).Text.Trim();
                        string EmiratesIdExpiry = (GV_User.FooterRow.FindControl("txt_EmiratesIdExpiryFooter") as TextBox).Text.Trim();
                        string VisaExpiry = (GV_User.FooterRow.FindControl("txt_VisaExpiryFooter") as TextBox).Text.Trim();
                        string PassportExpiry = (GV_User.FooterRow.FindControl("txt_PassportExpiryFooter") as TextBox).Text.Trim();
                        string MedicalExpiry = (GV_User.FooterRow.FindControl("txt_MedicalExpiryFooter") as TextBox).Text.Trim();



                        DateTime DLicenseExpiry = DateTime.ParseExact(LicenseExpiry, "yyyy-MM-dd", null);
                        DateTime DEmiratesIdExpiry = DateTime.ParseExact(EmiratesIdExpiry, "yyyy-MM-dd", null);
                        DateTime DVisaExpiry = DateTime.ParseExact(VisaExpiry, "yyyy-MM-dd", null);
                        DateTime DPassportExpiry = DateTime.ParseExact(PassportExpiry, "yyyy-MM-dd", null);
                        DateTime DMedicalExpiry = DateTime.ParseExact(MedicalExpiry, "yyyy-MM-dd", null);


                        //operations

                        Tbl_User obj_user = new Tbl_User
                        {
                            RoleId = RoleId,
                            Name = Name,
                            Contact = Contact,
                            Email = Email,
                            Address = Address,
                            BasicSalary = BasicSalary,
                            LicenseNumner = LicenseNumber,
                            EmiratesId = EmiratesId,
                            VisaNumber = VisaNumber,
                            PassportNumber = PassportNumber,
                            MedicalNumber = MedicalNumber,
                            LicenseExpiry = DLicenseExpiry,
                            EmiratesIdExpiry = DEmiratesIdExpiry,
                            VisaExpiry = DVisaExpiry,
                            PassportExpiry = DPassportExpiry,
                            MedicalExpiry = DMedicalExpiry,
                            CreationDate = DateTime.Now,
                            CreatedBy = int.Parse(Session["UserId"].ToString()),
                        };

                        int result = DAL.Operations.OpUser.InsertRecord(obj_user);
                        if (result > 0)
                        {

                            foreach(HttpPostedFile file in FU_Foot.PostedFiles)
                            {
                                DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                                {
                                    filename = file.FileName,
                                    ItemId = result,
                                    AttachmentCategoryId = 7,
                                    fileinByte = ConvetStreamToByte(file.InputStream),
                                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                                    CreationDate = DateTime.Now
                                };
                                int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                            }

                            lbl_message.Text = "Record added successfully";
                        }
                        else
                        {
                            lbl_message.Text = "Something went wrong";
                        }


                        Enable_Footer();
                        populate_grid();
                        LoadAttachment("PH_View");
                    }
                    else
                    {
                        lbl_message.Text = "Please add a file";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }


        }
        protected void GV_User_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_User.DataKeys[e.RowIndex].Value.ToString());
                int roleid = int.Parse((GV_User.Rows[e.RowIndex].FindControl("ddl_Roles") as DropDownList).SelectedValue.ToString());
                string Name = (GV_User.Rows[e.RowIndex].FindControl("txt_Name") as TextBox).Text.Trim();
                string Contact = (GV_User.Rows[e.RowIndex].FindControl("txt_Contact") as TextBox).Text.Trim();
                string Email = (GV_User.Rows[e.RowIndex].FindControl("txt_Email") as TextBox).Text.Trim();
                string Address = (GV_User.Rows[e.RowIndex].FindControl("txt_Address") as TextBox).Text.Trim();
                string BasicSalary = (GV_User.Rows[e.RowIndex].FindControl("txt_BasicSalary") as TextBox).Text.Trim();
                string LicenseNumber = (GV_User.Rows[e.RowIndex].FindControl("txt_LicenseNumber") as TextBox).Text.Trim();
                string EmiratesId = (GV_User.Rows[e.RowIndex].FindControl("txt_EmiratesId") as TextBox).Text.Trim();
                string VisaNumber = (GV_User.Rows[e.RowIndex].FindControl("txt_VisaNumber") as TextBox).Text.Trim();
                string PassportNumber = (GV_User.Rows[e.RowIndex].FindControl("txt_PassportNumber") as TextBox).Text.Trim();
                string MedicalNumber = (GV_User.Rows[e.RowIndex].FindControl("txt_MedicalNumber") as TextBox).Text.Trim();
                string LicenseExpiry = (GV_User.Rows[e.RowIndex].FindControl("txt_LicenseExpiry") as TextBox).Text.Trim();
                string EmiratesIdExpiry = (GV_User.Rows[e.RowIndex].FindControl("txt_EmiratesIdExpiry") as TextBox).Text.Trim();
                string VisaExpiry = (GV_User.Rows[e.RowIndex].FindControl("txt_VisaExpiry") as TextBox).Text.Trim();
                string PassportExpiry = (GV_User.Rows[e.RowIndex].FindControl("txt_PassportExpiry") as TextBox).Text.Trim();
                string MedicalExpiry = (GV_User.Rows[e.RowIndex].FindControl("txt_MedicalExpiry") as TextBox).Text.Trim();
                FileUpload FU_Edit = (GV_User.Rows[e.RowIndex].FindControl("FU_Edit") as FileUpload);


                DateTime DLicenseExpiry = DateTime.Parse(LicenseExpiry);
                DateTime DEmiratesIdExpiry = DateTime.Parse(EmiratesIdExpiry);
                DateTime DVisaExpiry = DateTime.Parse(VisaExpiry);
                DateTime DPassportExpiry = DateTime.Parse(PassportExpiry);
                DateTime DMedicalExpiry = DateTime.Parse(MedicalExpiry);

                GV_User.EditIndex = -1;

                Tbl_User obj_user = new Tbl_User
                {
                    RoleId = roleid,
                    Name = Name,
                    Contact = Contact,
                    Email = Email,
                    Address = Address,
                    BasicSalary = BasicSalary,
                    LicenseNumner = LicenseNumber,
                    EmiratesId = EmiratesId,
                    VisaNumber = VisaNumber,
                    PassportNumber = PassportNumber,
                    MedicalNumber = MedicalNumber,
                    LicenseExpiry = DLicenseExpiry,
                    EmiratesIdExpiry = DEmiratesIdExpiry,
                    VisaExpiry = DVisaExpiry,
                    PassportExpiry = DPassportExpiry,
                    MedicalExpiry = DMedicalExpiry,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                };

                int result = DAL.Operations.OpUser.UpdateRecord(obj_user, id);
                if (result > 0)
                {

                    if (FU_Edit.HasFile)
                    {
                        foreach (HttpPostedFile file in FU_Edit.PostedFiles)
                        {
                            DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                            {
                                filename = file.FileName,
                                ItemId = id,
                                AttachmentCategoryId = 7,
                                fileinByte = ConvetStreamToByte(file.InputStream),
                                CreatedBy = int.Parse(Session["UserId"].ToString()),
                                CreationDate = DateTime.Now
                            };
                            int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                        }
                    }

                    lbl_message.Text = "Record updated successfully";

                }
                else
                {
                    lbl_message.Text = "Something went wrong";
                }

                Enable_Footer();
                populate_grid();
                LoadAttachment("PH_View");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_User_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GV_User.DataKeys[e.RowIndex].Value.ToString());
                if (DAL.Operations.OpUser.DeleteById(id))
                {
                    lbl_message.Text = "Record deleted successfully";
                }
                else
                {
                    lbl_message.Text = "Record deleltion failed";
                }
                populate_grid();
                LoadAttachment("PH_View");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_User_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                GV_User.EditIndex = e.NewEditIndex;
                GV_User.FooterRow.Visible = false;
                Disable_Footer();
                populate_grid();

                LoadAttachment("PH_Edit");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_User_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_User.EditIndex = -1;
                Enable_Footer();
                populate_grid();
                LoadAttachment("PH_View");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_User_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_User.PageIndex = e.NewPageIndex;
                populate_grid();
                Enable_Footer();
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void GV_User_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (GV_User.EditIndex == e.Row.RowIndex)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddl_Roles");
                        ddlRole.DataSource = DAL.Operations.OpUserRoles.GetAll();
                        ddlRole.DataTextField = "RoleDescription";
                        ddlRole.DataValueField = "Rank";
                        ddlRole.DataBind();
                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        DropDownList ddlFRRole = (DropDownList)e.Row.FindControl("ddl_RolesFooter");
                        ddlFRRole.DataSource = DAL.Operations.OpUserRoles.GetAll();
                        ddlFRRole.DataTextField = "RoleDescription";
                        ddlFRRole.DataValueField = "Rank";
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
            GV_User.ShowFooter = true;
        }
        private void Disable_Footer()
        {
            GV_User.ShowFooter = false;
        }

        private void LoadAttachment(string PH_Name)
        {
            try
            {
                DataTable dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpUser.GetAll());
                if(dt!=null)
                {
                    foreach(DataRow row1 in dt.Rows)
                    {

                        int i = int.Parse(row1["Id"].ToString());
                        DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(i, 7));

                        if (dt_Attachment.Rows.Count > 0)
                        {
                            
                            int row3 = IndexOfGridView(GV_User, i);

                            PlaceHolder Place_Attachment = (PlaceHolder)GV_User.Rows[row3].Cells[17].FindControl(PH_Name);


                            foreach (DataRow row in dt_Attachment.AsEnumerable())
                            {

                                if (PH_Name == "PH_View")
                                {
                                    string filename = row["filename"].ToString();
                                    string AttachmentID = row["Id"].ToString();

                                    LinkButton LB_Attachment = new LinkButton();
                                    LB_Attachment.Text = filename + "<br/>";
                                    LB_Attachment.ID = AttachmentID;
                                    LB_Attachment.CommandArgument = AttachmentID;
                                    LB_Attachment.CommandName = AttachmentID;
                                    LB_Attachment.Click += LB_Attachment_Click;
                                    Place_Attachment.Controls.Add(LB_Attachment);
                                }
                                else if (PH_Name == "PH_Edit")
                                {
                                    string filename = row["filename"].ToString();
                                    string AttachmentID = row["Id"].ToString();

                                    LinkButton LB_Attachment = new LinkButton();
                                    LB_Attachment.Text = filename + "         ";
                                    LB_Attachment.ID = AttachmentID;
                                    LB_Attachment.CommandArgument = AttachmentID;
                                    LB_Attachment.CommandName = AttachmentID;

                                    LinkButton LB_Delete = new LinkButton();
                                    LB_Delete.Text = "Remove File " + "<br/>";
                                    LB_Delete.ID = AttachmentID + "-Delete";
                                    LB_Delete.CommandArgument = AttachmentID;
                                    LB_Delete.CommandName = "Delete File";

                                    LB_Attachment.Click += LB_Attachment_Click;
                                    LB_Delete.Click += LB_Delete_Click; ;
                                    Place_Attachment.Controls.Add(LB_Attachment);
                                    Place_Attachment.Controls.Add(LB_Delete);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
               
            }
        }
        private void LB_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var link = sender as LinkButton;
                int FileId = int.Parse(link.CommandArgument);

                if (DAL.Operations.OpAttachment.DeleteById(FileId))
                {
                    lbl_message.Text = "File removed from ticket";
                    LoadAttachment("PH_Edit");
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LB_Attachment_Click(object sender, EventArgs e)
        {
            try
            {
                var link = sender as LinkButton;
                var Attachment = DAL.Operations.OpAttachment.GetRecordById(int.Parse(link.ID), 7);

                string filename = Attachment.filename;
                byte[] Byte_File = Attachment.fileinByte;
                string B64_filecontent = System.Text.Encoding.UTF8.GetString(Byte_File);

                string AppDataDir = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
                string FileDir = AppDataDir + "\\" + filename;
                System.IO.File.WriteAllBytes(FileDir, Byte_File);
                download_string(FileDir);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void download_string(string path)
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
        private static byte[] ConvetStreamToByte(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private static int IndexOfGridView(GridView gridView, int dataKeyId)
        {
            int index = 0;
            foreach (GridViewRow gvRow in gridView.Rows)
            {
                var dataKey = gridView.DataKeys[gvRow.DataItemIndex];
                if (dataKey == null || (int)dataKey.Value != dataKeyId) continue;
                index = gvRow.DataItemIndex;
                break;
            }
            return index;
        }
    }
}