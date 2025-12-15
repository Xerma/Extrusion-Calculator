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

        private static SortedSet<InventoryPiece> _inventoryData = new();

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

        public static SortedSet<InventoryPiece> LoadInventory()
        {
            if (!DoesAppInventoryFileExist())
            {
                _inventoryData = new SortedSet<InventoryPiece>();
                return _inventoryData;
            }

            string json = File.ReadAllText(AppInventory);
            return JsonSerializer.Deserialize<SortedSet<InventoryPiece>>(json)!;
        }

        public static void SaveInventory(SortedSet<InventoryPiece> i)
        {
            string json = JsonSerializer.Serialize(i, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(AppInventory, json);
        }
    }
}
