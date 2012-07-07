
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
            var textFieldName = document.getElementById("textFieldName");
            textFieldName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Text Field";                

                var field_type = document.getElementById("field_type");
                if (ArgList[1].length == 0) {
                    field_type[0].selected = true;
                }
                else {

                    for (var i = 0; i < field_type.length; i++) {
                        if (field_type[i].value == ArgList[1]) {
                            field_type[i].selected = true;
                        }
                    }
                }
 
                var textFieldDefaultValue = document.getElementById("textFieldDefaultValue");
                if (ArgList[4].length > 0)
                    textFieldDefaultValue.value = ArgList[4];
                else
                    textFieldDefaultValue.value = '';
            }
            style = ArgList[3];
            getStyle();
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var textFieldName = document.getElementById("textFieldName");
            if (textFieldName.value == null || textFieldName.value == '') {
                alert('Text Field Name must be filled');
                return;
            }

            if (!IsValidObjectName(textFieldName.value)) {
                alert('Text Field Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            textFieldName.value = textFieldName.value.replaceAll(" ", "_");
            ArgList[0] = textFieldName.value;

            var field_type = document.getElementById("field_type");
            ArgList[1] = field_type.value;

            ArgList[2] = 'keyboard: ;validation: ';
            ArgList[3] = style;
            var textFieldDefaultValue = document.getElementById("textFieldDefaultValue");
            if (textFieldDefaultValue.value.length > 0) {
                 ArgList[4] = textFieldDefaultValue.value;
            }
            else
                ArgList[4] = null;
            parent.window.InsertTextFieldCallback(ArgList);
        }
