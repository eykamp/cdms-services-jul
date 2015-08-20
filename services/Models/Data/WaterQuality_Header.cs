using System;

namespace services.Models.Data
{
    public class WaterQuality_Header : DataHeader
    {
        public string DataType { get; set; }
        public DateTime? SampleDate { get; set; }
        public string SampleID { get; set; }
    }
}