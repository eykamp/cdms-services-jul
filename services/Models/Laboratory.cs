using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace services.Models
{
    public class Laboratory
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfLimit { get; set; }
        public int UserId { get; set; }

        public virtual List<LaboratoryCharacteristic> Characteristics {  get; set; }


        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual List<Project> ProjectLaboratories { get; set; }

        public Laboratory()
        {
            CreateDateTime = DateTime.Now;
        }
    }


    public class LaboratoryCharacteristic
    {
        public int Id { get; set; }
        public int LaboratoryId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Name { get; set; }
        public string MDL { get; set; } 
        public string MethodId { get; set; }
        public string Context { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual Laboratory Laboratory { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public LaboratoryCharacteristic()
        {
            CreateDateTime = DateTime.Now;
        }
    }
}