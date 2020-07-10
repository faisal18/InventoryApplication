using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

namespace InventoryApplication.UI_Component.Inventory.Maintenance
{
    public partial class MaintenanceView : System.Web.UI.Page
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
                            Loader(CheckQueryString());
                        }
                        else
                        {
                            LoadAttachment(CheckQueryString());
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
                lbl_message.Text = ex.Message; DAL.Operations.OpLogger.LogError(ex);
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

        private void Loader(int RecordId)
        {
            try
            {
                DAL.Tbl_Maintenance obj = DAL.Operations.OpMaintenance.GetRecordbyId(RecordId);

                txt_CarRegistration.Text = DAL.Operations.OpCar.GetRecordbyId(obj.CarId).Description;
                txt_Cost.Text = obj.Cost;
                txt_Description.Text = obj.Description;
                txt_DriverName.Text = DAL.Operations.OpUser.GetRecordbyId(obj.DriverId.Value).Name;
                txt_DueDate.Text = obj.DueDate.ToString();
                txt_MaintenanceCategoryDescription.Text = DAL.Operations.OpMaintenanceType.GetRecordbyId(obj.MaintenanceCategoryId).Description;
                txt_NumberofHours.Text = obj.NumberofHours.ToString();
                if(obj.MaintenanceCategoryId == 1)
                {
                    LoadTyre(obj);
                }

                LoadAttachment(RecordId);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LoadTyre(DAL.Tbl_Maintenance obj)
        {
            try
            {

                div_Brand.Visible = true;
                div_Quantity.Visible = true;
                div_Serial.Visible = true;

                int TotalTyreId = obj.TotalTyreId.Value;
                DAL.Tbl_TyreTotal tyre = DAL.Operations.OpTyreTotal.GetRecordbyId(TotalTyreId);

                txt_BrandName.Text = tyre.BrandName;
                txt_SerialNumber.Text = tyre.SerialNumber;
                lbl_QuantityAvailable.Text = obj.NumberofTyres.ToString();
            }
            catch (Exception ex)
            {

                DAL.Operations.OpLogger.LogError(ex); 
            }
        }
        private void LoadAttachment(int RecordId)
        {
            try
            {

                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(RecordId, 3));

                Place_Attachment.Dispose();
                Place_Attachment.Controls.Clear();
                foreach (DataRow row in dt_Attachment.AsEnumerable())
                {
                    string filename = row["filename"].ToString();
                    string AttachmentID = row["Id"].ToString();

                    LinkButton LB_Attachment = new LinkButton();
                    LB_Attachment.Text = filename + "<br/>";
                    LB_Attachment.ID = AttachmentID;
                    LB_Attachment.CommandArgument = AttachmentID;
                    LB_Attachment.CommandName = AttachmentID;
                    LB_Attachment.Click += LB_Attachment_Click; ;
                    Place_Attachment.Controls.Add(LB_Attachment);
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

        protected void btn_EditTicket_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MaintenanceEdit.aspx?RecordId=" + CheckQueryString(), false);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);

            }
        }
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //string Gallons = DAL.Operations.OpFuelTotal.GetRecordbyId(1).TotalFuel.Value.ToString();
                //if (DAL.Operations.OpPurchase_Fuel.DeleteById(CheckQueryString()))
                //{
                //    UpdateFuelTotal(Gallons);
                //}
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
    }
}