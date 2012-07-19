//global static variables
var storyBoardWindow = null; 

var dialog_input_args;
function getDialogInputArgs() {
        return dialog_input_args;
}

$(function () {
    // a workaround for a flaw in jquery
    $("#dialog:ui-dialog").dialog("destroy");

    $("#new_app_dialog").dialog({ autoOpen: false, width: 620, height: 350, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#rename_app_dialog").dialog({ autoOpen: false, width: 350, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#duplicate_app_dialog").dialog({ autoOpen: false, width: 350, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#convert_app_dialog").dialog({ autoOpen: false, width: 500, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#new_page_dialog").dialog({ autoOpen: false, width: 350, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#rename_page_dialog").dialog({ autoOpen: false, width: 350, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#duplicate_page_dialog").dialog({ autoOpen: false, width: 350, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#tool_help_dialog").dialog({ autoOpen: false, width: 800, height: 550, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#design_page_help_dialog").dialog({ autoOpen: false, width: 420, height: 280, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#custom_header_html_dialog").dialog({ autoOpen: false, width: 700, height: 800, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#on_app_open_dialog").dialog({ autoOpen: false, width: 800, height: 280, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#app_images_dialog").dialog({ autoOpen: false, width: 1200, height: 1000, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#app_description_dialog").dialog({ autoOpen: false, width: 500, height: 280, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#page_transition_type_dialog").dialog({ autoOpen: false, width: 500, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });    
    $("#app_device_dialog").dialog({ autoOpen: false, width: 500, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#web_data_sources_dialog").dialog({ autoOpen: false, width: 800, height: 500, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#google_spreadsheet_data_source_dialog").dialog({ autoOpen: false, width: 900, height: 260, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#rss_data_surce_dialog").dialog({ autoOpen: false, width: 800, height: 500, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#rest_web_service_data_source_dialog").dialog({ autoOpen: false, width: 800, height: 500, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#soap_web_service_data_source_dialog").dialog({ autoOpen: false, width: 800, height: 500, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#sql_database_data_source_dialog").dialog({ autoOpen: false, width: 800, height: 500, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#manage_page_data_dialog").dialog({ autoOpen: false, width: 1100, height: 1000, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#account_identifier_dialog").dialog({ autoOpen: false, width: 400, height: 250, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#app_background_color_dialog").dialog({ autoOpen: false, width: 585, height: 375, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
  
    $("#label_dialog").dialog({ autoOpen: false, width: 500, height: 310, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#label").button().click(function () { InsertLabelOpen(null); });

    $("#html_panel_dialog").dialog({ autoOpen: false, width: 750, height: 990, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#html_panel").button().click(function () { InsertHtmlPanelOpen(null); });

    $("#text_field_dialog").dialog({ autoOpen: false, width: 500, height: 420, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#text_field").button().click(function () {InsertTextFieldOpen(null); });

    $("#text_area_dialog").dialog({ autoOpen: false, width: 500, height: 540, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#text_area").button().click(function () { InsertTextAreaOpen(null);});

   $("#image_dialog").dialog({ autoOpen: false, width: 770, height: 600, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#image").button().click(function () {InsertImageOpen(null)});

   $("#button_dialog").dialog({ autoOpen: false, width: 800, height: 505, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#button").button().click(function () {InsertButtonOpen(null);});

   $("#image_button_dialog").dialog({ autoOpen: false, width: 800, height: 520, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#image_button").button().click(function () {InsertImageButtonOpen(null);});

   $("#switch_dialog").dialog({ autoOpen: false, width: 800, height: 405, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#switch").button().click(function () {InsertSwitchOpen(null);});

    $("#checkbox_dialog").dialog({ autoOpen: false, width: 800, height: 405, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#checkbox").button().click(function () { InsertCheckBoxOpen(null); });

   $("#table_dialog").dialog({ autoOpen: false, width: 800, height: 530, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#table").button().click(function () {InsertTableOpen(null);});

   $("#picker_dialog").dialog({ autoOpen: false, width: 750, height: 520, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#picker").button().click(function () {InsertPickerOpen(null);});

   $("#web_view_dialog").dialog({ autoOpen: false, width: 600, height: 260, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#web_view").button().click(function () {InsertWebViewOpen(null);});

   $("#gps_dialog").dialog({ autoOpen: false, width: 440, height: 260, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#gps").button().click(function () {InsertGPSOpen(null);});

   $("#slider_dialog").dialog({ autoOpen: false, width: 400, height: 300, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#slider").button().click(function () {InsertSliderOpen(null);});

   $("#photo_dialog").dialog({ autoOpen: false, width: 500, height: 265, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#photo").button().click(function () {InsertPhotoOpen(null);});

   $("#audio_recorder_dialog").dialog({ autoOpen: false, width: 500, height: 150, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#audio_recorder").button().click(function () {InsertAudioRecorderOpen(null);});

   $("#audio_dialog").dialog({ autoOpen: false, width: 680, height: 345, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#audio").button().click(function () {InsertAudioOpen(null);});

   $("#alert_dialog").dialog({ autoOpen: false, width: 590, height: 225, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#alert").button().click(function () {InsertAlertOpen(null);});

   $("#map_dialog").dialog({ autoOpen: false, width: 600, height: 280, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
    $("#map").button().click(function () {InsertMapOpen(null);});

   $("#hidden_field_dialog").dialog({ autoOpen: false, width: 600, height: 200, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
   $("#hidden_field").button().click(function () { InsertHiddenFieldOpen(null); });
});

    function BringToMostFront() {
   
        var selElement = getSelectedElement();
        if (selElement == null)
        return;

        var max_z = getMaxZIndex();

        selElement.css("z-index",'' + (max_z+1));
    }

    function SendToMostBack() {
        var selElement = getSelectedElement();
        if (selElement == null)
        return;

        var min_z = getMinZIndex();

        selElement.css("z-index", '' + (min_z - 1));
    }


        var editorSelectedHtml;
        function doEditProperties()  {
            //get the selected HTML:

            var selectedHTML = getSelectedHtml();
            if (selectedHTML == null)
                return;
            editorSelectedHtml = selectedHTML; //for other functions

            var start = selectedHTML.indexOf('title="MobiFlex ');
            start += 16;
            var end = selectedHTML.indexOf('"', start);
            var type = selectedHTML.substring(start, end);
            //alert("type is: " + type);
            var inputs;
            switch (type) {
                case 'HtmlPanel':
                    dialog_input_args = ParseHtmlPanel(selectedHTML);
                    InsertHtmlPanelOpen(dialog_input_args);
                    return;
                case 'Label':
                    dialog_input_args =  ParseLabel(selectedHTML);
                    InsertLabelOpen(dialog_input_args) ;
                    return;
                case 'Slider':
                    dialog_input_args = ParseSlider(selectedHTML);
                    InsertSliderOpen(dialog_input_args);                    
                    return;
                case 'Switch':
                    dialog_input_args = ParseSwitch(selectedHTML);
                    InsertSwitchOpen(dialog_input_args);
                    return;
                case 'CheckBox':
                    dialog_input_args = ParseCheckBox(selectedHTML);
                    InsertCheckBoxOpen(dialog_input_args);
                    return;
                case 'TextField':
                    dialog_input_args = ParseTextField(selectedHTML);
                    InsertTextFieldOpen(dialog_input_args);
                    return;
                case 'TextArea':
                    dialog_input_args = ParseTextArea(selectedHTML);
                    InsertTextAreaOpen(dialog_input_args);                   
                    break;
                case 'Table':
                    dialog_input_args = ParseTable(selectedHTML);
                    InsertTableOpen(dialog_input_args);
                  
                    return;
                case 'WebView':
                    dialog_input_args = ParseWebView(selectedHTML);
                    InsertWebViewOpen(dialog_input_args);
                   
                    return;
                case 'Map':
                    dialog_input_args = ParseMap(selectedHTML);
                    InsertMapOpen(dialog_input_args);
                   
                    return;
                case 'Button':
                    dialog_input_args = ParseButton(selectedHTML);
                    InsertButtonOpen(dialog_input_args);
                    
                    return;
                case 'ImageButton':
                    dialog_input_args = ParseImageButton(selectedHTML);
                    InsertImageButtonOpen(dialog_input_args);
                    
                    return;
                case 'Image':
                    dialog_input_args = ParseImage(selectedHTML);
                    InsertImageOpen(dialog_input_args);
                   
                    return;
                case 'Audio':
                    dialog_input_args = ParseAudio(selectedHTML);
                    InsertAudioOpen(dialog_input_args);
                   
                    return;
                case 'Picker':
                    dialog_input_args = ParsePicker(selectedHTML);
                    InsertPickerOpen(dialog_input_args);
                   
                    return;
                case 'Photo':
                    dialog_input_args = ParsePhotoTaker(selectedHTML);
                    InsertPhotoOpen(dialog_input_args);
                   
                    return;
                case 'AudioRecorder':
                    dialog_input_args = ParseAudioRecorder(selectedHTML);
                    InsertAudioRecorderOpen(dialog_input_args);
                   
                    return;
                case 'Alert':
                    dialog_input_args = ParseAlert(selectedHTML);
                    InsertAlertOpen(dialog_input_args);
                   
                    return;
                case 'GPS':
                    dialog_input_args = ParseGPS(selectedHTML);
                    InsertGPSOpen(dialog_input_args);
                   
                    return;
                case 'HiddenField':
                    dialog_input_args = ParseHiddenField(selectedHTML);
                     InsertHiddenFieldOpen(dialog_input_args);
                   
                    return;
            };
        };

        var tool_left_position = 570;
        var tool_top_position = 200;
        //--------------------------------------------------------------
        function InsertHtmlPanelOpen(inputs) {
            if (inputs)
                dialog_input_args = inputs;
            else {
                dialog_input_args = new Array(3);
                var z_index = getNextZIndex();
                var width_on_screen = '320px';
                if (isDeviceTypeiPad()) {
                    width_on_screen = '740px';
                }
                dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:40px;left:0px;width:{1};height:420px;font-family:Verdana;font-size:12px;color:#000000;font-style:normal;font-weight:normal;text-decoration:none;background-color:#ffffff;overflow:hidden", z_index, width_on_screen);
            }
            if (isDeviceTypeiPad()) {
                $("#html_panel_dialog").dialog("option", "width", 808);
            }
            else{
                $("#html_panel_dialog").dialog("option", "width", 750);
            }
            $("#html_panel_dialog").dialog("open");
            addIFrame($("#html_panel_dialog"), 'EditorTools/InsertHtmlPanel.aspx');
        }
        function InsertHtmlPanelCallback(args) {
            if (args == null)
                return;
            deleteSelectedElement();
            var html_field = String.format("<div title=\"MobiFlex HtmlPanel\" id=\"{0}\" style=\"{2}\" >{1}</div>", args[0], args[1], args[2]);

            DoAddToCanvas(html_field, args[0]);
            $("#html_panel_dialog").dialog("close");

        }
        function ParseHtmlPanel(html) {
            var inputs = new Array(3);
            inputs[0] = GetAttribute(html, 'id');
            inputs[1] = GetHtmlPanelInnerHtml(html);
            inputs[2] = GetStyleAttribute(html);
            return inputs;
        };
 

        //--------------------------------------------------------------
        function InsertLabelOpen(inputs) {

            if (inputs)
                dialog_input_args = inputs;
            else {
                dialog_input_args = new Array(3);
                var z_index = getNextZIndex();
                dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:160px;left:10px;width:300px;height:30px;font-family:Verdana;font-size:16px;color:#000000;font-style:normal;font-weight:normal;text-decoration:none;", z_index);
            }
            $("#label_dialog").dialog("open"); 
            addIFrame($("#label_dialog"), 'EditorTools/InsertLabel.aspx');
        }
        function InsertLabelCallback(args) {               
                if (args == null)
                    return;
                deleteSelectedElement();
                var html_field = String.format("<div title=\"MobiFlex Label\" id=\"{0}\" style=\"{2}\" >{1}<img src=\"images/spacer.gif\" style=\"position:relative;top:-16px;width:100%;height:100%\" /></div>", args[0], args[1], args[2]);
                DoAddToCanvas(html_field, args[0]);
                $("#label_dialog").dialog("close");           
        }
        function ParseLabel(html) {
            var inputs = new Array(3);
            inputs[0] = GetAttribute(html, 'id');
            inputs[1] = GetInnerHtml(html);
            inputs[2] = GetStyleAttribute(html);
            return inputs;
        };

        //-------------------------------------------------------------------
        function InsertTextFieldOpen(inputs) {
            if (inputs)
                dialog_input_args = inputs;
            else {
                dialog_input_args = new Array(5);
                var z_index = getNextZIndex();
                dialog_input_args[3] = String.format("position:absolute;z-index:{0};top:160px;left:10px;width:200px;height:30px;font-family:Verdana;font-size:16px;color:#000000;font-style:normal;font-weight:normal;text-decoration:none", z_index);
            }
            $("#text_field_dialog").dialog("open"); 
            addIFrame($("#text_field_dialog"), 'EditorTools/InsertTextField.aspx');
         }
        function InsertTextFieldCallback(args) {           
                
                if (args == null)
                    return;
                deleteSelectedElement();
                var html_field = null;
                if (args[4] != null)
                {
                    args[4] = encodeHtml(args[4]);
                    html_field = String.format("<div title=\"MobiFlex TextField\" id=\"{0}\" type=\"{1}\" alt=\"{2}\"  style=\"{3}\" text=\"{4}\"><img src=\"images/editor_images/text_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3], args[4]);
                }
                else
                    html_field = String.format("<div title=\"MobiFlex TextField\" id=\"{0}\" type=\"{1}\" alt=\"{2}\"  style=\"{3}\" ><img src=\"images/editor_images/text_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);
               
                DoAddToCanvas(html_field, args[0]);

                $("#text_field_dialog").dialog("close"); 
        }
     function ParseTextField(html) {
         var inputs = new Array(5);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'type');
         inputs[2] = GetAttribute(html, 'alt');
         inputs[3] = GetStyleAttribute(html);
         inputs[4] = decodeHtml(GetAttribute(html, 'text'));
         return inputs;
     };

     //-------------------------------------------------------------------------
     function InsertTextAreaOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
             var z_index = getNextZIndex();
             dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:44px;left:10px;width:250px;height:300px;font-family:Verdana;font-size:16px;color:#000000;font-style:normal;font-weight:normal;text-decoration:none;background-image:url('images/editor_images/text_area.png');background-repeat:no-repeat;background-size:100% 100%;overflow:hidden;", z_index);
         }
          $("#text_area_dialog").dialog("open"); 
          addIFrame($("#text_area_dialog"), 'EditorTools/InsertTextArea.aspx');
     }
     function InsertTextAreaCallback(args) {
             if (args == null)
                 return;
             deleteSelectedElement();
             var filter_text = "";
             if (args[1] != null && args[1].length > 0) {
                 args[1] = encodeHtml(args[1]);
                 args[1] = args[1].replaceAll('\r', '').replaceAll('\n', '\\n');
                 filter_text = args[1].replaceAll('\\n', '<br/>').replaceAll('``','"').replaceAll('~~','=');
             }
             var html_field = String.format("<div title=\"MobiFlex TextArea\" id=\"{0}\" text=\"{1}\" style=\"{2}\" type=\"{3}\" ><div style=\"padding:5px\">{4}</div></div>", args[0], args[1], args[2],args[3],filter_text);

             DoAddToCanvas(html_field, args[0]);

             $("#text_area_dialog").dialog("close"); 
     }
     function ParseTextArea(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = decodeHtml(GetTextAttribute(html, 'text'));
         inputs[2] = GetStyleAttribute(html);
         inputs[3] = GetAttribute(html, 'type');
         return inputs;
     };

     //-------------------------------------------------------------------
     function InsertImageOpen(inputs) {
         if (inputs)
             dialog_input_args = inputs;
         else {
              dialog_input_args = new Array(3);
             var z_index = getNextZIndex();
             dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:160px;left:10px;height:100px;width:100px", z_index);
         }
         $("#image_dialog").dialog("open"); 
         addIFrame($("#image_dialog"), 'EditorTools/InsertImage.aspx');
     }
     function InsertImageCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             if (args[1] == 'images/no_icon.png')
                 var html_field = String.format("<div title=\"MobiFlex Image\" id=\"{0}\" style=\"{2}\" ><img src=\"{1}\" alt=\"no image\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2]);
            else
                var html_field = String.format("<div title=\"MobiFlex Image\" id=\"{0}\" style=\"{2}\" ><img src=\"{1}\" alt=\"image\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2]);
             
             DoAddToCanvas(html_field, args[0]);

             $("#image_dialog").dialog("close"); 
     }
     function ParseImage(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetImageSource(html);
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };
     //---------------------------------------------------
     function InsertSliderOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
         }
         $("#slider_dialog").dialog("open"); 
         addIFrame($("#slider_dialog"), 'EditorTools/InsertSlider.aspx');
      }
     function InsertSliderCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
               var z_index = getNextZIndex();
                if (args[3] == null || args[3].length == 0) {
                    if (args[1] == 'horizontal') {
                        var html_field = String.format("<div title=\"MobiFlex Slider\" id=\"{0}\" value=\"{2}\" style=\"position:absolute;z-index:{3};top:160px;left:10px;height:30px;width:277px;\" type=\"{1}\" ><img src=\"images/editor_images/horizontal_slider.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], z_index);
                    }
                    else {
                        var html_field = String.format("<div title=\"MobiFlex Slider\" id=\"{0}\" value=\"{2}\" style=\"position:absolute;z-index:{3};top:160px;left:10px;height:277px;width:30px;\" type=\"{1}\" ><img src=\"images/editor_images/vertical_slider.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], z_index);
                    }
                }
                else {
                    if (args[1] == 'horizontal') {
                        var html_field = String.format("<div title=\"MobiFlex Slider\" id=\"{0}\" value=\"{2}\" style=\"{3}\" type=\"{1}\" ><img src=\"images/editor_images/horizontal_slider.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);
                    }
                    else {
                        var html_field = String.format("<div title=\"MobiFlex Slider\" id=\"{0}\" value=\"{2}\" style=\"{3}\" type=\"{1}\" ><img src=\"images/editor_images/vertical_slider.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);
                    }
                }
             DoAddToCanvas(html_field, args[0]);

             $("#slider_dialog").dialog("close"); 
     }
        function ParseSlider(html) {
            var inputs = new Array(4);
            inputs[0] = GetAttribute(html,'id');
            inputs[1] = GetAttribute(html, 'type');
            inputs[2] = GetAttribute(html, 'value');
            inputs[3] = GetStyleAttribute(html);
            return inputs;
        };       

     //-------------------------------------------------------------------
     function InsertSwitchOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(5);
         }
          $("#switch_dialog").dialog("open"); 
          addIFrame($("#switch_dialog"), 'EditorTools/InsertSwitch.aspx');
      }
     function InsertSwitchCallback(args) {             
             if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
             var html_field = null;
             var image_url = null;
             switch (args[2]) {
                 case 'on':
                     image_url = 'images/editor_images/switch_on.png';
                     break;
                 case 'off':
                     image_url = 'images/editor_images/switch_off.png';
                     break;
                 case 'yes':
                     image_url = 'images/editor_images/switch_yes.png';
                     break;
                 case 'no':
                     image_url = 'images/editor_images/switch_no.png';
                     break;
                 case 'true':
                     image_url = 'images/editor_images/switch_true.png';
                     break;
                 case 'false':
                     image_url = 'images/editor_images/switch_false.png';
                     break;
             }
             if (args[4] == null || args[4].length == 0) {
                 html_field = String.format("<div title=\"MobiFlex Switch\" id=\"{0}\" style=\"position:absolute;z-index:{4};top:160px;left:10px;height:27px;width:94px;\"  type=\"{1}\" value=\"{2}\" submit=\"{3}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3], z_index);
             }
             else
                 html_field = String.format("<div title=\"MobiFlex Switch\" id=\"{0}\" style=\"{4}\" type=\"{1}\" value=\"{2}\" submit=\"{3}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3], args[4]);
             
             DoAddToCanvas(html_field, args[0]);
             $("#switch_dialog").dialog("close"); 
     }
     function ParseSwitch(html) {
         var inputs = new Array(5);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'type');
         inputs[2] = GetAttribute(html, 'value');
         inputs[3] = GetAttribute(html, 'submit');
         inputs[4] = GetStyleAttribute(html);
         return inputs;
     };

     //-------------------------------------------------------------------
     function InsertCheckBoxOpen(inputs) {

         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
         }
         $("#checkbox_dialog").dialog("open");
         addIFrame($("#checkbox_dialog"), 'EditorTools/InsertCheckBox.aspx');
     }
     function InsertCheckBoxCallback(args) {
         if (args == null)
             return;
         deleteSelectedElement();
         var z_index = getNextZIndex();
         var html_field = null;
         var image_url = null;
         switch (args[1]) {
             case 'checked':
                 image_url = 'images/editor_images/checkbox_on.png';
                 break;
             case 'unchecked':
                 image_url = 'images/editor_images/checkbox_off.png';
                 break;
         }
         if (args[3] == null || args[3].length == 0) 
                  html_field = String.format("<div title=\"MobiFlex CheckBox\" id=\"{0}\" style=\"position:absolute;z-index:{3};top:160px;left:10px;height:30px;width:30px;\" value=\"{1}\" submit=\"{2}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2],  z_index);
         
         else
             html_field = String.format("<div title=\"MobiFlex CheckBox\" id=\"{0}\" style=\"{3}\" value=\"{1}\" submit=\"{2}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);

         DoAddToCanvas(html_field, args[0]);
         $("#checkbox_dialog").dialog("close");
     }
     function ParseCheckBox(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'value');
         inputs[2] = GetAttribute(html, 'submit');
         inputs[3] = GetStyleAttribute(html);
         return inputs;
     };

     //-------------------------------------------------------------------
     function InsertRadioButtonOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(2);
         }
     }
     function InsertRadioButtonCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             if (args[1] == null || args[1].length == 0)
                 var html_field = String.format("<input title=\"MobiFlex RadioButton\" id=\"{0}\" type=\"radio\" value=\"on\" style=\"position:absolute;z-index:{1};top:160px;left:10px;\" />", args[0],z_index);
             else
                 var html_field = String.format("<input title=\"MobiFlex RadioButton\" id=\"{0}\" type=\"radio\" value=\"on\" style=\"{1}\" />", args[0], args[1]);
             
             DoAddToCanvas(html_field, args[0]);
             $("#radio_button_dialog").dialog("close"); 
         
     }
      function ParseRadioButton(html) {
         var inputs = new Array(2);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetStyleAttribute(html);
         return inputs;
     };
     //---------------------------------------------------------------
     function InsertTableOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(6);
         }
         $("#table_dialog").dialog("open"); 
          addIFrame($("#table_dialog"), 'EditorTools/InsertTableView.aspx');
     }
     function InsertTableCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             var parts = args[2].split(':');
             if (args[4] != null){
                    args[4] = encodeHtml(args[4]);
              }
             if (args[5] == null || args[5].length == 0) {
                 if (parts[0] == '1text|text' || parts[0] == '1texthidden|text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"position:absolute;z-index:{5};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/largetableview1textfield.jpg');background-repeat:no-repeat\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], z_index);
                 }
                 else if (parts[0] == '2texts|text,text' || parts[0] == '2textshidden|text,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"position:absolute;z-index:{5};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/largetableview2textfields.jpg');background-repeat:no-repeat\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], z_index);
                 }
                 else if (parts[0] == 'image1text|image,text' || parts[0] == 'imagetext|image,text' || parts[0] == 'image1texthidden|image,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"position:absolute;z-index:{5};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/LargeTableView1image1textField.jpg');background-repeat:no-repeat\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], z_index);
                 }
                 else if (parts[0] == 'image2texts|image,text,text' || parts[0] == 'image2textshidden|image,text,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"position:absolute;z-index:{5};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/LargeTableView1image2textFields.jpg');background-repeat:no-repeat\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], z_index);
                 }
             }
             else {
                 if (parts[0] == '1text|text' || parts[0] == '1texthidden|text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"{5}\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], args[5]);
                     html_field = replaceBackgroundImageSource(html_field, "images/editor_images/largetableview1textfield.jpg");
                 }
                 else if (parts[0] == '2texts|text,text' || parts[0] == '2textshidden|text,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"{5}\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], args[5]);
                     html_field = replaceBackgroundImageSource(html_field, "images/editor_images/largetableview2textfields.jpg");
                 }
                 else if (parts[0] == 'image1text|image,text' || parts[0] == 'imagetext|image,text' || parts[0] == 'image1texthidden|image,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"{5}\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], args[5]);
                     html_field = replaceBackgroundImageSource(html_field, "images/editor_images/LargeTableView1image1textField.jpg");
                 }
                 else if (parts[0] == 'image2texts|image,text,text' || parts[0] == 'image2textshidden|image,text,text,hidden') {
                     var html_field = String.format("<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"{5}\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />", args[0], args[1], args[2], args[3], args[4], args[5]);
                     html_field = replaceBackgroundImageSource(html_field, "images/editor_images/LargeTableView1image2textFields.jpg");
                 }
             }
             DoAddToCanvas(html_field, args[0]);
             $("#table_dialog").dialog("close"); 
         
     }
     function ParseTable(html) {
         var inputs = new Array(6);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'name');
         inputs[2] = GetAttribute(html, 'fields');
         inputs[3] = GetAttribute(html, 'submit');
         inputs[4] = decodeHtml(GetAttribute(html, 'options'));
         inputs[5] = GetStyleAttribute(html);
         return inputs;
     };

     //-----------------------------------------------------------
     function InsertWebViewOpen(inputs) {          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(3);
             var z_index = getNextZIndex();
             if(isDeviceTypeiPad())
                 dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:44px;left:0px;height:980px;width:768px;",z_index);
             else
                dialog_input_args[2] = String.format("position:absolute;z-index:{0};top:44px;left:0px;height:416px;width:320px;",z_index);
         }
          $("#web_view_dialog").dialog("open"); 
          addIFrame($("#web_view_dialog"), 'EditorTools/InsertWebView.aspx');
      }
     function InsertWebViewCallback(args) {               
             if (args == null)
                 return;
             deleteSelectedElement();
             var html_field = null;
            if (args[1].length > 0)
                html_field = String.format("<div title=\"MobiFlex WebView\" alt=\"MobiFlex WebView\" id=\"{0}\" style=\"{2}\" url=\"{1}\" >" + addIFrameString(args[1]) + "</div>", args[0], args[1], args[2]);
            else{
                var image_url = 'images/editor_images/WebView.jpg';
                if (isDeviceTypeiPad())
                 image_url = 'images/editor_images/WebView_ipad.jpg';

                html_field = String.format("<div title=\"MobiFlex WebView\" alt=\"MobiFlex WebView\" id=\"{0}\" style=\"{2}\" url=\"{1}\" > <div style=\"width:100%;height:100%;background-image:url('" + image_url + "');background-repeat:no-repeat\"/></div>", args[0], args[1], args[2]);
            }
             DoAddToCanvas(html_field, args[0]);

             $("#web_view_dialog").dialog("close"); 
     } 
     function ParseWebView(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'url');
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };

     //-----------------------------------------------------------
     function InsertMapOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(3);
         }
          $("#map_dialog").dialog("open"); 
          addIFrame($("#map_dialog"), 'EditorTools/InsertMap.aspx');
       }
     function InsertMapCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             if (args[2] == null || args[2].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex Map\" alt=\"MobiFlex Map\" id=\"{0}\" style=\"position:absolute;z-index:{2};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/map.jpg');background-repeat:no-repeat\"  url=\"{1}\" />", args[0], args[1], z_index);
             else
                 var html_field = String.format("<div title=\"MobiFlex Map\" alt=\"MobiFlex Map\" id=\"{0}\" style=\"{2}\"  url=\"{1}\" />", args[0], args[1], args[2], z_index);
            
             DoAddToCanvas(html_field, args[0]);

             $("#map_dialog").dialog("close"); 
     }
     function ParseMap(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'url');
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };


     //-----------------------------------------------------------
     function InsertSpeechRecognitionOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(3);
          }
      }
     function InsertSpeechRecognitionCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             if (args[2] == null || args[2].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex SpeechRecognition\" alt=\"MobiFlex Speech Recognition\" id=\"{0}\" style=\"position:absolute;z-index:{2};top:44px;left:0px;height:416px;width:320px;\" choice_title=\"{1}\"  ><img src=\"images/editor_images/speech_recognition.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], z_index);
            else
                var html_field = String.format("<div title=\"MobiFlex SpeechRecognition\" alt=\"MobiFlex Speech Recognition\" id=\"{0}\" style=\"{2}\" choice_title=\"{1}\"  ><img src=\"images/editor_images/speech_recognition.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2]);
            
             DoAddToCanvas(html_field, args[0]);

             $("#speech_reco_dialog").dialog("close"); 
     }
     function ParseSpeechRecognition(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'choice_title');
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };

     //------------------------------------------------------------
     function InsertButtonOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
              dialog_input_args = new Array(5);
             var z_index = getNextZIndex();
             dialog_input_args[3] = String.format("position:absolute;z-index:{0};font-family:Verdana;font-size:16px;color:#ffffff;font-style:normal;font-weight:normal;text-decoration:none;width:144px;height:30px;top:160px;left:10px;", z_index);
         }
         $("#button_dialog").dialog("open"); 
         addIFrame($("#button_dialog"), 'EditorTools/InsertButton.aspx');
      }
     function InsertButtonCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();

             //get value for top that places the text vertically in the middle
             //get font size
             var start = args[3].indexOf("font-size") + 10;
             var end = args[3].indexOf("px", start);
             var font_size = args[3].substring(start, end);

             start = args[3].indexOf("height") + 7;
             var end = args[3].indexOf("px", start);
             var button_height = args[3].substring(start, end);
             var top = (-button_height / 2) - (5 * font_size/3 );
 
             var html_field = String.format("<div title=\"MobiFlex Button\" style=\"{3}\" id=\"{0}\" align=\"center\"  submit=\"{1}\"><img src=\"{4}\" style=\"width:100%;height:100%\" /><p style=\"position:relative;top:{5}px;\">{2}</p></div>", args[0], args[1], args[2], args[3],args[4],top);
             DoAddToCanvas(html_field, args[0]);

             $("#button_dialog").dialog("close"); 
     }
     function ParseButton(html) {
         var inputs = new Array(5);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'submit');
         inputs[2] = GetPInnerHtml(html);
         inputs[3] = GetStyleAttribute(html);
         inputs[4] = GetImgSrc(html);
         return inputs;
     };
     //----------------------------------------------------------
     function InsertImageButtonOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
             var z_index = getNextZIndex();
             dialog_input_args[3] = String.format("position:absolute;z-index:{0};top:160px;left:10px;height:100px;width:100px", z_index);
         }
         $("#image_button_dialog").dialog("open"); 
         addIFrame($("#image_button_dialog"), 'EditorTools/InsertImageButton.aspx');
     }
     function InsertImageButtonCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             var html_field = String.format("<div title=\"MobiFlex ImageButton\"  style=\"{3}\" id=\"{0}\"  submit=\"{2}\"><img src=\"{1}\" alt=\"button\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);
             DoAddToCanvas(html_field, args[0]);

             $("#image_button_dialog").dialog("close"); 
     }
    function ParseImageButton(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetImageSource(html);
         inputs[2] = GetAttribute(html, 'submit');
         inputs[3] = GetStyleAttribute(html);
         return inputs;
     };
     //---------------------------------------------
     function InsertSelectOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
         }
      }
     function InsertSelectCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             if (args[3] == null || args[3].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex Select\" style=\"position:absolute;z-index:{3};top:160px;left:10px;width: 100px; height: 22px;font-family: tahoma; font-size: 12px;\" id=\"{0}\" type=\"{1}\" >{2}</div>", args[0], args[1], args[2],z_index);
             else
                 var html_field = String.format("<div title=\"MobiFlex Select\" style=\"{3}\" id=\"{0}\" type=\"{1}\" >{2}</div>", args[0], args[1], args[2], args[3]);
             
             DoAddToCanvas(html_field, args[0]);

             $("#select_dialog").dialog("close"); 
     }
     function ParseSelect(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'type');
         inputs[2] = GetOptions(html);
         inputs[3] = GetStyleAttribute(html);
         return inputs;
     };

     //----------------------------------------------------
     function InsertPickerOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
         }
         $("#picker_dialog").dialog("open"); 
         var appType = getAppType();
         if (appType == 'web' || appType=='hybrid')
             addIFrame($("#picker_dialog"), 'EditorTools/InsertWebAppPicker.aspx');
          else
             addIFrame($("#picker_dialog"), 'EditorTools/InsertPicker.aspx');
     }
     function InsertPickerCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
             var parts = args[1].split(':');
             if (args[2] != null){
                  args[2] = encodeHtml(args[2]);
             }
              var html_field = null;
              var appType = getAppType();
             if (args[3] == null || args[3].length == 0) {
                 if (parts[0].indexOf('date') >= 0) {                     
                     if (appType == 'web' || appType == 'hybrid')
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:5px;width:310px; height:40px;background-image:url('images/editor_images/spinner.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"\"  />", args[0], args[1], z_index);
                     else
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/datepicker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"\"  />", args[0], args[1], z_index);
                 }
                 else if (parts[0].indexOf('time') >= 0) {
                     if (appType == 'web' || appType == 'hybrid')
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:5px;width:310px; height:40px;background-image:url('images/editor_images/spinner.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"\" />", args[0], args[1], z_index);
                     else 
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/time_picker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"\" />", args[0], args[1], z_index);
                 }
                 else if (parts[0].indexOf('1_section') >= 0) {

                     if (appType == 'web' || appType == 'hybrid')
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:5px;width:310px; height:40px;background-image:url('images/editor_images/spinner.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], z_index);
                     else
                         html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/1section_picker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], z_index);
                 }
                 else if (parts[0].indexOf('2_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/2section_picker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], z_index);
                 else if (parts[0].indexOf('3_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/3section_picker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], z_index);
                 else if (parts[0].indexOf('4_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/4section_picker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], z_index);
             }
             else {
                  if (parts[0].indexOf('date') >= 0)
                       html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{2}\" id=\"{0}\"  type=\"{1}\" options=\"\"  />", args[0], args[1], args[3]);
                 else if (parts[0].indexOf('time') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{2}\" id=\"{0}\"  type=\"{1}\" options=\"\"  />", args[0], args[1], args[3]);
                 else if (parts[0].indexOf('1_section') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{3}\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], args[3]);
                 else if (parts[0].indexOf('2_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{3}\" id=\"{0}\"  type=\"{1}\" options=\"{2}\"  />", args[0], args[1], args[2], args[3]);
                 else if (parts[0].indexOf('3_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{3}\" id=\"{0}\"  type=\"{1}\" options=\"{2}\"  />", args[0], args[1], args[2], args[3]);
                 else if (parts[0].indexOf('4_sections') >= 0)
                      html_field = String.format("<div title=\"MobiFlex Picker\" style=\"{3}\" id=\"{0}\"  type=\"{1}\" options=\"{2}\" />", args[0], args[1], args[2], args[3]);
             }
             
             DoAddToCanvas(html_field, args[0]);

             $("#picker_dialog").dialog("close"); 
     }
     function ParsePicker(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'type');
         inputs[2] = decodeHtml(GetAttribute(html, 'options'));
         inputs[3] = GetStyleAttribute(html);
         return inputs;
     };

     //-----------------------------------------------------
     function InsertPhotoOpen(inputs) {
          
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(4);
          }
          $("#photo_dialog").dialog("open"); 
          addIFrame($("#photo_dialog"), 'EditorTools/InsertPhotoTaker.aspx');
     }
     function InsertPhotoCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
             if (args[3] == null || args[3].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex Photo\" id=\"{0}\" compression=\"{1}\"  icon_field=\"{2}\" style=\"position:absolute;z-index:{2};top:160px;left:10px;height:48px;width:48px;\"  ><img src=\"images/editor_images/picture_taker.gif\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], z_index);
            else
                var html_field = String.format("<div title=\"MobiFlex Photo\" id=\"{0}\" compression=\"{1}\" icon_field=\"{2}\" style=\"{3}\"  ><img src=\"images/editor_images/picture_taker.gif\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2], args[3]);
             
             DoAddToCanvas(html_field, args[0]);

             $("#photo_dialog").dialog("close"); 
     }
     function ParsePhotoTaker(html) {
         var inputs = new Array(4);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'compression');
         inputs[2] = GetAttribute(html, 'icon_field');
         inputs[3] = GetStyleAttribute(html);
         return inputs;
     };
     //--------------------------------------------------------------
     function InsertAlertOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(2);
         }
          $("#alert_dialog").dialog("open"); 
          addIFrame($("#alert_dialog"), 'EditorTools/InsertAlert.aspx');
     }
     function InsertAlertCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
              var z_index = getNextZIndex();
              if (args[1] == null || args[1].length == 0)
                  var html_field = String.format("<div title=\"MobiFlex Alert\" id=\"{0}\" style=\"position:absolute;z-index:{1};top:410px;left:270px;height:50px;width:50px;\"  ><img src=\"images/editor_images/alert.png\" style=\"height:100%;width:100%;\"/></div>", args[0], z_index);
            else
                var html_field = String.format("<div title=\"MobiFlex Alert\" id=\"{0}\" style=\"{1}\"  ><img src=\"images/editor_images/alert.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1]);
             
             DoAddToCanvas(html_field, args[0]);

             $("#alert_dialog").dialog("close"); 
     }
     function ParseAlert(html) {
         var inputs = new Array(2);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetStyleAttribute(html);
         return inputs;
     };
     //-------------------------------------------------------------- 
     function InsertGPSOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(2);
         }
         $("#gps_dialog").dialog("open"); 
         addIFrame($("#gps_dialog"), 'EditorTools/InsertGPS.aspx');
     }
     function InsertGPSCallback(args) {        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
              if (args[1] == null || args[1].length == 0)
                  var html_field = String.format("<div title=\"MobiFlex GPS\" id=\"GPS\" alt=\"{0}\" style=\"position:absolute;z-index:{1};top:160px;left:10px;height:30px;width:30px;\"><img src=\"images/editor_images/gps.png\" style=\"height:100%;width:100%;\"/></div>", args[0], z_index);
             else
                 var html_field = String.format("<div title=\"MobiFlex GPS\" id=\"GPS\" alt=\"{0}\" style=\"{1}\"  ><img src=\"images/editor_images/gps.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1]);

             DoAddToCanvas(html_field, "GPS");

             $("#gps_dialog").dialog("close"); 
     }
     function ParseGPS(html) {
         var inputs = new Array(2);
         inputs[0] = GetAttribute(html, 'alt');
         inputs[1] = GetStyleAttribute(html);
         return inputs;
     };

     //--------------------------------------------------------------
     function InsertHiddenFieldOpen(inputs)
     {
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(3);
         }
         $("#hidden_field_dialog").dialog("open"); 
         addIFrame($("#hidden_field_dialog"), 'EditorTools/InsertHiddenField.aspx')
     }
     function InsertHiddenFieldCallback( args) {
              if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
             if (args[1] != null) {
                 //encode html value
                args[1]  = encodeHtml(args[1]) ;
             }
             if (args[2] == null || args[2].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex HiddenField\" id=\"{0}\" value=\"{1}\" style=\"position:absolute;z-index:{2};top:410px;left:0px;height:30px;width:30px;\"  ><img src=\"images/editor_images/hidden_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], z_index );
             else
                 var html_field = String.format("<div title=\"MobiFlex HiddenField\" id=\"{0}\" value=\"{1}\" style=\"{2}\"  ><img src=\"images/editor_images/hidden_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1],args[2]);

             DoAddToCanvas(html_field, args[0]);
             $("#hidden_field_dialog").dialog("close");
     }
     function ParseHiddenField(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = decodeHtml(GetAttribute(html, 'value'));
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };

     //--------------------------------------------------------------
     function InsertAudioOpen(inputs) {
         
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(3);
         }
         $("#audio_dialog").dialog("open"); 
         addIFrame($("#audio_dialog"), 'EditorTools/InsertAudio.aspx');
     }
     function InsertAudioCallback(args) {
        
             
             if (args == null)
                 return;
             deleteSelectedElement();
            var z_index = getNextZIndex();
             if (args[2] == null || args[2].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex Audio\" id=\"{0}\" source=\"{1}\" style=\"position:absolute;z-index:{2};top:410px;left:145px;height:30px;width:30px;\"  ><img src=\"images/editor_images/audio_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1],z_index);
             else
                 var html_field = String.format("<div title=\"MobiFlex Audio\" id=\"{0}\" source=\"{1}\" style=\"{2}\"  ><img src=\"images/editor_images/audio_field.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1], args[2]);
             
             DoAddToCanvas(html_field, args[0]);
             
            $("#audio_dialog").dialog("close");

     }
     function ParseAudio(html) {
         var inputs = new Array(3);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetAttribute(html, 'source');
         inputs[2] = GetStyleAttribute(html);
         return inputs;
     };
     //--------------------------------------------------------------
     function InsertAudioRecorderOpen(inputs) {  
         if (inputs)
             dialog_input_args = inputs;
         else {
             dialog_input_args = new Array(2);
          }
           $("#audio_recorder_dialog").dialog("open"); 
           addIFrame($("#audio_recorder_dialog"), 'EditorTools/InsertAudioRecorder.aspx');
     }
     function InsertAudioRecorderCallback(args) {
        
             
             if (args == null)
                 return;
             deleteSelectedElement();
             var z_index = getNextZIndex();
             if (args[1] == null || args[1].length == 0)
                 var html_field = String.format("<div title=\"MobiFlex AudioRecorder\" id=\"{0}\" style=\"position:absolute;z-index:{1};top:160px;left:10px;height:84px;width:198px;\"  ><img src=\"images/editor_images/audio_recorder.png\" style=\"height:100%;width:100%;\"/></div>", args[0], z_index);
            else
                var html_field = String.format("<div title=\"MobiFlex AudioRecorder\" id=\"{0}\" style=\"{1}\"  ><img src=\"images/editor_images/audio_recorder.png\" style=\"height:100%;width:100%;\"/></div>", args[0], args[1]);
            
             DoAddToCanvas(html_field, args[0]);

             $("#audio_recorder_dialog").dialog("close"); 
     }
     function ParseAudioRecorder(html) {
         var inputs = new Array(2);
         inputs[0] = GetAttribute(html, 'id');
         inputs[1] = GetStyleAttribute(html);
        return inputs;
     };
     //--------------------------------------------------------------
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
     function SaveAppPage() {
         var PageName = document.getElementById("PageName");
         var value = PageName.value;
         if (value.length > 0 && !IsValidObjectName(value)) {
             alert('Page Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
             return;
         }
         else {
             var SaveAppPost = document.getElementById("SaveAppPost");
             getCanvasHtml();
             SaveAppPost.click();
             document.getElementById('storyBoard').src = document.getElementById('storyBoard').src 
         }
     }

     function checkStoryBoardRefresh() {
         var SavedCanvasHtml = document.getElementById("SavedCanvasHtml");
         if(SavedCanvasHtml.value.length > 0)
             document.getElementById('storyBoard').src = document.getElementById('storyBoard').src
     }

     function xmlEncode(string) {
         return string.replace(/\&/g, '&' + 'amp;').replace(/</g, '&' + 'lt;')
        .replace(/>/g, '&' + 'gt;').replace(/\'/g, '&' + 'apos;').replace(/\"/g, '&' + 'quot;');
     }

    function DoAddToCanvas(html_field,id) {
        var iframe = document.getElementById("canvas");
        iframe.contentWindow.insertHtml(html_field, id);
    }
 
     function getCanvasHtml() {
         var iframe = document.getElementById("canvas");
         var form1 = iframe.contentDocument.getElementById("form1");
         var SavedCanvasHtml = document.getElementById("SavedCanvasHtml");
         SavedCanvasHtml.value = xmlEncode(form1.innerHTML);
     }

     var shouldHideTools = false;
     function onWebAppsDesignAjaxResponseEnd(sender, args) {
         if (shouldHideTools)
             shouldHideTools = false;
         else {
             var AppName = document.getElementById("AppName");
             var edit_tools = document.getElementById("edit_tools");
             if (AppName && AppName.value != '') 
                  edit_tools.style.display = "inline";
             
             else
                 edit_tools.style.display = "none";
         }
     }

     function onWebAppsDesignAjaxRequestStart(sender, args) {
         if (args.EventTarget == 'DisplayMode') {
             var DisplayMode = document.getElementById("DisplayModeButton");
             var selectedToggleStateIndex = DisplayMode.control.get_selectedToggleStateIndex();
             var edit_tools = document.getElementById("edit_tools");
             if (selectedToggleStateIndex == 1) {
                 getCanvasHtml();
                 edit_tools.style.display = "none";
                 shouldHideTools = true;
             }
             else {
                 edit_tools.style.display = "inline";
             }
         }
     }

     function onChangeAppProperties(appProperties,event) {
         var value = appProperties.get_value();
         switch (value) {
             case 'web_data_sources':
                 setWebDataSources();
                 break;
             case 'app_description':
                 setAppDescription();
                 break;
             case 'page_transition_type':
                 setPageTransitionType();
                 break;
             case 'app_device':
                 setAppDevice();
                 break;
             case 'app_background_color':
                 setAppBackgroundColor();
                 break;
             case 'app_background_image':
                 showSetBackgroundClient();
                 break;
             case 'url_account_identifier':
                 setUrlAccountIdentifier();
                 break;
             case 'app_images':
                 appImages();
                 break;
             case 'on_app_open':
                 onAppOpenSetting();
                 break;
             case 'custom_header_html':
                 setCustomHeaderHtml();
                 break;
              default:
                 return;
         }
         appProperties.trackChanges();
         var comboItem = appProperties.findItemByText('Select ->');
         comboItem.select(); 
         appProperties.commitChanges();
     }  
     function setWebDataSources() {
         $("#web_data_sources_dialog").dialog("open");
         addIFrame($("#web_data_sources_dialog"), 'PageData/WebDataSources.aspx');
     }
     function setWebDataSourcesCallback(web_data_source) {
         var WebDataSource = document.getElementById("WebDataSource");
         WebDataSource.value = web_data_source;
         if (web_data_source.length > 0) {
             var SaveWebDataSource = document.getElementById("SaveWebDataSource");
             SaveWebDataSource.click();
         }
     }

     function editGoogleSpreadsheetDataSource(data_source_id) {
         $("#google_spreadsheet_data_source_dialog").dialog("open");
         addIFrame($("#google_spreadsheet_data_source_dialog"), 'PageData/GoogleSpreadsheetDataSource.aspx?id=' + data_source_id.replaceAll(' ', '%20'));
     }
     function addGoogleSpreadsheetDataSource() {
          $("#google_spreadsheet_data_source_dialog").dialog("open");
          addIFrame($("#google_spreadsheet_data_source_dialog"), 'PageData/GoogleSpreadsheetDataSource.aspx');
          $("#google_spreadsheet_data_source_dialog").dialog({
              close: function (event, data) {
                  $("#manage_page_data_dialog:first").find("#hidden_field_iframe:first").contents().find("#AppDataSourcesPost:first").click();
              }
          });
      }

      function editRSSDataSource(data_source_id) {
          $("#rss_data_surce_dialog").dialog("open");
          addIFrame($("#rss_data_surce_dialog"), 'PageData/RSSDataSource.aspx?id=' + data_source_id.replaceAll(' ', '%20'));
      }
      function addRSSDataSource() {
          $("#rss_data_surce_dialog").dialog("open");
          addIFrame($("#rss_data_surce_dialog"), 'PageData/RSSDataSource.aspx');
      }
 
      function editRESTWebServiceDataSource(data_source_id) {
          $("#rest_web_service_data_source_dialog").dialog("open");
          addIFrame($("#rest_web_service_data_source_dialog"), 'PageData/RESTWebServiceDataSource.aspx?id=' + data_source_id.replaceAll(' ', '%20'));
      }
      function addRESTWebServiceDataSource() {
          $("#rest_web_service_data_source_dialog").dialog("open");
          addIFrame($("#rest_web_service_data_source_dialog"), 'PageData/RESTWebServiceDataSource.aspx');
      }
 
      function editSOAPWebServiceDataSource(data_source_id) {
          $("#soap_web_service_data_source_dialog").dialog("open");
          addIFrame($("#soap_web_service_data_source_dialog"), 'PageData/SOAPWebServiceDataSource.aspx?id=' + data_source_id.replaceAll(' ', '%20'));
      }
      function addSOAPWebServiceDataSource() {
          $("#soap_web_service_data_source_dialog").dialog("open");
          addIFrame($("#soap_web_service_data_source_dialog"), 'PageData/SOAPWebServiceDataSource.aspx');
      }

      function editSQLDatabaseDataSource(data_source_id) {
          $("#sql_database_data_source_dialog").dialog("open");
          addIFrame($("#sql_database_data_source_dialog"), 'PageData/SQLDatabaseDataSource.aspx?id=' + data_source_id.replaceAll(' ', '%20'));
      }
      function addSQLDatabaseDataSource() {
          $("#sql_database_data_source_dialog").dialog("open");
          addIFrame($("#sql_database_data_source_dialog"), 'PageData/SQLDatabaseDataSource.aspx');
      }

      function managePageData() {
          var PageName = document.getElementById("PageName");
          if (PageName && PageName.value != '') {
              $("#manage_page_data_dialog").dialog("option", 'title', 'Manage Page Data for Page: ' + PageName.value);
          }

           $("#manage_page_data_dialog").dialog("open");
          addIFrame($("#manage_page_data_dialog"), 'PageData/ManagePageData.aspx');
      }

      function appImages() {
          $("#app_images_dialog").dialog("open");
          addIFrame($("#app_images_dialog"), 'Dialogs/Design/AppImages.aspx');
      }

      function onAppOpenSetting() {
          $("#on_app_open_dialog").dialog("open");
          addIFrame($("#on_app_open_dialog"), 'Dialogs/Design/OnAppOpen.aspx');
      }
      function setCustomHeaderHtml() {
          $("#custom_header_html_dialog").dialog("open");
          addIFrame($("#custom_header_html_dialog"), 'Dialogs/Design/SetCustomHeaderHtml.aspx');
      }    
      function checkURLIdentifier(sender, args) {
           var UrlAccountIdentifier = document.getElementById("UrlAccountIdentifier");
           if (UrlAccountIdentifier.value.length > 0) {
               var DisplayMode = document.getElementById("DisplayMode");
               DisplayMode.click();
           }
           else
               setUrlAccountIdentifier();
      }
     function setUrlAccountIdentifier() {
         $("#account_identifier_dialog").dialog("open");
         addIFrame($("#account_identifier_dialog"), 'Dialogs/Publish/AccountIdentifier.aspx');
     }
     function setUrlAccountIdentifierCallback(account_identifier) {
         var UrlAccountIdentifier = document.getElementById("UrlAccountIdentifier");
         UrlAccountIdentifier.value = account_identifier;
         if (account_identifier.length > 0) {
             var DisplayMode = document.getElementById("DisplayMode");
             DisplayMode.click();
         }
     }
     function setAppDescription() {
         $("#app_description_dialog").dialog("open");
         addIFrame($("#app_description_dialog"), 'Dialogs/Design/AppDescription.aspx');
     }
     function setPageTransitionType() {
         $("#page_transition_type_dialog").dialog("open");
         addIFrame($("#page_transition_type_dialog"), 'Dialogs/Design/PageTransitionType.aspx');

     }
     function setAppDevice() {    
         $("#app_device_dialog").dialog("open");
         addIFrame($("#app_device_dialog"), 'Dialogs/Design/AppDevice.aspx');
         $("#app_device_dialog").bind("dialogclose", function (event, ui) {
             var SetViewForDeviceTypePost = document.getElementById("SetViewForDeviceTypePost");
             SetViewForDeviceTypePost.click(); 
        });     
     }
  
     function setAppBackgroundColor() {
         $("#app_background_color_dialog").dialog("open");
         addIFrame($("#app_background_color_dialog"), 'Dialogs/Design/AppBackgroundColor.aspx');
     }
     function setAppBackgroundColorCallback() {
        //refresh canvas
         var DisplayMode = document.getElementById("DisplayMode");
         DisplayMode.click();
     }
     function GetTdAttribute(html_input, attribute) {
         var start = html_input.indexOf('<td');
         return GetAttribute(html_input.substring(start), attribute);
     }

     function GetTableAttribute(html_input, attribute) {
         var start = html_input.indexOf('<table');
         return GetAttribute(html_input.substring(start), attribute);
     }

     function GetImgSrc(html_input) {
         var start = html_input.indexOf(' src=\"') + 6;
         var end = html_input.indexOf('\"', start);
         if (end < 0)
             return '';
         return html_input.substring(start, end);
     }

      function GetTextAttribute(html, attribute) {
         //get tag html
         var start = html.indexOf(attribute + '=\"');
         start += attribute.length + 2;
         var end = html.indexOf('\"',start);
         if (end < 0)
             return '';
         return html.substring(start, end);
         
     }

     function GetStyleAttribute(html_input) {
         //isolate just the first element
         var element_end = html_input.indexOf('>');
         if (element_end < 0)
             return "";
         var html = html_input.substring(0, element_end);
         var start = html.indexOf('style="');
         if (start >= 0) {
             start += 7;
             var end = html.indexOf('"', start);
             if (end >= 0) {
                 var output = html.substring(start, end);
                 return output;
             }
             return "";
         }
         else { //certain browsers switch ' for " , so we are going to switch them back at the end of this code
             var start = html.indexOf('style=\'');
             if (start >= 0) {
                 start += 7;
                 var end = html.indexOf('\'', start);
                 if (end >= 0) {
                     var output = html.substring(start, end);
                     return output.replaceAll('"','\'');
                 }
                 return "";
             }
             else
                 return "";
         }
        return "";
    }
    function GetAttribute(html_input, attribute) 
    {
         var html = html_input.replaceAll('  ', ' ').replace(' =', '=').replace('= ','=');
         //get tag html
         var start = html.indexOf(' ');
         start += 1;
         var end = html.indexOf('>',start);
         if (end < 0)
             return '';
         var tag = html.substring(start, end);
         var pieces = tag.split(' ');

         var attributes = new Array(pieces.length); //maximum dimension

         //combine pieces that belong to each attribute
         var j = 0;
         var in_quotes = false;
         var accumulate = '';
         for (var i = 0; i < pieces.length; i++) {
             if(!in_quotes) {
                 var quote_pos = pieces[i].indexOf('"');
                 if (quote_pos >= 0) {
                     //check for 2 quotes
                     var quote_pos2 = pieces[i].lastIndexOf('"');
                     if (quote_pos == quote_pos2) { //1 quote
                         in_quotes = true;
                         accumulate += pieces[i] + ' ';
                     }
                     else { //2 quotes
                         attributes[j++] = pieces[i];
                     }
                 }
                 else {
                     attributes[j++] = pieces[i];
                 }
             }
             else { //in_quotes
                 if (pieces[i].indexOf('"') >= 0) {
                     in_quotes = false;
                     accumulate += pieces[i];
                     attributes[j++] = accumulate.trim();
                     accumulate = '';
                 }
                 else {
                     accumulate += pieces[i] + ' ';
                 }
             }
         }
         
         for ( j = 0; j < attributes.length; j++) {
             if (attributes[j]==null || attributes[j].length == 0)
                 continue;

             var attr = attributes[j].split('=');
             if (attr[0] == attribute) {

                 //get attribute
                 var values = attr[1].split('"');
                 if (values.length == 3)
                     return values[1];
                 else
                     return attr[1];
             }
         }
     }

     function replaceBackgroundImageSource(html, image_source) {
         var start = html.indexOf('images/');
         var end = html.indexOf('.jpg', start);
         if (end < 0)
             return '';
         end +=4;
         var output = html.substring(0, start) + image_source + html.substring(end);
         return output;
     }

     function getMaxZIndex() {
         var html = returnCanvasHtml();
         var max_z = 0;
         var start = 0;
         while(true){
             start = html.toLowerCase().indexOf('z-index:',start);
             if (start < 0)
                 break;
             start += 8;
             var end = html.indexOf(';', start);
             if (end < 0)
                 break;
             var z = parseInt(html.substring(start, end));
             if (z > max_z)
                 max_z = z;
             start = end + 1;
         };
         return max_z;
     }

     function getMinZIndex() {
         var html = returnCanvasHtml();
         var min_z = 1000000;
         var start = 0;
         while (true) {
             start = html.toLowerCase().indexOf('z-index:', start);
             if (start < 0)
                 break;
             start += 8;
             var end = html.indexOf(';', start);
             if (end < 0)
                 break;
             var z = parseInt(html.substring(start, end));
             if (z < min_z)
                 min_z = z;
             start = end + 1;
         };
         return min_z;
     }

     function setZIndex(html, z_index) {
         var start = html.toLowerCase().indexOf('z-index:');
         if (start < 0) {
             start = html.toLowerCase().indexOf('style="');
             if (start < 0)
                 return '';
             else {
                 start += 7;
                 z_index = 'z-index:' + z_index + ';';
                 end = start;
             }
         }
         else {
             start += 8;
             var end = html.indexOf(';', start);
             if (end < 0)
                 return '';
         }
         var tag = html.substring(0, start) + z_index + html.substring(end);
         return tag;
     }

     function GetOptions(html) {
         var start = 0;
         var option_tag = '<option';
         var end_option_tag = '</option>';
         var list = '';
         while (true) {
             var end = html.toLowerCase().indexOf(option_tag, start);
             if (end < 0)
                 break;
             start = end + option_tag.length;

             end = html.indexOf('>', start);
             if (end < 0)
                 break;

             start = end + 1;

             end = html.toLowerCase().indexOf(end_option_tag, start);
             if (end < 0)
                 break;

             list += html.substring(start, end) + '\n';
             start = end + end_option_tag.length;
         };

         return list.substring(0, list.length - 1);

     }

     function GetInnerHtml(html) {
           var start = html.indexOf('>',0) + 1;
           var end = html.indexOf('<',start);
           return html.substring(start, end);
     }
     function GetPInnerHtml(html) {
         var start = html.toLowerCase().indexOf('<p', 0) + 2;
         start = html.indexOf('>', start) + 1;
         var end = html.indexOf('<', start);
         return html.substring(start, end);
     }

     function GetTdInnerHtml(html) {
         var end_tag = '</td';
         var end = html.toLowerCase().indexOf(end_tag);
         var start = html.lastIndexOf('>', end) + 1;
         var list = html.substring(start, end);
         return list;
     }
     function GetHtmlPanelInnerHtml(html) {
         var start = html.indexOf('>', 0) + 1;
         var end = html.length - 6;
         var all_inner_html = html.substring(start, end);
         end = all_inner_html.lastIndexOf('<div');
         end = all_inner_html.lastIndexOf('<div',end-1);
         end = all_inner_html.lastIndexOf('<div',end-1);
         return all_inner_html.substring(0, end);
     }
     function GetImageSource(html) {
         var start = html.toLowerCase().indexOf('src', 0) + 5;
         var end = html.indexOf('"', start);
         var list = html.substring(start, end);
         return list;
     }

    function getHtmlSelection(editor) {
        var elem = getSelectedElement();
        if (!elem || elem.nodeName.toLowerCase() == 'body')
            return '';

        if (elem.nodeName.toLowerCase() == 'p')
            elem = elem.parentNode;

       return  OuterHTML(elem);
 
    }

    function OuterHTML(element) {
        var container = document.createElement("div");
        container.appendChild(element.cloneNode(true));
        return container.innerHTML;
    }

    function encodeHtml(input) {
        return input.replace(/</g, '&' + 'lt;')
        .replace(/>/g, '&' + 'gt;').replace(/\"/g, '``').replace(/\=/g, '~~');
    }

    function decodeHtml(input) {
        if (input) {
            return input.replaceAll('&' + 'lt;', '<')
        .replaceAll('&' + 'gt;', '>').replaceAll('``', '"').replaceAll('~~', '=').replaceAll('&' + 'amp;', '&');
        }
        else
            return null;
    }

    function getSelectedHtml() {
        var iframe = document.getElementById("canvas");
        var prev_selected_html = iframe.contentDocument.getElementById("prev_selected_html");
        if (prev_selected_html == null)
            return null;
        return prev_selected_html.value;
    }

    function getSelectedElement() {
        var iframe = document.getElementById("canvas");
        return iframe.contentWindow.getSelectedElement();
    }

    function copySelectedElement() {
        var iframe = document.getElementById("canvas");
        iframe.contentWindow.copySelectedElement();
    }

    function pasteSelectedElement() {
        var iframe = document.getElementById("canvas");
        iframe.contentWindow.pasteSelectedElement();
    }

    function deleteSelectedElement() {
        var iframe = document.getElementById("canvas");
        iframe.contentWindow.deleteSelectedElement();
    }

    function getNextZIndex() {
        //z index is length of html in canvas, so new items are always in front of old ones
        var iframe = document.getElementById("canvas");
        var form1 = iframe.contentDocument.getElementById("form1");
        return form1.innerHTML.length;
    }

    function returnCanvasHtml() {
        var iframe = document.getElementById("canvas");
        var form1 = iframe.contentDocument.getElementById("form1");
        return form1.innerHTML;
    }

    var isFirstTimeAlertForChangingDeviceView = true;
    function checkChangingDeviceView() {
        if (isFirstTimeAlertForChangingDeviceView) {
            isFirstTimeAlertForChangingDeviceView = false;
            return confirm('Changing the device view will only show fields and background that have been saved. Do you want to continue?');
        }
        else
            return true;
    }

    function checkChangingDeviceDesign() {
            return confirm('Changing the device design will rescale all the pages of this app that have been saved. Do you want to continue?');
        }

        function addIFrame(parentElement, src) {
            var newIFrame = document.createElement('iframe');
            newIFrame.setAttribute('id', 'hidden_field_iframe');
            newIFrame.setAttribute('src', src);
            newIFrame.setAttribute('width', '100%');
            newIFrame.setAttribute('height', '100%');
            newIFrame.setAttribute('marginheight', '0');
            newIFrame.setAttribute('marginwidth', '0');
            newIFrame.setAttribute('scrolling', 'no');
            newIFrame.setAttribute('style', 'border-width:0;');
            parentElement.append(newIFrame);
        }

        function deleteIFrame(parentElement) {
            var newIFrame = document.createElement('iframe');
        }

        function addIFrameString(src) {
            return "<iframe src='" + src + "' width='99%' height='99.4%' marginheight='0' marginwidth='0' scrolling='no' style='border-width:0;' />";
        }

        function isDeviceTypeiPad()
        {
            var DeviceType = document.getElementById('DeviceType');
            return  (DeviceType.value == 'ipad')?true:false;
        }

        function onSetBackgroundClientClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (arg) {
                if (arg == 'upload') {
                    var oWin = radopen('Dialogs/Design/UploadBackgroundImage.aspx', 'SetOwnBackgroundBox');
                    oWin.set_visibleTitlebar(false);
                    oWin.set_visibleStatusbar(false);
                    oWin.set_modal(true);
                    oWin.setSize(500, 150);
                    oWin.moveTo(200, 150);
                    oWin.add_close(onSetBackgroundClientClose);
                    return false;
                }
                else {
                    var Background = document.getElementById("Background");
                    if (Background.value == '' && arg != '') {
                        Background.value = arg;
                        var SetBackgroundPost = document.getElementById("SetBackgroundPost");
                        getCanvasHtml();
                        SetBackgroundPost.click();
                    }
                }
            }
        }

        function showSetBackgroundClient() {
            var oWin = radopen('Dialogs/CoverFlow/BackgroundIndex.html', 'SetBackgroundBox');
            oWin.set_visibleTitlebar(true);
            oWin.set_visibleStatusbar(false);
            oWin.set_modal(true);
            oWin.setSize(1024, 580);
            oWin.moveTo(50, 50);
            oWin.add_close(onSetBackgroundClientClose);
            return false;
        }

        function getAppType() {
            var AppType = document.getElementById("AppType");
            if (AppType == null)
                return "native";
            else
                return AppType.value;
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