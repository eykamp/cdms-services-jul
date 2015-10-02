namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StreamNetRperSReportingViews : DbMigration
    {
        public override void Up()
        {

            Sql(@"
    create view StreamNet_RperS_vw as
    select
    d.Id, d.LastModifiedOn, d.CommonName, d.Run, d.PopFit, 
    d.SpawnerLocation, d.RecruitLocation, d.BroodYear, d.RperStype, d.TRTmethod, d.ContactAgency, d.MethodNumber, d.RperS, d.Comments, 
    d.NullRecord, d.DataStatus, d.ContactPersonFirst, d.ContactPersonLast, d.ContactPhone, d.ContactEmail, d.Age10Adults, d.Age11PlusAdults, 
    d.Age1Juvs, d.Age2Adults, d.Age2Juvs, d.Age3Adults, d.Age3Juvs, d.Age4Adults, d.Age4PlusJuvs, d.Age5Adults, d.Age6Adults, d.Age7Adults, 
    d.Age8Adults, d.Age9Adults, d.CBFWApopName, d.CommonPopName, d.CompilerRecordID, d.DataEntry, d.DataEntryNotes, d.ESU_DPS, d.HarvestAdj, 
    d.HatcherySpawners, d.HatcherySpawnersAlpha, d.HatcherySpawnersLowerLimit, d.HatcherySpawnersUpperLimit, d.IndicatorLocation, d.LastUpdated, 
    d.MainstemHarvest, d.MajorPopGroup, d.MeasureLocation, d.MetaComments, d.MethodAdjustments, d.MetricLocation, d.NOBroodStockRemoved, 
    d.OceanHarvest, d.PopFitNotes, d.PopID, d.ProtMethDocumentation, d.ProtMethName, d.ProtMethURL, d.Publish, d.RecoveryDomain, d.Recruits, 
    d.RecruitsAlpha, d.RecruitsLowerLimit, d.RecruitsMissing, d.RecruitsMissingExplanation, d.RecruitsUpperLimit, d.RefID, d.RperSAlpha,
    d.RperSLowerLimit, d.RperSUpperLimit, d.SubmitAgency, d.TotalSpawners, d.TotalSpawnersAlpha, d.TotalSpawnersLowerLimit, d.TotalSpawnersUpperLimit,
    d.TribHarvest, d.UpdDate, d.YOY, d.ShadowId,

    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, a.ActivityDate, w.id as WaterbodyId, w.name as WaterbodyName, l.id as LocationId, 
    l.name as LocationName

from StreamNet_RperS_detail d 
join StreamNet_RperS_Header h on d.ActivityId = h.ActivityId
join activities a on a.id = h.ActivityId
join locations l on l.id = a.locationid
join waterbodies w on w.id = l.waterbodyid



go
CREATE VIEW [dbo].[StreamNet_RperS_detail_VW]
AS
SELECT        *
FROM            dbo.StreamNet_RperS_detail AS d
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.StreamNet_RperS_detail AS dd
                               WHERE        (ActivityId = d.ActivityId) AND (RowId = d.RowId)))
go

CREATE VIEW [dbo].[StreamNet_RperS_Header_VW]
AS
SELECT        *
FROM            dbo.StreamNet_RperS_Header AS h
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.StreamNet_RperS_Header AS hh
                               WHERE        (ActivityId = h.ActivityId)));
");
        }


        public override void Down()
        {
            Sql(@"
drop view StreamNet_RperS_vw
go
drop view StreamNet_RperS_VW
go
drop view StreamNet_RperS_Header_VW
go
");
        }
    }
}
