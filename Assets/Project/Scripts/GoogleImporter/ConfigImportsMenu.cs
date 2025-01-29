using Project.Scripts.Storage;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.GoogleImporter
{
    public class ConfigImportsMenu
    {
        private const string SpreadSheetId = "1iAaPrtWuk4j_PjGhnbbAaK5NDEr_gaWDcrcMwfLJ-1c";
        private const string ItemsSheetsName = "InventoryItems";
        private const string SettingsFileName = "GameSettings";
        private const string CredentialPath = "inventoryconfigs-2a78890166af.json";
        private const string GameSettingsPath = @"D:\Repositoris\TestovoeInventory\Assets\Project\Resources";

        private static readonly JsonToFileStorageService _storageService = new();
        
        [MenuItem("InventoryConfigs/Import Items Settings")]
        private static async void LoadItemsSettings()
        {
            var sheetsImporter = new GoogleSheetsImporter(CredentialPath, SpreadSheetId);
            var gameSettings = LoadSettings();
            
            var itemsParser = new ItemSettingsParser(gameSettings);
            await sheetsImporter.DownloadAndParseSheet(ItemsSheetsName, itemsParser);

            var jsonForSaving = JsonUtility.ToJson(gameSettings);
            _storageService.Save(SettingsFileName, GameSettingsPath, gameSettings);
        }

        private static GameSettings LoadSettings()
        {
            GameSettings gameSettings = null;

            _storageService.Load<GameSettings>(SettingsFileName, GameSettingsPath, data =>
            {
                gameSettings = data;
            });
            
            if(gameSettings != null)
                return gameSettings;
            
            return gameSettings = new GameSettings();
        }
    }
}