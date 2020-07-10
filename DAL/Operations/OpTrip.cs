using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpTrip
    {
        public static Tbl_Trip GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Trip users = DBContext.Tbl_Trip.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_Trip> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_Trip> users = DBContext.Tbl_Trip.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_Trip _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Trip.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_Trip _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_Trip users = GetRecordbyId(_RecordId);

                    users.CarId = _user.CarId;
                    users.ClientId = _user.ClientId;
                    
                    users.DateofEntry = _user.DateofEntry;
                    users.InvoiceNumber = _user.InvoiceNumber;
                    users.Description = _user.Description;
                    users.DriverCommission = _user.DriverCommission;
                    users.DriverId = _user.DriverId;
                    users.FromDestination = _user.FromDestination;
                    users.ToDestination = _user.ToDestination;
                    users.TollCharges = _user.TollCharges;
                    users.TotalCharges = _user.TotalCharges;
                    users.TotalDistance = _user.TotalDistance;
                    //users.TripRate = _user.TripRate;
                    users.CreditStatusId = _user.CreditStatusId;
                    users.UpdatedBy = _user.UpdatedBy;
                    users.UpdatedDate = _user.UpdatedDate;
                    users.WaitingCharges = _user.WaitingCharges;
                    users.DomainCompanyId = _user.DomainCompanyId;


                    //users.Ports = _user.Ports;
                    //users.RoadPermission = _user.RoadPermission;
                    //users.LoadingPermission = _user.LoadingPermission;
                    
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
                    Tbl_Trip user = DBContext.Tbl_Trip.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_Trip.Remove(user);
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
