using WeaponSystem.WpfClient.Helpers;

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
        private ICommand quitApplicationCommand;
        private ICommand initializatorDirektorCommand;

        private bool isGetMOngoDataActive = false;
        private bool isCreateMsSqlActive = true;
        private bool isUniversalButtonActive = true;
        private string universalButttonText = "Create MsSQL DB";

        private int step = 1;

        public string UniversalButttonText
        {
            get
            {
                return this.universalButttonText;
            }

            set
            {
                this.universalButttonText = value;
                this.OnPropertyChanged("UniversalButttonText");
            }
        }

        public bool IsUniversalButtonActive
        {
            get
            {
                return this.isUniversalButtonActive;
            }

            set
            {
                this.isUniversalButtonActive = value;
                this.OnPropertyChanged("IsUniversalButtonActive");
            }
        }

        public ICommand IinitializatorDirector
        {
            get
            {
                if (this.initializatorDirektorCommand == null)
                {
                    this.initializatorDirektorCommand = new RelayCommand(this.HandleIinitializatorDirectorCommand);
                }

                return this.initializatorDirektorCommand;
            }
        }

        private void HandleIinitializatorDirectorCommand(object parameter)
        {
            switch (this.step)
            {
                case 1:
                    this.HandleCreateSqlDCommand(null);
                    break;
                case 2:
                    this.HandleGetMongoDbDataCommand(null);
                    break;
                case 3:
                    this.GetZipDataCommand(null);
                    break;
                case 4:
                    this.GetXmlAndFillSqlAndMongoDb(null);
                    break;
                case 5:
                    this.PageSwitcher.Switch(ViewSelector.Reports);
                    break;
                default:
                    break;
            }
        }

        private async void GetXmlAndFillSqlAndMongoDb(object parameter)
        {
            try
            {
                this.IsUniversalButtonActive = false;
                this.UniversalButttonText = "...getting data from XML and inserting to Sql and MongoDb..";

                var agent = new XmlToMsSqlAndMongoDbAgent();

                var msg = await agent.GetXmlData();

                this.UniversalButttonText = "Let's mess with some reports";

                this.IsUniversalButtonActive = true;
                this.step = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xml to MsSql and MongoDb");
            }
        }

        private async void GetZipDataCommand(object parameter)
        {
            try
            {
                this.UniversalButttonText = "...getting data from zipped .xls..";

                var agent = new ZippedXlsToMsSqlAgent();

                var msgCal = await agent.TransferCalibers();
                var msgWC = await agent.TransferWeapons();
                
                this.UniversalButttonText = "Get XML data and transfer to MsSql and MongoDb";

                this.IsUniversalButtonActive = true;
                this.step = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Zipped .xls transfer");
            }
        }

        private async void HandleCreateSqlDCommand(object parameter)
        {
            try
            {
                this.IsUniversalButtonActive = false;
                this.UniversalButttonText = "...Creating MS SQL DB...";

                var repo = new MsSqlRepo();

                await repo.CreateDb();

                this.UniversalButttonText = "Get data from MongoDB";
                this.IsUniversalButtonActive = true;
                this.step = 2;
            }
            catch (Exception ex)
            {
                var exmes = ex.Message;
                MessageBox.Show(exmes, "SQL DB creation");
            }
        }

        private async void HandleGetMongoDbDataCommand(object parameter)
        {
            this.IsUniversalButtonActive = false;
            this.UniversalButttonText = "...Start getting data from MongoDB...";

            try
            {
                var agent = new MongoDbToMsSqlAgent();

                var msgWC = await agent.TransferWeaponCategories();
                this.UniversalButttonText = msgWC;

                var msgTC = await agent.TransferTargetCategories();
                this.UniversalButttonText = msgTC;

                var msgC = await agent.TransferCountries();
                this.UniversalButttonText = msgC;

                var msgM = await agent.TransferManufacturers();
                this.UniversalButttonText = msgM;

                this.step = 3;
                this.IsUniversalButtonActive = true;
                this.UniversalButttonText = "Get data from zipped .xls";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MongoDb transfer");
            }
        }
    }
}