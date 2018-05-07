using System;
using System.Data.Entity;

namespace FitnesClubCL.CF_EMDB
{
    public class DataBaseFCContext : DbContext
    {

        #region Конструктор
        public DataBaseFCContext()
        {

        }

        public DataBaseFCContext(string connString)
            : base(connString)
        {

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
