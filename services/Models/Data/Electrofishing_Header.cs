
using System;

namespace services.Models.Data
{
    public class Electrofishing_Header : DataHeader
    {
        public string FishNumber { get; set; }
        public string EventType { get; set; }
        public string FileTitle { get; set; }
        public string ClipFiles { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ReleaseTime { get; set; }
        public string ReleaseLocation { get; set; }
        public string VisitID { get; set; }
        public string Unit { get; set; }
        public string Crew { get; set; }
        public double? StartTemp { get; set; }
        public double? ReleaseTemp { get; set; }
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