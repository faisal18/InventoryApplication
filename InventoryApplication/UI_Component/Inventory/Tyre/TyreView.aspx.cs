using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Text;
using System.Data;

namespace InventoryApplication.UI_Component.Inventory.Tyre
{
    public partial class TyreView : System.Web.UI.Page
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
                    LoadData(CheckQueryString());
                }
                else
                {
                    LoadAttachment(CheckQueryString());
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
                Tbl_Purchase_Tyre Obj =  DAL.Operations.OpPurchase_Tyre.GetRecordbyId(RecordId);

                txt_Amount.Text = Obj.Amount;
                txt_BrandName.Text = Obj.BrandName;
                txt_Company.Text = Obj.Company;
                txt_DateOfPurchase.Text = Obj.DateOfPurchase.ToString();
                txt_Description.Text = Obj.Description;
                txt_Gross.Text = Obj.Gross;
                txt_InvoiceNumber.Text = Obj.InvoiceNumber;
                txt_MOPId.Text = DAL.Operations.OpMaintenanceType.GetRecordbyId(Obj.MOPId.Value).Description;
                txt_NumberOfTyres.Text = Obj.NumberOfTyres;
                txt_SerialNumber.Text = Obj.SerialNumber;
                txt_Vat.Text = Obj.Vat;
                txt_CreditStatus.Text = DAL.Operations.OpCreditStatus.GetRecordbyId(Obj.CreditStatusId.Value).Description;
                txt_VendorName.Text = DAL.Operations.OpVendor.GetRecordbyId(Obj.VendorId.Value).Name;
                txt_DomainCompany.Text = DAL.Operations.OpDomainCompany.GetRecordbyId(Obj.DomainCompanyId.Value).CompanyName;


                LoadAttachment(RecordId);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        private void LoadAttachment(int RecordId)
        {
            try
            {

                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(RecordId, 1));

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
                var Attachment = DAL.Operations.OpAttachment.GetRecordById(int.Parse(link.ID), 1);

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
                Response.Redirect("TyreEdit.aspx?RecordId=" + CheckQueryString(), false);
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
                DAL.Operations.OpPurchase_Tyre.DeleteById(CheckQueryString());
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
    }
}