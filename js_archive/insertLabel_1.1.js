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
                insertButtonID.value = "Update Label";                

                var LabelName = document.getElementById("LabelName");
                LabelName.value = ArgList[0];

                var label = document.getElementById("label");
                label.value = ArgList[1];
            }
            style = ArgList[2];
            getStyle();
        }

        function insertLabel() //fires when the Insert Link Label is clicked
        {
            var LabelName = document.getElementById("LabelName");
            if (LabelName.value == null || LabelName.value == '') {
                alert('Internal Label Name must be filled');
                return;
            }
            if (!IsValidObjectName(LabelName.value)) {
                alert('Label Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            LabelName.value = LabelName.value.replaceAll(" ","_");

            var phone = document.getElementById("phone");
            var link = document.getElementById("link");

            ArgList[0] = LabelName.value;    
 
  
            var label = document.getElementById("label");
            if (label.value == null || label.value == '') {
                alert('Label must be filled');
                return;
            }
 
            ArgList[1] = label.value;
            ArgList[2] = style;
            parent.window.InsertLabelCallback(ArgList);
        }
