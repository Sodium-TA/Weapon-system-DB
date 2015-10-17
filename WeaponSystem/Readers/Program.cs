using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUnziper.UnzipFile("Weapons - Inventory.zip", "UnZippedFiles");
        }
    }
}
