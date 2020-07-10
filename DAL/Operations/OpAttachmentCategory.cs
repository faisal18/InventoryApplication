using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class OpAttachmentCategory
    {
        public static Tbl_AttachmentCategory GetRecordbyCategory(string category)
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    Tbl_AttachmentCategory users = DBContext.Tbl_AttachmentCategory.Where(x => x.AttachmentCategory == category).First();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

        public static List<Tbl_AttachmentCategory> GetAll()
        {
            try
            {
                using (var DBContext = new InventoryEntities())
                {
                    List<Tbl_AttachmentCategory> users = DBContext.Tbl_AttachmentCategory.ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                OpLogger.LogError(ex);
                return null;
            }
        }

    }
}
