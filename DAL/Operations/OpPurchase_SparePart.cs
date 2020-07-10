using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpPurchase_SparePart
    {
        public static Tbl_Purchase_SparePart GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Purchase_SparePart users = DBContext.Tbl_Purchase_SparePart.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Purchase_SparePart> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Purchase_SparePart> users = DBContext.Tbl_Purchase_SparePart.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Purchase_SparePart _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Purchase_SparePart.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_Purchase_SparePart _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Purchase_SparePart users = GetRecordbyId(_RecordId);

                    users.Amount = _user.Amount;
                    users.CompanyName = _user.CompanyName;
                    users.DateofPurchase = _user.DateofPurchase;
                    users.Description = _user.Description;
                    users.Gross = _user.Gross;
                    users.InvoiceNumber = _user.InvoiceNumber;
                    users.MOPId = _user.MOPId;
                    users.Vat = _user.Vat;
                    users.DomainCompanyId = _user.DomainCompanyId;
                    users.VendorId = _user.VendorId;
                    users.CreditStatusId = _user.CreditStatusId;




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
                    Tbl_Purchase_SparePart user = DBContext.Tbl_Purchase_SparePart.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Purchase_SparePart.Remove(user);
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
