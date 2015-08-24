﻿
using System;

namespace services.Models.Data
{
    public class SnorkelFish_Header : DataHeader
    {
        public string Team { get; set; }
        public string NoteTaker { get; set; } 
        public string StartTime { get; set; } 
        public string EndTime { get; set; } 
        public double? WaterTemperature { get; set; }
        public int? Visibility { get; set; }
        public string WeatherConditions{ get; set; } 
        public int? VisitId { get; set; }
        public string Comments { get; set; }
        public string CollectionType { get; set; }
        public double? DominantSpecies { get; set; } 
        public double? CommonSpecies { get; set; } 
        public double? RareSpecies { get; set; } 
    }
}
