﻿using System;

namespace services.Models.Data
{
    public class WaterQuality_Detail : DataDetail
    {
        public string CharacteristicName { get; set; }
        public string Result { get; set; }
        public string ResultUnits { get; set; }
        public string LabDuplicate { get; set; }
        public string Comments { get; set; }
        public string MdlResults { get; set; }
        public DateTime? SampleDate { get; set; }
        public string SampleID { get; set; }

    }
}