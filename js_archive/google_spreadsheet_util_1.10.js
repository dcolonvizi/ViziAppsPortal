//GoogleDocs globals
var google_spreadsheet_web_service_url = 'https://viziapps.mobi/mobiflex/WebServices/GoogleDocs/GoogleSpreadsheet.aspx';
var google_apps_spreadsheet_web_service_url = 'https://viziapps.mobi/gapps/default.aspx';
var GoogleDocsParams;
var initGoogleDocsParams;
var nSqlCommands;
var isGoogleDocsBusy = false;
var sqlCommandIndex;
var GoogleDocs_if_condition = false;
var GoogleDocs_ready_for_then = false;
var GoogleDocs_ready_for_else = false;
var GoogleDocsSqlCommands;
var doGoogleApps = false;

function doGoogleDocsInterface(connection_string, sql_commands) {
    showLoading();
    GoogleDocsSqlCommands = sql_commands;
	var parts = connection_string.split(";");
    var spreadsheet = null;
    var username = null;
    var password = null;
    var consumer_key = null;
    var consumer_secret = null;
    var requestor_id = null;
    for(var i=0;i<parts.length;i++)
    {
        var parms = parts[i].split("=");

        if (parms[0] == "spreadsheet")
            	spreadsheet = parms[1];
        else if (parms[0] == "username")
                username = parms[1];
        else if (parms[0] == "password")
            password = parms[1];
        else if (parms[0] == "consumer_key")
            consumer_key = parms[1];
        else if (parms[0] == "consumer_secret")
            consumer_secret = parms[1];
        else if (parms[0] == "requestor_id")
            requestor_id = parms[1];
            
    }
    if (password != null) {
        initGoogleDocsParams = '?username=' + username + '&password=' + password + '&spreadsheet=' + escape(spreadsheet);
        doGoogleApps = false;
    }
    else {
        initGoogleDocsParams = '?requestor_id=' + requestor_id + '&consumer_key=' + consumer_key + '&consumer_secret=' + consumer_secret + '&spreadsheet=' + escape(spreadsheet);
        doGoogleApps = true;
    }

	nSqlCommands = sql_commands.length;
	sqlCommandIndex = 0; 
    doSqlCommand();
}

function doSqlCommand() {
  
    GoogleDocsParams = initGoogleDocsParams;
    var sql_command = GoogleDocsSqlCommands[sqlCommandIndex]; //hashtable
    if (!sql_command)
        return;
    //debug log
    sql_command.each(function (key, value) {
        var x = key;
        var y = value;
    });
    //end debug
    var command =  sql_command.get("command");
            
    if(command.toLowerCase() == "if")
    {
        var command_condition_device_field1 =sql_command.get("command_condition_device_field1");
        var command_condition_operation =sql_command.get("command_condition_operation");
        var command_condition_device_field2 =sql_command.get("command_condition_device_field2");              
        var command_condition_device_field1_value = null;
        var command_condition_device_field2_value = null;

        //check Output values first
        var value = getFieldValue(command_condition_device_field1);
        if(value != null)
        {
            command_condition_device_field1_value = value;
        }
        else
        {
            //then check previous field values
            command_condition_device_field1_value = getFieldValue(command_condition_device_field1);
            if(command_condition_device_field1_value == null)
                command_condition_device_field1_value = command_condition_device_field1;
        }
        value = getFieldValue(command_condition_device_field2);
        if(value != null)
        {
            command_condition_device_field2_value = value;
        }
        else
        {
            //then check previous field values
            command_condition_device_field2_value = getFieldValue(command_condition_device_field2);
            if(command_condition_device_field2_value == null)
                command_condition_device_field2_value = command_condition_device_field2;
        }              
        if(command_condition_operation == "=")
        {
            if(command_condition_device_field1_value == command_condition_device_field2_value)
            {
                GoogleDocs_if_condition = true;
            }  
            else
                GoogleDocs_if_condition = false;
        }
        else{
            if(!command_condition_device_field1_value == command_condition_device_field2_value)
            {
                GoogleDocs_if_condition = true;
            }  
            else
                GoogleDocs_if_condition = false;
                    
        }
        GoogleDocs_ready_for_then = true;
        shouldDoSqlCommand();
        return;
    }
    else{ //regular DB commands           

        var table = sql_command.get("table");
        GoogleDocsParams += '&table=' + escape(table);   
	            
	    if(startsWith(command,"then"))
	    {
	        if(GoogleDocs_ready_for_then && !GoogleDocs_if_condition)
	        {
	            GoogleDocs_ready_for_then = false;
	            GoogleDocs_ready_for_else = true;
	            shouldDoSqlCommand();
                 return;
	        }
	    }
	    else if(startsWith(command,"else"))
	    {
	        if(!GoogleDocs_ready_for_else)
	        {
	            GoogleDocs_ready_for_else = false;
	            shouldDoSqlCommand();
                return;
	        }
	    }
	    GoogleDocs_ready_for_else = false;
	    GoogleDocs_ready_for_then = false;
	    if(startsWith(command,"then") || startsWith(command,"else"))
	    {
	        command = command.substring(5);
	    }

	    if (command == "go to page") {
	        var page = sql_command.get("page");
	        $.mobile.changePage($("#" + page), { transition: viziapps_transition_type });
	        shouldDoSqlCommand();
            return;
	    }       

	    var fields = sql_command.get("database_fields");
	    if (fields != null) {
	        var field_array = new Array();
	        var value_array = new Array();
	        for (var j = 0; j < fields.length; j++) {
	            var field_item = fields[j]; //hashtable

	            if (field_item.length == 0)
	                continue;
	            field_array[j] = field_item.get("database_field");
	            value_array[j] = field_item.get("device_field");
	        }
	    }
	    var is_first = true;
	    var index = 0;
	    command =  command.toLowerCase();
	    GoogleDocsParams += '&query_type=' + escape(command);
	    var conditions = sql_command.get("conditions"); //array of hashtables
	    var sb_conditions ='';
	    if (conditions != null && conditions.length > 0)
	    {
	        for(var k=0;k<conditions.length;k++)
	        {
	            var condition = conditions[k];
	            if(k>0)
	            {
	                var condition_op =condition.get("condition_operation");
	                sb_conditions += " " + condition_op.toLowerCase() + " ";
	            }
	            sb_conditions += condition.get("condition_1st_field");
	            sb_conditions += condition.get("field_operation");
	            //get value of 2nd field
	            var value = condition.get("condition_2nd_field");
                var field_value = getFieldValue(value);
                if (field_value != null)
	        	{
	        	    value = field_value;
	        	}
	        	value = "|" + value + "| "; //for some reason quotes do not encode correctly through url query 
	        			
	        	if(value == null)
	        	{
	        		alert("Internal Error", "Parameter has null value");
	        		return;
	        	}
	            sb_conditions += value;
	        }
	        GoogleDocsParams += '&conditions=' + escape(sb_conditions);
	    }
 
	    if(command=="select from") {
	        var order_by = "";
	        var order_direction = "";
	        var order_by_node = sql_command.get("order_by");
	        if (order_by_node != null )
	        {
	            order_by =  order_by_node.get("sort_field");
	            sort_direction = order_by_node.get("sort_direction");
	            GoogleDocsParams += '&order_by=' + order_by + '&sort_direction=' + sort_direction.toLowerCase();
	        }
	        googleSpreadsheetQuery(GoogleDocsParams);
	    }
	    else if(command == "insert into")
	    {
	        var insert = '';
	        for(var k=0;k<field_array.length;k++)
	        {
	            var field = field_array[k];
	                	
	            //value is initially the phone field
	            var phone_field = value_array[k];
	            var value = null;
	                   
	            if(isImageField(phone_field))
	            {
	                /*var field_dict = mainActivity.fieldMap.get(phone_field);
					var imageData=  field_dict.get("image"); 
	
					if(imageData != null && imageData.length > 0)
					{
						var file_name = "image" + UUID.randomUUID().toString() + ".jpg";
						value = UploadDataToS3(imageData, file_name);
						field_dict.put("image_source", value);
						fieldMap.put(phone_field,field_dict);
					}		*/					
				}
                //check if the value is a field name or constant
                else {
                    var field_value = getFieldValue(phone_field);
                    if (field_value != null) {
                        value = field_value;
                    }
                }
                if (value == null)
                    value = phone_field;

                insert += field + '=|' + value + '|;';	//for some reason quotes do not encode correctly through url query           
	        }
	        GoogleDocsParams += '&insert=' + escape(insert);
	        googleSpreadsheetQuery(GoogleDocsParams);	
	    }
	    else if(command == "update")
	    {
	        var update = '';
	        for(var k=0;k<field_array.length;k++)
	        {
	            var field = field_array[k];
	            //value is initially the phone field
	            var phone_field = value_array[k];
	            var value = null;
	            if(isImageField(phone_field))
	            {
	               /* var field_dict = fieldMap.get(phone_field);
					var imageData= field_dict.get("image");
					if(imageData != null && imageData.length > 0)
					{
						var file_name = "image" + randomUUID().toString() + ".jpg";
						value = UploadDataToS3(imageData, file_name);
						field_dict.put("image_source", value);
						fieldMap.put(phone_field,field_dict);
					}	*/						
				}
				//check if the value is a field name or constant
				else 
				{
				    var field_value = getFieldValue(phone_field);
				    if (field_value != null) {
				        value = field_value;
				    }
				}
	            if(value == null)
	                value = phone_field;

	            update += field + '=|' + value + '|;'; //for some reason quotes do not encode correctly through url query 
            }

	        GoogleDocsParams += '&update=' + escape(update);
	        googleSpreadsheetQuery(GoogleDocsParams);	
	    }
	    else if(command == "delete from") {
	        googleSpreadsheetQuery(GoogleDocsParams);	
	    }     
    }
}

function parseSelectResponse(rows) {
    if (rows != null) {
        //get the DB fields we are looking for
        var DBToDeviceMap = new Hashtable();
        var sql_command = GoogleDocsSqlCommands[sqlCommandIndex]; //hashtable
        var field_items = sql_command.get("database_fields");

        //create database to device field map
        for (var kk = 0; kk < field_items.length; kk++) {
            var field_item = field_items[kk];
            var database_field = field_item.get("database_field");
            var device_field = field_item.get("device_field");
            DBToDeviceMap.put(database_field, device_field);
        }

        //loop through output and combine  values       
        if (rows.row.length == undefined) { //for single values
            var row = rows.row;
            for (var jj = 0; jj < row.field.length; jj++) {
                var DB_field_name = row.field[jj];
                if (!DBToDeviceMap.containsKey(DB_field_name))
                    continue;
                var device_field = DBToDeviceMap.get(DB_field_name);
                var value = row.value[jj];
                if (getFieldType(device_field) == "alert") {
                    hideLoading();
                    showAlert(device_field, "Data Alert", value);
                }
                else
                    setFieldValue(device_field, value);
            }
        }
        else { //for array values
            var n_rows = rows.row.length;
            var n_fields = rows.row[0].field.length; //fields in first row
            for (var i = 0; i < n_fields; i++) {
                var DB_field_name = rows.row[0].field[i];
                if (!DBToDeviceMap.containsKey(DB_field_name))
                    continue;
                var device_field = DBToDeviceMap.get(DB_field_name);
                var value_array = new Array(n_rows);
                for (var j = 0; j < n_rows; j++) {
                    value_array[j] = rows.row[j].value[i];
                }
                setFieldArrayValues(device_field, value_array);
           }
        }
    }
}


var intervalID;

function googleSpreadsheetQuery(params) {
    var surl = null;
    if (doGoogleApps)
        surl = google_apps_spreadsheet_web_service_url + params + '&callback=?';
    else
        surl = google_spreadsheet_web_service_url + params + '&callback=?';

    var t = setTimeout("hideLoading()", 10000); //in case there is an issue the spinner will stop in 10 s. in any case
    isGoogleDocsBusy = true;
    $.getJSON(surl, function (json) {
        if (json.response.status == "success") {
            //check if select command
            var sql_command = GoogleDocsSqlCommands[sqlCommandIndex]; //hashtable
            var command = sql_command.get("command");
            if (startsWith(command, "then") || startsWith(command, "else")) {
                command = command.substring(5);
            }
            if (command.toLowerCase() == "select from")
                parseSelectResponse(json.response.rows);

            sqlCommandIndex++;
            if (sqlCommandIndex >= nSqlCommands)
                hideLoading();   
            
            isGoogleDocsBusy = false;            
        }
        else {
            isGoogleDocsBusy = false;
            hideLoading(); //this stops loop
            alert('Error in spreadsheet operation: ' + json.response.status);
        }

    });
    intervalID = setInterval("waitForGoogleDocs()",50);
}


function waitForGoogleDocs() {
    if (!isGoogleDocsBusy) {
        clearInterval(intervalID);
        if (sqlCommandIndex < nSqlCommands)        
            doSqlCommand();        
    }
}

function shouldDoSqlCommand() {
    sqlCommandIndex++;
    if (sqlCommandIndex < nSqlCommands)
        doSqlCommand(); 
    else
        hideLoading(); //this stops loop
}


function getJsonObjects(obj, key, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getJsonObjects(obj[i], key, val));
        } else if (i == key && obj[key] == val) {
            objects.push(obj);
        }
    }
    return objects;
}


function startsWith(input, str) {
    return input.indexOf(str) == 0;
}

String.prototype.replaceAll = function (stringToFind, stringToReplace) {
    var temp = this;
    var index = temp.indexOf(stringToFind);
    while (index != -1) {
        temp = temp.replace(stringToFind, stringToReplace);
        index = temp.indexOf(stringToFind);
    }
    return temp;
}
