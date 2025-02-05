using Project.Scripts.GoogleImporter.SheetService;

namespace Project.Scripts.Inventory.Data
{
    public interface IGameStateProvider
    {
        public GameSettings GameSettings { get; }
        
        public void SaveGameState();
        public void LoadGameState();
    }
}