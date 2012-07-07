        var combo;
        var do_select = false;
        var selected_item;
        function OnClientValueTypeLoadHandler(sender) {
            combo = sender;
        }

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
            var HiddenName = document.getElementById("HiddenName");
            HiddenName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Hidden Field";

                selected_item = ArgList[1];

                if (selected_item == "mobile_device_id" ||
                    selected_item == "mobile_device_model" ||
                     selected_item == "mobile_system_version") {
                    if (combo.get_value() != selected_item) {
                        combo.trackChanges();
                        var item = combo.findItemByValue(selected_item);
                        if (item) {
                            item.select();
                        }
                        combo.commitChanges();
                    }
                    var default_value = document.getElementById("default_value");
                    default_value.value = '';
                }
                else {
                    var default_value = document.getElementById("default_value");
                    default_value.value = ArgList[1];
                    combo.trackChanges();
                    var item = combo.findItemByValue("default_value");
                    if (item) {
                        item.select();
                    }
                    combo.commitChanges();
                }
            }

            style = ArgList[2];
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var HiddenName = document.getElementById("HiddenName");
            if (HiddenName.value == null || HiddenName.value == '') {
                alert('Hidden Name must be filled');
                return;
            }
            if (!IsValidObjectName(HiddenName.value)) {
                alert('Hidden Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            HiddenName.value = HiddenName.value.replaceAll(" ", "_");

            ArgList[0] = HiddenName.value;
            selected_item = combo.get_value();
            if (selected_item == 'default_value') {
                var default_value = document.getElementById("default_value");
                ArgList[1] = default_value.value;
            }
            else
                ArgList[1] = selected_item;

            ArgList[2] = style;

            parent.window.InsertHiddenFieldCallback(ArgList);
        }
