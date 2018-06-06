using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
   public class GymRepository : Repository<Gym>,IGymRepository
    {
        public GymRepository(DbContext context) : base(context)
        {
        }
        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;

        /// <summary>
        /// Метод создает или обновляет существующий зал
        /// </summary>
        /// <param name="data"></param>
        public void CreateOrUpdateRecord(Gym data)
        {
            DataBaseFcContext.Gyms.AddOrUpdate(data);
            DataBaseFcContext.SaveChanges();
        }
    }
}
