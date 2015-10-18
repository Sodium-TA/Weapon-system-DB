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
                //this.OnPropertyChanged("IsGetMOngoDataActive");
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
                //this.OnPropertyChanged("IsGetMOngoDataActive");
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
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());
          this.IsGetMOngoDataActive = false;

            try
            {
                var repo = new MongoDbRepository();

                var weaponCategories = (await repo.GetWeaponCategories()).ToList();

                var ctx = new WeaponSystemContext();
                using (ctx)
                {
                    foreach (var cat in weaponCategories)
                    {
                        if (!ctx.WeaponCategoies.Any(c => c.Name == cat.Name))
                        {
                            ctx.WeaponCategoies.Add(cat);
                            MessageBox.Show(cat.Name);
                        }
                    }

                    ctx.SaveChanges();
                }

                MessageBox.Show("Data from mongoDB taken!");
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

