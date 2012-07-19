var DataSourceRef;
function setDataSource(sender,args) {
    DataSourceRef = sender;
}
function onSelectAppDataSource(sender, args) {
    var EditAppDataSource = document.getElementById("EditAppDataSource");
    var DeleteAppDataSource = document.getElementById("DeleteAppDataSource");
    if (sender._text == 'Select ->' || sender._itemData.length == 0) {
        EditAppDataSource.style.display = "none";
        DeleteAppDataSource.style.display = "none";
    }
    else {
        EditAppDataSource.style.display = "inline";
        DeleteAppDataSource.style.display = "inline";
    }
}
function onManagePageDataLoad() {
    var EditAppDataSource = document.getElementById("EditAppDataSource");
    var DeleteAppDataSource = document.getElementById("DeleteAppDataSource");
    if (DataSourceRef._text == 'Select ->' || DataSourceRef._itemData.length == 0) {
        EditAppDataSource.style.display = "none";
        DeleteAppDataSource.style.display = "none";
    }
    else {
        EditAppDataSource.style.display = "inline";
        DeleteAppDataSource.style.display = "inline";
    }
}
function newAppDataSource() {
    var NewAppDataSourceContainer = document.getElementById("NewAppDataSourceContainer");
    NewAppDataSourceContainer.style.display = "inline";
    var EditAppDataSource = document.getElementById("EditAppDataSource");
    EditAppDataSource.style.display = "none";
    var DeleteAppDataSource = document.getElementById("DeleteAppDataSource");
    DeleteAppDataSource.style.display = "none";
}
function addAppDataSource(sender, args) {
    switch (sender.get_value()) {
        case 'google_spreadsheet':
            parent.window.addGoogleSpreadsheetDataSource();
            break;
        case 'rss_feed':
            parent.window.addRSSDataSource();
            break;
        case 'rest_web_service':
            parent.window.addRESTWebServiceDataSource();
            break;
        case 'soap_web_service':
            parent.window.addSOAPWebServiceDataSource();
            break;
        case 'sql_database':
            parent.window.addSQLDatabaseDataSource();
            break;
        default:
            return;
    }
    sender.trackChanges();
    var comboItem = sender.findItemByText('Select ->');
    comboItem.select();
    sender.commitChanges();
    cancelNewAppDataSource();

    var EditAppDataSource = document.getElementById("EditAppDataSource");
    var DeleteAppDataSource = document.getElementById("DeleteAppDataSource");
    if (sender._text == 'Select ->' || sender._itemData.length == 0) {
        EditAppDataSource.style.display = "none";
        DeleteAppDataSource.style.display = "none";
    }
    else {
        EditAppDataSource.style.display = "inline";
        DeleteAppDataSource.style.display = "inline";
    }
}  
function cancelNewAppDataSource() {
    var NewAppDataSourceContainer = document.getElementById("NewAppDataSourceContainer");
    NewAppDataSourceContainer.style.display = "none";
    var EditAppDataSource = document.getElementById("EditAppDataSource");
    EditAppDataSource.style.display = "inline";
    var DeleteAppDataSource = document.getElementById("DeleteAppDataSource");
    DeleteAppDataSource.style.display = "inline";
}
function editAppDataSource() {
    var value = DataSourceRef.get_value()
    var type = value.substring(value.indexOf(':') + 1);
    var id = value.substring(0, value.indexOf(':'));
    switch (type) {
        case 'google_spreadsheet':
            parent.window.editGoogleSpreadsheetDataSource(id);
            break;
        case 'rss_feed':
            parent.window.editRSSDataSource(id);
            break;
        case 'rest_web_service':
            parent.window.editRESTWebServiceDataSource(id);
            break;
        case 'soap_web_service':
            parent.window.editSOAPWebServiceDataSource(id);
            break;
        case 'sql_database':
            parent.window.editSQLDatabaseDataSource(id);
            break;
        default:
            return;
    }
}
function showSelectDataSource() {
    var SelectDataSourceContainer = document.getElementById("SelectDataSourceContainer");
    SelectDataSourceContainer.style.display = "inline";
    var RemovePageDataSource = document.getElementById("RemovePageDataSource");
    RemovePageDataSource.style.display = "none";
}
function cancelShowPageDataSource() {
    var SelectDataSourceContainer = document.getElementById("SelectDataSourceContainer");
    SelectDataSourceContainer.style.display = "none";
    var RemovePageDataSource = document.getElementById("RemovePageDataSource");
    RemovePageDataSource.style.display = "inline";
}
function onNodeDragging(sender, args) {
    var target = args.get_htmlElement();

    if (!target) return;

    if (target.tagName == "INPUT") {
        target.style.cursor = "hand";
    }
}
   
function onNodeDropping(sender, args) {
    var target = args.get_htmlElement();
    if (target.tagName.toLowerCase() == "input" && target.className != "rcbInput") {
        target.style.cursor = "default";
        target.value = args.get_sourceNode().get_text();
        args.set_cancel(true);
        if (target.id.indexOf('command_condition_device_field1') >= 0) {
            command_condition_device_field1_Changed(target);
        }
        else if (target.id.indexOf('command_condition_device_field2') >= 0) {
            command_condition_device_field2_Changed(target);
        }
        else if (target.id.indexOf('condition_2nd_field') >= 0) {
            condition_2nd_field_Changed(target);
        }
        else if (target.id.indexOf('device_field') >= 0) {
            phone_field_Changed(target);
        }
        return true;
    }
}

function reloadManageDataURL(sender, args) {
    var ManageDataPanel = document.getElementById("ManageDataPanel");
    var ManageData = document.getElementById("ManageData");
    if (args.EventTarget == 'RemovePageDataSource') {
        var PageDataSources = document.getElementById("PageDataSources");
        var items = PageDataSources.control.get_items();
        if (items.get_count() == 0) {
            ManageDataPanel.style.display = "none";
            return;
        }
        ManageDataPanel.style.display = "inline";
        ManageData.src = ManageData.src;
    }
    else if(args.EventTarget == 'PageDataSources' ||
        args.EventTarget == 'SelectAppDataSource') {
            ManageDataPanel.style.display = "none";
        }
    else if (args.EventTarget == 'EventField')
    {
        var EventField = document.getElementById("EventField").control.get_value();
        if (EventField == "->") {
            ManageDataPanel.style.display = "none";
            return;
        }
        else{
            ManageDataPanel.style.display = "inline";
            ManageData.src = ManageData.src;
        }
    }
 }