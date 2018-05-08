namespace FitnesClubCL.CF_EMDB
{
    using System.Data.Entity;
    public class DataBaseFcContext : DbContext
    {
        #region Конструкторы

        /// <summary>
        /// статический конструктор. Всегда инициализируется первым
        /// </summary>
        static DataBaseFcContext()
        {
            // Для справки: существует 3 варианта создания БД
            //   Database.SetInitializer(new DropCreateDatabaseAlways<DataBaseFcContext>()); //создавать БД всегда
            // Database.SetInitializer(new CreateDatabaseIfNotExists<DataBaseFCContext>()); //создавить БД если её не существует
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataBaseFcContext>());
            //    Database.SetInitializer(new InitializationModel()); //создавать БД если модель изменена
            Database.SetInitializer(new InitializationModel()); //создавать БД если модель изменена
        }

        public DataBaseFcContext()
        {

        }

        public DataBaseFcContext(string connString)
            : base(connString)
        {
          //  Database.SetInitializer(new InitializationModel()); //создавать БД если модель изменена
        }
        #endregion

        #region Свойства доступа к полям БД
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountStatus> AccountStatuses { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public DbSet<Gym> Gyms { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingList> TrainingLists { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AccountConfig());
            modelBuilder.Configurations.Add(new AccountStatusConfig());
            modelBuilder.Configurations.Add(new EmployeeConfig());
            modelBuilder.Configurations.Add(new EmployeeRoleConfig());
            modelBuilder.Configurations.Add(new GymConfig());
            modelBuilder.Configurations.Add(new SalaryConfig());
            modelBuilder.Configurations.Add(new TrainingConfig());
            modelBuilder.Configurations.Add(new TrainingListConfig());
        }
    }
}
