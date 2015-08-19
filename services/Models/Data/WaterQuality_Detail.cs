namespace services.Models.Data
{
    public class WaterQuality_Detail : DataDetail
    {
        public string CharacteristicName { get; set; }
        public float? Result { get; set; }
        public string ResultUnits { get; set; }
        public string LabDuplicate { get; set; }
        public string Comments { get; set; }
    }
}