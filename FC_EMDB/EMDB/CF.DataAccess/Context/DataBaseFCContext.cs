using System.Data.Entity;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.DataAccess.Configuration;

namespace FC_EMDB.EMDB.CF.DataAccess.Context
{
    public class DataBaseFcContext : DbContext
    {
        #region Конструкторы

        /// <summary>
        /// статический конструктор. Всегда инициализируется первым
        /// </summary>
        static DataBaseFcContext()
        {
            // Для справки: существует 3 варианта создания БД
            // Database.SetInitializer(new DropCreateDatabaseAlways<DataBaseFcContext>()); //создавать БД всегда
            // Database.SetInitializer(new CreateDatabaseIfNotExists<DataBaseFCContext>()); //создавить БД если её не существует
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataBaseFcContext>());//создавать БД если модель изменена
            Database.SetInitializer(new InitializationModel()); //создавать БД если модель изменена
        }

        public DataBaseFcContext()
        {

        }

        public DataBaseFcContext(string connString)
            : base(connString)
        {
 
        }
        #endregion

        #region Свойства доступа к полям БД
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeWorkingStatus> EmployeeWorkingStatuses { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<StatusTraining> StatusTrainings { get; set; }
        public virtual DbSet<Tarif> Tarifs { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<TrainingList> TrainingLists { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AccountConfig());
            modelBuilder.Configurations.Add(new AccountStatusConfig());
            modelBuilder.Configurations.Add(new EmployeeConfig());
            modelBuilder.Configurations.Add(new EmployeeRoleConfig());
            modelBuilder.Configurations.Add(new EmployeeWorkingStatusConfig());
            modelBuilder.Configurations.Add(new GymConfig());
            modelBuilder.Configurations.Add(new ServiceConfig());
            modelBuilder.Configurations.Add(new StatusTrainingConfig());
            modelBuilder.Configurations.Add(new TarifConfig());
            modelBuilder.Configurations.Add(new TrainingConfig());
            modelBuilder.Configurations.Add(new TrainingListConfig());
        }
    }
}
