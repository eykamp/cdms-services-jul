
using System;

namespace services.Models.Data
{
    public class Electrofishing_Header : DataHeader
    {
        public string FishNumber { get; set; }
        public string EventType { get; set; }
        public string FileTitle { get; set; }
        public string ClipFiles { get; set; }
        public DateTime? TagDateTime { get; set; }
        public DateTime? ReleaseDateTime { get; set; }
        public string Tagger { get; set; }
        public string Crew { get; set; }
        public string CaptureMethod { get; set; }
        public int? MigratoryYear { get; set; }
        public double? TaggingTemp { get; set; }
        public double? ReleaseTemp { get; set; }
        public string TaggingMethod { get; set; }
        public string Organization { get; set; }
        public string CoordinatorID { get; set; }
        public double? Conductivity { get; set; }
        public string EFModel { get; set; }
        public double? SiteLength { get; set; }
        public double? SiteWidth { get; set; }
        public double? SiteDepth { get; set; }
        public double? SiteArea { get; set; }
        public string HabitatType { get; set; }
        public string Visibility { get; set; }
        public string ActivityComments { get; set; }
        public string ReleaseSite { get; set; }
        public string Weather { get; set; }
        public string ReleaseRiverKM { get; set; }
        public string PassNumber { get; set; }
        public string TimeBegin { get; set; }
        public string TimeEnd { get; set; }
        public double? TotalSecondsEF { get; set; }
        public double? WaterTempBegin { get; set; }
        public double? WaterTempStop { get; set; }
        public double? Hertz { get; set; }
        public double? Freq { get; set; }
        public double? Volts { get; set; }
        public int? TotalFishCaptured { get; set; }  
    }
}