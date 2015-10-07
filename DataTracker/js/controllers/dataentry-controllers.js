//Data Entry Controller
'use strict';

var mod_de = angular.module('DataEntryControllers', ['ui.bootstrap']);

mod_de.controller('ModalQuickAddAccuracyCheckCtrl', ['$scope','$modalInstance', 'DataService','DatastoreService',
  function($scope,  $modalInstance, DataService, DatastoreService){

    $scope.ac_row = {};

    $scope.save = function(){
      
      var promise = DatastoreService.saveInstrumentAccuracyCheck($scope.viewInstrument.Id, $scope.ac_row);
      promise.$promise.then(function(){
          $scope.reloadProject();  
          $modalInstance.dismiss();  
      });
    };

    $scope.cancel = function(){
      $modalInstance.dismiss();
    };

  }
]);


mod_de.controller('ModalQuickAddCharacteristicCtrl', ['$scope','$modalInstance', 'DataService','DatastoreService',
  function($scope,  $modalInstance, DataService, DatastoreService){

    $scope.char_row = {};

    $scope.save = function(){
      
      var promise = DatastoreService.saveCharacteristic($scope.viewLabCharacteristic.Id, $scope.char_row);
      promise.$promise.then(function(){
          $scope.reloadProject();  
          $modalInstance.dismiss();  
      });
    };

    $scope.cancel = function(){
      $modalInstance.dismiss();
    };

  }
]);


//datasheet version of the data entrypage
mod_de.controller('DataEntryDatasheetCtrl', ['$scope','$routeParams','DataService','$modal','$location','$rootScope','ActivityParser','DataSheet','$route','DatastoreService',
	function($scope, $routeParams, DataService, $modal, $location, $rootScope, ActivityParser, DataSheet, $route, DatastoreService){

		initEdit(); // stop backspace from ditching in the wrong place.

		$scope.userId = $rootScope.Profile.Id;
		$scope.fields = { header: [], detail: [], relation: {} };
		$scope.colDefs = [];
        
        //setup the data array that will be bound to the grid and filled with the json data objects
        $scope.dataSheetDataset = [];
		
		$scope.datasetLocations = [[]];	
		$scope.datasetLocationType=0;		
		$scope.primaryProjectLocation = 0;
		$scope.primaryDatasetLocation = 0;		
		$scope.sortedLocations = [];
        
		//datasheet grid definition
		$scope.gridDatasheetOptions = {
			data: 'dataSheetDataset',
			enableCellSelection: true,
	        enableRowSelection: false,
	        enableCellEdit: true,
	        columnDefs: 'datasheetColDefs',
	        enableColumnResize: true,
	        
		};

        //config the fields for the datasheet - include mandatory location and activityDate fields
		//$scope.datasheetColDefs = DataSheet.getColDefs();
		DataSheet.initScope($scope);

		//fire up our dataset
        $scope.dataset = DataService.getDataset($routeParams.Id);

		//update our location options as soon as our project is loaded.
        $scope.$watch('project.Name', function(){
        	if(!$scope.project) return;

        	//console.dir($scope.project);
			//$scope.locationOptions = $rootScope.locationOptions = makeObjects(getUnMatchingByField($scope.project.Locations,PRIMARY_PROJECT_LOCATION_TYPEID,"LocationTypeId"), 'Id','Label') ; // Original line

			$scope.datasetLocationType = DatastoreService.getDatasetLocationType($scope.DatastoreTablePrefix);
			console.log("LocationType = " + $scope.datasetLocationType);			

			console.log("ProjectLocations is next...");
			console.dir($scope.project.Locations);
			//var locInd = 0;
			for (var i = 0; i < $scope.project.Locations.length; i++ )
			{
				//console.log("projectLocations Index = " + $scope.project.Locations[i].Label);
				//console.log($scope.project.Locations[i].Id + "  " + $scope.project.Locations[i]);
				if ($scope.project.Locations[i].LocationTypeId === $scope.datasetLocationType)
				{
					//console.log("Found one");
					var strLabel = $scope.project.Locations[i].Label.toLowerCase();
					console.log("strLabel = " + strLabel);
					var intLabelLength = strLabel.length;
					var strHomeText = "home base";
					console.log("strHomeText = " + strHomeText);
					var intHomeTextLength = strHomeText.length;

					// If we do indexOf on a string that is smaller than the search string, the results may be unpredictable,
					// so let's verify the search string is bigger than the label string first.
					if ((intLabelLength > intHomeTextLength) && (strLabel.indexOf(strHomeText) > -1))
					{
						console.log("Found Primary Dataset Location...");
						$scope.primaryDatasetLocation = $scope.project.Locations[i].Id;
						
						// The form version uses this.
						//$scope.row['locationId'] = $scope.primaryDatasetLocation;
						//console.log("$scope.row is next...");
						//console.dir($scope.row);

						// The datasheet version uses this.
						$scope.dataSheetDataset[0].locationId = $scope.primaryDatasetLocation; // The datasheet version uses this.	
						console.log("$scope.dataSheetDataset is next...");
						console.dir($scope.dataSheetDataset);
					}
					
					$scope.datasetLocations.push([$scope.project.Locations[i].Id, $scope.project.Locations[i].Label]);
					//console.log("datasetLocations length = " + $scope.datasetLocations.length);
					//locInd++;
				}
				else if ($scope.project.Locations[i].LocationTypeId === 3)
				{
					//$scope.datasetLocations.push([$scope.project.Locations[i].Id, $scope.project.Locations[i].Label]);  // The label is NULL, so do not add it.
					$scope.primaryProjectLocation = $scope.project.Locations[i].Id;
					console.log("Found a primary location.  LocId = " + $scope.primaryProjectLocation);
				}					
			}
			console.log("datasetLocations is next...");
			console.dir($scope.datasetLocations);
			
			// When we built the array, it started adding at location 1 for some reason, skipping 0.
			// Therefore, row 0 is blank.  The simple solution is to just delete row 0.
			$scope.datasetLocations.shift();

			$scope.datasetLocations.sort(order2dArrayByAlpha);
			console.log("datasetLocations sorted...");
			console.dir($scope.datasetLocations);

			// Convert our 2D array into an array of objects.
			for (var i = 0; i < $scope.datasetLocations.length; i++)
			{
				$scope.sortedLocations.push({Id: $scope.datasetLocations[i][0], Label: $scope.datasetLocations[i][1]});
			}
			$scope.datasetLocations = [[]]; // Clean up
			
			
			// Convert our array of objects into a list of objects, and put it in the select box.
			$scope.locationOptions = $rootScope.locationOptions = makeObjects($scope.sortedLocations, 'Id','Label') ;

			console.log("locationOptions is next...");
			console.dir($scope.locationOptions);
			
			console.log("$scope.project.Instruments is next...");
			console.dir($scope.project.Instruments);
        	if($scope.project.Instruments.length > 0)
        	{
        		$scope.instrumentOptions = $rootScope.instrumentOptions = makeInstrumentObjects($scope.project.Instruments);
        		//getByField($scope.datasheetColDefs, 'Instrument','Label').visible=true;
			}

			//check authorization -- need to have project loaded before we can check project-level auth
			if(!$rootScope.Profile.isProjectOwner($scope.project) && !$rootScope.Profile.isProjectEditor($scope.project))
			{
				$location.path("/unauthorized");
			}

        });

         //setup a listener to populate column headers on the grid
		$scope.$watch('dataset.Fields', function() { 
			if(!$scope.dataset.Fields ) return;
			
			$scope.DatastoreTablePrefix = $scope.dataset.Datastore.TablePrefix;
			console.log("$scope.DatastoreTablePrefix = " + $scope.DatastoreTablePrefix);
			$scope.datasheetColDefs = DataSheet.getColDefs($scope.DatastoreTablePrefix);  // Pass the TablePrefix (name of the dataset), because it will never change.			
			
			//load our project based on the projectid we get back from the dataset
        	$scope.project = DataService.getProject($scope.dataset.ProjectId);
			
        	$scope.QAStatusOptions = $rootScope.QAStatusOptions = makeObjects($scope.dataset.QAStatuses, 'Id','Name');
 

			//iterate the fields of our dataset and populate our grid columns
			angular.forEach($scope.dataset.Fields.sort(orderByIndex), function(field){
								
				parseField(field, $scope);
				
				if(field.FieldRoleId == FIELD_ROLE_HEADER)
				{
					$scope.fields.header.push(field);
					$scope.datasheetColDefs.push(makeFieldColDef(field, $scope));
				}
				else if(field.FieldRoleId == FIELD_ROLE_DETAIL)
				{
					$scope.fields.detail.push(field);
    				$scope.datasheetColDefs.push(makeFieldColDef(field, $scope));
				}				
    		});

			//now everything is populated and we can do any post-processing.
			if($scope.datasheetColDefs.length > 2)
			{
				$scope.addNewRow();
			}

			if($scope.dataset.Config)
			{
				var filteredColDefs = [];

				angular.forEach($scope.datasheetColDefs, function(coldef){
					if($scope.dataset.Config.DataEntryPage &&
						!$scope.dataset.Config.DataEntryPage.HiddenFields.contains(coldef.field))
					{
						filteredColDefs.push(coldef);
					}
				});

				$scope.datasheetColDefs = $scope.colDefs = filteredColDefs;
			}

			$scope.recalculateGridWidth($scope.datasheetColDefs.length);
            $scope.validateGrid($scope);

    	});

		$scope.doneButton = function()
		{
		 	$scope.activities = undefined;
		 	$scope.dataset = undefined;
		 	$route.reload();
		 	//DataSheet.initScope($scope); //needed?
		}

		$scope.viewButton = function()
		{
			$location.path("/"+$scope.dataset.activitiesRoute+"/"+$scope.dataset.Id);
		}

		 $scope.cancel = function(){
		 	if($scope.dataChanged)
		 	{	
			 	if(!confirm("Looks like you've made changes.  Are you sure you want to leave this page?"))
			 		return;
			}

		 	$location.path("/"+$scope.dataset.activitiesRoute+"/"+$scope.dataset.Id);
		 };

		//adds row to datasheet grid
		$scope.addNewRow = function()
		{
			var row = makeNewRow($scope.datasheetColDefs);
			row.QAStatusId = $scope.dataset.DefaultActivityQAStatusId;
			row.RowQAStatusId = $scope.dataset.DefaultRowQAStatusId;
			$scope.dataSheetDataset.push(row);
			$scope.onRow = row;

		};

		$scope.saveData = function() {

			var sheetCopy = angular.copy($scope.dataSheetDataset);

            $scope.activities = ActivityParser.parseActivitySheet(sheetCopy, $scope.fields);
            
            if(!$scope.activities.errors)
            {
                var promise = DataService.saveActivities($scope.userId, $scope.dataset.Id, $scope.activities);
                promise.$promise.then(function(){
                	$scope.new_activity = $scope.activities.new_records;
                });
            }

		};

		
	}
]);


//Fieldsheet / form version of the dataentry page
mod_de.controller('DataEntryFormCtrl', ['$scope','$routeParams','DataService','$modal','$location','$rootScope','ActivityParser','DataSheet','$route','FileUploadService','DatastoreService',
	function($scope, $routeParams, DataService, $modal, $location, $rootScope, ActivityParser, DataSheet, $route, UploadService,DatastoreService){

		initEdit(); // stop backspace from ditching in the wrong place.

		$scope.userId = $rootScope.Profile.Id;
		$scope.fields = { header: [], detail: [], relation: []}; 
		$scope.datasheetColDefs = [];
        
		$scope.filesToUpload = {};

        $scope.dataSheetDataset = [];
        // $scope.row = {ActivityQAStatus: {}, activityDate: new Date()}; //header field values get attached here by dbcolumnname

		$scope.datasetLocations = [[]];	
		$scope.datasetLocationType=0;		
		$scope.primaryProjectLocation = 0;
		$scope.primaryDatasetLocation = 0;
		$scope.sortedLocations = [];		
        
		//datasheet grid
		$scope.gridDatasheetOptions = {
			data: 'dataSheetDataset',
			enableCellSelection: true,
	        enableRowSelection: false,
	        enableCellEdit: true,
	        columnDefs: 'datasheetColDefs',
	        enableColumnResize: true,
	        
		};

        //config the fields for the datasheet - include mandatory location and activityDate fields
		//$scope.datasheetColDefs = DataSheet.getColDefs();
		DataSheet.initScope($scope);

		//fire up our dataset
        $scope.dataset = DataService.getDataset($routeParams.Id);

        //update our location options as soon as our project is loaded.
        $scope.$watch('project.Name', function(){
        	if(!$scope.project) return;
        	//console.dir($scope.project);

			//$scope.locationOptions = $rootScope.locationOptions = makeObjects(getUnMatchingByField($scope.project.Locations,PRIMARY_PROJECT_LOCATION_TYPEID,"LocationTypeId"), 'Id','Label') ; // Original code		
			
			$scope.datasetLocationType = DatastoreService.getDatasetLocationType($scope.DatastoreTablePrefix);			
			console.log("LocationType = " + $scope.datasetLocationType);			
			
			console.log("ProjectLocations is next...");
			if ($scope.project.Locations)
				console.dir($scope.project.Locations);
			
			//var locInd = 0;
			for (var i = 0; i < $scope.project.Locations.length; i++ )
			{
				//console.log("projectLocations Index = " + $scope.project.Locations[i].Label);
				//console.log($scope.project.Locations[i].Id + "  " + $scope.project.Locations[i]);
				if ($scope.project.Locations[i].LocationTypeId === $scope.datasetLocationType)
				{
					//console.log("Found one");
					var strLabel = $scope.project.Locations[i].Label.toLowerCase();
					console.log("strLabel = " + strLabel);
					var intLabelLength = strLabel.length;
					var strHomeText = "home base";
					console.log("strHomeText = " + strHomeText);
					var intHomeTextLength = strHomeText.length;

					// If we do indexOf on a string that is smaller than the search string, the results may be unpredictable,
					// so let's verify the search string is bigger than the label string first.
					if ((intLabelLength > intHomeTextLength) && (strLabel.indexOf(strHomeText) > -1))
					{
						console.log("Found Primary Dataset Location...");
						$scope.primaryDatasetLocation = $scope.project.Locations[i].Id;
						
						// The form version uses this.
						$scope.row['locationId'] = $scope.primaryDatasetLocation;
						console.log("$scope.row is next...");
						console.dir($scope.row);

						// The datasheet version uses this.
						//$scope.dataSheetDataset[0].locationId = $scope.primaryDatasetLocation; // The datasheet version uses this.	
						//console.log("$scope.dataSheetDataset is next...");
						//console.dir($scope.dataSheetDataset);
					}
					
					$scope.datasetLocations.push([$scope.project.Locations[i].Id, $scope.project.Locations[i].Label]);
					//console.log("datasetLocations length = " + $scope.datasetLocations.length);
					//locInd++;
				}
				else if ($scope.project.Locations[i].LocationTypeId === 3)
				{
					//$scope.datasetLocations.push([$scope.project.Locations[i].Id, $scope.project.Locations[i].Label]);  // The label is NULL, so do not add it.
					$scope.primaryProjectLocation = $scope.project.Locations[i].Id;
					console.log("Found a primary location.  LocId = " + $scope.primaryProjectLocation);
				}					
			}
			console.log("datasetLocations is next...");
			console.dir($scope.datasetLocations);
			
			// When we built the array, it started adding at location 1 for some reason, skipping 0.
			// Therefore, row 0 is blank.  The simple solution is to just delete row 0.
			$scope.datasetLocations.shift();

			$scope.datasetLocations.sort(order2dArrayByAlpha);
			console.log("datasetLocations sorted...");
			console.dir($scope.datasetLocations);

			// Convert our 2D array into an array of objects.
			for (var i = 0; i < $scope.datasetLocations.length; i++)
			{
				$scope.sortedLocations.push({Id: $scope.datasetLocations[i][0], Label: $scope.datasetLocations[i][1]});
			}
			$scope.datasetLocations = [[]]; // Clean up
			
			
			// Convert our array of objects into a list of objects, and put it in the select box.
			$scope.locationOptions = $rootScope.locationOptions = makeObjects($scope.sortedLocations, 'Id','Label') ;

			console.log("locationOptions is next...");
			console.dir($scope.locationOptions);
			
			//if there is only one location, just set it to that location
			if(array_count($scope.locationOptions)==1)
			{
				//there will only be one.
				angular.forEach(Object.keys($scope.locationOptions), function(key){
					console.log(key);
					$scope.row['locationId'] = key;	
				});
				
			}
			
			//check authorization -- need to have project loaded before we can check project-level auth
			if(!$rootScope.Profile.isProjectOwner($scope.project) && !$rootScope.Profile.isProjectEditor($scope.project))
			{
				$location.path("/unauthorized");
			}

			//if ?LocationId=123 is passed in then lets set it to the given LocationId
			if($routeParams.LocationId)
			{
				$scope.row['locationId'] = ""+$routeParams.LocationId;
			}

        });	

         //setup a listener to populate column headers on the grid
		$scope.$watch('dataset.Fields', function() { 
			if(!$scope.dataset.Fields ) return;

			$scope.DatastoreTablePrefix = $scope.dataset.Datastore.TablePrefix;
			console.log("$scope.DatastoreTablePrefix = " + $scope.DatastoreTablePrefix);
			$scope.datasheetColDefs = DataSheet.getColDefs($scope.DatastoreTablePrefix, "form");  // Pass the TablePrefix (name of the dataset), because it will never change.

			//load our project based on the projectid we get back from the dataset
        	$scope.project = DataService.getProject($scope.dataset.ProjectId);
        	if ($scope.DatastoreTablePrefix === "CreelSurvey" || $scope.DatastoreTablePrefix === "SpawningGroundSurvey")
				$scope.row = {ActivityQAStatus: {}}; //header field values get attached here by dbcolumnname; leave activityDate blank for CreelSurvey.								
			else
				$scope.row = {ActivityQAStatus: {}, activityDate: new Date()}; //header field values get attached here by dbcolumnname				

			
        	$scope.QAStatusOptions = $rootScope.QAStatusOptions = makeObjects($scope.dataset.QAStatuses, 'Id','Name');

        	//iterate the fields of our dataset and populate our grid columns
			angular.forEach($scope.dataset.Fields.sort(orderByIndex), function(field){
				
				parseField(field, $scope);

				if(field.FieldRoleId == FIELD_ROLE_HEADER)
				{
					$scope.fields.header.push(field);
				}
				else if (field.FieldRoleId == FIELD_ROLE_DETAIL)
				{
					//console.log("Adding to details:  " + field.DbColumnName + ", " + field.Label);
					$scope.fields.detail.push(field);
    				$scope.datasheetColDefs.push(makeFieldColDef(field, $scope));

    				//a convention:  if your dataset has a ReadingDateTime field then we enable timezones for an activity.
    				if(field.DbColumnName == "ReadingDateTime")
    				{
    					$scope.row.Timezone = getByField($scope.SystemTimezones, new Date().getTimezoneOffset() * -60000, "TimezoneOffset"); //set default timezone
    				}
				}
    		});

			//now everything is populated and we can do any post-processing.
			if($scope.datasheetColDefs.length > 2)
			{
				$scope.addNewRow();
			}

			//set defaults for header fields
			angular.forEach($scope.fields.header, function(field){
				$scope.row[field.DbColumnName] = (field.DefaultValue) ? field.DefaultValue : null;

				//FEATURE: any incoming parameter value that matches a header will get copied into that header value.
				if($routeParams[field.DbColumnName])
				{
					$scope.row[field.DbColumnName] = $routeParams[field.DbColumnName];
				}

			});

			$scope.row.ActivityQAStatus.QAStatusId = ""+$scope.dataset.DefaultActivityQAStatusId;

			$scope.recalculateGridWidth($scope.fields.detail.length);

			$scope.validateGrid($scope);

			console.log("$scope at end of dataset.Fields watcher...");
			console.dir($scope);
    	});

		$scope.reloadProject = function(){
                //reload project instruments -- this will reload the instruments, too
                DataService.clearProject();
                $scope.project = DataService.getProject($scope.dataset.ProjectId);
                var watcher = $scope.$watch('project.Id', function(){
                	$scope.selectInstrument();	
                	watcher();
                });
                
         };


		$scope.openAccuracyCheckModal = function(){

            var modalInstance = $modal.open({
              templateUrl: 'partials/instruments/modal-new-accuracycheck.html',
              controller: 'ModalQuickAddAccuracyCheckCtrl',
              scope: $scope, //very important to pass the scope along... 
        
            });
		};

        $scope.createInstrument = function(){
            $scope.viewInstrument = null;
            var modalInstance = $modal.open({
              templateUrl: 'partials/instruments/modal-create-instrument.html',
              controller: 'ModalCreateInstrumentCtrl',
              scope: $scope, //very important to pass the scope along...
            });
         };


		$scope.getDataGrade = function(check){ return getDataGrade(check)}; //alias from service

		$scope.selectInstrument = function(){
			if(!$scope.row.InstrumentId)
				return;

			//get latest accuracy check
			$scope.viewInstrument = getByField($scope.project.Instruments, $scope.row.InstrumentId, "Id");
			$scope.row.LastAccuracyCheck = $scope.viewInstrument.AccuracyChecks[$scope.viewInstrument.AccuracyChecks.length-1];
			$scope.row.DataGradeText = getDataGrade($scope.row.LastAccuracyCheck) ;

			if($scope.row.LastAccuracyCheck)
				$scope.row.AccuracyCheckId = $scope.row.LastAccuracyCheck.Id;
		};

		$scope.selectLaboratory = function(){
			if(!$scope.row.LaboratoryId)
				return;

			//get latest accuracy check
			$scope.viewLaboratory = getByField($scope.project.Laboratories, $scope.row.LaboratoryId, "Id");
			$scope.row.LastCharacteristic = $scope.viewLaboratory.Characteristics[$scope.viewLaboratory.Characteristics.length-1];
			$scope.row.DataGradeText = getDataGrade($scope.row.LastCharacteristic) ;

			if($scope.row.LastCharacteristic)
				$scope.row.LastCharacteristicId = $scope.row.LastCharacteristic.Id;
		};

		$scope.cancel = function(){
		 	if($scope.dataChanged)
		 	{	
			 	if(!confirm("Looks like you've made changes.  Are you sure you want to leave this page?"))
			 		return;
			}

		 	$location.path("/"+$scope.dataset.activitiesRoute+"/"+$scope.dataset.Id);
		 };
		

		//adds row to datasheet grid
		$scope.addNewRow = function()
		{
			var row = makeNewRow($scope.datasheetColDefs);
			row.QAStatusId = $scope.dataset.DefaultRowQAStatusId;
			$scope.dataSheetDataset.push(row);
			$scope.onRow = row;
		};

   	    //overriding the one in our service because we don't want to allow removing of a blank row.
        $scope.removeRow = function(){
        	if($scope.dataSheetDataset.length > 1)
        		DataSheet.removeOnRow($scope);
        };


		$scope.doneButton = function()
		{
		 	$scope.activities = undefined;
		 	$route.reload();
			//DataSheet.initScope($scope);		
		}

		$scope.viewButton = function()
		{
			$location.path("/"+$scope.dataset.activitiesRoute+"/"+$scope.dataset.Id);
		}

		$scope.viewRelation = function(row, field_name)
        {
        	console.dir(row.entity);
        	var field = $scope.FieldLookup[field_name];
        	console.dir(field);

        	$scope.openRelationEditGridModal(row.entity, field);
        }


		$scope.openRelationEditGridModal = function(row, field)
		{
			$scope.relationgrid_row = row;
			$scope.relationgrid_field = field;
			$scope.isEditable = true;
			var modalInstance = $modal.open({
				templateUrl: 'partials/modals/relationgrid-edit-modal.html',
				controller: 'RelationGridModalCtrl',
				scope: $scope, 
			});
		};

		/* -- these functions are for uploading - */
		$scope.openFileModal = function(row, field)
        {
            //console.dir(row);
            //console.dir(field);
            $scope.file_row = row;
            $scope.file_field = field;
            
            var modalInstance = $modal.open({
                templateUrl: 'partials/modals/file-modal.html',
                controller: 'FileModalCtrl',
                scope: $scope, //scope to make a child of
            });
        };

		$scope.openWaypointFileModal = function(row, field)
        {
            $scope.file_row = row;
            $scope.file_field = field;
            
            var modalInstance = $modal.open({
                templateUrl: 'partials/modals/waypoint-file-modal.html',
                controller: 'FileModalCtrl',
                scope: $scope, //scope to make a child of
            });
        };

        //field = DbColumnName
        $scope.onFileSelect = function(field, files)
        {
            //console.log("file selected! " + field)
            $scope.filesToUpload[field] = files;
        };

        //this function gets called when a user clicks the "Add" button in a GRID file cell
        $scope.addFiles = function(row, field_name)
        {
            var field = $scope.FieldLookup[field_name];

            //console.dir(row);
            //console.dir(field);
            $scope.openFileModal(row.entity, field);

            //go ahead and mark this row as being updated.
            if($scope.updatedRows)
                $scope.updatedRows.push(row.entity.Id);

        }
      
		$scope.saveData = function(){
			console.log("save!");

			var promise = UploadService.uploadFiles($scope.filesToUpload, $scope);
			promise.then(function(data){

				//spin through the files that we uploaded
				angular.forEach($scope.filesToUpload, function(files, field){

					if(field == "null" || field == "")
						return;
					
					var local_files = [];

					for(var i = 0; i < files.length; i++)
			      	{
			          var file = files[i];
			          
			          if(file.data && file.data.length == 1) //since we only upload one at a time...
			          {
			          		//console.dir(file.data);
			          		local_files.push(file.data[0]); //only ever going to be one if there is any...
			          		//console.log("file id = "+file.data[0].Id);
			          }
			          else
			          {
			          	//console.log("no file id.");
			          	$scope.errors.heading.push("There was a problem saving file: " + file.Name + " - Try a unique filename.");
			          	throw "Problem saving file: " + file.Name;
			          }
			      	}

			      	//if we already had actual files in this field, copy them in
			      	if($scope.file_row[field])
			      	{
			      		var current_files = angular.fromJson($scope.file_row[field]);
			      		angular.forEach(current_files, function(file){
			      			if(file.Id) //our incoming files don't have an id, just actual files.
			      				local_files.push(file);		
			      		});
			      	}

					$scope.file_row[field] = angular.toJson(local_files);
					//console.log("Ok our new list of files: "+$scope.row[field]);
				});

				var sheetCopy = angular.copy($scope.dataSheetDataset);		

				$scope.activities = ActivityParser.parseSingleActivity($scope.row, sheetCopy, $scope.fields);
				if(!$scope.activities.errors)
				{
					DataService.saveActivities($scope.userId, $scope.dataset.Id, $scope.activities);
				}
			});
		};

		//this function gets called when a user clicks the "Add" button in a GRID file cell
		$scope.addFiles = function(row, field_name)
		{
			var field = $scope.FieldLookup[field_name];

			//console.dir(row);
			//console.dir(field);
			$scope.openFileModal(row.entity, field);

		}		
	}
]);




//not being used.	
mod_de.controller('ModalDataEntryCtrl', ['$scope', '$modalInstance', 
	function($scope, $modalInstance){
		//DRY alert -- this was copy and pasted... how can we fixy?
		$scope.alerts = [];

		$scope.ok = function(){
			try{
				$scope.addGridRow($scope.row);
				$scope.row = {};
				$scope.alerts.push({type: 'success',msg: 'Added.'});
			}catch(e){
				console.dir(e);
			}
		};

		$scope.cancel = function() {
			$modalInstance.dismiss('cancel');
		};

		$scope.closeAlert = function(index) {
		    $scope.alerts.splice(index, 1);
		};

		$scope.row = {}; //modal fields are bound here

		$scope.dateOptions = {
		    'year-format': "'yy'",
		    'starting-day': 1
		};


	}
	]);


