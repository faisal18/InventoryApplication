using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpUserRoles
    {


        public static Tbl_UserRoles GetRecordbyRank(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_UserRoles users = DBContext.Tbl_UserRoles.Where(x => x.Rank == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_UserRoles> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_UserRoles> users = DBContext.Tbl_UserRoles.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        //public static int InsertRecord(Tbl_User _Tbluser)
        //{
        //    try
        //    {
        //        using (var DBContext = new InventoryDBEntities())
        //        {
        //            DBContext.Tbl_User.Add(_Tbluser);
        //            DBContext.SaveChanges();

        //            int LastInsertedID = _Tbluser.Id;
        //            return LastInsertedID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        OpLogger.LogError(ex);
        //        return -1;
        //    }
        //}

        //public static int UpdateRecord(Tbl_User _user, int _RecordId)
        //{
        //    try
        //    {
        //        using (var DBContext = new InventoryDBEntities())
        //        {
        //            Tbl_User users = GetRecordbyId(_RecordId);

        //            users.LicenseExpiry = _user.LicenseExpiry;
        //            users.LicenseNumner = _user.LicenseNumner;
        //            users.MedicalExpiry = _user.MedicalExpiry;
        //            users.MedicalNumber = _user.MedicalNumber;
        //            users.Name = _user.Name;
        //            users.PassportExpiry = _user.PassportExpiry;
        //            users.PassportNumber = _user.PassportNumber;
        //            users.RoleId = _user.RoleId;
        //            users.Address = _user.Address;
        //            users.BasicSalary = _user.BasicSalary;
        //            users.Contact = _user.Contact;
        //            users.Email = _user.Email;
        //            users.EmiratesId = _user.EmiratesId;
        //            users.EmiratesIdExpiry = _user.EmiratesIdExpiry;

        //            users.CreatedBy = _user.CreatedBy;
        //            users.CreationDate = _user.CreationDate;
        //            users.UpdatedBy = _user.UpdatedBy;
        //            users.UpdatedDate = _user.UpdatedDate;
        //            DBContext.Entry(users).State = System.Data.Entity.EntityState.Modified;
        //            return DBContext.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        OpLogger.LogError(ex);
        //        return -1;
        //    }
        //}

        //public static bool DeleteById(int _RecordId)
        //{
        //    try
        //    {
        //        using (var DBContext = new InventoryDBEntities())
        //        {
        //            Tbl_User user = DBContext.Tbl_User.SingleOrDefault(x => x.Id == _RecordId);
        //            DBContext.Tbl_User.Remove(user);
        //            DBContext.SaveChanges();
        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        OpLogger.LogError(ex);
        //        return false;
        //    }
        //}
    }
}
