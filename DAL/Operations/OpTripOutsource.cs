using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpTripOutsource
    {

        public static Tbl_TripOutsource GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_TripOutsource users = DBContext.Tbl_TripOutsource.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_TripOutsource> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_TripOutsource> users = DBContext.Tbl_TripOutsource.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_TripOutsource _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_TripOutsource.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_TripOutsource _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_TripOutsource users = GetRecordbyId(_RecordId);

                    users.CarRegistration = _user.CarRegistration;
                    users.ClientName = _user.ClientName;

                    users.DateofEntry = _user.DateofEntry;
                    users.Description = _user.Description;
                    users.DriverCommission = _user.DriverCommission;
                    users.DriverName = _user.DriverName;
                    users.FromDestination = _user.FromDestination;
                    users.ToDestination = _user.ToDestination;
                    users.TollCharges = _user.TollCharges;
                    users.TotalCharges = _user.TotalCharges;
                    users.TotalDistance = _user.TotalDistance;
                    users.UpdatedBy = users.UpdatedBy;
                    users.UpdateDate = users.UpdateDate;
                    users.WaitingCharges = users.WaitingCharges;

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
                    
                    Tbl_TripOutsource user = DBContext.Tbl_TripOutsource.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_TripOutsource.Remove(user);
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
