  
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

            var latitude = document.getElementById("latitude");
            var longitude = document.getElementById("longitude");

            if (ArgList[0] == '') {
                latitude.value = 'latitude';
                longitude.value = 'longitude';
            }
            else {
                 var parts = ArgList[0].split(';');
                latitude.value = parts[0];
                longitude.value = parts[1];
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update GPS";
            }

            style = ArgList[1];
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var latitude = document.getElementById("latitude");
            if (latitude.value == null || latitude.value == '') {
                alert('latitude Name must be filled');
                return;
            }

            var longitude = document.getElementById("longitude");
            if (longitude.value == null || longitude.value == '') {
                alert('longitude Name must be filled');
                return;
            }
            if (!IsValidObjectName(latitude.value)) {
                alert('Latitude Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            if (!IsValidObjectName(longitude.value)) {
                alert('Longitude Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            latitude.value = latitude.value.replaceAll(" ", "_");
            longitude.value = longitude.value.replaceAll(" ", "_");

            ArgList[0] = latitude.value + ';' + longitude.value;
            ArgList[1] = style;

            parent.window.InsertGPSCallback(ArgList);
        }
 