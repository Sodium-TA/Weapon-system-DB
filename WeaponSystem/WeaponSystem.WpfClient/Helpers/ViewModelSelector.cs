namespace WeaponSystem.WpfClient.Helpers
{
    using ViewModels;

    /// <summary>
    /// ViewModelsSelector class keeps only instances of each view model to ensure there only one instance of each.
    /// </summary>
    public static class ViewModelsSelector
    {
        /// <summary>
        /// Holds GameSettingsViewModel.
        /// </summary>
        public static readonly MainWindowViewModel MainWindowViewModel = new MainWindowViewModel();

        public static readonly ReportsViewModel ReportsViewModel = new ReportsViewModel();
    }
}
