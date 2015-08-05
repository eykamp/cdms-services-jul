using System;

namespace services.Models.Data
{
    public class SpawningGroundSurvey_Header : DataHeader
    {
        public DateTime ActivityDate { get; set; }
        public string Species { get; set; }
        public string Technicians { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StartTemperature { get; set; }
        public int EndTemperature { get; set; }
        public int StartEasting { get; set; }
        public int StartNorthing { get; set; }
        public int EndEasting { get; set; }
        public int EndNorthing { get; set; }
        public string Flow { get; set; }
        public int WaterVisibility { get; set; }
        public string Weather { get; set; }
        public int FlaggedRedds { get; set; }
        public int NewRedds { get; set; }
        public string HeaderComments { get; set; }
        public string FieldsheetLink { get; set; }
    }
}