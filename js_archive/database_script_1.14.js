
var DatabaseCommandsViewTree;

function DatabaseCommandsView_load(sender, eventArgs) {
    DatabaseCommandsViewTree = sender;
}

var SpreadsheetCommandsViewTree;
function SpreadsheetCommandsView_load(sender, eventArgs) {
    SpreadsheetCommandsViewTree = sender;
}

//AddCommand functions
function add_command_click(sender, eventArgs) {
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "add_command;" ;
    DoDatabaseChange();
}

function add_command_condition_click(sender, eventArgs) {
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "add_command_condition;" ;
    DoDatabaseChange();
}

//CommandCondition functions
function command_condition_device_field1_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_device_field1;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function command_condition_device_field1_Changed(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_device_field1;" + index + ";" + sender.value;
    DoDatabaseChange();
}

function command_condition_operation_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_operation;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}
function command_condition_device_field2_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_device_field2;" + index + ";" + sender._text;
    DoDatabaseChange();
}

function command_condition_device_field2_Changed(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_device_field2;" + index + ";" + sender.value;
    DoDatabaseChange();
}

function delete_command_condition_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "delete_command_condition;" + index;
    DoDatabaseChange();
}
function command_condition_add_then_sql_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.get_uniqueID());
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_add_then_sql;" + index;
    DoDatabaseChange();
}
function command_condition_add_then_action_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.get_uniqueID());
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_add_then_action;" + index;
    DoDatabaseChange();
}

function command_condition_add_else_sql_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.get_uniqueID());
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_add_else_sql;" + index;
    DoDatabaseChange();
}

function command_condition_add_else_action_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.get_uniqueID());
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command_condition_add_else_action;" + index;
    DoDatabaseChange();
}

//   Command functions
function command_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "command;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}
function table_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "table;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}
function page_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "page;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function add_field_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "add_field;" + index;
    DoDatabaseChange();
}

function add_condition_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "add_condition;" + index;
    DoDatabaseChange();
}

function add_order_by_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "add_order_by;" + index;
    DoDatabaseChange();
}
function test_query_click(sender, eventArgs) {
    var SaveDatabaseConfig = document.getElementById("SaveDatabaseConfig");
    SaveDatabaseConfig.click();
    var index = GetCommandIndexFromEventTarget(sender.name);
    //TestQueryURL is a global javascript var in parent aspx
    PopUp(TestQueryURL + '?query_index=' + index, 'height=800, width=830, left=20, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=yes, resizable=yes');
}

function delete_command_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "delete_command;" + index;
    DoDatabaseChange();
}

//   Field functions
function database_field_SelectedIndexChanged(sender, eventArgs) {
    var index = GetFieldIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "database_field;" + index[0] + ";" + index[1] + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function phone_field_SelectedIndexChanged(sender, eventArgs) {
    var index = GetFieldIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "device_field;" + index[0] + ";" + index[1] + ";" + sender._text;
    DoDatabaseChange();
}

function phone_field_Changed(sender, eventArgs) {
    var index = GetFieldIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "device_field;" + index[0] + ";" + index[1] + ";" + sender.value;
    DoDatabaseChange();
}

function delete_field_click(sender, eventArgs) {
     var index = GetFieldIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "delete_field;" + index[0] + ";" + index[1];
    DoDatabaseChange();
}

// WHERE functions
function condition_operation_SelectedIndexChanged(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "condition_operation;" + index[0] + ";" + index[1] + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function condition_1st_field_SelectedIndexChanged(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender._uniqueId);
     var DBTreeInfo = document.getElementById("DBTreeInfo");
     DBTreeInfo.value = "condition_1st_field;" + index[0] + ";" + index[1] + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function field_operation_SelectedIndexChanged(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "field_operation;" + index[0] + ";" + index[1] + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function condition_2nd_field_SelectedIndexChanged(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "condition_2nd_field;" + index[0] + ";" + index[1] + ";" + sender._text;
    DoDatabaseChange();
}

function condition_2nd_field_Changed(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "condition_2nd_field;" + index[0] + ";" + index[1] + ";" + sender.value;
    DoDatabaseChange();
}

function delete_condition_click(sender, eventArgs) {
    var index = GetWhereIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "delete_condition;" + index[0] + ";" + index[1];
    DoDatabaseChange();
}

//ORDER BY Functions
function sort_field_SelectedIndexChanged(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "sort_field;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

 function sort_direction_SelectedIndexChanged(sender, eventArgs) {
     var index = GetCommandIndexFromEventTarget(sender._uniqueId);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "sort_direction;" + index + ";" + eventArgs._item._text;
    DoDatabaseChange();
}

function delete_order_by_click(sender, eventArgs) {
    var index = GetCommandIndexFromEventTarget(sender.name);
    var DBTreeInfo = document.getElementById("DBTreeInfo");
    DBTreeInfo.value = "delete_order_by;" + index;
    DoDatabaseChange();
}

//Utility functions
function GetCommandIndexFromEventTarget(target) {
        //get location of click
        var split = target.split('$');
        return split[2].substring(1);
    }

function GetFieldIndexFromEventTarget(target) {
    //get location of click
    var split = target.split('$');
    var indicies = new Array(2);
    indicies[0] = split[2].substring(1);
    indicies[1] = split[3].substring(1);
    return indicies;
}

function GetWhereIndexFromEventTarget(target) {
    //get location of click
    var split = target.split('$');
    var indicies = new Array(2);
    indicies[0] = split[2].substring(1);
    indicies[1] = split[3].substring(1);
    return indicies;
}

function DoDatabaseChange() {
    try{
         DatabaseCommandsViewTree.get_nodes().getNode(0).get_nodes().clear();
    }
     catch (err) { } //exception when DatabaseCommandsViewTree is hidden
     
    try{
     SpreadsheetCommandsViewTree.get_nodes().getNode(0).get_nodes().clear();
    }
    catch (err) { } //exception when SpreadsheetCommandsViewTree is hidden

    var DBClick = document.getElementById("DBTreeClick");
    DBClick.click();
}

var first_post_back;
function showViewConnections() {
    var oWin = radopen('Help/ManageData/ViewConnectionString.aspx', 'ViewConnectionStringHelp');
    oWin.setSize(900, 200);
    oWin.set_visibleTitlebar(true);
    oWin.set_visibleStatusbar(false);
    oWin.set_modal(true);
    oWin.moveTo(300, 200);
    if (first_post_back == true)
        oWin.reload();
    else
        first_post_back = true;
    return true;
}

function getDatabaseInfo(sender) {
    var oWin = radopen('Dialogs/ManageData/GetDatabaseInfo.aspx', 'DatabaseSpecsWindow');
    oWin.setSize(450, 350);
    oWin.set_visibleTitlebar(false);
    oWin.set_visibleStatusbar(false);
    oWin.set_modal(true);
    oWin.moveTo(400, 200);
    oWin.add_close(onGetDatabaseInfoClientClose);
}

function onGetDatabaseInfoClientClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg && arg[0] == 'saved') {
        var ConnectionString = document.getElementById("ConnectionString");
        ConnectionString.value = arg[1];
        var UpdateDatabaseTree = document.getElementById("UpdateDatabaseTree");
        UpdateDatabaseTree.click();
    }
}

function getGoogleDocsInfo(sender) {
    var oWin = radopen('Dialogs/ManageData/GetGoogleDocsInfo.aspx', 'DatabaseSpecsWindow');
    oWin.setSize(900, 210);
    oWin.set_visibleTitlebar(false);
    oWin.set_visibleStatusbar(false);
    oWin.set_modal(true);
    oWin.moveTo(150, 200);
    oWin.add_close(onGetGoogleDocsInfoClose);
}


function onGetGoogleDocsInfoClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg == 'saved') {
        var UpdateDatabaseTree = document.getElementById("UpdateDatabaseTree");
        UpdateDatabaseTree.click();
    }
}

function onWebServiceURLClientClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg) {
        var WebServiceURLString = document.getElementById("WebServiceURLString");
        WebServiceURLString.value = arg;
        var UpdateDatabaseTree = document.getElementById("UpdateDatabaseTree");
        UpdateDatabaseTree.click();
    }
}

function checkChangingManageDataType(sender) {
    if (sender.value == 'Select ->')
        return true;
    return radconfirm('<span style="font-family:verdana; font-size:14px; font-weight:normal">Are you sure you want to change the Manage Data Type of this application? If you do, all stored configurations for the current Manage Data Type will be erased.</span>');
}
 
function phone_field_key_down(e)
{		
    var code = e.keyCode;
    if (code == '13') {
        phone_field_Changed(e.currentTarget, null);
        e.preventDefault();
        e.stopPropagation();
    }
};

function command_condition_device_field1_key_down(e)
{	
    var code = e.keyCode;
    if (code == '13') {
        command_condition_device_field1_Changed(e.currentTarget, null);
        e.preventDefault();
        e.stopPropagation();
    }
};

function command_condition_device_field2_key_down(e)
{	
    var code = e.keyCode;
    if (code == '13') {
        command_condition_device_field2_Changed(e.currentTarget, null);
        e.preventDefault();
        e.stopPropagation();
    }
};

function condition_2nd_field_key_down (e) {
    var code = e.keyCode;
    if (code == '13') {
        condition_2nd_field_Changed(e.currentTarget, null);
        e.preventDefault();
        e.stopPropagation();
    }
};
