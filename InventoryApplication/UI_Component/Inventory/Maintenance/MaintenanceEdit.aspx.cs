using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Inventory.Maintenance
{
    public partial class MaintenanceEdit : System.Web.UI.Page
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
                            LoadData(CheckQueryString());
                        }
                        else
                        {
                            LoadAttachment();
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
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private int CheckQueryString()
        {
            int RecordId = 0;
            try
            {
                if (Request.QueryString.HasKeys())
                {
                    RecordId = int.Parse(Request.QueryString["RecordId"]);
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }

            return RecordId;
        }

        
        private void LoadData(int RecordId)
        {
            try
            {
                DAL.Tbl_Maintenance obj =  DAL.Operations.OpMaintenance.GetRecordbyId(RecordId);

                txt_Cost.Text = obj.Cost;
                txt_Description.Text = obj.Description;
                txt_DueDate.Text = obj.DueDate.ToString();
                txt_NumberofHours.Text = obj.NumberofHours.ToString();

                if(obj.MaintenanceCategoryId == 1)
                {
                    div_Brand.Visible = true;
                    div_Quantity.Visible = true;
                    div_Serial.Visible = true;
                    div_SelectedQuanity.Visible = true;

                    LoadDropdownTyre(DAL.Operations.OpTyreTotal.GetRecordbyId(obj.TotalTyreId.Value), obj.NumberofTyres.Value);
                }

                LoadDropdown(obj);
                LoadAttachment();


            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);

            }
        }
        private void LoadDropdown(DAL.Tbl_Maintenance obj)
        {
            try
            {
                try
                {
                    ddl_CarRegistration.DataSource = DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId);
                    ddl_CarRegistration.DataTextField = "PlateNumber";
                    ddl_CarRegistration.DataValueField = "Id";
                    ddl_CarRegistration.DataBind();
                    ddl_CarRegistration.Items.FindByValue(obj.CarId.ToString()).Selected = true;


                    ddl_DriverName.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId);
                    ddl_DriverName.DataTextField = "Name";
                    ddl_DriverName.DataValueField = "Id";
                    ddl_DriverName.DataBind();
                    ddl_DriverName.Items.FindByValue(obj.DriverId.ToString()).Selected = true;



                    ddl_MaintenanceCategoryDescription.DataSource = DAL.Operations.OpMaintenanceType.GetAll();
                    ddl_MaintenanceCategoryDescription.DataTextField = "Description";
                    ddl_MaintenanceCategoryDescription.DataValueField = "Id";
                    ddl_MaintenanceCategoryDescription.DataBind();
                    ddl_MaintenanceCategoryDescription.Items.FindByValue(obj.MaintenanceCategoryId.ToString()).Selected = true;



                }
                catch (Exception ex)
                {
                    lbl_message.Text = ex.Message;
                    DAL.Operations.OpLogger.LogError(ex);
                }
            }
            catch (Exception ex)
            {

                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LoadDropdownTyre(DAL.Tbl_TyreTotal obj,int quantity)
        {
            try
            {

                ddl_BrandName.DataSource = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId);
                ddl_BrandName.DataTextField = "BrandName";
                ddl_BrandName.DataValueField = "BrandName";
                ddl_BrandName.DataBind();
                ddl_BrandName.Items.FindByValue(obj.BrandName).Selected = true;


                ddl_SerialNumber.DataSource = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId);
                ddl_SerialNumber.DataTextField = "SerialNumber";
                ddl_SerialNumber.DataValueField = "SerialNumber";
                ddl_SerialNumber.DataBind();
                ddl_SerialNumber.Items.FindByValue(obj.SerialNumber).Selected = true;

                div_Quantity.Visible = true;
                txt_SelectedQuantity.Text = quantity.ToString();
                lbl_QuantityAvailable.Text = obj.Quantity.ToString();

            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LoadAttachment()
        {
            try
            {
                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(CheckQueryString(), 3).Where(x => x.DomainCompanyId == DomainCompanyId).ToList());

                Place_Attachment.Dispose();
                foreach (DataRow row in dt_Attachment.AsEnumerable())
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

                    LB_Attachment.Click += LB_Attachment_Click; ;
                    LB_Delete.Click += LB_Delete_Click; 
                    Place_Attachment.Controls.Add(LB_Attachment);
                    Place_Attachment.Controls.Add(LB_Delete);
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
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
                    Place_Attachment.Dispose();
                    Place_Attachment.Controls.Clear();
                    LoadAttachment();
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
                var Attachment = DAL.Operations.OpAttachment.GetRecordById(int.Parse(link.ID), 4);

                string filename = Attachment.filename;
                byte[] Byte_File = Attachment.fileinByte;
                string B64_filecontent = Encoding.UTF8.GetString(Byte_File);

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
                
        protected void ddl_MaintenanceCategoryDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(ddl_MaintenanceCategoryDescription.SelectedValue);
                if (id == 1)
                {
                    div_Brand.Visible = true;
                    ddl_BrandName.Items.Clear();


                    ddl_BrandName.DataSource = DAL.Operations.OpTyreTotal.GetAll();
                    ddl_BrandName.DataTextField = "BrandName";
                    ddl_BrandName.DataValueField = "BrandName";

                    ddl_BrandName.DataBind();
                    ddl_BrandName.Items.Insert(0, new ListItem("Select Brand", ""));

                }
                else
                {
                    div_Brand.Visible = false;
                    div_Quantity.Visible = false;
                    div_SelectedQuanity.Visible = false;
                    div_Serial.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void ddl_BrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                div_Serial.Visible = true;
                ddl_SerialNumber.Items.Clear();

                ddl_SerialNumber.DataSource = DAL.Operations.OpTyreTotal.GetAll();
                ddl_SerialNumber.DataTextField = "SerialNumber";
                ddl_SerialNumber.DataValueField = "SerialNumber";
                ddl_SerialNumber.DataBind();
                ddl_SerialNumber.Items.Insert(0, new ListItem("Select SerialNumber", ""));

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        protected void ddl_SerialNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                div_Quantity.Visible = true;
                div_SelectedQuanity.Visible = true;
                lbl_QuantityAvailable.Text = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.BrandName == ddl_BrandName.SelectedItem.Text && x.SerialNumber == ddl_SerialNumber.SelectedItem.Text).Select(x => x.Quantity).First().ToString();

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Place_Attachment.Controls.Count > 1 || file_Attachment.HasFiles)
                {
                    if (int.Parse(ddl_MaintenanceCategoryDescription.SelectedValue) == 1)
                    {
                        CreateTyreRecord();
                    }
                    else
                    {
                        int result = CreateMaintenance();
                        if (result > 1)
                        {
                            CreateAttachment(result);
                            DAL.Operations.OpLogger.Info("Maintenace record added");
                        }
                    }
                }
                else
                {
                    lbl_message.Text = "Please upload document";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }



        private void CreateTyreRecord()
        {
            try
            {
                if (int.Parse(txt_SelectedQuantity.Text) > int.Parse(lbl_QuantityAvailable.Text))
                {
                    lbl_message.Text = "Selected quantiy is greater than available quantity";
                }
                else
                {
                    int tyrecount = int.Parse(txt_SelectedQuantity.Text);
                    string brandname = ddl_BrandName.SelectedValue;
                    string serialnumber = ddl_SerialNumber.SelectedValue;


                    DAL.Tbl_TyreTotal obj = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.BrandName == brandname && x.SerialNumber == serialnumber).Where(x => x.DomainCompanyId == DomainCompanyId).First();

                    if (obj != null)
                    {
                        int result = CreateMaintenance(tyrecount, obj.Id);
                        if (result > -1)
                        {
                            CreateAttachment(result);
                            DAL.Operations.OpLogger.Info("Maintenace record added");


                            int prev = DAL.Operations.OpMaintenance.GetRecordbyId(CheckQueryString()).NumberofTyres.Value;
                            int now = int.Parse(txt_SelectedQuantity.Text);
                            if (now != prev)
                            {
                                int resultTyreTotal = InsertTotalTyre();
                            }

                            Response.Redirect("MaintenanceView.aspx?RecordId=" + result, false);
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


        private int CreateMaintenance()
        {
            int result = -1;
            try
            {
                if (ddl_CarRegistration.SelectedValue != "" && ddl_DriverName.SelectedValue != "" && ddl_MaintenanceCategoryDescription.SelectedValue != "")
                {
                    DAL.Tbl_Maintenance obj = new DAL.Tbl_Maintenance
                    {

                        Description = txt_Description.Text,
                        Cost = txt_Cost.Text,
                        NumberofHours = int.Parse(txt_NumberofHours.Text),
                        DueDate = Convert.ToDateTime(txt_DueDate.Text),
                        CarId = int.Parse(ddl_CarRegistration.SelectedValue),
                        DriverId = int.Parse(ddl_DriverName.SelectedValue),
                        DomainCompanyId = DomainCompanyId,
                        MaintenanceCategoryId = int.Parse(ddl_MaintenanceCategoryDescription.SelectedValue),

                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    result = DAL.Operations.OpMaintenance.UpdateRecord(obj, CheckQueryString());
                }
                else
                {
                    lbl_message.Text = "Please select all dropdown values";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private int CreateMaintenance(int numberoftyres,int tyreid)
        {
            int result = -1;
            try
            {
                if (ddl_CarRegistration.SelectedValue != "" && ddl_DriverName.SelectedValue != "" && ddl_MaintenanceCategoryDescription.SelectedValue != "")
                {
                    DAL.Tbl_Maintenance obj = new DAL.Tbl_Maintenance
                    {

                        Description = txt_Description.Text,
                        Cost = txt_Cost.Text,
                        NumberofHours = int.Parse(txt_NumberofHours.Text),
                        DueDate = Convert.ToDateTime(txt_DueDate.Text),
                        CarId = int.Parse(ddl_CarRegistration.SelectedValue),
                        DriverId = int.Parse(ddl_DriverName.SelectedValue),
                        MaintenanceCategoryId = int.Parse(ddl_MaintenanceCategoryDescription.SelectedValue),
                        NumberofTyres = numberoftyres,
                        DomainCompanyId = DomainCompanyId,
                        TotalTyreId = tyreid,

                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    result = DAL.Operations.OpMaintenance.UpdateRecord(obj, CheckQueryString());
                }
                else
                {
                    lbl_message.Text = "Please select all dropdown values";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private void CreateAttachment(int RecordID)
        {
            try
            {
                if (file_Attachment.HasFile || file_Attachment.HasFiles)
                {
                    foreach (HttpPostedFile file in file_Attachment.PostedFiles)
                    {
                        DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                        {
                            filename = file.FileName,
                            ItemId = RecordID,
                            AttachmentCategoryId = 3,
                            DomainCompanyId = DomainCompanyId,
                            fileinByte = ConvetStreamToByte(file.InputStream),
                            CreatedBy = int.Parse(Session["UserId"].ToString()),
                            CreationDate = DateTime.Now
                        };
                        int result = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private int updateMaintenance(int MaintenanceId, int TyreId)
        {
            int result = -1;
            try
            {

                DAL.Tbl_Maintenance obj = DAL.Operations.OpMaintenance.GetRecordbyId(MaintenanceId);
                obj.TotalTyreId = TyreId;

                result = DAL.Operations.OpMaintenance.UpdateRecord(obj, obj.Id);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private int InsertTotalTyre()
        {
            int result = -1;
            try
            {

                string brandname = ddl_BrandName.SelectedValue;
                string serialnumber = ddl_SerialNumber.SelectedValue;


                DAL.Tbl_TyreTotal obj = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.BrandName == brandname && x.SerialNumber == serialnumber).Where(x => x.DomainCompanyId == DomainCompanyId).First();

                if (obj != null)
                {
                    
                        obj.Quantity = (obj.Quantity - int.Parse(txt_SelectedQuantity.Text));
                        result = DAL.Operations.OpTyreTotal.UpdateRecord(obj, obj.Id);
                    
                }

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
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
    }
}