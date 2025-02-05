using System;
using Project.Scripts.Inventory.Data;

namespace Project.Scripts.Inventory
{
    public class Equipment
    {
        private readonly EquipmentData _data;
        
        public Equipment(EquipmentData data)
        {
            _data = data;
            
            GetDataOfHeadArmor(_data.HeadArmorId, _data.HeadCapacity, _data.HeadArmorIconName, _data.HeadDescription,
                _data.HeadArmorCharacteristics, _data.HeadWeight, _data.HeadClassItem, _data.HeadTitle,
                _data.HeadSpecialization, _data.HeadAmount);
            
            GetDataOfBodyArmor(_data.BodyArmorId, _data.BodyCapacity, _data.BodyArmorIconName, _data.BodyDescription,
                _data.BodyArmorCharacteristics, _data.BodyWeight, _data.BodyClassItem, _data.BodyTitle,
                _data.BodySpecialization, _data.BodyAmount);
        }

        public string HeadArmorId { get; private set; }
        public int HeadAmount { get; private set; }
        public int HeadCapacity { get; private set; }
        public string HeadArmorIconName { get; private set; }
        public string HeadDescription { get; private set; }
        public int HeadArmorCharacteristics { get; private set; }
        public float HeadWeight { get; private set; }
        public string HeadClassItem { get; private set; }
        public string HeadTitle { get; private set; }
        public string HeadSpecialization { get; private set; }
        
        public string BodyArmorId { get; private set; }
        public int BodyAmount { get; private set; }
        public int BodyCapacity { get; private set; }
        public string BodyArmorIconName { get; private set; }
        public string BodyDescription { get; private set; }
        public int BodyArmorCharacteristics { get; private set; }
        public float BodyWeight { get; private set; }
        public string BodyClassItem { get; private set; }
        public string BodyTitle { get; private set; }
        public string BodySpecialization { get; private set; }

        public void GetDataOfHeadArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount)
        {
            HeadArmorId = itemId;
            HeadCapacity = itemSlotCapacity;
            HeadArmorIconName = iconName;
            HeadDescription = description;
            HeadArmorCharacteristics = itemCharacteristics;
            HeadWeight = weight;
            HeadClassItem = classItem;
            HeadTitle = title;
            HeadSpecialization = specialization;
            HeadAmount = amount;

            if (_data.HeadArmorId != itemId)
            {
                _data.HeadArmorId = itemId;
                _data.HeadCapacity = itemSlotCapacity;
                _data.HeadArmorIconName = iconName;
                _data.HeadDescription = description;
                _data.HeadArmorCharacteristics = itemCharacteristics;
                _data.HeadWeight = weight;
                _data.HeadClassItem = classItem;
                _data.HeadTitle = title;
                _data.HeadSpecialization = specialization;
                _data.HeadAmount = amount;
            }
        }

        public void GetDataOfBodyArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount)
        {
            BodyArmorId = itemId;
            BodyCapacity = itemSlotCapacity;
            BodyArmorIconName = iconName;
            BodyDescription = description;
            BodyArmorCharacteristics = itemCharacteristics;
            BodyWeight = weight;
            BodyClassItem = classItem;
            BodyTitle = title;
            BodySpecialization = specialization;
            BodyAmount = amount;

            if (_data.BodyArmorId != itemId)
            {
                _data.BodyArmorId = itemId;
                _data.BodyCapacity = itemSlotCapacity;
                _data.BodyArmorIconName = iconName;
                _data.BodyDescription = description;
                _data.BodyArmorCharacteristics = itemCharacteristics;
                _data.BodyWeight = weight;
                _data.BodyClassItem = classItem;
                _data.BodyTitle = title;
                _data.BodySpecialization = specialization;
                _data.BodyAmount = amount;
            }
        }
    }
}