using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpMaintenance
    {
        public static Tbl_Maintenance GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Maintenance users = DBContext.Tbl_Maintenance.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Maintenance> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Maintenance> users = DBContext.Tbl_Maintenance.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Maintenance _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Maintenance.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_Maintenance _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Maintenance users = GetRecordbyId(_RecordId);

                    users.CarId = _user.CarId;
                    users.Cost = _user.Cost;
                    users.Description = _user.Description;
                    users.DriverId = _user.DriverId;
                    users.DueDate = _user.DueDate;
                    users.NumberofHours = _user.NumberofHours;
                    users.MaintenanceCategoryId = _user.MaintenanceCategoryId;
                    users.TotalTyreId = _user.TotalTyreId;
                    users.NumberofTyres = _user.NumberofTyres;
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
                    Tbl_Maintenance user = DBContext.Tbl_Maintenance.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Maintenance.Remove(user);
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
