using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Text;

namespace InventoryApplication.UI_Component.Inventory.Insurance
{
    public partial class InsuranceEdit : System.Web.UI.Page
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
                Tbl_Purchase_Insurance Obj = DAL.Operations.OpPurchase_Insurance.GetRecordbyId(RecordId);

                txt_Amount.Text = Obj.Amount;
                txt_DateOfPurchase.Text = Obj.DateOfPurchase.ToString();
                txt_Gross.Text = Obj.Gross;
                txt_InsuranceCompany.Text = Obj.InsuranceCompany;
                txt_InsuranceExpiryDate.Text = Obj.InsuranceExpiryDate.ToString();
                txt_InsuranceStartDate.Text = Obj.InsuranceStartDate.ToString();
                //txt_InsuranceTypeId.Text = DAL.Operations.OpInsuranceType.GetRecordbyId(Obj.InsuranceTypeId.Value).InsuranceType;
                txt_Insurer.Text = Obj.Insurer;
                txt_InvoiceNumber.Text = Obj.InvoiceNumber;
                //txt_MOPId.Text = DAL.Operations.OpModeofPayment.GetRecordbyId(Obj.MOPId.Value).ModeOfPayment;
                txt_PolicyNumber.Text = Obj.PolicyNumber;
                txt_Vat.Text = Obj.Vat;

                LoadDropdown(Obj.MOPId.ToString(),Obj.InsuranceTypeId.ToString(),Obj.DomainCompanyId.ToString(),Obj.CreditStatusId.ToString(),Obj.VendorId.ToString());
                LoadAttachment();


            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);

            }
        }
        private void LoadDropdown(string MOP,string IT,string DC,string CS,string Vendor)
        {
            try
            {
                txt_MOPId.DataSource = DAL.Operations.OpModeofPayment.GetAll();
                txt_MOPId.DataTextField = "ModeOfPayment";
                txt_MOPId.DataValueField = "Id";
                txt_MOPId.DataBind();
                txt_MOPId.Items.FindByValue(MOP).Selected = true;

                txt_InsuranceTypeId.DataSource = DAL.Operations.OpInsuranceType.GetAll();
                txt_InsuranceTypeId.DataTextField = "InsuranceType";
                txt_InsuranceTypeId.DataValueField = "Id";
                txt_InsuranceTypeId.DataBind();
                txt_InsuranceTypeId.Items.FindByValue(IT).Selected = true;

                ddl_domainCompany.DataSource = DAL.Operations.OpDomainCompany.GetAll().Where(x => x.Id == DomainCompanyId).ToList();
                ddl_domainCompany.DataTextField = "CompanyName";
                ddl_domainCompany.DataValueField = "Id";
                ddl_domainCompany.DataBind();
                ddl_domainCompany.Items.FindByValue(DC).Selected = true;

                ddl_CreditStaus.DataSource = DAL.Operations.OpCreditStatus.GetAll();
                ddl_CreditStaus.DataTextField = "Description";
                ddl_CreditStaus.DataValueField = "Id";
                ddl_CreditStaus.DataBind();
                ddl_CreditStaus.Items.FindByValue(CS).Selected = true;

                ddl_Vendor.DataSource = DAL.Operations.OpVendor.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                ddl_Vendor.DataTextField = "Name";
                ddl_Vendor.DataValueField = "Id";
                ddl_Vendor.DataBind();
                ddl_Vendor.Items.FindByValue(Vendor).Selected = true;


            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private void LoadAttachment()
        {
            try
            {
                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(CheckQueryString(), 3));

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
                    LB_Delete.Click += LB_Delete_Click; ;
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
                var Attachment = DAL.Operations.OpAttachment.GetRecordById(int.Parse(link.ID), 3);

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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Place_Attachment.Controls.Count > 1)
                {
                    int recordID = UpdatePurchaseInsurance();
                    if (recordID > 0)
                    {
                        CreateAttachment(recordID);
                        lbl_message.Text = "Record added successfully";
                        DAL.Operations.OpLogger.Info("PurchaseInsurance record updated by " + Session["Name"].ToString());

                        Response.Redirect("InsuranceView.aspx?RecordId=" + recordID, false);

                    }
                }
                else
                {
                    lbl_message.Text = "Please upload document";
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }


        private int UpdatePurchaseInsurance()
        {
            int result = -1;
            try
            {

                int companyid = int.Parse(ddl_domainCompany.SelectedValue);

                DAL.Tbl_Purchase_Insurance obj = new DAL.Tbl_Purchase_Insurance
                {

                    MOPId = int.Parse(txt_MOPId.SelectedValue),
                    InsuranceTypeId = int.Parse(txt_InsuranceTypeId.SelectedValue),
                    VendorId = int.Parse(ddl_Vendor.SelectedValue),
                    Insurer = txt_Insurer.Text,
                    PolicyNumber = txt_PolicyNumber.Text,
                    InsuranceCompany = txt_InsuranceCompany.Text,
                    InsuranceStartDate = Convert.ToDateTime(txt_InsuranceStartDate.Text),
                    InsuranceExpiryDate = Convert.ToDateTime(txt_InsuranceExpiryDate.Text),
                    DateOfPurchase = Convert.ToDateTime(txt_DateOfPurchase.Text),
                    InvoiceNumber = txt_InvoiceNumber.Text,
                    Vat = txt_Vat.Text,
                    Amount = txt_Amount.Text,
                    Gross = txt_Gross.Text,
                    CreditStatusId = int.Parse(ddl_CreditStaus.SelectedValue),

                    DomainCompanyId = companyid,
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                result = DAL.Operations.OpPurchase_Insurance.InsertRecord(obj);
            }
            catch (Exception ex)
            {
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
                            AttachmentCategoryId = 5,
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
    }
}