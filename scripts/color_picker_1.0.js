function HandleColorChange(sender, eventArgs) {
    $get("ColorPickerSelectedColor").value = sender.get_selectedColor();
    if (style2 && style2.length > 0)
        setStyle2("color", sender.get_selectedColor());
     else if(style)
         setStyle("color", sender.get_selectedColor());
}

var colorPicker;
function OnColorClientLoad(sender) {
    colorPicker = sender;
}

function GetColorPicker() {
    return $find("<%= FontColor.ClientID %>");
}

function SetValue() {
    var newSelectedColor = $get("ColorPickerNewColor").value;
    var colorPicker = GetColorPicker();
    colorPicker.set_selectedColor(newSelectedColor);
    if(style)
        setStyle("color", newSelectedColor);
}

function rgbConvert(str) {
    str = str.replace(/rgb\(|\)/g, "").split(",");
    str[0] = parseInt(str[0], 10).toString(16).toLowerCase();
    str[1] = parseInt(str[1], 10).toString(16).toLowerCase();
    str[2] = parseInt(str[2], 10).toString(16).toLowerCase();
    str[0] = (str[0].length == 1) ? '0' + str[0] : str[0];
    str[1] = (str[1].length == 1) ? '0' + str[1] : str[1];
    str[2] = (str[2].length == 1) ? '0' + str[2] : str[2];
    return ('#' + str.join(""));
}

function SetColor(newSelectedColor) {
    if (newSelectedColor.indexOf('rgb') >= 0)
        newSelectedColor = rgbConvert(newSelectedColor);
    colorPicker.set_selectedColor(newSelectedColor);
}


function GetValue() {
    var colorPicker = GetColorPicker();
    alert(colorPicker.get_selectedColor());
}

function SetNoColor() {
    var colorPicker = GetColorPicker();
    return colorPicker.set_selectedColor(null); // set the value to null to select the no color option
}

function ShowPalette() {
    var colorPicker = GetColorPicker();
    colorPicker.showPalette();
}

function HidePalette() {
    var colorPicker = GetColorPicker();
    colorPicker.hidePalette();
}
