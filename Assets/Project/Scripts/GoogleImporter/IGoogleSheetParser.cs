﻿namespace Project.Scripts.GoogleImporter
{
    public interface IGoogleSheetParser
    {
        public void Parse(string header, string token);
    }
}