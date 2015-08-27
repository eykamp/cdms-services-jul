
using System;

namespace services.Models.Data
{
    public class ScrewTrap_Header : DataHeader
    {
        public string FileTitle { get; set; }
        public string ClipFiles { get; set; }
        public DateTime? TagDateTime { get; set; }
        public DateTime? ReleaseDateTime { get; set; }
        public string Tagger { get; set; }
        public string CaptureMethod { get; set; }
        public int? MigratoryYear { get; set; }
        public double? LivewellTemp { get; set; }
        public double? TaggingTemp { get; set; }
        public double? PostTaggingTemp { get; set; }
        public double? ReleaseTemp { get; set; }
        public string TaggingMethod { get; set; }
        public string Organization { get; set; }
        public string CoordinatorID { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartTime { get; set; }
        public double? ArrivalRPMs { get; set; }
        public double? DepartureRPMs { get; set; }
        public int? Hubometer { get; set; }
        public DateTime? HubometerTime { get; set; }
        public DateTime? TrapStopped { get; set; }
        public DateTime? TrapStarted { get; set; }
        public DateTime? FishCollected { get; set; }
        public DateTime? FishReleased { get; set; }
        public string Flow { get; set; }
        public string Turbitity { get; set; }
        public string TrapDebris { get; set; }
        public string RiverDebris { get; set; }
        public string Task { get; set; }
        public string ActivityComments { get; set; }
        public string ReleaseSite { get; set; }
        public string ReleaseRiverKM { get; set; }
    }
}