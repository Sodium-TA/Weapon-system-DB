namespace WeaponSystem.WpfClient.ViewModels
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using WeaponSystem.MsSql.Data;
    using WeaponSystem.Transfers;
    using WeaponSystem.WpfClient.Commands;

    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand createSqlDbCommand;
        private ICommand getMongoDbDataCommand;
        private ICommand getZipDataCommand;
        private ICommand quitApplicationCommand;

        private bool isGetMOngoDataActive = false;
        private bool isCreateMsSqlActive = true;
        private bool isGetZipDataActive = false;

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

        public bool IsGetZipDataActive
        {
            get
            {
                return this.isGetZipDataActive;
            }

            set
            {
                this.isGetZipDataActive = value;
                this.OnPropertyChanged("IsGetZipDataActive");
            }
        }

        public bool IsCreateMsSqlActive
        {
            get
            {
                return this.isCreateMsSqlActive;
            }

            set
            {
                this.isCreateMsSqlActive = value;
                this.OnPropertyChanged("IsCreateMsSqlActive");
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

        public ICommand GetZipData
        {
            get
            {
                if (this.getZipDataCommand == null)
                {
                    this.getZipDataCommand = new RelayCommand(this.GetZipDataCommand);
                }

                return this.getZipDataCommand;
            }
        }

        private async void GetZipDataCommand(object parameter)
        {
            try
            {
                var agent = new ZippedXlsToMsSqlAgent();

                var msgWC = await agent.TransferWeapons();
                MessageBox.Show(msgWC);

                this.IsGetZipDataActive = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message, "Zipped .xls transfer");
            }
        }

        private async void HandleCreateSqlDCommand(object parameter)
        {
            this.IsCreateMsSqlActive = false;

            try
            {
                var repo = new MsSqlRepo();

                await repo.CreteDb();

                this.IsGetMOngoDataActive = true;
                MessageBox.Show("SQL DB created", "SQL DB creation");
            }
            catch (Exception ex)
            {
                var exmes = ex.Message;
                MessageBox.Show(exmes, "SQL DB creation");
            }
        }

        private async void HandleGetMongoDbDataCommand(object parameter)
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());
            this.IsGetMOngoDataActive = false;

            try
            {
                var agent = new MongoDbToMsSqlAgent();

                var msgWC = await agent.TransferWeaponCategories();
                MessageBox.Show(msgWC);

                var msgTC = await agent.TransferTargetCategories();
                MessageBox.Show(msgTC);

                var msgC = await agent.TransferCountries();
                MessageBox.Show(msgC);

                var msgM = await agent.TransferManufacturers();
                MessageBox.Show(msgM);

                this.IsGetZipDataActive = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message, "MongoDb transfer");
            }
        }
    }
}