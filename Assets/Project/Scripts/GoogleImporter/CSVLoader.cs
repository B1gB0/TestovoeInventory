using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Scripts.GoogleImporter
{
    public class CsvLoader
    {
        private const char CellSeporator = ',';
        private const int StartRawDataIndex = 1;
    
        private readonly List<string> _headers = new();

        public async Task DownloadRawCsvTable(string actualUrl, string sheetName, IGoogleSheetParser parser)
        {
            using UnityWebRequest request = UnityWebRequest.Get(actualUrl);
        
            try
            { 
                await request.SendWebRequest();
                Debug.Log($"Sheet downloaded successfully: {sheetName}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error retrieving Google Sheets data: {e.Message}");
                throw;
            }
            
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string firstLineEnding = "\n";
                string secondLineEnding = "\r";

                string text = request.downloadHandler.text.Replace(firstLineEnding, "");
                string[] rows = text.Split(secondLineEnding);

                var firstRow = rows[0];
                string[] cellsInFirstRow = rows[0].Split(CellSeporator);

                foreach (var cell in cellsInFirstRow)
                {
                    _headers.Add(cell);
                }

                var rowsCount = rows.Length;

                for (var i = StartRawDataIndex; i < rowsCount; i++)
                {
                    string[] cells = rows[i].Split(CellSeporator);
                
                    var rowLength = rowsCount;

                    for (var j = 0; j < rowLength + 1; j++)
                    {
                        var cell = cells[j];
                        var header = _headers[j];

                        parser.Parse(header, cell);

                        Debug.Log($"Header: {header}, value: {cell}");
                    }
                }
                
                Debug.Log($"Sheet parsed successfully");
            }
        }
    }
}
