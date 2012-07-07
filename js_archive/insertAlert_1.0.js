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
            var alertName = document.getElementById("alertName");
            alertName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Alert";                
            }

            style = ArgList[1];
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var alertName = document.getElementById("alertName");
            if (alertName.value == null || alertName.value == '') {
                alert('Alert Name must be filled');
                return;
            }

            if (!IsValidObjectName(alertName.value)) {
                alert('Alert Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            alertName.value = alertName.value.replaceAll(" ", "_");

            ArgList[0] = alertName.value;
            ArgList[1] = style;
            parent.window.InsertAlertCallback(ArgList);
        }

