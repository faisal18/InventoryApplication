using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Text;
using System.Data;
using System.IO;

namespace InventoryApplication.UI_Component.Inventory.Fuel
{
    public partial class FuelEdit : System.Web.UI.Page
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
                Tbl_Purchase_Fuel Obj = DAL.Operations.OpPurchase_Fuel.GetRecordbyId(RecordId);

                txt_Amount.Text = Obj.Amount;
                txt_DateofPurchase.Text = Obj.DateofPurchase.ToString();
                txt_GallonsPurchased.Text = Obj.GallonsPurchased;
                txt_Gross.Text = Obj.Gross;
                txt_InvoiceNumber.Text = Obj.InvoiceNumber;
                //txt_MOPId.Text = Obj.MOPId.ToString();
                txt_Vat.Text = Obj.Vat.ToString();

                LoadDropdown(Obj.MOPId.ToString());
                LoadAttachment();


            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
                
            }
        }
        private void LoadDropdown(string SelectedValue)
        {
            try
            {
                txt_MOPId.DataSource = DAL.Operations.OpModeofPayment.GetAll();
                txt_MOPId.DataTextField = "ModeOfPayment";
                txt_MOPId.DataValueField = "Id";
                txt_MOPId.DataBind();
                txt_MOPId.Items.FindByValue(SelectedValue).Selected = true;

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
                //ddl_Vendor.Items.FindByValue(Vendor).Selected = true;
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
                DataTable dt_Attachment = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByItemId(CheckQueryString(), 4));

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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            try
            {
                if (Place_Attachment.Controls.Count > 1)
                {
                    string prev = DAL.Operations.OpPurchase_Fuel.GetRecordbyId(CheckQueryString()).GallonsPurchased.ToString();
                    string now = txt_GallonsPurchased.Text;


                    int recordID = UpdateRecord();
                    if (recordID > 0)
                    {
                        UpdateAttachment(recordID);
                        lbl_message.Text = "Record added successfully";

                        if (int.Parse(now) > int.Parse(prev)) 
                        {
                            UpdateFuelTotal(now, true);
                        }
                        else if(int.Parse(now) < int.Parse(prev))
                        {
                            UpdateFuelTotal(now, false);
                        }

                        DAL.Operations.OpLogger.Info("Inventory Fuel record updated by " + Session["Name"].ToString());
                        Response.Redirect("FuelView.aspx?RecordId=" + recordID, false);
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
                lbl_message.Text = ex.Message;
            }
            
        }

        private int UpdateRecord()
        {
            int result = -1;
            try
            {
                int companyid = int.Parse(ddl_domainCompany.SelectedValue);
                DAL.Tbl_Purchase_Fuel obj = new DAL.Tbl_Purchase_Fuel
                {
                    MOPId = int.Parse(txt_MOPId.SelectedValue),
                    InvoiceNumber = txt_InvoiceNumber.Text,
                    Vat = txt_Vat.Text,
                    Amount = txt_Amount.Text,
                    Gross = txt_Gross.Text,
                    GallonsPurchased = txt_GallonsPurchased.Text,
                    DateofPurchase = Convert.ToDateTime(txt_DateofPurchase.Text),
                    DomainCompanyId = companyid,
                    VendorId = int.Parse(ddl_Vendor.SelectedValue),

                    CreditStatusId = int.Parse(ddl_CreditStaus.SelectedValue),
                    UpdatedBy = int.Parse(Session["UserId"].ToString()),
                    UpdatedDate = DateTime.Now
                };

                result = DAL.Operations.OpPurchase_Fuel.UpdateRecord(obj,CheckQueryString());

            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private void UpdateAttachment(int RecordID)
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
                            AttachmentCategoryId = 4,
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

        private void UpdateFuelTotal(string Gallons, bool toadd)
        {
            try
            {

                decimal GallonsStored = DAL.Operations.OpFuelTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).First().TotalFuel.Value;
                decimal DGallons = 0;
                //int DomainCompanyId = int.Parse(ddl_domainCompany.SelectedValue);

                if (toadd)
                {
                    DGallons = GallonsStored + decimal.Parse(Gallons);
                }
                else if(!toadd)
                {
                    DGallons = GallonsStored - decimal.Parse(Gallons);
                }

                if (DGallons != 0)
                {
                    DAL.Tbl_FuelTotal obj = new DAL.Tbl_FuelTotal
                    {
                        TotalFuel = DGallons,
                        DomainCompanyId = DomainCompanyId,
                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now
                    };
                    int result = DAL.Operations.OpFuelTotal.UpdateRecord(obj, 1);
                }

                
                DAL.Operations.OpLogger.Info("FuelTotal has been updated");

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
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