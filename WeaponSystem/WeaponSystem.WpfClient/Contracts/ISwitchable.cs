namespace WeaponSystem.WpfClient.Contracts
{
    /// <summary>
    /// Interface for ISwitchable.
    /// </summary>
    public interface ISwitchable
    {
        /// <summary>
        /// The method should utilize the state of the object to bi switched.
        /// </summary>
        /// <param name="state">Object's state.</param>
        void UtilizeState(object state);
    }
}
