namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateViewToIncludeMoreSGSData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
drop view FishScales_vw
go
create view FishScales_vw as
select
    h.RunYear, h.Technician,

    d.FieldScaleId, d.GumCardScaleID, d.ScaleCollectionDate, d.Species, d.Circuli, d.FreshwaterAge, d.SaltWaterAge, d.TotalAdultAge,
    d.SpawnCheck, d.Regeneration, d.Stock, d.RowId, d.RowStatusId, d.ScaleComments, d.BadScale, d.QAStatusId,

    ef.unit, 
    coalesce(st.HatNat, 
              --ef.origin, 
            sgs.origin, aw.origin) as HatNat,
    coalesce(st.ForkLength, ef.ForkLength, sgs.ForkLength, aw.ForkLength) as ForkLength,
    coalesce( --st.LifeStage, 
            --ef.LifeStage, 
            --sgs.LifeStage, 
            NULL, aw.LifeStage) as LifeStage,
    coalesce(st.Weight, ef.Weight, --sgs.Weight, 
            aw.Weight) as Weight,

    coalesce(--st.FinClips, 
             --ef.FinClips, 
            sgs.FinClips, aw.FinClip) as FinClips,
    coalesce(--st.Marks, 
            --ef.Marks, 
            sgs.Marks, aw.Mark) as Marks,
    coalesce(--st.MeHPLength, 
             --ef.MeHPLength, 
            sgs.MeHPLength, NULL --aw.MeHPLength
        ) as MeHPLength,
    coalesce(--st.PercentRetained, 
                --ef.PercentRetained, 
        sgs.SpawningStatus, NULL --aw.PercentRetained
        ) as PercentRetained,
    coalesce(--st.Sex, 
             --ef.Sex, 
             sgs.Sex, aw.Sex) as Sex,


    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, a.ActivityDate, a.CreateDate,   
    aq.QAStatusName as QAStatusName, aq.Comments AS ActivityQAComments, aq.QAStatusId AS ActivityQAStatusId,

    --w.id as WaterbodyId, w.name as WaterbodyName, 
    coalesce(st.LocationLabel, ef.LocationLabel, sgs.LocationLabel--, aw.LocationLabel
        ) as LocationLabel,
    coalesce(st.LocationLabel, ef.LocationLabel, sgs.LocationLabel--, aw.LocationLabel
        ) as LocationId
from FishScales_Detail_vw d 
left join FishScales_Header_vw h on d.ActivityId = h.ActivityId
left join activities a on a.id = h.ActivityId

--left join waterbodies w on w.id = l.waterbodyid

-- External tables
left join screwTrap_vw st on st.textualcomments = d.FieldScaleID
left join electrofishing_vw ef on ef.textualcomments = d.FieldScaleID
left join spawninggroundsurvey_vw sgs on sgs.scaleId = d.FieldScaleID
left join adultWeir_vw aw on aw.ScaleId = d.FieldScaleId

left join ActivityQAs_VW AS aq ON aq.ActivityId = a.Id


go

");
        }
        
        public override void Down()
        {
            Sql(@"
drop view FishScales_vw
go
create view FishScales_vw as
select
    h.RunYear, h.Technician,

    d.FieldScaleId, d.GumCardScaleID, d.ScaleCollectionDate, d.Species, d.LifeStage, d.Circuli, d.FreshwaterAge, d.SaltWaterAge, d.TotalAdultAge,
    d.SpawnCheck, d.Regeneration, d.Stock, d.RowId, d.RowStatusId, d.ScaleComments, d.BadScale, d.QAStatusId,

    ef.unit, st.HatNat, coalesce(st.ForkLength, ef.ForkLength) as ForkLength,
    coalesce(st.Weight, ef.Weight) as Weight,

    sgs.FinClips,
    sgs.Marks,
    sgs.MeHPLength,
    sgs.SpawningStatus as PercentRetained,
    sgs.Sex,


    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, a.ActivityDate, a.CreateDate,   
    aq.QAStatusName as QAStatusName, aq.Comments AS ActivityQAComments, aq.QAStatusId AS ActivityQAStatusId,

    w.id as WaterbodyId, w.name as WaterbodyName, 
    l.id as LocationId, 

    l.name as LocationLabel

from FishScales_Detail_vw d 
left join FishScales_Header_vw h on d.ActivityId = h.ActivityId
left join activities a on a.id = h.ActivityId
left join locations l on l.id = a.locationid
left join waterbodies w on w.id = l.waterbodyid
left join screwTrap_vw st on st.textualcomments = d.FieldScaleID
left join electrofishing_vw ef on ef.textualcomments = d.FieldScaleID
left join spawninggroundsurvey_vw sgs on sgs.scaleId = d.FieldScaleID
left join ActivityQAs_VW AS aq ON aq.ActivityId = a.Id

go

");
        }
    }
}
