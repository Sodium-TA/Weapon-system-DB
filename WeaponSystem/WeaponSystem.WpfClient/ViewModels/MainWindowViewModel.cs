namespace WeaponSystem.WpfClient.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using WeaponSystem.WpfClient.Commands;

    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Windows.Forms;
    using WeaponSystem.MongoDb.Data;
    using WeaponSystem.MsSql.Data;
    using WeaponSystem.MsSql.Data.Migrations;

    public class MainWindowViewModel : BaseViewModel
    {
        private bool isGetMOngoDataActive = false;

        private ICommand getMongoDbDataCommand;

        private ICommand createSqlDbCommand;

        private ICommand quitApplicationCommand;

        private ICommand getZipDataCommand;

        public bool IsGetMOngoDataActive
        {
            get
            {
                return this.isGetMOngoDataActive;
            }
            set
            {
                this.isGetMOngoDataActive = value;
                this.OnPropertyChanged("IsGetMOngoDataActive");
            }
        }

        public ICommand GetMongoDbData
        {
            get
            {
                if (this.getMongoDbDataCommand == null)
                {
                    this.getMongoDbDataCommand = new RelayCommand(this.HandleGetMongoDbDataCommand);
                }

                return this.getMongoDbDataCommand;
            }
        }

        public ICommand CreateSqlDb
        {
            get
            {
                if (this.createSqlDbCommand == null)
                {
                    this.createSqlDbCommand = new RelayCommand(this.HandleCreateSqlDCommand);
                }

                return this.createSqlDbCommand;
            }
        }

        public ICommand GtZipData
        {
            get
            {
                if (this.getZipDataCommand == null)
                {
                    this.getZipDataCommand = new RelayCommand(this.GtZipDataCommand);
                }

                return this.getZipDataCommand;
            }
        }
        private void GtZipDataCommand(object parameter)
        {
            MessageBox.Show("ZIpdata loaded");
        }

        private async void HandleCreateSqlDCommand(object parameter)
        {
            this.IsGetMOngoDataActive = false;

            var repo = new MsSqlRepo();

            try
            {
                await repo.CreteDb();

                this.IsGetMOngoDataActive = true;
                MessageBox.Show("SQL DB created",
                    "SQL DB creation");

            }
            catch (Exception ex)
            {
                var exmes = ex.Message;
                MessageBox.Show(exmes,
                    "SQL DB creation");
            }
        }

        private async void HandleGetMongoDbDataCommand(object parameter)
        {
          // Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());
          this.IsGetMOngoDataActive = false;

            try
            {
                var repo = new MongoDbRepository();

                var weaponCategories = (await repo.GetWeaponCategories()).ToList();
                using (WeaponSystemContext ctx = new WeaponSystemContext())
                {
                    foreach (var cat in weaponCategories)
                    {
                        if (!ctx.WeaponCategoies.Any(c => c.Name == cat.Name))
                        {
                            ctx.WeaponCategoies.Add(cat);
                        }
                    }

                    ctx.SaveChanges();

                    MessageBox.Show("Weapon Categories from mongoDB taken!");
                }

                var targetCategories = (await repo.GetTargetCategories()).ToList();
                using (WeaponSystemContext ctx = new WeaponSystemContext())
                {
                    foreach (var cat in targetCategories)
                    {
                        if (!ctx.TargetCategories.Any(c => c.Name == cat.Name))
                        {
                            ctx.TargetCategories.Add(cat);
                        }
                    }

                    ctx.SaveChanges();

                    MessageBox.Show("Target Categories from mongoDB taken!");
                }

                var countries = (await repo.GetCountries()).ToList();
                using (WeaponSystemContext ctx = new WeaponSystemContext())
                {
                    foreach (var cou in countries)
                    {
                        if (!ctx.Countries.Any(c => c.Name == cou.Name))
                        {
                            ctx.Countries.Add(cou);
                        }
                    }

                    ctx.SaveChanges();

                    MessageBox.Show("Countries from mongoDB taken!");
                }


                var manufacturers = (await repo.GetManufacturers()).ToList();
                using (WeaponSystemContext ctx = new WeaponSystemContext())
                {
                    foreach (var man in manufacturers)
                    {
                        if (!ctx.Manufacturers.Any(c => c.Name == man.Name))
                        {
                            ctx.Manufacturers.Add(man);
                        }
                    }

                    ctx.SaveChanges();

                    MessageBox.Show("Manufacturers from mongoDB taken!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "MongoDb");
            }
        }
    }
}

