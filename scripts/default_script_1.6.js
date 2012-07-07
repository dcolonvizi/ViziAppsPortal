String.prototype.replaceAll = function (stringToFind, stringToReplace) {
    var temp = this;
    var index = temp.indexOf(stringToFind);
    while (index != -1) {
        temp = temp.replace(stringToFind, stringToReplace);
        index = temp.indexOf(stringToFind);
    }
    return temp;
}

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}

function checkSessionTimeOut(sender, args) {
    SessionTimeOut = document.getElementById("SessionTimeOut");
    if (SessionTimeOut != null && SessionTimeOut.value.length > 0) {
        var url = SessionTimeOut.value;
        alert('Your session has timed out.');
        if (url.indexOf('../../') >= 0)
            parent.window.parent.window.location = url.substring(3);
        else if (url.indexOf('../') >= 0)
            parent.window.location = url;
        window.location = url;
    }
}

function timeOut(url) {
    alert('Your session has timed out.');
    if (url.indexOf('../../') >= 0)
        parent.window.parent.window.location = url.substring(3);
    else if (url.indexOf('../') >= 0)
        parent.window.location = url;
    window.location = url;
}

var LoginButton;

if (typeof $ != 'undefined') 
{
    $(function () {
        $('#Password').keydown(function (e) {
            var code = e.keyCode;
            if (code == '13') {
                LoginButton = document.getElementById("ViziAppsLogin");
                if (LoginButton) {
                    LoginButton.click();
                }
            }
        });
    });
}

function loginClick() {
    if (LoginButton) {
        LoginButton.click();
        LoginButton = null;
    }
}

function isFireFoxBrowser() {
    if (navigator.userAgent.indexOf('Firefox') >= 0)
        return true;
    else
        return false;
}

function isSafariBrowser() {
    if (navigator.userAgent.indexOf('Safari') >= 0)
        return true;
    else
        return false;
}
function isOperaBrowser() {
    if (navigator.userAgent.indexOf('Opera') >= 0)
        return true;
    else
        return false;
}
function isChromeBrowser() {
    if (navigator.userAgent.indexOf('Chrome') >= 0)
        return true;
    else
        return false;
}

function isIEBrowser() {
    if (navigator.appName == 'Microsoft Internet Explorer')
        return true;
    else
        return false;
}

if (!isIEBrowser()) 
    document.captureEvents(Event.KEYDOWN);

function BrowserBeingClosed() {
    //Note that if the user clicks the mouse and then moves it away quickly, the x,y coordinates will not be accurate.
    var b_close = new Boolean();
    if (window.event.clientX < 0 && window.event.clientY < 0) //this takes care of IE6
        b_close = true;
    else if (window.event.clientX > 0 && window.event.clientX < 100 && window.event.clientY < 0) // these are Back/Forward buttons in IE7
        b_close = false;
    else //otherwise assume the window is closing. This does not take care of side mouse button for Back
        b_close = true;

    return b_close;
}

window.onunload = function LogoutUser() {
    try {
        if(storyBoardWindow != undefined)
            storyBoardWindow.close();
    }
    catch(err){}
}

function PopUp(url, features) {
    var PUtest = window.open(url, '_blank', features);
    if (PUtest == null) {
        alert('For correct operation, popups need to be allowed from this website.');
    }
}

function IsValidObjectName(sText) {
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_ ";
    var Char;
    for (i = 0; i < sText.length; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            return false;
        }
    }
    return true;
}

