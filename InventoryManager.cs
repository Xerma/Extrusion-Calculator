using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public class InventoryManager
    {
        private static readonly string AppDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string AppFolder = Path.Combine(AppDataRoot, "Extrusion Calculator");
        private static readonly string AppInventory = Path.Combine(AppFolder, "Inventory.json");

        private static SortedSet<double> InventoryData;

        public static void SetupInventory()
        {
            if (!DoesAppFolderExist())
            {
                Directory.CreateDirectory(AppFolder);
            }

            if (!DoesAppInventoryFileExist())
            {
                File.WriteAllText(AppInventory, "[]");
            }
        }

        private static bool DoesAppFolderExist()
        {
            return Directory.Exists(AppFolder);
        }

        private static bool DoesAppInventoryFileExist()
        {
            return File.Exists(AppInventory);
        }

        public static SortedSet<double> LoadInventory()
        {
            if (DoesAppInventoryFileExist())
            {
                string json = File.ReadAllText(AppInventory);
                InventoryData = JsonSerializer.Deserialize<SortedSet<double>>(json)!;
                return InventoryData;
            }
            else
            {
                InventoryData = new SortedSet<double>();
                return InventoryData;
            }
        }

        public static void SaveInventory(SortedSet<double> i)
        {
            string json = JsonSerializer.Serialize(i, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(AppInventory, json);
        }
    }
}
