
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
        public string DominantSpecies { get; set; } 
        public string CommonSpecies { get; set; } 
        public string RareSpecies { get; set; } 
        public string Unit { get; set; }
        public string IsAEM { get; set; }

    }
}

