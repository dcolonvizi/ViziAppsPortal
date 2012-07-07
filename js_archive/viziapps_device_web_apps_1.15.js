var reportURL = "http://www.viziapps.mobi/mobiflex/ViziAppsPortal/WebAppService.aspx";
var GAppsAccessUrl = "http://www.viziapps.mobi/gapps/default.aspx";
var AWSServiceUrl = "http://www.viziapps.mobi/AWSService/UploadFile.aspx";
var previous_page_name;
var current_page_name;
var previous_orientation;

// ************************************* Startup
$(function () {
    $.support.cors = true;
    $(document).bind("mobileinit", function () {
        $.mobile.allowCrossDomainPages = true;
    });

    $('nav').touchScroll(); 

    $('div[data-role="page"]').live('pageshow', function (event, ui) {
        previous_page_name = ui.prevPage;
        current_page_name = $(this).id;
    });
});

// *************************************** Field Utilities
    function storeTableSelectedIndex(event) {
        var row = event.target.parentNode;
        var max = 0;
        while(row.localName != 'li') //depending on where the tap was
        {
            row = row.parentNode;
            max++;
            if (max > 10) {
                alert('Could not find selected row');
                return;
            }
        }
        var row_id = row.getAttribute('id');
        if (row_id == null) //depending on wher the tap was 
            row_id = row.parentNode.getAttribute('id');
        var parent_table = row.parentNode.parentNode.parentNode;
        var index = row_id.substring(row_id.lastIndexOf('_')+1);
        var id = parent_table.getAttribute('id');
        $('#' + id).data('selectedIndex',index);
        event.stopPropagation();
    }
    function getFieldFromFieldName(field_name) {
        var fields = document.getElementsByName(field_name);
        var field = null;
        if (fields == null || fields.length == 0)
            field = document.getElementById(field_name);
        else
            field = fields[0];
        return field;
    }   
     function getFieldType(field_name) {
        var field = getFieldFromFieldName(field_name);
        if (field == null)
            return null;
        return field.getAttribute('field_type');
    }   
	function getFieldValue(field_name) {
	    var field = getFieldFromFieldName(field_name);
	    if (field == null)
	        return null;
	    var field_type = field.getAttribute('field_type');
        switch (field_type) {
            case 'table_field':
                var parent_ul = field.parentNode;
                var max = 0;
                while (parent_ul.localName != 'ul') //depending on how the row is formatted
                {
                    parent_ul = parent_ul.parentNode;
                    max++;
                    if (max > 10) {
                        alert('Could not find selected row');
                        return;
                    }
                }
                var id = parent_ul.parentNode.parentNode.getAttribute('id'); //div is 2 levels above ul
                //get selected index
                var index = $('#' + id).data("selectedIndex");
                var node = $('[name="' + field_name + '"]').get(index);
                if (node.localName == "input")
                    return node.value;
                else
                    return node.textContent;
            case 'longitude': 						
						    //determine if the handset has client side geo location capabilities
				if (geo_position_js.init()) {
					geo_position_js.getCurrentPosition(success_callback, error_callback);
				} 
                else 
					alert("GPS is not available");						    						
                return longitude;
            case 'latitude':						
				//determine if the handset has client side geo location capabilities
				if (geo_position_js.init()) {
					geo_position_js.getCurrentPosition(success_callback, error_callback);
				} 
                else 
					alert("GPS is not available");
                return latitude;
            case 'hidden_field':
                if (field.value && field.value.indexOf('|') >= 0) {
                    var array = field.value.split('|');
                    return array[0];
                }
                else
                    return field.value;      
            default:
                return field.value;
        }
    }
    function getFieldArrayValues(field_name) {
        var field = getFieldFromFieldName(field_name);
        if (field == null)
            return null;
        var array_values = new Array();
        return array_values;
    }
    function getFieldFloatValue(field_name) {
        var field = getFieldFromFieldName(field_name);
        if (field == null)
            return null;
    }

    function setFieldArrayValues(field_name, array_values) {
        var field = getFieldFromFieldName(field_name);
        if (field == null)
            return null;
        var n_values = array_values.length;
        var field_type = field.getAttribute('field_type');
        switch (field_type) {
            case 'table_field':
                var row_parent = field;
                var max = 0;
                while (row_parent.localName != 'li') //depending on how the row is formatted
                {
                    row_parent = row_parent.parentNode;
                    max++;
                    if (max > 10) {
                        alert('Could not find selected row');
                        return;
                    }
                }
                //check number of rows + header
                var ul = row_parent.parentNode;
                var n_rows = ul.childNodes.length;
                if (n_rows - 1 != n_values) {
                    //remove previously used rows
                    var children = ul.childNodes;
                    for (var i = 2; i < children.length; i++) { //yes starts at 2 to skip the header and first  row
                        ul.removeChild(children[i]);
                    }

                    //create all the rows we need
                    for (var i = 1; i < n_values; i++) { //yes starts at 1 to skip the first row
                        ul.appendChild(row_parent.cloneNode(true));
                    }
                }
                //set value in the correct field in all rows
                var isTdTag = false;
                $('td[name="' + field_name + '"]').each(function (index, element) {
                    isTdTag = true;
                    //get field type
                    if (element.childNodes[0].nodeName == 'IMG') //must be img element
                        $(this).children().attr('src', array_values[index]);
                    else
                        $(this).children().text(array_values[index]);
                    var table_id = $(this).parent().parent().parent().parent().parent().parent().parent().parent().parent().parent().attr('id');
                    $(this).parent().parent().parent().parent().parent().parent().parent().attr('id', table_id + '_' + index);
                    if (index == n_values - 1) {
                        $(this).parent().parent().parent().parent().parent().parent().parent().addClass("ui-corner-bottom");
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().parent().parent().removeClass("ui-corner-bottom ");
                    }
                });
                if (!isTdTag) {
                    $('input[name="' + field_name + '"]').each(function (index, element) {
                        $(this).val(array_values[index]);
                     });
                }
                $('nav').touchScroll();   //to get scrolling
                break;
            case 'hidden_field':
                var isFirst = true;
                var field_value = '';
                for (var ii = 0; ii < n_values; ii++) {
                    if (isFirst)
                        isFirst = false;
                    else
                        field_value += '|';

                    field_value += array_values[ii]
                }
                $('#' + field_name).val(field_value);

                break;
        }
    }
    function setFieldValue(field_name, field_value) {
        var field = getFieldFromFieldName(field_name);
        if (field == null)
            return null;
        var field_type = field.getAttribute('field_type');
        switch(field_type) {
            case 'web_view':
                field.firstChild.setAttribute('src', field_value);
                if (field_value.toLowerCase().indexOf('youtube.com') >= 0)
                    field.firstChild.setAttribute('class', 'youtube-player');
                else
                    field.removeAttribute('class');
                break;
            case 'text_area':
                $('#' + field_name).children().first().children().first().text(field_value);
                break;
            case 'text_field':
                $('#' + field_name).attr('value',field_value);
                break;
            case 'switch':
                if (field_value == 'true') {
                    $('#' + field_name + '_0').attr('checked', 'checked');
                    $('#' + field_name + '_0').next().removeClass('ui-checkbox-off');
                    $('#' + field_name + '_0').next().addClass('ui-checkbox-on');
                    $('#' + field_name + '_0').next().children().first().children().last().removeClass('ui-icon-checkbox-off');
                    $('#' + field_name + '_0').next().children().first().children().last().addClass('ui-icon-checkbox-on');
                }
                else {
                    $('#' + field_name + '_0').removeAttr('checked');
                    $('#' + field_name + '_0').next().removeClass('ui-checkbox-on');
                    $('#' + field_name + '_0').next().addClass('ui-checkbox-off');
                    $('#' + field_name + '_0').next().children().first().children().last().removeClass('ui-icon-checkbox-on');
                    $('#' + field_name + '_0').next().children().first().children().last().addClass('ui-icon-checkbox-off');
                }
                break;
            case 'hidden_field':
                $('#' + field_name).val(field_value);
                break;
            default:
                $('#' +field_name).text(field_value);
                break;
        }
        $('nav').touchScroll();
    }

    function isNumber(input) {
        return (isNaN(Number(input))) ? false : true;
    }
    function isImageField(field_name) {
        var field = getFieldFromFieldName(field_name);
        var field_type = field.getAttribute('field_type');
        return (field_type == "image") ? true : false;
    }

    function gotoPage(page_name) {
        var page = '#' + page_name;
        $.mobile.changePage(page, { transition: viziapps_transition_type });
    }

    function gotoPageFromTableField( table_field_name) {
        var page = '#' + getFieldValue(table_field_name);
        $.mobile.changePage(page, { transition: viziapps_transition_type });
    }

    // **************************************** Alert
    function showAlert(field_name, title, message) {
        $('#' + field_name).click();
        $('#' + field_name + '_title').text(title);
        $('#' + field_name + '_message').text(message);
    }

    // ****************************************** Compute Utilities
     function doFieldCompute(field, compute) {
 		if(compute != null && compute.length > 0)
		{
			var statements = compute.split('|');
			for( var j=0;j<statements.length;j++)
			{
				var statement = statements[j];
				var rawparts = statement.split(";");
				var parts = new Array();
				for (var i = 0; i < rawparts.length; i++) {
				    if (rawparts[i] != '')
				        parts.push(rawparts[i]);
				}
				var value =  0.0;
				var assigned_field_name = null;
				var first_text_field_name = null;
				if(parts.length >= 3) // numberic computation
				{
					for( var i=0;i<parts.length;i++)
					{
						var part = parts[i];
						var ops = part.split(":");
						var op = ops[0];
						if(op == "assign")
						{
							assigned_field_name = ops[1];
							if(checkComputeKeyword(assigned_field_name))
								break;
						}
						else if(op == "first")
						{
							var first_field_name = ops[1];
							
							if(isNumber(first_field_name)) //for number value
								value = Number(first_field_name);
							
							else //for field name that contains a number						
							    value = getFieldFloatValue(first_field_name);
							
						}
						else if(op == "op")
						{
							var operation = ops[1];
							var op_parts = operation.split(",");
							var op_type = op_parts[0];
							var op_field_name = op_parts[1];
							
							//check if number
							var f_op_field_value =  0.0;
							if(isNumber(op_field_name))
								f_op_field_value = op_field_name;
							
							else						
								f_op_field_value = getFieldFloatValue(op_field_name);
							
							
							if(op_type == "+")
							{
								value += f_op_field_value;
							}
							else if(op_type == "-")
							{
								value -= f_op_field_value;
							}
							else if(op_type == "*")
							{
								value *= f_op_field_value;
							}
							else if(op_type == "/")
							{
								value /= f_op_field_value;
							}
						}
					}
					var field_dict5 = fieldMap.get(assigned_field_name);
					var field_type =  field_dict5.get("type");
					if(field_type == "text_field")
					{
						var field_dict6 = fieldMap.get(assigned_field_name);
						var text_field = field_dict6.get("field")	;
						text_field.setText(format("%.2f",value));
					}
					else if(field_type == "label")
					{
						var field_dict7 =  fieldMap.get(assigned_field_name);
						var label = field_dict7.get("field")	;
						label.setText(format("%.2f",value));
					}
				}
				
				else //for assigning any value to a field
				{
					var has_keyword = false;
					for( var i=0;i<parts.length;i++)
					{
						var part = parts[i];
						var ops = part.split(":");
						if(ops.length < 2)
							break;
						var op = ops[0];
						if(op == "assign")
						{
							assigned_field_name = ops[1];
							if(checkComputeKeyword(assigned_field_name))
							{
								has_keyword = true;
								break;
							}
						}
						else if(op == "first")
						{
							first_text_field_name = ops[1];
						}
					}
					if(has_keyword)
						continue;
					
					var text_value = null;
					
					var array_value = new Array();
					// ----------- from -----------
					if(first_text_field_name == null)
						return;
					if (first_text_field_name.toLowerCase() == "now()") {
                        var now = new Date();
                        text_value = now.getFullYear + "-" + now.getMonth + "-" + now.getDay + " " + now.getHours + ":" + now.getMinutes + ":" + now.getSeconds;
                    }
                    else if (first_text_field_name.toLowerCase() == "true" || first_text_field_name.toLowerCase() == "false") {
                        text_value = first_text_field_name.toLowerCase();
                    }
                    else {
                        text_value = getFieldValue(first_text_field_name);
                        if (text_value == null) {
                            alert('Cannot find field value for ' + first_text_field_name);
                        }

 						//missing code to check for arrays
					}
					
					//  ---------- to --------
					if(array_value != null && array_value.length > 0)
						setFieldArrayValues(assigned_field_name,array_value);
					else
						setFieldValue(assigned_field_name,text_value);
				}				
			}
		}	
    }

    function checkComputeKeyword(keyword) {
        var lower_keyword = keyword.toLowerCase();
        if (lower_keyword.indexOf("playaudio(") >= 0) {
            var start = keyword.indexOf("(");
            if (start >= 0) {
                start++;
                var end = keyword.indexOf(")", start);
                var field_name = keyword.substring(start, end);
                //var audio_url = getFieldValue(field_name);
                //Audio.playAudio(mainActivity,audio_url);
            }
            return true;
        }
        else if (lower_keyword.indexOf("showwebpage(") >= 0) {
            var start = keyword.indexOf("(");
            if (start >= 0) {
                start++;
                var end = keyword.indexOf(")", start);
                var field_name = keyword.substring(start, end);
                var page_url = getFieldValue(field_name);
                window.location.href = page_url;
            }
            return true;
        }
        return false;
    }

    // *********************************************************  Cordova camera  
    function getPhoto() {
        navigator.camera.getPicture(onPhotoURISuccess, onPhotoFail,
            {
                quality: 50,
                destinationType: navigator.camera.DestinationType.FILE_URI 
            });
    };

    function onPhotoURISuccess(imageURI) {
        //console.log("imageURI --" + imageURI);
        var capturedImg = document.getElementById('viziapps_photo_image');
        capturedImg.style.display = 'block';
        capturedImg.src = imageURI;
    };

    function onPhotoFail(imageURI) {
        alert("Photo Capture Failed");
    };    

    // *********************************************************** Cordova GPS  
    function getGPS() {
        gps_timer_id = navigator.geolocation.watchPosition(onGPSSuccess, onGPSFail,
        {
            enableHighAccuracy: true,
            timeout: 2 
        });
    };

    function stopGPS() {
        if (gps_timer_id > 0) {
            navigator.geolocation.clearWatch(gps_timer_id);
            gps_timer_id = -1;
        }
    };

    var gps_timer_id = -1;

    var onGPSSuccess = function (p) {

        if (!p)
            return;

        //console.log("Location = " + p.coords.latitude + "," + p.coords.longitude);
        document.getElementById('viziapps_gps_latitude').value = p.coords.latitude;
        document.getElementById('viziapps_gps_latitude').value = p.coords.longitude;
    };

    var onGPSFail = function (e) {
        alert("Error: " + e.code);
    };

    // ************************************************************* Web GPS
    var gps_interval_id;
    function startWebGPS() {
        gps_interval_id = window.setInterval("getWebGPS()", 2000);

    }
    function stopWebGPS() {
        window.clearInterval(gps_interval_id);
    }

    function getWebGPS() {
        navigator.geolocation.getCurrentPosition(function (location) {
            // Use location.coords.latitude and location.coords.longitude
            document.getElementById('viziapps_gps_latitude').value = location.coords.latitude;
            document.getElementById('viziapps_gps_latitude').value = location.coords.longitude;

        });
    }

    // *************************************************************** Utilities
    //display spinner
    $("#viziapps_spinner").bind("ajaxSend", function () {
        $(this).show();
    }).bind("ajaxStop", function () {
        $(this).hide();
    }).bind("ajaxError", function () {
        $(this).hide();
    });

    function showLoading() {
        $("#viziapps_spinner").show(); 
    }
    function hideLoading() {
        $("#viziapps_spinner").hide();
    }
/**
* Converts passed XML var into a DOM element.
* @param xmlStr {String}
*/
function getXmlDOMFromString(xmlStr) {
    if (window.ActiveXObject && window.GetObject) {
        // for Internet Explorer
        var dom = new ActiveXObject('Microsoft.XMLDOM');
        dom.loadXML(xmlStr);
        return dom;
    }
    if (window.DOMParser) { // for other browsers
        return new DOMParser().parseFromString(xmlStr, 'text/xml');
    }
    throw new Error('No XML parser available');
}

function getDeviceType() {
    var ua = navigator.userAgent;
    var checker = {
        iphone: ua.match(/(iPhone)/),
        ipod: ua.match(/(iPod)/),
        ipad: ua.match(/(iPad)/),
        blackberry: ua.match(/BlackBerry/),
        android: ua.match(/Android/)
    };
    if (checker.android) {
        return 'android';
    }
    else if (checker.iphone) {
        return 'iphone';
    }
    else if (checker.ipod) {
        return 'ipod';
    }
    else if (checker.ipad) {
        return 'ipad';
    }
    else if (checker.blackberry) {
        return 'blackberry';
    }
    else {
        return 'unknown';
    }
}

function startsWith(input,str){
    return input.indexOf(str) == 0;
}

// **************************************************** google analytics 
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-25325643-3']);
_gaq.push(['_trackPageview']);

(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();

/* submits to google analytics at each page */
$('[data-role=page]').live('pageshow', function (event, ui) {
    try {
        var pageTracker = _gat._getTracker("UA-25325643-3");
        pageTracker._trackPageview();
    } catch (err) {

    }
});