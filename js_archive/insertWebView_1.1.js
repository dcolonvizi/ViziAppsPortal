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
            var webViewName = document.getElementById("webViewName");
            webViewName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Web View";
            }

            var url = document.getElementById("url");
            url.value = ArgList[1];
            style = ArgList[2];
            setLocationFromStyle(style);
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var webViewName = document.getElementById("webViewName");
            if (webViewName.value == null || webViewName.value == '') {
                alert('Web View Name must be filled');
                return;
            }

            if (!IsValidObjectName(webViewName.value)) {
                alert('Web View Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            webViewName.value = webViewName.value.replaceAll(" ", "_");

            ArgList[0] = webViewName.value;

            var url = document.getElementById("url");

            ArgList[1] = url.value;
            ArgList[2] = setStyleFromLocation(style);
            parent.window.InsertWebViewCallback(ArgList);
        }
 