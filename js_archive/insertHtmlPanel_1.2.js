  
        if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        var ArgList = null;

        function initDialog() {
            ArgList = parent.window.getDialogInputArgs();
            for (var i = 0; i < ArgList.length; i++) {
                if (ArgList[i] == null)
                    ArgList[i] = '';
            }
            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Html Panel";                

                var htmlPanelName = document.getElementById("htmlPanelName");
                htmlPanelName.value = ArgList[0];
                var editor = document.getElementById("editor");
                editor.control.set_html(ArgList[1]);
            }
            style = ArgList[2];
            setLocationFromStyle(style);
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var htmlPanelName = document.getElementById("htmlPanelName");
            if (htmlPanelName.value == null || htmlPanelName.value == '') {
                alert('Html Panel Name must be filled');
                return;
            }
            if (!IsValidObjectName(htmlPanelName.value)) {
                alert('Html Panel Name can only contain either a letter, number, space or "_" and be 1 to 50 characters long');
                return;
            }
            htmlPanelName.value = htmlPanelName.value.replaceAll(" ", "_");


            ArgList[0] = htmlPanelName.value;

            var editor = document.getElementById("editor");
            ArgList[1] = editor.control.get_html(false);

            ArgList[2] = setStyleFromLocation(style);
            parent.window.InsertHtmlPanelCallback(ArgList);
        }

        var dialog_input_args;
        function getDialogInputArgs() {
            return dialog_input_args;
        }

        $(function () {
            // a workaround for a flaw in jquery
            $("#dialog:ui-dialog").dialog("destroy");

            $("#html_panel_insert_image_dialog").dialog({ autoOpen: false, width: 770, height: 600, modal: true, beforeClose: function (event, ui) { $(this).children().first().remove(); } });
            $("#html_panel_insert_image").button().click(function () { InsertHtmlPanelImageOpen(null); });

            $('.InsertMyImage').css("background-image", " url('../images/editor_images/image_icon.png')");

            $('.viziapps-field-location').keydown(function (e) {
                var code = e.keyCode;
                if (code == '13') {
                    e.preventDefault();
                    e.stopPropagation();
                }
            });
        });

        function InsertHtmlPanelImageOpen(inputs) {
            PopUp('InsertImageInHtmlPanel.aspx', 'height=300, width=770, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
        }
 