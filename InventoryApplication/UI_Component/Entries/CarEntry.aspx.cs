using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class CarEntry : System.Web.UI.Page
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
                            LoadAttachment("PH_View");
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
                dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x=>x.DomainCompanyId == int.Parse(Session["DomainCompanyId"].ToString())).ToList());
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return dt;
        }
        private DataTable MergeTable(DataTable dt_car, DataTable dt_cartype)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("Id", typeof(int));

                dt.Columns.Add("PlateNumber", typeof(string));
                dt.Columns.Add("ChasisNumber", typeof(string));
                dt.Columns.Add("RegistrationNumber", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Active", typeof(bool));
                dt.Columns.Add("PortTags", typeof(string));
                dt.Columns.Add("RoadPermission", typeof(bool));
                dt.Columns.Add("LoadingPermission", typeof(bool));
                dt.Columns.Add("CarTypeId", typeof(int));
                dt.Columns.Add("CarTypeDesc", typeof(string));



                var result = from t1 in dt_car.AsEnumerable()
                             join t2 in dt_cartype.AsEnumerable()
                             on t1.Field<int>("CarTypeId") equals t2.Field<int>("Id")

                             select dt.LoadDataRow(new object[]
                             {
                                t1.Field<int>("Id"),
                                t1.Field<string>("PlateNumber"),
                                t1.Field<string>("ChasisNumber"),
                                t1.Field<string>("RegistrationNumber"),
                                t1.Field<string>("Model"),
                                t1.Field<string>("Description"),
                                t1.Field<bool>("Active"),
                                t1.Field<string>("PortTags"),
                                t1.Field<bool>("RoadPermission"),
                                t1.Field<bool>("LoadingPermission"),
                                t2.Field<int>("Id"),
                                t2.Field<string>("CarType"),

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
                    FileUpload FU_Foot = (GV_One.FooterRow.FindControl("FU_Foot") as FileUpload);
                    if (FU_Foot.HasFile)
                    {

                        string PlateNumber = (GV_One.FooterRow.FindControl("txt_PlateNumberFooter") as TextBox).Text.Trim();
                        string ChasisNumber = (GV_One.FooterRow.FindControl("txt_ChasisNumberFooter") as TextBox).Text.Trim();
                        string RegistrationNumber = (GV_One.FooterRow.FindControl("txt_RegistrationNumberFooter") as TextBox).Text.Trim();
                        string Model = (GV_One.FooterRow.FindControl("txt_ModelFooter") as TextBox).Text.Trim();
                        string Description = (GV_One.FooterRow.FindControl("txt_DescriptionFooter") as TextBox).Text.Trim();
                        string Active = (GV_One.FooterRow.FindControl("txt_ActiveFooter") as DropDownList).SelectedValue.Trim();
                        string PortTags = (GV_One.FooterRow.FindControl("txt_PortTagsFooter") as TextBox).Text.Trim();
                        string RoadPermission = (GV_One.FooterRow.FindControl("txt_RoadPermissionFooter") as DropDownList).SelectedValue.Trim();
                        string LoadingPermission = (GV_One.FooterRow.FindControl("txt_LoadingPermissionFooter") as DropDownList).SelectedValue.Trim();
                        string CarTypeId = (GV_One.FooterRow.FindControl("txt_CarTypeIdFooter") as TextBox).Text.Trim();


                        if (PlateNumber.Length > 0 && RegistrationNumber.Length > 0)
                        {
                            DAL.Tbl_Car obj = new DAL.Tbl_Car
                            {

                                CarTypeId = CarTypeId,

                                PlateNumber = PlateNumber,
                                ChasisNumber = ChasisNumber,
                                RegistrationNumber = RegistrationNumber,
                                Model = Model,
                                Description = Description,
                                PortTags = PortTags,
                                Active = bool.Parse(Active),
                                RoadPermission = bool.Parse(RoadPermission),
                                LoadingPermission = bool.Parse(LoadingPermission),
                                DomainCompanyId = DomainCompanyId,

                                CreatedBy = int.Parse(Session["UserId"].ToString()),
                                CreationDate = DateTime.Now

                            };

                            int result = DAL.Operations.OpCar.InsertRecord(obj);
                            if (result > 0)
                            {
                                lbl_message.Text = "Record added successfully";
                                foreach (HttpPostedFile file in FU_Foot.PostedFiles)
                                {
                                    DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                                    {
                                        filename = file.FileName,
                                        ItemId = result,
                                        AttachmentCategoryId = 9,
                                        DomainCompanyId = DomainCompanyId,
                                        fileinByte = ConvetStreamToByte(file.InputStream),
                                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                                        CreationDate = DateTime.Now
                                    };
                                    int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                                }
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
                            lbl_message.Text = "Please enter data in mandatory fields";
                        }
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
                string PlateNumber = (GV_One.Rows[e.RowIndex].FindControl("txt_PlateNumber") as TextBox).Text.Trim();
                string ChasisNumber = (GV_One.Rows[e.RowIndex].FindControl("txt_ChasisNumber") as TextBox).Text.Trim();
                string RegistrationNumber = (GV_One.Rows[e.RowIndex].FindControl("txt_RegistrationNumber") as TextBox).Text.Trim();
                string Model = (GV_One.Rows[e.RowIndex].FindControl("txt_Model") as TextBox).Text.Trim();
                string Description = (GV_One.Rows[e.RowIndex].FindControl("txt_Description") as TextBox).Text.Trim();
                string Active = (GV_One.Rows[e.RowIndex].FindControl("txt_Active") as DropDownList).SelectedValue.Trim();
                string PortTags = (GV_One.Rows[e.RowIndex].FindControl("txt_PortTags") as TextBox).Text.Trim();
                string RoadPermission = (GV_One.Rows[e.RowIndex].FindControl("txt_RoadPermission") as DropDownList).SelectedValue.Trim();
                string LoadingPermission = (GV_One.Rows[e.RowIndex].FindControl("txt_LoadingPermission") as DropDownList).SelectedValue.Trim();
                string CarTypeId = (GV_One.Rows[e.RowIndex].FindControl("txt_CarTypeId") as TextBox ).Text.Trim();
                FileUpload FU_Edit = (GV_One.Rows[e.RowIndex].FindControl("FU_Edit") as FileUpload);



                int id = Convert.ToInt32(GV_One.DataKeys[e.RowIndex].Value.ToString());
                GV_One.EditIndex = -1;

                if (PlateNumber.Length > 0 && RegistrationNumber.Length > 0)
                {

                    DAL.Tbl_Car obj = new DAL.Tbl_Car
                    {

                        PlateNumber = PlateNumber,
                        ChasisNumber = ChasisNumber,
                        RegistrationNumber = RegistrationNumber,
                        Model = Model,
                        Description = Description,
                        Active = bool.Parse(Active),
                        PortTags = PortTags,
                        RoadPermission = bool.Parse(RoadPermission),
                        LoadingPermission = bool.Parse(LoadingPermission),
                        CarTypeId = CarTypeId,
                        DomainCompanyId = DomainCompanyId,

                        UpdatedBy = int.Parse(Session["UserId"].ToString()),
                        UpdateDate = DateTime.Now
                    };


                    int result = DAL.Operations.OpCar.UpdateRecord(obj, id);
                    if (result > 0)
                    {
                        foreach (HttpPostedFile file in FU_Edit.PostedFiles)
                        {
                            DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                            {
                                filename = file.FileName,
                                ItemId = id,
                                AttachmentCategoryId = 9,
                                DomainCompanyId = DomainCompanyId,
                                fileinByte = ConvetStreamToByte(file.InputStream),
                                CreatedBy = int.Parse(Session["UserId"].ToString()),
                                CreationDate = DateTime.Now
                            };
                            int result2 = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
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
                if (DAL.Operations.OpCar.DeleteById(id))
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
        protected void GV_One_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                GV_One.EditIndex = e.NewEditIndex;
                GV_One.FooterRow.Visible = false;
                Disable_Footer();
                populate_grid();
                LoadAttachment("PH_Edit");

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
                LoadAttachment("PH_View");

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
                LoadAttachment("PH_View");

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
                //if (GV_One.EditIndex == e.Row.RowIndex)
                //{
                //    if (e.Row.RowType == DataControlRowType.DataRow)
                //    {
                //        DropDownList ddlRole = (DropDownList)e.Row.FindControl("txt_CarTypeId");
                //        ddlRole.DataSource = DAL.Operations.OpCarType.GetAll();
                //        ddlRole.DataTextField = "CarType";
                //        ddlRole.DataValueField = "Id";
                //        ddlRole.DataBind();
                //    }
                //    if (e.Row.RowType == DataControlRowType.Footer)
                //    {
                //        DropDownList ddlFRRole = (DropDownList)e.Row.FindControl("txt_CarTypeIdFooter");
                //        ddlFRRole.DataSource = DAL.Operations.OpCarType.GetAll();
                //        ddlFRRole.DataTextField = "CarType";
                //        ddlFRRole.DataValueField = "Id";
                //        ddlFRRole.DataBind();
                //    }
                //}
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

        private void LoadAttachment(string PH_Name)
        {
            try
            {
                DataTable dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpCar.GetAll().Where(x=>x.DomainCompanyId == DomainCompanyId).ToList());
                if (dt != null)
                {
                    foreach (DataRow row1 in dt.Rows)
                    {

                        int i = int.Parse(row1["Id"].ToString());
                        DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(i, 9).Where(x=>x.DomainCompanyId == DomainCompanyId).ToList());

                        if (dt_Attachment.Rows.Count > 0)
                        {

                            int row3 = IndexOfGridView(GV_One, i);

                            PlaceHolder Place_Attachment = (PlaceHolder)GV_One.Rows[row3].Cells[10].FindControl(PH_Name);


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
                var Attachment = DAL.Operations.OpAttachment.GetRecordById(int.Parse(link.ID), 9);

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
        private static byte[] ConvetStreamToByte(System.IO.Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
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