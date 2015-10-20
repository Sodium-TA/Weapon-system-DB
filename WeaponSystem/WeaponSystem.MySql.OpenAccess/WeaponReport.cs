using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace WeaponSystem.MySql.OpenAccess
{
    public class WeaponReport
    {
        public string weaponId { get; set; }
        public string weaponName { get; set; }
        public string manufacturer { get; set; }
    }
}
