﻿using WeaponSystem.ExcelData;
using WeaponSystem.MySql.OpenAccess;

namespace WeaponSystem.WpfClient.ViewModels
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using WeaponSystem.MsSql.Data;
    using WeaponSystem.Transfers;
    using WeaponSystem.WpfClient.Commands;
    using Reports;

    public class ReportsViewModel : BaseViewModel
    {
        private ICommand generatePdfReportCommand;
        private ICommand generateJsonReportCommand;
        private ICommand generateXmlReportCommand;
        private ICommand generateReportsToMySqlCommand;
        private ICommand generateReportsOMRMySql;

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

        

                public ICommand GenerateReportsOMRMySql
        {
            get
            {
                if (this.generateReportsOMRMySql == null)
                {
                    this.generateReportsOMRMySql = new RelayCommand(this.HandleGenerateReportsOMRMySqllCommand);
                }

                return this.generateReportsOMRMySql;
            }
        }

        private void HandleGenerateReportsOMRMySqllCommand(object parameter)
        {
           

            try
            {
                var repo = new MySqlRepo();
                repo.UpdateDatabase();

                repo.ImportDbDataFromJson("../../../../Generated Reports/JSON/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MySql report");
            }
        }

        public ICommand GenerateReportsToMySql
        {
            get
            {
                if (this.generateReportsToMySqlCommand == null)
                {
                    this.generateReportsToMySqlCommand = new RelayCommand(this.HandleGenerateReportsToMySqlCommand);
                }

                return this.generateReportsToMySqlCommand;
            }
        }

        private async void HandleGenerateReportsToMySqlCommand(object parameter)
        {
            // PUT REPORTS TO MYSQL METHODS HERE!!!!
            try
            {
                var reporter = new MagicMessMaker();
                var msg = await reporter.MessWithFinessSqlLiteToExcelHArd();

                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SqlLite report");
            }
        }

        public ICommand GeneratePdfReport
        {
            get
            {
                if (this.generatePdfReportCommand == null)
                {
                    this.generatePdfReportCommand = new RelayCommand(this.HandleGeneratePdfReportCommand);
                }

                return this.generatePdfReportCommand;
            }
        }

        public ICommand GenerateJsonReport
        {
            get
            {
                if (this.generateJsonReportCommand == null)
                {
                    this.generateJsonReportCommand = new RelayCommand(this.HandleGenerateJsonReportCommand);
                }

                return this.generateJsonReportCommand;
            }
        }

        public ICommand GenerateXmlReport
        {
            get
            {
                if (this.generateXmlReportCommand == null)
                {
                    this.generateXmlReportCommand = new RelayCommand(this.HandleGenerateXmlReportCommand);
                }

                return this.generateXmlReportCommand;
            }
        }

        private async void HandleGeneratePdfReportCommand(object parameter)
        {
            try
            {
                var reporter = new ReportPdf();
                var msg = await reporter.GeneratePdfReport();

                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Json report");
            }

        }

        private async void HandleGenerateJsonReportCommand(object parameter)
        {
            try
            {
                var reporter = new ReportJson();
                var msg = await reporter.GenerateJsonReport();

                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Json report");
            }
        }

        private async void HandleGenerateXmlReportCommand(object parameter)
        {
            try
            {
                var reporter = new ReportXml();
                var msg = reporter.GenerateXmlReport();

                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                var exmes = ex.Message;
                MessageBox.Show(exmes, "Xml report");
            }
        }
    }
}