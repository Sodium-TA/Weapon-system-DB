namespace WeaponSystem.WpfClient
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    
    using WeaponSystem.WpfClient.Contracts;
    using WeaponSystem.WpfClient.Helpers;
    /// <summary>
    /// Interaction logic for MainWindow.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.PageSwitcher = ViewSwitcher.Instance;
            this.PageSwitcher.PageSwitcher = this;
            this.PageSwitcher.Switch(ViewSelector.Main);
        }

        /// <summary>
        /// Gets or sets main window's page switcher.
        /// </summary>
        /// <value>PageSwitcher as ViewSwitcher.</value>
        private ViewSwitcher PageSwitcher { get; set; }

        /// <summary>
        /// The method changes the content of the window by given UserControl.
        /// </summary>
        /// <param name="nextPage">The view as UserControl.</param>
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        /// <summary>
        /// The method changes the content of the window by given UserControl and object state.
        /// </summary>
        /// <param name="nextPage">The view as UserControl.</param>
        /// <param name="state">The object's state.</param>
        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable stateStatus = nextPage as ISwitchable;

            if (stateStatus != null)
            {
                stateStatus.UtilizeState(state);
            }
            else
            {
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
            }
        }
    }
}
