using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpInsuranceType
    {
        public static Tbl_InsuranceType GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_InsuranceType users = DBContext.Tbl_InsuranceType.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_InsuranceType> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_InsuranceType> users = DBContext.Tbl_InsuranceType.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_InsuranceType _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_InsuranceType.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_InsuranceType _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_InsuranceType users = GetRecordbyId(_RecordId);

                    users.Description = _user.Description;
                    users.InsuranceType = _user.InsuranceType;
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
                    Tbl_InsuranceType user = DBContext.Tbl_InsuranceType.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_InsuranceType.Remove(user);
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
