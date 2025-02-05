﻿using Project.Scripts.Storage;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.GoogleImporter
{
    public class ConfigImportsMenu
    {
        private const int GridId = 0;
        private const string SpreadSheetId = "1mtWN2IIvnFd4nWerlyEW0tkPRKPIIv3kSc7Hz0_97PM";
        private const string ItemsSheetsName = "InventoryItems";
        private const string SettingsFileName = "GameSettings";
        
        private const string URLTable =
            "https://docs.google.com/spreadsheets/d/1mtWN2IIvnFd4nWerlyEW0tkPRKPIIv3kSc7Hz0_97PM/export?format=csv";

        private static readonly JsonToFileStorageService _storageService = new();
        
        [MenuItem("InventoryConfigs/Open Table")]
        public static void OpenUrl()
        {
            Application.OpenURL($"https://docs.google.com/spreadsheets/d/{SpreadSheetId}/edit#gid={GridId}");
        }
        
        [MenuItem("InventoryConfigs/Import Items Settings")]
        private static async void LoadItemsSettings()
        {
            var CSVLoader = new CsvLoader();
            
            var gameSettings = LoadSettings();
            
            var itemsParser = new ItemSettingsParser(gameSettings);
            await CSVLoader.DownloadRawCsvTable(URLTable, ItemsSheetsName, itemsParser);

            var jsonForSaving = JsonUtility.ToJson(gameSettings);
            _storageService.Save(SettingsFileName, Application.persistentDataPath, gameSettings);
        }

        private static GameSettings LoadSettings()
        {
            GameSettings gameSettings = null;

            _storageService.Load<GameSettings>(SettingsFileName, Application.persistentDataPath, data =>
            {
                gameSettings = data;
            });
            
            if(gameSettings != null)
                return gameSettings;
            
            return gameSettings = new GameSettings();
        }
    }
}