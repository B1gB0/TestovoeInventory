using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class EquipmentController
    {
        private EquipmentView _equipmentView;

        public EquipmentController(EquipmentView equipmentView)
        {
            _equipmentView = equipmentView;
        }

        public void OnChangedArmor()
        {
            
        }
    }
}