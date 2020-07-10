using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpCar
    {
        public static Tbl_Car GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Car users = DBContext.Tbl_Car.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Car> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Car> users = DBContext.Tbl_Car.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Car _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Car.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_Car _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Car users = GetRecordbyId(_RecordId);

                    users.Active = _user.Active;
                    users.CarTypeId = _user.CarTypeId;
                    users.ChasisNumber = _user.ChasisNumber;
                    users.Description = _user.Description;
                    users.LoadingPermission = _user.LoadingPermission;
                    users.Model = _user.Model;
                    users.PlateNumber = _user.PlateNumber;
                    users.PortTags = _user.PortTags;
                    users.RegistrationNumber = _user.RegistrationNumber;
                    users.RoadPermission = _user.RoadPermission;
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
                    Tbl_Car user = DBContext.Tbl_Car.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Car.Remove(user);
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
