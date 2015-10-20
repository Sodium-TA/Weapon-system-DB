namespace WeaponSystem.Transfers
{
    using System.Threading.Tasks;

    public class ZippedXlsToMsSqlAgent
    {
        private const string MessageEnd = " from zipped .xls transfered to MsSQL!";

        public async Task<string> TransferCalibers()
        {
            KPKNameClass1.AddCalibers();

            return "Calibers" + MessageEnd;
        }

        public async Task<string> TransferWeapons(string path = "put default path here")
        {
            KPKNameClass1.AddWeapons();

            return "Weapons" + MessageEnd;

        }

        public async Task<string> TransferWeaponTypes()
        {
            KPKNameClass1.AddWeaponTypes();

            return "Weapon types" + MessageEnd;
        }
    }
}
