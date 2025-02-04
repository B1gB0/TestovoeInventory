using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace Project.Scripts.GoogleImporter
{
    public class GoogleSheetsImporter
    {
        private readonly List<string> _headers = new();
        private readonly string _sheetId;
        private readonly SheetsService _service;

        public GoogleSheetsImporter(string credentialPath, string sheetId)
        {
            _sheetId = sheetId;
            
            GoogleCredential credential;
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
            }

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }

        public async Task DownloadAndParseSheet(string sheetName, IGoogleSheetParser parser)
        {
            Debug.Log($"Starting downloading sheet (${sheetName})...)");

            var range = $"{sheetName}!A1:Z";
            var request = _service.Spreadsheets.Values.Get(_sheetId, range);

            ValueRange response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error retrieving Google Sheets data: {e.Message}");
                return;
            }

            if (response != null && response.Values != null)
            {
                var tableArray = response.Values;
                Debug.Log($"Sheet downloaded successfully: {sheetName}");

                var firstRow = tableArray[0];
                foreach (var cell in firstRow)
                {
                    _headers.Add(cell.ToString());
                }

                var rowsCount = tableArray.Count;
                Debug.Log(rowsCount);

                for (var i = 1; i < rowsCount; i++)
                {
                    var row = tableArray[i];
                    var rowLength = rowsCount;

                    for (var j = 0; j < rowLength; j++)
                    {
                        var cell = row[j];
                        var header = _headers[j];
                        
                        parser.Parse(header, cell.ToString());
                        
                        Debug.Log(header);

                        //Debug.Log($"Header: {header}, value: {cell}");
                    }
                }
                
                Debug.Log($"Sheet parsed successfully");
            }
            else
            {
                Debug.LogWarning("No data found in Google Sheets");
            }
        }
    }
}
