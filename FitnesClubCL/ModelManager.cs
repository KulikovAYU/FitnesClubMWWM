using System.Data.Entity;
using System.Linq;
using FitnesClubCL.CF_EMDB;

namespace FitnesClubCL
{
   public static class ModelManager
    {
        public static void CreateDb()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DataBaseFCContext>());
            using (var db = new DataBaseFCContext("dbContext1"))
            {
             var x = db.Accounts.ToList();
 
            }
        }
    }
}
