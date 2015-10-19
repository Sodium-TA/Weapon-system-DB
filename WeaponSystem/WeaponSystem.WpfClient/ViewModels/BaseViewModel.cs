namespace WeaponSystem.WpfClient.ViewModels
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Commands;
    using Helpers;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private ICommand switchWiewCommand;

        private ICommand openExternalLinkCommand;
        
        private ICommand quitApplicationCommand;
        
        public BaseViewModel()
        {
            this.PageSwitcher = ViewSwitcher.Instance;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICommand SwitchView
        {
            get
            {
                if (this.switchWiewCommand == null)
                {
                    this.switchWiewCommand = new RelayCommand(this.HandleSwitchViewCommand);
                }

                return this.switchWiewCommand;
            }
        }
        
        public ICommand OpenExternalLinkCommand
        {
            get
            {
                if (this.openExternalLinkCommand == null)
                {
                    this.openExternalLinkCommand = new RelayCommand(this.HandleOpenExternalLinkCommand);
                }

                return this.openExternalLinkCommand;
            }
        }

       
        public ICommand QuitApplication
        {
            get
            {
                if (this.quitApplicationCommand == null)
                {
                    this.quitApplicationCommand = new RelayCommand(this.HandleQuitApplicationCommand);
                }

                return this.quitApplicationCommand;
            }
        }
     
        protected ViewSwitcher PageSwitcher { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     
        protected virtual void HandleSwitchViewCommand(object parameter)
        {
            var buttonClicked = parameter as Button;

            if (buttonClicked != null)
            {
                string changeToViewName = buttonClicked.Name.ToString();

                switch (changeToViewName)
                {
                    case "ButtonGoToMainMenu":
                        // TODO: Switch to MainMenuView when ready
                        this.PageSwitcher.Switch(ViewSelector.Main);
                        break;
                    case "ButtonGoToReports":
                        // TODO: Switch to MainMenuView when ready
                        this.PageSwitcher.Switch(ViewSelector.Main);
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void HandleQuitApplicationCommand(object parameter)
        {
            Application.Current.MainWindow.Close();
        }

        private void HandleOpenExternalLinkCommand(object parameter)
        {
            var buttonClicked = parameter as Button;
            var url = buttonClicked.Content.ToString();

            Process.Start(new ProcessStartInfo(url));
        }

     
    }
}
