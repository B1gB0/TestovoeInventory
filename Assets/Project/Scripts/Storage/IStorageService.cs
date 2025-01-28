using System;

namespace Project.Scripts.Storage
{
    public interface IStorageService
    {
        void Save(string key, string path, object data, Action<bool> callback = null);
        void Load<T>(string key, string path, Action<T> callback);
    }
}