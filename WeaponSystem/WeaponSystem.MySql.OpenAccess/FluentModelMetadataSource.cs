using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata.Fluent;

namespace WeaponSystem.MySql.OpenAccess
{
    public partial class FluentModelMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var weaponMapping = new MappingConfiguration<WeaponReport>();
            weaponMapping.MapType(wr => new
            {
                Id = wr.weaponId,
                Name = wr.weaponName,
                EmailAddress = wr.manufacturer,
            }).ToTable("Weapon Reports");
            weaponMapping.HasProperty(c => c.weaponId).IsIdentity();

            configurations.Add(weaponMapping);

            return configurations;
        }
    }
}
