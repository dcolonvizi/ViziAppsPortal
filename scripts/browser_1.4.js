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

if (typeof $ != 'undefined') {
    $(function () {
        $(document).keydown(function (e) {
            var code = e.keyCode;
           // if (code == '37' || code == '38' || code == '39' || code == '40' || code == '46' || code == '8') {
            if (code == '37' || code == '38' || code == '39' || code == '40') {
               //this stops the scroll bars from moving with the arrow keys
                e.preventDefault();
                e.stopPropagation();
            }
        });
    });
}