﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpPettyCash_WithDrawil
    {
        public static Tbl_PettyCash_Withdrawl GetRecordbyId(int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_PettyCash_Withdrawl users = DBContext.Tbl_PettyCash_Withdrawl.Where(x => x.Id == _RecordId).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_PettyCash_Withdrawl> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_PettyCash_Withdrawl> users = DBContext.Tbl_PettyCash_Withdrawl.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static int InsertRecord(Tbl_PettyCash_Withdrawl _Tbluser)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_PettyCash_Withdrawl.Add(_Tbluser);
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

        public static int UpdateRecord(Tbl_PettyCash_Withdrawl _user, int _RecordId)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_PettyCash_Withdrawl users = GetRecordbyId(_RecordId);

                    users.Amount = _user.Amount;
                    users.Description = _user.Description;

                    users.UpdatedBy = _user.UpdatedBy;
                    users.DomainCompanyId = _user.DomainCompanyId;

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
                    Tbl_PettyCash_Withdrawl user = DBContext.Tbl_PettyCash_Withdrawl.SingleOrDefault(x => x.Id == _RecordId);
                    DBContext.Tbl_PettyCash_Withdrawl.Remove(user);
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
