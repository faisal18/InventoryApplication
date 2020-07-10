using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpPurchase_Insurance
    {
        public static Tbl_Purchase_Insurance GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Purchase_Insurance users = DBContext.Tbl_Purchase_Insurance.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Purchase_Insurance> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Purchase_Insurance> users = DBContext.Tbl_Purchase_Insurance.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Purchase_Insurance _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Purchase_Insurance.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_Purchase_Insurance _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Purchase_Insurance users = GetRecordbyId(_RecordId);

                    users.Amount = _user.Amount;
                    users.DateOfPurchase = _user.DateOfPurchase;
                    users.Gross = _user.Gross;
                    users.InsuranceCompany = _user.InsuranceCompany;
                    users.InsuranceExpiryDate = _user.InsuranceExpiryDate;
                    users.InsuranceStartDate = _user.InsuranceStartDate;
                    users.InsuranceTypeId = _user.InsuranceTypeId;
                    users.Insurer = _user.Insurer;
                    users.InvoiceNumber = _user.InvoiceNumber;
                    users.MOPId = _user.MOPId;
                    users.PolicyNumber = _user.PolicyNumber;
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
                    Tbl_Purchase_Insurance user = DBContext.Tbl_Purchase_Insurance.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Purchase_Insurance.Remove(user);
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
