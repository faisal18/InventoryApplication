using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpCarType
    {
        public static Tbl_CarType GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_CarType users = DBContext.Tbl_CarType.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_CarType> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_CarType> users = DBContext.Tbl_CarType.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_CarType _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_CarType.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_CarType _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_CarType users = GetRecordbyId(_RecordId);

                    users.CarType = _user.CarType;
                    users.Seats = _user.Seats;
                    users.DomainCompanyId = _user.DomainCompanyId;


                    users.UpdatedBy = _user.UpdatedBy;
                    users.UpdateDate = _user.UpdateDate;

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
                    Tbl_CarType user = DBContext.Tbl_CarType.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_CarType.Remove(user);
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
