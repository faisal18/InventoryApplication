using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace InventoryApplication.UI_Component.Inventory.Fuel
{
    public partial class FuelView : System.Web.UI.Page
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
                            //populate_grid();
                            //LoadAttachment("PH_View");
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
                Tbl_Purchase_Fuel Obj = DAL.Operations.OpPurchase_Fuel.GetRecordbyId(RecordId);

                txt_Amount.Text = Obj.Amount;
                txt_DateofPurchase.Text = Obj.DateofPurchase.ToString();
                txt_GallonsPurchased.Text = Obj.GallonsPurchased;
                txt_Gross.Text = Obj.Gross;
                txt_InvoiceNumber.Text = Obj.InvoiceNumber;
                txt_MOPId.Text = DAL.Operations.OpModeofPayment.GetRecordbyId(Obj.MOPId.Value).ModeOfPayment;
                txt_Vat.Text = Obj.Vat.ToString();
                txt_CreditStatus.Text = DAL.Operations.OpCreditStatus.GetRecordbyId(Obj.CreditStatusId.Value).Description;
                txt_DomainCompany.Text = DAL.Operations.OpDomainCompany.GetRecordbyId(Obj.DomainCompanyId.Value).CompanyName;
                txt_VendorName.Text = DAL.Operations.OpVendor.GetRecordbyId(Obj.VendorId.Value).Name;
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

                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(RecordId, 4));

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
                Response.Redirect("FuelEdit.aspx?RecordId=" + CheckQueryString(), false);
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
                string Gallons = DAL.Operations.OpFuelTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().TotalFuel.Value.ToString();
                if (DAL.Operations.OpPurchase_Fuel.DeleteById(CheckQueryString()))
                {
                    UpdateFuelTotal(Gallons);
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        private void UpdateFuelTotal(string Gallons)
        {
            try
            {

                decimal GallonsStored = DAL.Operations.OpFuelTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().TotalFuel.Value; ;
                decimal DGallons = GallonsStored - decimal.Parse(Gallons);

                DAL.Tbl_FuelTotal obj = new DAL.Tbl_FuelTotal
                {
                    TotalFuel = DGallons,
                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreatedDate = DateTime.Now
                };

                int result = DAL.Operations.OpFuelTotal.UpdateRecord(obj, 1);
                DAL.Operations.OpLogger.Info("FuelTotal has been updated");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
    }
}