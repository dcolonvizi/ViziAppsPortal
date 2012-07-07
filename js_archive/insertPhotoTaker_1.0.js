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

            if(ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Photo Taker";                

                var PhotoTakerName = document.getElementById("PhotoTakerName");
                PhotoTakerName.value = ArgList[0];

                 var compression = document.getElementById("compression");
                 compression.value = ArgList[1];
                 var icon_field = document.getElementById("icon_field");
                 var icon_width = document.getElementById("icon_width");
                 var icon_height = document.getElementById("icon_height");
                 if (ArgList[2] != '') {
                     var parse = ArgList[2].split(';');
                     // "field:" + icon_field.value + ";width:" + icon_width.value + ";height:" + icon_height.value;
                     for (var i = 0; i < parse.length; i++) {
                         var parts = parse[i].split(':');
                         switch(parts[0])
                         {
                             case 'field':
                                 icon_field.value = parts[1];
                                 break;
                             case 'width':
                                 icon_width.value = parts[1];
                                 break;
                             case 'height':
                                 icon_height.value = parts[1];
                                 break;
                         }
                     }
                 }
                 else {
                     icon_field.value = '';
                     icon_width.value = '60';
                     icon_height.value = '80';
                 }
                style = ArgList[3];
            }
        }

        function IsNumeric(input){ 
             return /^-?(0|[1-9]\d*|(?=\.))(\.\d+)?$/.test(input); 
        } 


        function insertButton() //fires when the Insert Link button is clicked
        {
            var PhotoTakerName = document.getElementById("PhotoTakerName");
            if (PhotoTakerName.value == null || PhotoTakerName.value == '') {
                alert('PhotoTaker Name must be filled');
                return;
            }
            if (!IsValidObjectName(PhotoTakerName.value)) {
                alert('PhotoTaker Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            PhotoTakerName.value = PhotoTakerName.value.replaceAll(" ", "_");
            ArgList[0] = PhotoTakerName.value;

            var compression = document.getElementById("compression");
            if (compression.value == null || compression.value == '') {
                alert('Compression field must be filled');
                return;
            }
            if (!IsNumeric(compression.value)) {
                alert('Compression must be between 0.001 to 1.0');
                return;
            }

            var f_compression = parseFloat(compression.value);

            if (f_compression < 0.0001 || f_compression > 1.0) {
                alert('Compression must be between 0.001 to 1.0');
                return;
            }

            ArgList[1] = compression.value;
            var icon_field = document.getElementById("icon_field");
            var icon_width = document.getElementById("icon_width");
            var icon_height = document.getElementById("icon_height");
            if (icon_field.value != '')
                ArgList[2] = "field:" + icon_field.value + ";width:" + icon_width.value + ";height:" + icon_height.value;
            else
                ArgList[2] = "";

           ArgList[3] = style;

           parent.window.InsertPhotoCallback(ArgList);
        }
