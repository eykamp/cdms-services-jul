namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WaterQualityViewsReduxe : DbMigration
    {
        public override void Up()
        {
            Sql(@"
drop view WaterQuality_vw
go
drop view WaterQuality_Detail_VW
go
drop view WaterQuality_Header_VW
go


CREATE VIEW [dbo].[WaterQuality_Detail_VW]
AS
SELECT        *
FROM            dbo.WaterQuality_Detail AS d
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.WaterQuality_Detail AS dd
                               WHERE        (ActivityId = d.ActivityId) AND (RowId = d.RowId)))
go


CREATE VIEW [dbo].[WaterQuality_Header_VW]
AS
SELECT        *
FROM            dbo.WaterQuality_Header AS h
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.WaterQuality_Header AS hh
                               WHERE        (ActivityId = h.ActivityId)));
go



create view WaterQuality_vw as
select
    h.DataType, h.FieldsheetLink,

    d.CharacteristicName, d.Result, d.ResultUnits, d.LabDuplicate, d.Comments, d.RowId, d.RowStatusId, d.MdlResults, d.SampleDate, d.SampleID,
    d.SampleFraction, d.MethodSpeciation, d.DetectionLimit, d.ContextID, d.MethodID, d.LabName,

    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, 
    a.ActivityDate, a.CreateDate,       -- required

    loc.OtherAgencyId,
    loc.id as LocationId,


    aq.QAStatusName as QAStatusName, aq.Comments AS ActivityQAComments, aq.QAStatusId, aq.QAStatusId AS ActivityQAStatusId  -- required
from WaterQuality_Detail_vw d 
join WaterQuality_Header_vw h on d.ActivityId = h.ActivityId
join activities a on a.id = h.ActivityId

join locations loc on loc.id = a.locationid
--join waterbodies w on w.id = l.waterbodyid
join ActivityQAs_VW AS aq ON aq.ActivityId = a.Id
go

");
        }
        
        public override void Down()
        {
            Sql(@"
drop view WaterQuality_vw
go
drop view WaterQuality_Detail_VW
go
drop view WaterQuality_Header_VW
go


CREATE VIEW [dbo].[WaterQuality_Detail_VW]
AS
SELECT        *
FROM            dbo.WaterQuality_Detail AS d
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.WaterQuality_Detail AS dd
                               WHERE        (ActivityId = d.ActivityId) AND (RowId = d.RowId)))
go


CREATE VIEW [dbo].[WaterQuality_Header_VW]
AS
SELECT        *
FROM            dbo.WaterQuality_Header AS h
WHERE        (EffDt =
                             (SELECT        MAX(EffDt) AS MaxEffDt
                               FROM            dbo.WaterQuality_Header AS hh
                               WHERE        (ActivityId = h.ActivityId)));
go



create view WaterQuality_vw as
select
    h.DataType,

    d.CharacteristicName, d.Result, d.ResultUnits, d.LabDuplicate, d.Comments, d.RowId, d.RowStatusId, d.MdlResults, d.SampleDate, d.SampleID,
    d.SampleFraction, d.MethodSpeciation, d.DetectionLimit, d.ContextID, d.MethodID, d.LabName,

    a.id as ActivityId, a.DatasetId, a.InstrumentId, a.LaboratoryId, 
    a.ActivityDate, a.CreateDate,       -- required

    loc.OtherAgencyId,
    loc.id as LocationId,


    aq.QAStatusName as QAStatusName, aq.Comments AS ActivityQAComments, aq.QAStatusId AS ActivityQAStatusId  -- required
from WaterQuality_Detail_vw d 
join WaterQuality_Header_vw h on d.ActivityId = h.ActivityId
join activities a on a.id = h.ActivityId

join locations loc on loc.id = a.locationid
--join waterbodies w on w.id = l.waterbodyid
join ActivityQAs_VW AS aq ON aq.ActivityId = a.Id
go

");
        }
    }
}
