function onLoginClientClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg == 'view_eula') {
        GetRadWindowManager().open("EULAAgreement.aspx", "EulaWindow");
    }
    else {
        var button = document.getElementById("Login");
        button.click();
    }
}

function onEulaClientClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg == 'agreed') {
        var button = document.getElementById("Login");
        button.click();
    }
}

var TemplateAppWin;
function showAddTemplateAppClient() {
    var oWin = radopen('Dialogs/CoverFlow/TemplateiPhoneIndex.html', 'AddTemplateAppBox');
    oWin.set_visibleTitlebar(true);
    oWin.set_visibleStatusbar(false);
    oWin.set_modal(true);
    oWin.setSize(900, 600);
    oWin.moveTo(50, 0);
    TemplateAppWin = oWin;
}

function closeAddTemplateAppClient() {
    TemplateAppWin.close();
    var CopyTemplateAppToAccount = document.getElementById("CopyTemplateAppToAccount");
    CopyTemplateAppToAccount.click();
}
function ToolHelp() {
         $("#tool_help_dialog").dialog("open");
         addIFrame($("#tool_help_dialog"), 'Help/Design/ToolHelp.aspx');
         return false;
}
function DesignPageHelp() {
    $("#design_page_help_dialog").dialog("open");
    addIFrame($("#design_page_help_dialog"), 'Help/Design/DesignHelp.htm');
}

 // App dialogs

 function SaveNewApp() {
    var AppName = document.getElementById("AppName");
    AppName.value = '';
    var IsNewApp = document.getElementById("IsNewApp");
    IsNewApp.value = 'true';
    $("#new_app_dialog").dialog("open");
    addIFrame($("#new_app_dialog"), 'Dialogs/Design/NewApp.aspx');
    return false;
}
function onNewAppClientClose(args) {
    $("#new_app_dialog").dialog("close");
    var AppName =  document.getElementById("AppName");
    AppName.value = args[0];
    var DeviceType = document.getElementById("DeviceType");
    DeviceType.value = args[1];
    var AppType = document.getElementById("AppType");
    AppType.value = args[2];
    var AppDescription = document.getElementById("AppDescription");
    AppDescription.value = args[3];
    var PageName = document.getElementById("PageName");
    PageName.value = args[4];

    //make edit tools visible
    var AppPropertiesLabel = document.getElementById("AppPropertiesLabel");
    AppPropertiesLabel.style.display = "inline";

    var AppProperties = document.getElementById("AppProperties");
    AppProperties.style.display = "inline";

    var edit_tools = document.getElementById("edit_tools");
    edit_tools.style.display = "inline";

    //post to server           
    var SaveAppAsPost = document.getElementById("SaveAppAsPost");
    SaveAppAsPost.click();
}

 function showRenameAppBox() {
         $("#rename_app_dialog").dialog("open");
         addIFrame($("#rename_app_dialog"), 'Dialogs/Design/RenameApp.aspx');
         return false;
 }

 function onRenameAppClientClose(arg) {
     $("#rename_app_dialog").dialog("close");
     document.getElementById("AppName").value = arg;
     var AddTemplateApp = document.getElementById("RenameAppPost");
     AddTemplateApp.click();
 }

function showDuplicateAppBox() {
       $("#duplicate_app_dialog").dialog("open");
       addIFrame($("#duplicate_app_dialog"), 'Dialogs/Design/DuplicateApp.aspx');
       return false;
}

function onDuplicateAppClientClose(arg) {
    $("#duplicate_app_dialog").dialog("close");
    document.getElementById("AppName").value = arg;
    var AddTemplateApp = document.getElementById("DuplicateAppPost");
    AddTemplateApp.click();
}
 
function convertApp() {
    $("#convert_app_dialog").dialog("open");
    addIFrame($("#convert_app_dialog"), 'Dialogs/Design/ConvertApp.aspx');
    return false;
}
function convertAppFinish(RedirectPage) {
    parent.window.location = '../../' + RedirectPage;
}
function isAppNameUnique(app_name) {
    var app = app_name.trim();
    var AllAppNames = parent.document.getElementById("AllAppNames");
    var apps = AllAppNames.value.split(';');

    for (i = 0; i < apps.length; i++) {
        if (apps[i] == app)        
            return false;        
    }
    return true;
}
// Page dialogs
function showNewPageBox() {
    $("#new_page_dialog").dialog("open");
    addIFrame($("#new_page_dialog"), 'Dialogs/Design/NewPage.aspx');
    return false;
}

function onNewPageClientClose(arg) {
    $("#new_page_dialog").dialog("close");
    document.getElementById("PageName").value = arg;
    var NewPagePost = document.getElementById("NewPagePost");
    NewPagePost.click();
}
    
function showRenamePageBox() {
    $("#rename_page_dialog").dialog("open");
    addIFrame($("#rename_page_dialog"), 'Dialogs/Design/RenamePage.aspx');
    return false;
}
function onRenamePageClientClose(arg) {
    $("#rename_page_dialog").dialog("close");
    document.getElementById("PageName").value = arg;
    var RenamePagePost = document.getElementById("RenamePagePost");
    RenamePagePost.click();
}

function showDuplicatePageBox() {
    $("#duplicate_page_dialog").dialog("open");
    addIFrame($("#duplicate_page_dialog"), 'Dialogs/Design/DuplicatePage.aspx');
    return false;
}

function onDuplicatePageClientClose(arg) {
    $("#duplicate_page_dialog").dialog("close");
    document.getElementById("PageName").value = arg;
    var DuplicatePagePost = document.getElementById("DuplicatePagePost");
    DuplicatePagePost.click();
}
 

function DeleteApp() {
    if (!confirm('Are you sure you want to delete this app?'))
        return false;
    var DeleteAppPost = document.getElementById("DeleteAppPost");
    DeleteAppPost.click();
    var iframe = document.getElementById("storyBoard");
}

function onUrlAccountIdentifierClientClose(sender, eventArgs) {
    var arg = eventArgs.get_argument();
    if (arg) {
        var AccountIdentifier = document.getElementById("UrlAccountIdentifier");
        if (AccountIdentifier.value == '' && arg != '') {
            AccountIdentifier.value = arg;
            var SaveUrlAccountIdentifier = document.getElementById("SaveUrlAccountIdentifier");
            SaveUrlAccountIdentifier.click();
        }
    }
}

function showUrlAccountIdentifierBox() {
    var oWin = radopen('Dialogs/Publish/AccountIdentifier.aspx', 'setUrlAccountIdentifierBox');
    oWin.set_visibleTitlebar(false);
    oWin.set_visibleStatusbar(false);
    oWin.set_modal(true);
    oWin.setSize(400, 250);
    var AccountIdentifier = document.getElementById("UrlAccountIdentifier");
    AccountIdentifier.value = '';
    oWin.add_close(onUrlAccountIdentifierClientClose);
    return false;
}

function shouldSaveWebApp() {
    var CurrentApp = document.getElementById("CurrentApp");
    if (CurrentApp.value.indexOf('->') >= 0)
        return true;
    if (confirm('Do you want to save this app as a web app?')) {
        var SaveBeforeChangingAppType = document.getElementById("SaveBeforeChangingAppType");
        SaveBeforeChangingAppType.value = 'yes';
    }
    return true;
}

function shouldSaveNativeApp() {
    var CurrentApp = document.getElementById("CurrentApp");
    if (CurrentApp.value.indexOf('->') >= 0)
        return true;
    if (confirm('Do you want to save this app as a native app?')) {
        var SaveBeforeChangingAppType = document.getElementById("SaveBeforeChangingAppType");
        SaveBeforeChangingAppType.value = 'yes';
    }
    return true;
}

var CurrentApp;
function onCurrentAppLoad(sender) {
    CurrentApp = sender;
}

function onCurrentAppChanged() {
    var edit_tools = document.getElementById("edit_tools");
    var value = CurrentApp.get_value();
    if(value.indexOf("->") >= 0){
          edit_tools.style.display = "none";
    }
    else {
        edit_tools.style.display = "inline";
    }
}