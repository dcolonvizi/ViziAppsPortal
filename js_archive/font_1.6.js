var style;
var style2;

var FontFamilyCombo;
function OnFontFamilyClientLoad(sender) {
    FontFamilyCombo = sender;
}

var FontSizeCombo;
function OnFontSizeClientLoad(sender) {
    FontSizeCombo = sender;
}

function OnFontFamilyClientSelectedIndexChanged(sender, eventArgs)
{
    var item = eventArgs.get_item();
    if (style2 && style2.length > 0)
        setStyle2("font-family", item.get_value());
    else
        setStyle("font-family", item.get_value());
}

function OnFontSizeClientSelectedIndexChanged(sender, eventArgs) {
    var item = eventArgs.get_item();
    if (style2 && style2.length > 0)
        setStyle2("font-size", item.get_value());
    else
        setStyle("font-size", item.get_value());
}

function onBoldClick() {
    var Bold = document.getElementById("Bold");
    if (style2 && style2.length > 0) {
        if (Bold.checked)
            setStyle2("font-weight", "bold");
        else
            setStyle2("font-weight", "normal");
    }
    else {
        if (Bold.checked)
            setStyle("font-weight", "bold");
        else
            setStyle("font-weight", "normal");
    }
}
function onItalicClick() {
    var Italic = document.getElementById("Italic");
    if (style2 && style2.length > 0) {
        if (Italic.checked)
            setStyle2("font-style", "italic");
        else
            setStyle2("font-style", "normal");
    }
    else {
        if (Italic.checked)
            setStyle("font-style", "italic");
        else
            setStyle("font-style", "normal");
    }
}
function onUnderlineClick() {
    var Underline = document.getElementById("Underline");
    if (style2 && style2.length > 0) {
        if (Underline.checked)
            setStyle2("text-decoration", "underline");
        else
            setStyle2("text-decoration", "none");
    }
    else {
        if (Underline.checked)
            setStyle("text-decoration", "underline");
        else
            setStyle("text-decoration", "none");
    }
}

function setLocationFromStyle(style) {
    var attributes = style.split(';');
    for(i in attributes){
        var parts = attributes[i].split(':');
        if (parts.length < 2)
            continue;
        var value = parts[1].trim().replace('px','');
        switch (parts[0].trim()) {
            case 'left':
                var left_var = document.getElementById("left");
                left_var.value = value;
                break;
            case 'top':
                var top_var = document.getElementById("top");
                top_var.value = value;
                break;
            case 'width':
                var width_var = document.getElementById("width");
                width_var.value = value;
                break;
            case 'height':
                var height_var = document.getElementById("height");
                height_var.value = value;
                break;
        }
    }
 }

 function setStyleFromLocation(style) {
     var new_style = '';
     var attributes = style.split(';');
      for(i in attributes){
          var parts = attributes[i].split(':');
         switch (parts[0].trim()) {
             case 'left':
                 var left_var = document.getElementById("left");
                 new_style += parts[0] + ':' + left_var.value + 'px;';
                 break;
             case 'top':
                 var top_var = document.getElementById("top");
                 new_style += parts[0] + ':' + top_var.value + 'px;';
                 break;
             case 'width':
                 var width_var = document.getElementById("width");
                 new_style += parts[0] + ':' + width_var.value  + 'px;';
                 break;
             case 'height':
                 var height_var = document.getElementById("height");
                 new_style += parts[0] + ':' + height_var.value + 'px;';
                 break;
             default:
                 new_style += parts[0] + ':' + parts[1] + ';';
                 break;
         }
     }
     return new_style;
}

function getStyle() {
    var split = style.split(';');
    getStyleSplit(split);
}

function getStyle2() {
    var split = style2.split(';');
    getStyleSplit(split);
}

function getStyleSplit(split){
    for (var i = 0; i < split.length; i++) {
        if (split[i] == null || split[i].length == 0)
            continue;
        var parts = split[i].split(':');
        parts[0] = parts[0].toLowerCase().replace(' ', '');
        if(parts.length > 1)
            parts[1] = parts[1].replace(' ', '');

        switch (parts[0]) {
            case "font-family":
                FontFamilyCombo.trackChanges();
                var item = FontFamilyCombo.findItemByText(parts[1]);
                if (item) {
                    item.select();
                }
                FontFamilyCombo.commitChanges();
                break;
            case "font-size":
                FontSizeCombo.trackChanges();
                var item = FontSizeCombo.findItemByText(parts[1]);
                if (item) {
                    item.select();
                }
                FontSizeCombo.commitChanges();
                break;
            case "color":
                SetColor(parts[1]);
                break;
            case "font-weight":
                var Bold = document.getElementById("Bold");
                if (Bold != null) {
                    if (parts[1] == 'bold')
                        Bold.checked = true;
                    else
                        Bold.checked = false;
                }
                break;
            case "font-style":
                var Italic = document.getElementById("Italic");
                if (Italic != null) {
                    if (parts[1] == 'italic')
                        Italic.checked = true;
                    else
                        Italic.checked = false;
                }
                break;
            case "text-decoration":
                var Underline = document.getElementById("Underline");
                if (Underline != null)
                {
                    if (parts[1] == 'underline')
                        Underline.checked = true;
                    else
                        Underline.checked = false;
                }
                break;
        }
    }
}

function setStyle(attribute, value) {
   
    var split = style.split(';');
    for (var i = 0; i < split.length; i++) {
        var parts = split[i].split(':');
        if (parts[0].replace(' ','').toLowerCase() == attribute) {
            parts[1] = value;
            split[i] = parts[0] + ':' + parts[1];
            style = '';
            for (var j = 0; j < split.length; j++) {
                if (j < split.length - 1)
                    style += split[j] + ';';
                else
                    style += split[j];
            }
            return;
        }
    }
}

function setStyle2(attribute, value) {

    var split = style2.split(';');
    for (var i = 0; i < split.length; i++) {
        var parts = split[i].split(':');
        if (parts[0].replace(' ', '').toLowerCase() == attribute) {
            parts[1] = value;
            split[i] = parts[0] + ':' + parts[1];
            style2 = '';
            for (var j = 0; j < split.length; j++) {
                if (j < split.length - 1)
                    style2 += split[j] + ';';
                else
                    style2 += split[j];
            }
            return;
        }
    }
}

