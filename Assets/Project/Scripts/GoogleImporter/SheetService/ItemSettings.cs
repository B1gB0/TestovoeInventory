using System;

namespace Project.Scripts.GoogleImporter
{
    [Serializable]
    public class ItemSettings
    {
        public string Id;
        public int CellCapacity;
        public string Title;
        public string Description;
        public string IconName;
        public int ItemCharacteristics;
        public float Weight;
        public string ClassItem;
        public string Specialization;
    }
}