namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElectrofishingViewForReporting : DbMigration
    {
        public override void Up()
        {
            Sql(@"
create view Electrofishing_vw as
select
    h.FishNumber, h.EventType, h.FileTitle, h.ClipFiles, h.Crew, h.ReleaseTemp, h.Conductivity, h.EFModel, h.SiteLength, h.SiteWidth, h.SiteDepth, h.SiteArea, h.HabitatType, 
    h.Visibility, h.ActivityComments, h.ReleaseSite, h.Weather, h.ReleaseRiverKM, h.PassNumber, h.TimeBegin, h.TimeEnd, h.TotalSecondsEF, h.WaterTempBegin, h.WaterTempStop, 
    h.Hertz, h.Freq, h.Volts, h.TotalFishCaptured, h.ReleaseLocation, h.VisitID, h.Unit, h.StartTime, h.EndTime, h.ReleaseTime, h.StartTemp,

    d.Sequence, d.PitTagCode, d.SpeciesRunRearing, d.ForkLength, d.Weight, d.OtherSpecies, d.FishCount, d.ConditionalComment, d.TextualComments, d.Note, 
    d.OtolithID, d.GeneticID, d.OtherID, d.FishComments, d.RowId, d.RowStatusId, d.ByUserId, d.QAStatusId, d.TagStatus, d.ClipStatus, d.SizeCategory

    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, a.activitydate, w.id as WaterbodyId, w.name as WaterbodyName, l.id as LocationId, 
    l.name as LocationName
from Electrofishing_Detail d 
join Electrofishing_Header h on d.ActivityId = h.ActivityId
join activities a on a.id = h.ActivityId
join locations l on l.id = a.locationid
join waterbodies w on w.id = l.waterbodyid

go
CREATE VIEW [dbo].[Electrofishing_Detail_VW]
AS
SELECT        *
FROM            dbo.Electrofishing_Detail AS d
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.Electrofishing_Detail AS dd
                               WHERE        (ActivityId = d.ActivityId) AND (RowId = d.RowId)))
go

CREATE VIEW [dbo].[Electrofishing_Header_VW]
AS
SELECT        *
FROM            dbo.Electrofishing_Header AS h
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.Electrofishing_Header AS hh
                               WHERE        (ActivityId = h.ActivityId)));
");
        }

        public override void Down()
        {
            Sql(@"
drop view Electrofishing_vw
go
drop view Electrofishing_Detail_VW
go
drop view Electrofishing_Header_VW

");
        }
    }
}