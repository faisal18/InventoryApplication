using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class PurchaseInsuranceAdd : System.Web.UI.Page
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
                            LoadData();
                            //LoadAttachment("PH_View");
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
        private void LoadData()
        {
            try
            {
                txt_MOPId.DataSource = DAL.Operations.OpModeofPayment.GetAll();
                txt_MOPId.DataTextField = "ModeOfPayment";
                txt_MOPId.DataValueField = "Id";
                txt_MOPId.DataBind();

                txt_InsuranceTypeId.DataSource = DAL.Operations.OpInsuranceType.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                txt_InsuranceTypeId.DataTextField = "InsuranceType";
                txt_InsuranceTypeId.DataValueField = "Id";
                txt_InsuranceTypeId.DataBind();

                ddl_domainCompany.DataSource = DAL.Operations.OpDomainCompany.GetAll().Where(x => x.Id == DomainCompanyId).ToList();
                ddl_domainCompany.DataTextField = "CompanyName";
                ddl_domainCompany.DataValueField = "Id";
                ddl_domainCompany.DataBind();

                ddl_CreditStaus.DataSource = DAL.Operations.OpCreditStatus.GetAll();
                ddl_CreditStaus.DataTextField = "Description";
                ddl_CreditStaus.DataValueField = "Id";
                ddl_CreditStaus.DataBind();


                ddl_Vendor.DataSource = DAL.Operations.OpVendor.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                ddl_Vendor.DataTextField = "Name";
                ddl_Vendor.DataValueField = "Id";
                ddl_Vendor.DataBind();
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
                if (file_Attachment.HasFiles)
                {
                    int recordID = CreatePurchaseInsurance();
                    if (recordID > 0)
                    {
                        CreateAttachment(recordID);
                        lbl_message.Text = "Record added successfully";
                        DAL.Operations.OpLogger.Info("PurchaseInsurance record added by " + Session["Name"].ToString());

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


        private int CreatePurchaseInsurance()
        {
            int result = -1;
            try
            {
                int companyid = int.Parse(ddl_domainCompany.SelectedValue);

                DAL.Tbl_Purchase_Insurance obj = new DAL.Tbl_Purchase_Insurance
                {

                    MOPId = int.Parse(txt_MOPId.SelectedValue),
                    VendorId = int.Parse(ddl_Vendor.SelectedValue),
                    InsuranceTypeId = int.Parse(txt_InsuranceTypeId.SelectedValue),
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
                    DomainCompanyId = companyid,
                    CreditStatusId = int.Parse(ddl_CreditStaus.SelectedValue),
                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreationDate = DateTime.Now
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