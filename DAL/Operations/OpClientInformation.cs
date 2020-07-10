using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpClientInformation
    {
        public static Tbl_ClientInformation GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_ClientInformation users = DBContext.Tbl_ClientInformation.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_ClientInformation> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_ClientInformation> users = DBContext.Tbl_ClientInformation.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_ClientInformation _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_ClientInformation.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_ClientInformation _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_ClientInformation users = GetRecordbyId(_RecordId);

                    users.Address = _user.Address;
                    users.CompanyName = _user.CompanyName;
                    users.ContactPerson = _user.ContactPerson;
                    users.Email = _user.Email;
                    users.Number = _user.Number;
                    users.UpdatedBy = _user.UpdatedBy;
                    users.UpdatedDate = _user.UpdatedDate;
                    users.DomainCompanyId = _user.DomainCompanyId;


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
                    Tbl_ClientInformation user = DBContext.Tbl_ClientInformation.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_ClientInformation.Remove(user);
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
