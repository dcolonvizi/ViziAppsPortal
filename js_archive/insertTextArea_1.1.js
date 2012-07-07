  
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
                insertButtonID.value = "Update Text Area";                

                var textAreaName = document.getElementById("textAreaName");
                textAreaName.value = ArgList[0];
                var text = document.getElementById("text");
                text.value = ArgList[1].replaceAll('\\n', '\n');

                var buttons = document.getElementsByName("edit_type");
                if(ArgList[3] == 'non_editable')
                {
                    buttons[0].checked = true;
                    buttons[1].checked = false;
                }
                else {
                    buttons[0].checked = false;
                    buttons[1].checked = true;
                }
            }
            style = ArgList[2];
            getStyle();
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var textAreaName = document.getElementById("textAreaName");
            if (textAreaName.value == null || textAreaName.value == '') {
                alert('Text Area Name must be filled');
                return;
            }
            if (!IsValidObjectName(textAreaName.value)) {
                alert('Text Area Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            textAreaName.value = textAreaName.value.replaceAll(" ", "_");


            ArgList[0] = textAreaName.value;

            var text = document.getElementById("text");
            ArgList[1] = text.value;

            ArgList[2] = style;

            var buttons = document.getElementsByName("edit_type");
            if(buttons[0].checked)
                ArgList[3] = 'non_editable';
            else
                ArgList[3] = 'editable';

            parent.window.InsertTextAreaCallback(ArgList);
        }
  
