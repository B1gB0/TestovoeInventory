using System.Collections.Generic;
using Project.Scripts.GoogleImporter;

namespace Project.Scripts.Inventory.Data
{
    public interface IGameStateProvider
    {
        public GameSettings GameSettings { get; }

        public Dictionary<string, ItemSettings> Items { get; }

        public void SaveGameState();
        public void LoadGameState();
    }
}