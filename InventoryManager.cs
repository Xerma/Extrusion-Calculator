using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public class InventoryManager
    {
        private static readonly string _appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string _appFolder = Path.Combine(_appDataRoot, "Extrusion Calculator");
        private static readonly string _appInventory = Path.Combine(_appFolder, "Inventory.json");

        public static void SetupInventory()
        {
            if (!DoesAppFolderExist())
            {
                Directory.CreateDirectory(_appFolder);
            }

            if (!DoesAppInventoryFileExist())
            {
                File.WriteAllText(_appInventory, "[]");
            }
        }

        private static bool DoesAppFolderExist()
        {
            return Directory.Exists(_appFolder);
        }

        private static bool DoesAppInventoryFileExist()
        {
            return File.Exists(_appInventory);
        }
    }
}
