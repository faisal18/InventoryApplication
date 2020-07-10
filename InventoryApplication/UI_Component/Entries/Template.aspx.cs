using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryApplication.UI_Component.Entries
{
    public partial class Template : System.Web.UI.Page
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
                            LoadDropdown(10);
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
            }
        }

        private void LoadDropdown(int TemplateCategoryID)
        {
            try
            {
                DataTable dt = DAL.Helper.ListToDatatable.ToDataTable(DAL.Operations.OpAttachment.GetAttachmentByCategoryId(TemplateCategoryID).Where(x => x.DomainCompanyId == DomainCompanyId).ToList());
                ddl_templateFile.Controls.Clear();
                ddl_templateFile.DataSource = dt;
                ddl_templateFile.DataTextField = "filename";
                ddl_templateFile.DataValueField = "itemid";
                ddl_templateFile.DataBind();

            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                throw;
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (file_upload.HasFile || file_upload.HasFiles)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        string filename = file.FileName;
                        int result = CreateTemplate(filename);
                        if (result > 0)
                        {
                            CreateAttachment(result, file);
                        }
                    }

                    LoadDropdown(int.Parse(ddl_templatecategory.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
        private int CreateTemplate(string filename)
        {
            int result = -1;
            try
            {
                DAL.Tbl_Template obj = new DAL.Tbl_Template
                {
                    DomainCompanyId = DomainCompanyId,
                    TemplateCategory = filename,
                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreatedDate = DateTime.Now,
                };

                result = DAL.Operations.OpTemplate.InsertRecord(obj);
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
            return result;
        }
        private void CreateAttachment(int RecordID, HttpPostedFile file)
        {
            try
            {

                DAL.Tbl_Attachment ticketAttachment = new DAL.Tbl_Attachment
                {
                    filename = file.FileName,
                    ItemId = RecordID,
                    DomainCompanyId = DomainCompanyId,
                    AttachmentCategoryId = int.Parse(ddl_templatecategory.SelectedValue),

                    fileinByte = ConvetStreamToByte(file.InputStream),
                    CreatedBy = int.Parse(Session["UserId"].ToString()),
                    CreationDate = DateTime.Now
                };
                int result = DAL.Operations.OpAttachment.InsertRecord(ticketAttachment);


            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void btn_download_Click(object sender, EventArgs e)
        {
            try
            {

                int itemId = int.Parse(ddl_templateFile.SelectedValue);
                int CategoryId = int.Parse(ddl_templatecategory.SelectedValue);
                var Attachment = DAL.Operations.OpAttachment.GetAttachmentByMany(itemId, CategoryId, DomainCompanyId).First();

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
                    //Response.End();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }

        protected void ddl_templatecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                LoadDropdown(int.Parse(ddl_templatecategory.SelectedValue));
            }
            catch (Exception ex)
            {
                DAL.Operations.OpLogger.LogError(ex);
                lbl_message.Text = ex.Message;
            }
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int templatecategory = int.Parse(ddl_templatecategory.SelectedValue);
                int templatefileId = int.Parse(ddl_templateFile.SelectedValue);
                int Domaincomp = DomainCompanyId;


                if (DAL.Operations.OpTemplate.DeleteById(templatefileId))
                {
                    if (DAL.Operations.OpAttachment.DeleteById(templatefileId, templatecategory, Domaincomp))
                    {
                        lbl_message.Text = "Attachment deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                DAL.Operations.OpLogger.LogError(ex);
            }
        }
    }
}