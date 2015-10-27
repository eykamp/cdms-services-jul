using System;

namespace services.Models.Data
{
    public class Electrofishing_Detail : DataDetail
    {
        public int? Sequence { get; set; }
        public string PitTagCode { get; set; }
        public string SpeciesRunRearing { get; set; }
        public double? ForkLength { get; set; }
        public double? Weight { get; set; }
        public double? TotalLength { get; set; }
        public string OtherSpecies { get; set; }
        public int? FishCount { get; set; }
        public string SizeCategory { get; set; }
        public string ConditionalComment { get; set; }
        public string TextualComments { get; set; }
        public string Note { get; set; }
        public string TagStatus { get; set; }
        public string ClipStatus { get; set; }
        public string OtolithID { get; set; }
        public string GeneticID { get; set; }
        public string OtherID { get; set; }
        public string AdditionalPositionalComment { get; set; }
        public string ChannelUnitType { get; set; }
    }
}