using System;

namespace Project.Scripts.Inventory.Data
{
    [Serializable]
    public class EquipmentData
    {
        public string HeadArmorId;
        public int HeadAmount;
        public int HeadCapacity;
        public string HeadArmorIconName;
        public string HeadDescription;
        public int HeadArmorCharacteristics;
        public float HeadWeight;
        public string HeadClassItem;
        public string HeadTitle;
        public string HeadSpecialization;

        public string BodyArmorId;
        public int BodyAmount;
        public int BodyCapacity;
        public string BodyArmorIconName;
        public string BodyDescription;
        public int BodyArmorCharacteristics;
        public float BodyWeight;
        public string BodyClassItem;
        public string BodyTitle;
        public string BodySpecialization;
    }
}
