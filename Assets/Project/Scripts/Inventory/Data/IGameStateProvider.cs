namespace Project.Scripts.Inventory.Data
{
    public interface IGameStateProvider
    {
        public void SaveGameState();
        public void LoadGameState();
    }
}