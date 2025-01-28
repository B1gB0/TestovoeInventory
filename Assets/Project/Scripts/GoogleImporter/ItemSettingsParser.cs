using System;
using System.Collections.Generic;

namespace Project.Scripts.GoogleImporter
{
    public class ItemSettingsParser : IGoogleSheetParser
    {
        private readonly GameSettings _gameSettings;
        private ItemSettings _currentItemSettings;

        public ItemSettingsParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.Items = new List<ItemSettings>();
        }
        
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "ID":
                    _currentItemSettings = new ItemSettings
                    {
                        Id = token
                    };
                    
                    _gameSettings.Items.Add(_currentItemSettings);
                    break;
                case "CellCapacity":
                    _currentItemSettings.CellCapacity = Convert.ToInt32(token);
                    break;
                case "Title":
                    _currentItemSettings.Title = token;
                    break;
                case "Description":
                    _currentItemSettings.Description = token;
                    break;
                case "IconName": 
                    _currentItemSettings.IconName = token;
                    break;
                case "ItemCharacteristics":
                    _currentItemSettings.ItemCharacteristics = token;
                    break;
                case "Weight":
                    _currentItemSettings.Weight = token;
                    break;
                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }
    }
}