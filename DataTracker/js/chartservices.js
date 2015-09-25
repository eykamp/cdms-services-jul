
//Chart Services - charting for different datasets.
//  If you want to build a custom chart for your dataset (we like it as a nice simple visual),
//  you'll just need to create your own chart service named "YourDataset_ChartService" for each one.
//  and use the below as example/ideas

var cmod = angular.module('ChartServices', []);


/*
		Each dataset type that you want to provide a generated graph needs to live here.
		You can do your graphing however you like for the particular kind of chart.d
*/
cmod.service('ChartService', ['AdultWeir_ChartService','WaterTemp_ChartService','SnorkelFish_ChartService',
	function(AdultWeir_ChartService, WaterTemp_ChartService, SnorkelFish_ChartService){
		var service = {
			buildChart: function(scope, data_in, dataset, config){

    				if(dataset == "AdultWeir") 
					{
						scope.chartConfig = AdultWeir_ChartService.getChartConfig();
		    			scope.chartData = AdultWeir_ChartService.getChartData(data_in);
					}
		    		else if(dataset == "WaterTemp")
		    		{
		    			WaterTemp_ChartService.buildChart(data_in, config);
		    		}
		    		else if(dataset == "SnorkelFish")
		    		{
		    			// SnorkelFish_ChartService.buildChart(data_in, config);
		    			scope.chartConfig = SnorkelFish_ChartService.getChartConfig();
		    			scope.chartData   = SnorkelFish_ChartService.getChartData(data_in);
		    		}		    		//else
		    		//	delete $scope.chartData; 
		    },
		};

		return service;
	}
]);

cmod.service('AdultWeir_ChartService',[ 
    function(){
        var service = {

        	dataset: "AdultWeir",

			getChartConfig: function(){
				var config = {
    			  title : 'Fish by Species',
				  tooltips: true,
				  labels : false,
				  
				  legend: {
				    display: true,
				    position: 'right'
				  }
				};

				return config;
			},


			getDefaultChartData: function()
			{
				var defaultChartData = {"series": [], "data":[{ "x": "Loading...", "y": [0],"tooltip": ""}]}; //default
				return defaultChartData;
			},


			getChartData: function(data)
			{
			    var dataCalc = {};

			    angular.forEach(data, function(row, key){
			        var num = (row.TotalFishRepresented) ? row.TotalFishRepresented : 1;
			        //console.log(row);

			        if(row.Species)
			        {

			            if(!dataCalc[row.Species])
			                dataCalc[row.Species] = { total: 0, males: 0, females: 0};

			            dataCalc[row.Species].total += num;

			            if(row.Sex == "M")
			                dataCalc[row.Species].males += num;
			            if(row.Sex == "F")
			                dataCalc[row.Species].females += num;
			            
			        }
			        
			        //console.log(row.Species + " = ");
			        //console.dir(dataCalc[row.Species]);
			        
			    });

			    var data = {
			              "series": [
			                "Total",
			                "Male",
			                "Female"
			              ],
			              "data": [
			              ]
			            };

			    angular.forEach(dataCalc, function(vals, species){
			        data['data'].push({
			          "x": species,
			          "y": [vals.total,vals.males,vals.females],
			        });
			    });

			//    console.log(data);

			    return data;

			},

			buildChart: function(){

			},

        };

        return service;
    }
]);

cmod.service('WaterTemp_ChartService',[ 
    function(){
        var service = {

        	dataset: "WaterTemp",

			buildChart: function(data_in, config)
			{
				if(data_in.length == 0)
					return;

				if(!config)
					config = {width: 400, height: 200};

					var margin = {top: 10, right: 10, bottom: 20, left: 30},
					    width = config.width - margin.left - margin.right,
					    height = config.height - margin.top - margin.bottom;

					var x = d3.time.scale()
					    .range([0, width]);

					var y = d3.scale.linear()
					    .range([height, 0]);

					var color = d3.scale.ordinal()
					  		.domain([1,12,13,14,15,16])
					  		.range(["FF0000","#009933" , "#0000FF","#0FF933" , "#00FFFF","#00FFAAFB"]);

					var xAxis = d3.svg.axis()
					    .scale(x)
					    .orient("bottom");

					var yAxis = d3.svg.axis()
					    .scale(y)
					    .orient("left");

					var line = d3.svg.line()
						//.interpolate("basis")
					    .x(function(d) { return x(d.chart_date); })
					    .y(function(d) { return y(d.chart_temp); });

					    d3.select("#chart-div").selectAll("svg").remove();

					var svg = d3.select("#chart-div").append("svg")
					    .attr("width", width + margin.left + margin.right)
					    .attr("height", height + margin.top + margin.bottom)
					  .append("g")
					    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

	
					//converting via new Date() seems to work better.
					//var parseDate = d3.time.format("%Y-%m-%dT%H:%M:%S").parse;

					//color.domain(d3.keys(data[0]).filter(function(key) { return key !== "date"; }));

					var data = [];

					data_in.forEach(function(d) {
					  	//only show rows with default QA status (OK)
					  	//if(d.QAStatusId == scope.dataset.DefaultRowQAStatusId)
					  	//{
					  	if(!isNaN(d.WaterTemperature))
					  	{
					  		d.chart_date = new Date(d.ReadingDateTime);
					    	d.chart_temp = +d.WaterTemperature;
					    	d.chart_QAStatusId = d.QAStatusId;
					    	data.push(d);
					    }
					    //}

					    //console.dir(d);
					  });

					//console.dir(data);

					  //x.domain(d3.extent(data, function(d) { return d.date; }));
					  //y.domain(d3.extent(data, function(d) { return d.close; }));


					  x.domain(d3.extent(data, function(d) { return d.chart_date; }));
					  y.domain(d3.extent(data, function(d) { return d.chart_temp; }));

					  svg.append("g")
					      .attr("class", "x axis")
					      .attr("transform", "translate(0," + height + ")")
					      .call(xAxis);

					  svg.append("g")
					      .attr("class", "y axis")
					      .call(yAxis)
					    .append("text")
					      .attr("transform", "rotate(-90)")
					      .attr("y", 6)
					      .attr("dy", ".71em")
					      .style("text-anchor", "end")
					      .text("H2O Temp (C)");

					  svg.append("path")
					      .datum(data)
					      .attr("class", "line")
					      .attr("d", line);
					      /*
					      .style("stroke", function(d,i) { 
					      	console.dir(d);
					      	console.dir(i);
					      	console.dir(color(d.chart_QAStatusId));
					      	return color(d.chart_QAStatusId); 
					      });*/
			},
        };

        return service;
    }
]);


cmod.service('SnorkelFish_ChartService',[ 
    function(){
		console.log("Inside SnorkelFish_ChartService...");
        var service = {

        	dataset: "SnorkelFish",

			getChartConfig: function(){
				var config = {
    			  title : 'Fish Count',
				  tooltips: true,
				  labels : false,
				  
				  legend: {
				    display: true,
				    position: 'right'
				  }
				};

				return config;
			},


			getDefaultChartData: function()
			{
				var defaultChartData = {"series": [], "data":[{ "x": "Loading...", "y": [0],"tooltip": ""}]}; //default
				return defaultChartData;
			},


			getChartData: function(data)
			{
			    var dataCalc = {};

			    angular.forEach(data, function(row, key){
			        //var num = (row.TotalFishRepresented) ? row.TotalFishRepresented : 1; // From AdultWeir
			        var num = row.FishCount;
			        console.log("row is next...");
					console.dir(row);
					console.log("num = " + num);

			        if(row.Species)
			        {
						console.log("Inside row.Species if...");
			            if(!dataCalc[row.Species])
			                dataCalc[row.Species] = { total: 0 };

			            dataCalc[row.Species].total += num;
			        }
			    });

			    var data = {
			              "series": ["Total"],
			              "data": [] 
			          };

			    angular.forEach(dataCalc, function(vals, species){
			        data['data'].push({
			          "x": species,
			          "y": [vals.total],
			        });
			    });

				console.log("data is next...");
			    console.dir(data);

			    return data;

			},

			buildChart: function(){

			},

        };

        return service;
    }
]);



// cmod.service('SnorkelFish_ChartService',[ 
//     function(){
//         var service = {

//         	dataset: "SnorkelFish",

// 			getChartConfig: function(){
// 				var config = {
//     			  title : 'Fish Count',
// 				  tooltips: true,
// 				  labels : false,
				  
// 				  legend: {
// 				    display: true,
// 				    position: 'right'
// 				  }
// 				};

// 				return config;
// 			},


// 			getDefaultChartData: function()
// 			{
// 				var defaultChartData = {"series": [], "data":[{ "x": "Loading...", "y": [0],"tooltip": ""}]}; //default
// 				return defaultChartData;
// 			},


// 			getChartData: function(data)
// 			{
// 			    var dataCalc = {};

// 			    angular.forEach(data, function(row, key){
// 			        var fishCount = (row.FishCount) ? row.FishCount : 1;
// 			        //console.log(row);

// 			        if(row.Species)
// 			        {

// 			            if(!dataCalc[row.FishCount])
// 			                dataCalc[row.FishCount] = 0;

// 			            dataCalc[row.FishCount].total += fishCount;

// 			        }
// 			        //console.dir(dataCalc[row.Species]);
			        
// 			    });

// 			    var data = {
// 			              "series": [
// 			                "Fish Count"
// 			              ],
// 			              "data": [
// 			              ]
// 			            };

// 			    angular.forEach(dataCalc, function(vals, species){
// 			        data['data'].push({
// 			          "x": species,
// 			          "y": [vals.total],
// 			        });
// 			    });

// 			    return data;

// 			},

// 			buildChart: function(data_in, config){
// 				if(data_in.length == 0)
// 					return;

// 				if(!config)
// 					config = {width: 400, height: 200};

// 					var margin = {top: 10, right: 10, bottom: 20, left: 30},
// 					    width = config.width - margin.left - margin.right,
// 					    height = config.height - margin.top - margin.bottom;

// 					var x = d3.time.scale()
// 					    .range([0, width]);

// 					var y = d3.scale.linear()
// 					    .range([height, 0]);

// 					var color = d3.scale.ordinal()
// 					  		.domain([1,12,13,14,15,16])
// 					  		.range(["FF0000","#009933" , "#0000FF","#0FF933" , "#00FFFF","#00FFAAFB"]);

// 					var xAxis = d3.svg.axis()
// 					    .scale(x)
// 					    .orient("bottom");

// 					var yAxis = d3.svg.axis()
// 					    .scale(y)
// 					    .orient("left");

// 					var line = d3.svg.line()
// 						//.interpolate("basis")
// 					    .x(function(d) { return x(d.chart_date); })
// 					    .y(function(d) { return y(d.chart_count); });

// 					    d3.select("#chart-div").selectAll("svg").remove();

// 					var svg = d3.select("#chart-div").append("svg")
// 					    .attr("width", width + margin.left + margin.right)
// 					    .attr("height", height + margin.top + margin.bottom)
// 					  .append("g")
// 					    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

	
// 					//converting via new Date() seems to work better.
// 					//var parseDate = d3.time.format("%Y-%m-%dT%H:%M:%S").parse;

// 					//color.domain(d3.keys(data[0]).filter(function(key) { return key !== "date"; }));

// 					var data = [];
// 					data.Vals = {};
// 					data.MaxCount = 0;

// 					data_in.forEach(function(d) {
// 					  	if(!isNaN(d.FishCount) && d.Species != '')
// 					  	{
// 					  		if(!data.Vals[d.Species])
// 					  			data.Vals[d.Species] = 0;

// 					  		data.Vals[d.Species] += d.FishCount;

// 					  		if(data.Vals[d.Species] > data.MaxCount)
// 					  			data.MaxCount = data.Vals[d.Species];
// 					    }
// 					  });


// 					  x.domain(d3.extent(data, function(d) { return Object.keys(d.Vals); }));
// 					  y.domain(d3.extent(data, function(d) { return d.MaxCount; }));

// 					var chart = d3.select(".chart")
// 					    .attr("width", width)
// 					    .attr("height", height);

// 					d3.tsv("data.tsv", type, function(error, data) {
// 					  y.domain([0, d3.max(data, function(d) { return d.value; })]);

// 					  var barWidth = width / data.length;

// 					  var bar = chart.selectAll("g")
// 					      .data(data)
// 					    .enter().append("g")
// 					      .attr("transform", function(d, i) { return "translate(" + i * barWidth + ",0)"; });

// 					  bar.append("rect")
// 					      .attr("y", function(d) { return y(d.value); })
// 					      .attr("height", function(d) { return height - y(d.value); })
// 					      .attr("width", barWidth - 1);

// 					  bar.append("text")
// 					      .attr("x", barWidth / 2)
// 					      .attr("y", function(d) { return y(d.value) + 3; })
// 					      .attr("dy", ".75em")
// 					      .text(function(d) { return d.value; });

//         };

//         return service;
//     }
// ]);


