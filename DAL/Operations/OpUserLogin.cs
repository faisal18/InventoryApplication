using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpUserLogin
    {
        public static Tbl_UserLogin GetRecordById(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_UserLogin users = DBContext.Tbl_UserLogin.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static Tbl_UserLogin GetRecordByCreds(string username,string password)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_UserLogin users = DBContext.Tbl_UserLogin.Where(x => x.Username == username && x.Password == password).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_UserLogin> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_UserLogin> users = DBContext.Tbl_UserLogin.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_UserLogin _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_UserLogin.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_UserLogin _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_UserLogin users = GetRecordById(_RecordId);

                    users.Password = _user.Password;
                    users.Username = _user.Username;

                   
                    users.UpdatedBy = _user.UpdatedBy;
                    users.UpdatedDate = _user.UpdatedDate;
                    //users.DomainCompanyId = _user.DomainCompanyId;


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
                    Tbl_UserLogin user = DBContext.Tbl_UserLogin.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_UserLogin.Remove(user);
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
