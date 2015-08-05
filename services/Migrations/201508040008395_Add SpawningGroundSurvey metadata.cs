namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddSpawningGroundSurveymetadata : DbMigration
    {
        public override void Up()
        {
            Sql(@"

-- Note: These MUST match values declared in Down()
declare @datasetBaseName as varchar(max) = 'Spawning Ground Survey'
declare @categoryName as varchar(max) = @datasetBaseName
declare @datastoreName as varchar(max) = @datasetBaseName


-- These are predefined project IDs -- the records for them should already exist
CREATE TABLE #ProjectInfo (id int, name varchar(max))
INSERT INTO #ProjectInfo (id) 
          SELECT id = 2249 
UNION ALL SELECT id = 1135 
UNION ALL SELECT id = 1188
UNION ALL SELECT id = 1177


-- Grab the project names
update #ProjectInfo set name = (select name from projects where projects.id = #ProjectInfo.id)


-- Create a field category
INSERT INTO dbo.FieldCategories (Name, Description) 
SELECT Name        = @categoryName,
       Description = @datasetBaseName + ' related fields'


-- Add records to the Datastores
INSERT INTO dbo.Datastores (Name, Description, TablePrefix, DatastoreDatasetId, OwnerUserId, FieldCategoryId) 
SELECT
	Name               = @datastoreName, 
	Description        = NULL, 
	TablePrefix        = REPLACE(@datastoreName, ' ', ''), -- Strip spaces
	DatastoreDatasetId = NULL, 
	OwnerUserId        = 1081,      -- George
	FieldCategoryId    = (SELECT IDENT_CURRENT('dbo.FieldCategories'))


CREATE TABLE #NewDatasetIds (id int)        -- This will contain a list of ids of all dataset records inserted below

-- Add record to the Datasets --> will create one record per project
INSERT INTO	Datasets (ProjectId, DefaultRowQAStatusId, StatusId, CreateDateTime, Name, Description, DefaultActivityQAStatusId, DatastoreId, Config)
OUTPUT INSERTED.id into #NewDatasetIds
SELECT 
    ProjectId                 = p.id,
    DefaultRowQAStatusId      = 1,
    StatusId                  = 1,
    CreateDateTime            = GetDate(),
    Name                      = @datasetBaseName,
    Description               = @datasetBaseName + ': ' + p.name,
    DefaultActivityQAStatusId = 6,
    DatastoreId               = (SELECT IDENT_CURRENT('dbo.Datastores')),
    Config                    = NULL
FROM #ProjectInfo as p

-------------------------

-- This will info about field records inserted below
CREATE TABLE #NewFieldInfo (id int, fieldName nvarchar(max), [validation] nvarchar(max), ControlType nvarchar(max), [Rule] nvarchar(max), FieldRoleId int, OrderIndex int IDENTITY(1,1))    

-----
-- Insert any new fields we'll need (George says don't reuse fields)
-- Header Fields
INSERT INTO dbo.Fields (FieldCategoryId, Name, [Description], Units, [Validation], DataType, PossibleValues, DbColumnName, ControlType, [Rule])
OUTPUT INSERTED.id, INSERTED.Name, INSERTED.[Validation], INSERTED.ControlType , INSERTED.[Rule], NULL INTO #NewFieldInfo

          SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'ActivityDate',
         Description = 'Date activity was performed',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'DateTime',
         PossibleValues = NULL,
         DbColumnName = 'ActivityDate',
         ControlType = 'string',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'Species',
         Description = 'Species the survey is targeting',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues = '[""CHF"", ""CO"", ""STS"", ""CHS"", ""PL""]',
         DbColumnName = 'Species',
         ControlType = 'string',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'Technicians',
         Description = 'Name of the technician who visited the site and performed the activity',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues = NULL,
         DbColumnName = 'Technicians',
         ControlType = 'string',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'StartTime',
         Description = 'Time the technician started the section survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'DateTime',
         PossibleValues = NULL,
         DbColumnName = 'StartTime',
         ControlType = 'string',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'EndTime',
         Description = 'Time the technician ended the section survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'DateTime',
         PossibleValues = NULL,
         DbColumnName = 'EndTime',
         ControlType = 'string',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'StartTemperature',
         Description = 'Temperature of the water at the time the survey began',
         Units = 'C',
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'StartTemperature',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'EndTemperature',
         Description = 'Temperature of the water at the time the survey ended',
         Units = 'C',
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'EndTemperature',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'StartEasting',
         Description = 'Easting location of the Start location of the Spawning Survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'StartEasting',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'StartNorthing',
         Description = 'Northing location of the Start Location of the Spawning Survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'StartNorthing',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'EndEasting',
         Description = 'Easting Location of the End Location of the Spawning Survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'EndEasting',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'EndNorthing',
         Description = 'Northing Location of the End Location of the Spawning survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'EndNorthing',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'Flow',
         Description = 'Condition of the stream at the time of the Spawning survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues = '[""Dry"", ""Low"", ""Moderate"", ""High"", ""Flooding""]',
         DbColumnName = 'Flow',
         ControlType = 'select',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'WaterVisibility',
         Description = 'Can see bottom of Riffles and Pools (1), Can see bottom of riffles (2), Cannot see bottom of riffles or pools (3)',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'WaterVisibility',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'Weather',
         Description = 'Describes the current weather conditions:',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues =  '[""Clear (C)"", ""Overcast (O)"", ""Rain (R)"", ""Snow (S)"", ""Foggy (F)"", ""Partly Cloudy (P)""]',
         DbColumnName = 'Weather',
         ControlType = 'select',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'FlaggedRedds',
         Description = 'Number of old Redds that were already flagged ',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'FlaggedRedds',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'NewRedds',
         Description = 'Number of new Redds found during the Spawning Survey',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'int',
         PossibleValues = NULL,
         DbColumnName = 'NewRedds',
         ControlType = 'number',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'HeaderComments',
         Description = 'Comments about the site or any other observations',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues = NULL,
         DbColumnName = 'HeaderComments',
         ControlType = 'text',
         [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
         Name = 'FieldsheetLink',
         Description = 'Paper scan of the fieldsheet',
         Units = NULL,
         [Validation] = NULL,
         DataType = 'string',
         PossibleValues = NULL,
         DbColumnName = 'FieldsheetLink',
         ControlType = 'file',
         [Rule] = NULL

update #NewFieldInfo set FieldRoleId = 1 where FieldRoleId is NULL   -- 1 == header

-----
-- Details Fields

INSERT INTO dbo.Fields (FieldCategoryId, Name, [Description], Units, [Validation], DataType, PossibleValues, DbColumnName, ControlType, [Rule])
OUTPUT INSERTED.id, INSERTED.Name, INSERTED.[Validation], INSERTED.ControlType , INSERTED.[Rule], NULL INTO #NewFieldInfo
          SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Species',
                 Description = 'Species of the carcass found',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""CHF"", ""CO"", ""STS"", ""CHS"", ""PL""]',
                 DbColumnName = 'Species',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Time',
                 Description = 'Time the feature was discovered',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'DateTime',
                 PossibleValues = NULL,
                 DbColumnName = 'Time',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Temp',
                 Description = 'Water temperature at the feature',
                 Units = 'C',
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'Temp',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Easting',
                 Description = 'Easting location of the feature',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'Easting',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Northing',
                 Description = 'Northing Location of the Feature',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = NULL,
                 DbColumnName = 'Northing',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Channel',
                 Description = 'Channel type the Redd is located in ',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""Braided (B)"", ""Single (S)""]',
                 DbColumnName = 'Channel',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'ReddLocation',
                 Description = 'Location of the Redd',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""Middle (M)"", ""Side (S)""]',
                 DbColumnName = 'ReddLocation',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'ReddHabitat',
                 Description = 'Habitat the Redd is located in',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""Pool"", ""Pool Tail Out"", ""Riffle (RI)"", ""Glide (GL)""]',
                 DbColumnName = 'ReddHabitat',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'WaypointNumber',
                 Description = 'Waypoint Number as assigned by the GPS device',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'WaypointNumber',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'FishCount',
                 Description = 'Number of carcasses or live fish on the Redd',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'FishCount',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'FishLocation',
                 Description = 'FishLocation',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""Near Redd (NR)"", ""Off Redd (OR)""]',
                 DbColumnName = 'FishLocation',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Sex',
                 Description = 'Sex',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""Male (M)"", ""Female (F)"", ""Unknown (UNK)""]',
                 DbColumnName = 'Sex',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'FinClips',
                 Description = 'FinClips',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = (select PossibleValues from dbo.Fields where name = 'Fin Clip'),
                 DbColumnName = 'FinClips',
                 ControlType = 'multiselect',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Marks',
                 Description = 'Any man made mark',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""NONE"",""NA"",""1ROP"",""1LOP"",""2ROP"",""2LOP"",""3ROP"",""3LOP"",""1TAIL"",""2TAIL"",""1CAU"",""2CAU"",""3CAU"",""4CAU""]',
                 DbColumnName = 'Marks',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'SpawningStatus',
                 Description = 'Percent of Eggs or Sperm the carcass has RETAINED (i.e. 100 would indicate a pre-spawn mortality)',
                 Units = 'percentage',
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'SpawningStatus',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'ForkLength',
                 Description = 'Fork Length of the Carcass',
                 Units = 'mm',
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'ForkLength',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'MeHPLength',
                 Description = 'Mid Eye to Hypural Plate Length ',
                 Units = 'mm',
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'MeHPLength',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'SnoutID',
                 Description = 'Id from the snout card',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = NULL,
                 DbColumnName = 'SnoutID',
                 ControlType = 'string',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'ScaleID',
                 Description = 'Id from the scale card',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = NULL,
                 DbColumnName = 'ScaleID',
                 ControlType = 'string',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Tag',
                 Description = 'Does the fish have a tag of any sort?',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = '[""WIRE"",""RADIO"",""FLOY"",""PIT"",""VIE"",""OTHER"",""NONE""]',
                 DbColumnName = 'Tag',
                 ControlType = 'select',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'TagID',
                 Description = 'Identification number on the tag if available',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = NULL,
                 DbColumnName = 'TagID',
                 ControlType = 'string',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Comments',
                 Description = 'Any comments about the live/carcass or the Redd',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'string',
                 PossibleValues = NULL,
                 DbColumnName = 'Comments',
                 ControlType = 'string',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'Ident',
                 Description = 'Identification number attributed to the point (Waypoint number)',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'Ident',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'EastingUTM',
                 Description = 'NAD 83 Zone 11N UTM Easting (X or Longitude) coordinates for the site',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'EastingUTM',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'NorthingUTM',
                 Description = 'NAD 83 Zone 11N UTM Northing (Y or Latitude) coordinates for the site',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'int',
                 PossibleValues = NULL,
                 DbColumnName = 'NorthingUTM',
                 ControlType = 'number',
                 [Rule] = NULL

UNION ALL SELECT FieldCategoryId = (SELECT IDENT_CURRENT('dbo.FieldCategories')), 
                 Name = 'DateTime',
                 Description = 'Date and Time the waypoint was taken',
                 Units = NULL,
                 [Validation] = NULL,
                 DataType = 'DateTime',
                 PossibleValues = NULL,
                 DbColumnName = 'DateTime',
                 ControlType = 'text',
                 [Rule] = NULL
update #NewFieldInfo set FieldRoleId = 2 where FieldRoleId is NULL   -- 2 == details



-- Assign new fields to the datasets -- this will insert a new row for each combination of datasetId and fieldId for the records inserted above
INSERT INTO dbo.DatasetFields(DatasetId, FieldId, FieldRoleId, CreateDateTime, Label, DbColumnName, Validation, SourceId, InstrumentId, OrderIndex, ControlType, [Rule])
SELECT
    DatasetId      = d.id,
    FieldId        = f.id,
    FieldRoleId    = f.FieldRoleId,
    CreateDateTime = GetDate(),
    Label          = f.fieldName,
    DbColumnName   = f.fieldName,
    [Validation]     = f.validation,
    SourceId       = 1,
    InstrumentId   = NULL,
    OrderIndex     = f.OrderIndex * 10,     -- x10 to make it easier to insert intermediate orders
    ControlType    = f.ControlType,
    [Rule]         = f.[Rule]
FROM #NewDatasetIds as d, #NewFieldInfo as f


-- Add some new DatasetQAStatus records for our new datasets
CREATE TABLE #QaStatusIds (id int)
INSERT INTO #QaStatusIds (id) 
          SELECT id = 5     -- Approved
UNION ALL SELECT id = 6     -- Ready for QA

INSERT INTO dbo.DatasetQAStatus(Dataset_Id, QAStatus_id)
SELECT
    Dataset_Id  = d.id,
    QAStatus_id = q.id
FROM #NewDatasetIds as d, #QaStatusIds as q



-- Cleanup
drop table #ProjectInfo
drop table #NewFieldInfo
drop table #NewDatasetIds
drop table #QaStatusIds

");

        }

        public override void Down()
        {
            Sql(@"

-- Note: These MUST match values declared in Up()
declare @datasetBaseName as varchar(max) = 'Spawning Ground Survey'
declare @categoryName as varchar(max) = @datasetBaseName
declare @datastoreName as varchar(max) = @datasetBaseName

delete from dbo.DatasetQAStatus where Dataset_Id in (select id from dbo.Datasets where name = @datasetBaseName)
delete from dbo.DatasetFields where DatasetId in (select id from dbo.Datasets where name = @datasetBaseName)
delete from dbo.Fields where FieldCategoryId in (select id from dbo.FieldCategories where name = @categoryName)
delete from dbo.Datasets where name = @datasetBaseName
delete from dbo.FieldCategories where name = @categoryName
delete from dbo.Datastores where name = @datastoreName

");
        }
    }
}
