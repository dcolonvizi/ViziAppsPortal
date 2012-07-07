// Initializes a new instance of the StringBuilder class

// and appends the given value if supplied

function StringBuilder(value) {
    this.strings = new Array("");
    this.append(value);
}

// Appends the given value to the end of this instance.

StringBuilder.prototype.append = function (value) {
    if (value) {
        this.strings.push(value);
    }
}

// Clears the string buffer

StringBuilder.prototype.clear = function () {
    this.strings.length = 1;
}

// Converts this instance to a String.

StringBuilder.prototype.toString = function () {
    return this.strings.join("");
}

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
    return null;
}

function onLoadFunction() {

    var loc = "" + document.location;
    var username = loc.indexOf("?user=") != -1 ? loc.substring(loc.indexOf("?user=") + 6, loc.length) : "";
    if (username.length > 0)
    {
        setCookie("mobiflex", username, 1);
    }

    var skus = document.getElementsByName('sku');
    if (skus == null || skus.length == 0)
        return;

    var sb = new StringBuilder();

    var is_first = true;

    for (var i = 0; i < skus.length; i++) {
        var sku = skus[i];
        if (is_first) {
            sb.append(sku.innerHTML);
            is_first = false;
        }
        else
            sb.append('_' + sku.value);

    }
    var order = sb.toString();
    var email = document.getElementById('email');
    var confirm = document.getElementById('aServiceTracking');
    var cookie_value = getCookie("mobiflex");
    if(cookie_value != null)
        url = "http://viziapps.mobi/portal/OrderConfirmation.aspx?user=" + cookie_value + "&order=" + order + "&confirm=" + confirm.innerHTML;
    else
        url = "http://viziapps.mobi/portal/OrderConfirmation.aspx?email=" + email.innerHTML + "&order=" + order + "&confirm=" + confirm.innerHTML;

    window.location = url;

}

window.onload = onLoadFunction; 
