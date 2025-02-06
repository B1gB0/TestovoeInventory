using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

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
                case "ID" :
                    _currentItemSettings = new ItemSettings
                    {
                        Id = token
                    };

                    _gameSettings.Items.Add(_currentItemSettings);
                    break;
                case "CellCapacity" :
                    _currentItemSettings.CellCapacity = ParseInt(token);
                    break;
                case "Title" :
                    _currentItemSettings.Title = token;
                    break;
                case "Description" :
                    _currentItemSettings.Description = token;
                    break;
                case "IconName" : 
                    _currentItemSettings.IconName = token;
                    break;
                case "ItemCharacteristics" :
                    _currentItemSettings.ItemCharacteristics = ParseInt(token);
                    break;
                case "Weight" :
                    _currentItemSettings.Weight = ParseFloat(token);
                    break;
                case "ClassItem" :
                    _currentItemSettings.ClassItem = token;
                    break;
                case "Specialization" :
                    _currentItemSettings.Specialization = token;
                    break;
                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }
        
        private int ParseInt(string s)
        {
            int result = -1;
            if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't parse int, wrong text");
            }

            return result;
        }
    
        private float ParseFloat(string s)
        {
            float result = -1;
            if (!float.TryParse(s, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't pars float,wrong text ");
            }

            return result;
        }
    }
}