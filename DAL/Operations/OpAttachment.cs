using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpAttachment
    {

        public static List<Tbl_Attachment> GetAttachmentByCategoryId( int _CategoryId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Attachment> users = DBContext.Tbl_Attachment.Where(x => x.AttachmentCategoryId == _CategoryId).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Attachment> GetAttachmentByItemId(int _RecordId,int _CategoryId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Attachment> users = DBContext.Tbl_Attachment.Where(x => x.ItemId == _RecordId && x.AttachmentCategoryId == _CategoryId).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Attachment> GetAttachmentByMany(int _ItemId, int _CategoryId,int domainID)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Attachment> users = DBContext.Tbl_Attachment.Where(x => x.ItemId == _ItemId && x.AttachmentCategoryId == _CategoryId && x.DomainCompanyId == domainID).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static Tbl_Attachment GetRecordById(int _RecordId, int _CategoryId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Attachment users = DBContext.Tbl_Attachment.Where(x => x.Id == _RecordId && x.AttachmentCategoryId == _CategoryId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Attachment> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Attachment> users = DBContext.Tbl_Attachment.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Attachment _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Attachment.Add(_Tbluser);
                    DBContext.SaveChanges();

                    int LastInsertedID = _Tbluser.Id;
                    return LastInsertedID;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return -1;
            }
        }

        public static int UpdateRecord(Tbl_Attachment _user, int _RecordId,int _CategoryId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Attachment users = GetRecordById(_RecordId, _CategoryId);
                    
                    users.AttachmentCategoryId = _user.AttachmentCategoryId;
                    users.filename = _user.filename;
                    users.fileinByte = _user.fileinByte;
                    users.ItemId = _user.ItemId;
                    users.DomainCompanyId = _user.DomainCompanyId;

                    users.UpdatedBy = _user.UpdatedBy;
                    users.UpdatedDate = _user.UpdatedDate;

                    DBContext.Entry(users).State = System.Data.Entity.EntityState.Modified;
                    return DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return -1;
            }
        }

        public static bool DeleteById(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Attachment user = DBContext.Tbl_Attachment.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Attachment.Remove(user);
                    DBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteById(int itemid,int att_categoryid,int domainid)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Attachment user = DBContext.Tbl_Attachment.SingleOrDefault(x=>x.ItemId == itemid && x.AttachmentCategoryId == att_categoryid && x.DomainCompanyId == domainid);
                    DBContext.Tbl_Attachment.Remove(user);
                    DBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return false;
            }
        }
    }
}
