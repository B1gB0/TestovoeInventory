using System;
using System.IO;
using Newtonsoft.Json;

namespace Project.Scripts.Storage
{
    public class JsonToFileStorageService : IStorageService
    {
        private bool _isInProgressNow;

        public void Save(string key, string path, object data, Action<bool> callback = null)
        {
            if (!_isInProgressNow)
            {
                SaveAsync(key, path, data, callback);
            }
            else
            {
                callback?.Invoke(false);
            }
        }

        public void Load<T>(string key, string path, Action<T> callback = null)
        {
            path = BuildPath(path,key);
            
            if (!File.Exists(path))
                return;

            using var fileStream = new StreamReader(path);
            var json = fileStream.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);

            callback?.Invoke(data);
        }

        private async void SaveAsync(string key, string path, object data, Action<bool> callback)
        {
            path = BuildPath(path, key);
            string json = JsonConvert.SerializeObject(data);

            using (var fileStream = new StreamWriter(path))
            {
                await fileStream.WriteAsync(json);
            }
            
            callback?.Invoke(true);
        }

        private string BuildPath(string path, string key)
        {
            return Path.Combine(path, key);
        }
    }
}