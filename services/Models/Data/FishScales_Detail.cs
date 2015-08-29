using System;

namespace services.Models.Data
{
    public class FishScales_Detail : DataDetail
    {
        public string FieldScaleID { get; set; }
        public string GumCardScaleID { get; set; }
        public DateTime? ScaleCollectionDate { get; set; }
        public string Species { get; set; }
        public string LifeStage { get; set; }
        public int? Circuli { get; set; }
        public string JuvenileAge { get; set; }
        public int? FreshwaterAge { get; set; }
        public int? SaltWaterAge { get; set; }
        public int? TotalAdultAge { get; set; }
        public string SpawnCheck { get; set; }
        public string Regeneration { get; set; }
        public string Stock { get; set; }
        public string FishComments { get; set; }
    }
}