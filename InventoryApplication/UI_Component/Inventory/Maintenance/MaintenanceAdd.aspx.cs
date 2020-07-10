using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Inventory.Maintenance
{
    public partial class MaintenanceAdd : System.Web.UI.Page
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
                ddl_CarRegistration.DataSource = DAL.Operations.OpCar.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                ddl_CarRegistration.DataTextField = "PlateNumber";
                ddl_CarRegistration.DataValueField = "Id";
                ddl_CarRegistration.DataBind();

                ddl_DriverName.DataSource = DAL.Operations.OpUser.GetAll().Where(x => x.RoleId == 5).Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
                ddl_DriverName.DataTextField = "Name";
                ddl_DriverName.DataValueField = "Id";
                ddl_DriverName.DataBind();


                ddl_MaintenanceCategoryDescription.DataSource = DAL.Operations.OpMaintenanceType.GetAll();
                ddl_MaintenanceCategoryDescription.DataTextField = "Description";
                ddl_MaintenanceCategoryDescription.DataValueField = "Id";
                ddl_MaintenanceCategoryDescription.DataBind();

            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
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


                    ddl_BrandName.DataSource = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
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

                ddl_SerialNumber.DataSource = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.DomainCompanyId == DomainCompanyId).ToList();
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
                lbl_QuantityAvailable.Text = DAL.Operations.OpTyreTotal.GetAll().Where(x => x.BrandName == ddl_BrandName.SelectedItem.Text && x.SerialNumber == ddl_SerialNumber.SelectedItem.Text).Where(x => x.DomainCompanyId == DomainCompanyId).Select(x => x.Quantity).First().ToString();

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
                            Response.Redirect("MaintenanceView.aspx?RecordId=" + result, false);

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
                            InsertTotalTyre();
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

        private int CreateMaintenance(int numberoftyres, int tyreid)
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
                        TotalTyreId = tyreid,
                        DomainCompanyId = DomainCompanyId ,

                        CreatedBy = int.Parse(Session["UserId"].ToString()),
                        CreatedDate = DateTime.Now

                    };

                    result = DAL.Operations.OpMaintenance.InsertRecord(obj);
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
        private int CreateMaintenance()
        {
            int result = -1;
            try
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
                    //NumberofTyres = int.Parse(txt_SelectedQuantity.Text),
                    DomainCompanyId = DomainCompanyId ,

                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreatedDate = DateTime.Now

                };
                result = DAL.Operations.OpMaintenance.InsertRecord(obj);



            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private int updateMaintenance(int MaintenanceId,int TyreId)
        {
            int result = -1;
            try
            {
                DAL.Tbl_Maintenance obj =  DAL.Operations.OpMaintenance.GetRecordbyId(MaintenanceId);
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
                            DomainCompanyId = DomainCompanyId,
                            AttachmentCategoryId = 3,
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