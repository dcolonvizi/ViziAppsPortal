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

            var audioName = document.getElementById("audioName");
            if (audioName.value.length == 0)
                audioName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var audioPath = document.getElementById("audioPath");
                audioPath.value = ArgList[1];
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Audio";         
            }
            style = ArgList[2];
        }

        function audioManagerFunction(sender, args) {
            if (args == null)
                return;
            var selectedItem = args.get_value();

            var txt = document.getElementById("audioPath");
            if ($telerik.isIE) {
                var audio_path = selectedItem.outerHTML;  //this is the selected IMG tag element
                var start = audio_path.toLowerCase().indexOf("source=\"");
                start += 5;
                var end = audio_path.toLowerCase().indexOf("\"", start);
                txt.value = audio_path.substring(start, end);

            }
            else {
                var path = args.value.getAttribute("source", 2);
                txt.value = path;
            }
            var SaveToAccount = document.getElementById("SaveToAccount");
            SaveToAccount.click();
        }
        function insertButton() //fires when the Insert Link button is clicked
        {
            var audioName = document.getElementById("audioName");
            if (audioName.value == null || audioName.value == '') {
                alert('Audio Name must be filled');
                return;
            }

            if (!IsValidObjectName(audioName.value)) {
                alert('audio Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            audioName.value = audioName.value.replaceAll(" ", "_");
            ArgList[0] = audioName.value;


            var audioPath = document.getElementById("audioPath");
            if (audioPath.value != null && audioPath.value != '') {
                ArgList[1] = audioPath.value;
            }
 
            ArgList[2] = style;
            parent.window.InsertAudioCallback(ArgList);
        }
        function onSetAudioClientClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (arg) {
                var audioPath = document.getElementById("audioPath");
                audioPath.value = arg[0];
             }
        }
  
        function onUploadAudioClientClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (arg) {
                var audioPath = document.getElementById("audioPath");
                audioPath.value = arg[0];
               /* var audio_ref = document.getElementById("audio_ref");
               var attr= audio_ref.getAttributeNode("href");
               attr.value = audioPath.value;*/
             }
        }

        function showUploadAudioClient() {
            var oWin = radopen('../Dialogs/Design/UploadAudio.aspx', 'audioWindow');
            oWin.set_visibleTitlebar(false);
            oWin.set_visibleStatusbar(false);
            oWin.set_modal(true);
            oWin.setSize(500, 150);
            oWin.moveTo(50, 50);
            oWin.add_close(onUploadAudioClientClose);
            return false;
        }

        function playAudio() {
            var audioPath = document.getElementById("audioPath");
            if (audioPath.value == '')
                return;
            PopUp(audioPath.value, 'width=400, height=100, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');
        }
  