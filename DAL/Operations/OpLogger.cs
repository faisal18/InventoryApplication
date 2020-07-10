using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpLogger
    {

        //public static async Task<int> CreateLogAsyncOp(Tbl_Logger _LogID)
        //{
        //    try
        //    {
        //        using (var MemberIDContext = new InventoryDBEntities())
        //        {
        //            InventoryDBEntities checkerRepository = new InventoryDBEntities.Tbl_LoggerRepository(MemberIDContext);

        //            checkerRepository.Add(_LogID);
        //            await checkerRepository.SaveAsync();
        //            int LastInsertedID = _LogID.MLogID;

        //            checkerRepository.Dispose();
        //            MemberIDContext.Dispose();
        //            return LastInsertedID;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //Helper.Log4Net.Error(ex.ToString());
        //        return -1;
        //    }
        //}
        public static int InsertLogAsync(Tbl_Logger _Tbl)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    DBContext.Tbl_Logger.Add(_Tbl);
                    DBContext.SaveChanges();

                    int LastInsertedID = _Tbl.Id;
                    return LastInsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
                return -1;
            }
        }

        public static void Log(string ApplicationName, string apppath, string ErrorLevel, string MaintenanceDetails, string status, string ErrorDetails)
        {

            Tbl_Logger mLog = new Tbl_Logger();
            mLog.ApplicationName = ApplicationName;
            mLog.AppPath = apppath;
            mLog.Details = MaintenanceDetails;
            mLog.Status = status;
            mLog.ErrorDetails = ErrorDetails;
            mLog.ErrorLevel = ErrorLevel;

            InsertLogAsync(mLog);

        }
        public static void LogError(string ErrorLevel, string ErrorDetails, string methodname = "Calling Method")
        {

            Tbl_Logger mLog = new Tbl_Logger();
            mLog.ApplicationName = "MemberRegister_DAL";
            mLog.ErrorDetails = ErrorDetails;
            mLog.ErrorLevel = ErrorLevel;
            mLog.Details = methodname;

            InsertLogAsync(mLog);
        }
        public static InsertResponse LogError(Exception exc)
        {
            Tbl_Logger mLog = new Tbl_Logger();

            InsertResponse responseMessage = new InsertResponse();
            responseMessage.responseCode = -4;
            if (exc.InnerException != null)
            {
                responseMessage.ErrorMessage = exc.InnerException.ToString();
                mLog.ErrorDetails = exc.InnerException.ToString();

            }
            else
            {
                responseMessage.ErrorMessage = exc.Message;
                mLog.ErrorDetails = " INNER EXCEPTION IS NULL ";
            }
            try
            {
                mLog.ApplicationName = exc.Source;
                mLog.ErrorLevel = "CRITICAL";
                mLog.Details = exc.StackTrace;
                mLog.AppPath = exc.Message;

                InsertLogAsync(mLog);
                return responseMessage;
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
                return responseMessage;
            }
        }
        public static void Info(string MaintenanceDetails)
        {

            Tbl_Logger mLog = new Tbl_Logger();
            mLog.ApplicationName = "MemberRegister_DAL_";
            mLog.Details = MaintenanceDetails;
            mLog.ErrorLevel = "Info";

            InsertLogAsync(mLog);
        }


    }
}
