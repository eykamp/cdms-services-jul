using System;

namespace services.Models.Data
{
    public class ScrewTrap_Detail : DataDetail
    {
        public int? Sequence { get; set; }
        public string PitTagCode { get; set; }
        public string SpeciesRunRearing { get; set; }
        public double? ForkLength { get; set; }
        public double? Weight { get; set; }
        public string OtherSpecies { get; set; }
        public int? FishCount { get; set; }
        public string ConditionalComment { get; set; }
        public string TextualComments { get; set; }
        public string Note { get; set; }
        public string ReleaseLocation { get; set; }
        public string TagStatus { get; set; }
        public string ClipStatus { get; set; }
        public string FishComments { get; set; }
        public string AdditionalPositionalComments { get; set; }
    }
}