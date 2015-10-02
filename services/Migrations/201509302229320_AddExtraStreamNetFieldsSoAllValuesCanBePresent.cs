namespace services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraStreamNetFieldsSoAllValuesCanBePresent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age10Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age10PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age10PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusProp", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusPropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusPropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age2Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age2PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age2PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age3Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age3PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age3PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age4Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age4PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age4PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age5Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age5PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age5PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age6Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age6PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age6PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age7Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age7PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age7PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age8Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age8PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age8PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age9Prop", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age9PropLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Age9PropUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "AgePropAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "CBFWApopName", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Comments", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "CommonPopName", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "CompilerRecordID", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "DataEntry", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "DataEntryNotes", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "ESU_DPS", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "HOSJF", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "IndicatorLocation", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "LastUpdated", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "MajorPopGroup", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "MeasureLocation", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "MetaComments", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "MethodAdjustments", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "MetricLocation", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOBroodStockRemoved", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSJF", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSJFAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSJFLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "NOSJFUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "PopFitNotes", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "PopID", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "ProtMethDocumentation", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "ProtMethName", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "ProtMethURL", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "Publish", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "RecoveryDomain", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "RefID", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "SubmitAgency", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAEJ", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAEJAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAEJLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAEJUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAIJ", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAIJAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAIJLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "TSAIJUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "UpdDate", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSej", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSejAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSejLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSejUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSij", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSijAlpha", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSijLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_NOSA_Detail", "pHOSijUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "BroodStockRemoved", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "CBFWApopName", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "CommonPopName", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "CompilerRecordID", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "DataEntry", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "DataEntryNotes", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ESU_DPS", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "HarvestAdj", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "IndicatorLocation", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "LastUpdated", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MainstemHarvest", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MajorPopGroup", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MeasureLocation", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MetaComments", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MethodAdjustments", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "MetricLocation", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "OceanHarvest", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "PopID", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ProtMethDocumentation", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ProtMethName", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ProtMethURL", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "Publish", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "RecoveryDomain", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "RefID", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ReturnDef", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ReturnsMissing", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ReturnsMissingExplanation", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "SARAlpha", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "SARLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "SARUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "ScopeOfInference", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "SmoltLocPTcode", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "SubmitAgency", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TAR", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TARAlpha", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TARLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TARUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TSO", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TSOAlpha", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TSOLowerLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TSOUpperLimit", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "TribHarvest", c => c.String());
            AddColumn("dbo.StreamNet_SAR_Detail", "UpdDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StreamNet_SAR_Detail", "UpdDate");
            DropColumn("dbo.StreamNet_SAR_Detail", "TribHarvest");
            DropColumn("dbo.StreamNet_SAR_Detail", "TSOUpperLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "TSOLowerLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "TSOAlpha");
            DropColumn("dbo.StreamNet_SAR_Detail", "TSO");
            DropColumn("dbo.StreamNet_SAR_Detail", "TARUpperLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "TARLowerLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "TARAlpha");
            DropColumn("dbo.StreamNet_SAR_Detail", "TAR");
            DropColumn("dbo.StreamNet_SAR_Detail", "SubmitAgency");
            DropColumn("dbo.StreamNet_SAR_Detail", "SmoltLocPTcode");
            DropColumn("dbo.StreamNet_SAR_Detail", "ScopeOfInference");
            DropColumn("dbo.StreamNet_SAR_Detail", "SARUpperLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "SARLowerLimit");
            DropColumn("dbo.StreamNet_SAR_Detail", "SARAlpha");
            DropColumn("dbo.StreamNet_SAR_Detail", "ReturnsMissingExplanation");
            DropColumn("dbo.StreamNet_SAR_Detail", "ReturnsMissing");
            DropColumn("dbo.StreamNet_SAR_Detail", "ReturnDef");
            DropColumn("dbo.StreamNet_SAR_Detail", "RefID");
            DropColumn("dbo.StreamNet_SAR_Detail", "RecoveryDomain");
            DropColumn("dbo.StreamNet_SAR_Detail", "Publish");
            DropColumn("dbo.StreamNet_SAR_Detail", "ProtMethURL");
            DropColumn("dbo.StreamNet_SAR_Detail", "ProtMethName");
            DropColumn("dbo.StreamNet_SAR_Detail", "ProtMethDocumentation");
            DropColumn("dbo.StreamNet_SAR_Detail", "PopID");
            DropColumn("dbo.StreamNet_SAR_Detail", "OceanHarvest");
            DropColumn("dbo.StreamNet_SAR_Detail", "MetricLocation");
            DropColumn("dbo.StreamNet_SAR_Detail", "MethodAdjustments");
            DropColumn("dbo.StreamNet_SAR_Detail", "MetaComments");
            DropColumn("dbo.StreamNet_SAR_Detail", "MeasureLocation");
            DropColumn("dbo.StreamNet_SAR_Detail", "MajorPopGroup");
            DropColumn("dbo.StreamNet_SAR_Detail", "MainstemHarvest");
            DropColumn("dbo.StreamNet_SAR_Detail", "LastUpdated");
            DropColumn("dbo.StreamNet_SAR_Detail", "IndicatorLocation");
            DropColumn("dbo.StreamNet_SAR_Detail", "HarvestAdj");
            DropColumn("dbo.StreamNet_SAR_Detail", "ESU_DPS");
            DropColumn("dbo.StreamNet_SAR_Detail", "DataEntryNotes");
            DropColumn("dbo.StreamNet_SAR_Detail", "DataEntry");
            DropColumn("dbo.StreamNet_SAR_Detail", "CompilerRecordID");
            DropColumn("dbo.StreamNet_SAR_Detail", "CommonPopName");
            DropColumn("dbo.StreamNet_SAR_Detail", "CBFWApopName");
            DropColumn("dbo.StreamNet_SAR_Detail", "BroodStockRemoved");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSijUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSijLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSijAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSij");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSejUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSejLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSejAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "pHOSej");
            DropColumn("dbo.StreamNet_NOSA_Detail", "UpdDate");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAIJUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAIJLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAIJAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAIJ");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAEJUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAEJLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAEJAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "TSAEJ");
            DropColumn("dbo.StreamNet_NOSA_Detail", "SubmitAgency");
            DropColumn("dbo.StreamNet_NOSA_Detail", "RefID");
            DropColumn("dbo.StreamNet_NOSA_Detail", "RecoveryDomain");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Publish");
            DropColumn("dbo.StreamNet_NOSA_Detail", "ProtMethURL");
            DropColumn("dbo.StreamNet_NOSA_Detail", "ProtMethName");
            DropColumn("dbo.StreamNet_NOSA_Detail", "ProtMethDocumentation");
            DropColumn("dbo.StreamNet_NOSA_Detail", "PopID");
            DropColumn("dbo.StreamNet_NOSA_Detail", "PopFitNotes");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSJFUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSJFLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSJFAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSJF");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAIJAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOSAEJAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "NOBroodStockRemoved");
            DropColumn("dbo.StreamNet_NOSA_Detail", "MetricLocation");
            DropColumn("dbo.StreamNet_NOSA_Detail", "MethodAdjustments");
            DropColumn("dbo.StreamNet_NOSA_Detail", "MetaComments");
            DropColumn("dbo.StreamNet_NOSA_Detail", "MeasureLocation");
            DropColumn("dbo.StreamNet_NOSA_Detail", "MajorPopGroup");
            DropColumn("dbo.StreamNet_NOSA_Detail", "LastUpdated");
            DropColumn("dbo.StreamNet_NOSA_Detail", "IndicatorLocation");
            DropColumn("dbo.StreamNet_NOSA_Detail", "HOSJF");
            DropColumn("dbo.StreamNet_NOSA_Detail", "ESU_DPS");
            DropColumn("dbo.StreamNet_NOSA_Detail", "DataEntryNotes");
            DropColumn("dbo.StreamNet_NOSA_Detail", "DataEntry");
            DropColumn("dbo.StreamNet_NOSA_Detail", "CompilerRecordID");
            DropColumn("dbo.StreamNet_NOSA_Detail", "CommonPopName");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Comments");
            DropColumn("dbo.StreamNet_NOSA_Detail", "CBFWApopName");
            DropColumn("dbo.StreamNet_NOSA_Detail", "AgePropAlpha");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age9PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age9PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age9Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age8PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age8PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age8Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age7PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age7PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age7Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age6PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age6PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age6Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age5PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age5PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age5Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age4PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age4PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age4Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age3PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age3PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age3Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age2PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age2PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age2Prop");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusPropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusPropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age11PlusProp");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age10PropUpperLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age10PropLowerLimit");
            DropColumn("dbo.StreamNet_NOSA_Detail", "Age10Prop");
        }
    }
}
