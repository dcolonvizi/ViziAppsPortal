    if (window.attachEvent)
    {
        window.attachEvent("onload", initDialog);
    }
    else if (window.addEventListener)
    {
        window.addEventListener("load", initDialog, false);
    }

    var Arg = null;

    function initDialog() {

 
    }

    function ImageManagerFunction(sender, args) {
        if (args == null)
            return;
        var selectedItem = args.get_value();

        var txt = document.getElementById("imagePath");
        if ($telerik.isIE) {
            var image_path = selectedItem.outerHTML;  //this is the selected IMG tag element
            var start = image_path.toLowerCase().indexOf("src=\"");
            start += 5;
            var end = image_path.toLowerCase().indexOf("\"",start);
            txt.value = image_path.substring(start,end);
            
        }
        else {
            var path = args.value.getAttribute("src", 2);
            txt.value = path ;
        }
        var SaveToAccount = document.getElementById("SaveToAccount");
        SaveToAccount.click();
    }  
    function insertButton() //fires when the Insert Link button is clicked
    {
        var imagePath = document.getElementById("imagePath");
        if (imagePath.value == null || imagePath.value == '') {
           alert('No image URL');
           return;
       }
      /* var ImageWidth = document.getElementById("ImageWidth");
       if (ImageWidth.value.length > 0)
           setStyle("width", ImageWidth.value + "px");
       var ImageHeight = document.getElementById("ImageHeight");
       if (ImageHeight.value.length > 0)
           setStyle("height", ImageHeight.value + "px");*/
       window.opener.InsertImageInHtmlPanelCallback(imagePath.value);
       window.close();
   }
   function onSetImageClientClose(sender, eventArgs) {
       var arg = eventArgs.get_argument();
       if (arg) {
           var imagePath = document.getElementById("imagePath");
           imagePath.value = arg[0];
           var ImageWidth = document.getElementById("ImageWidth");
           ImageWidth.value = arg[1];
           var ImageHeight = document.getElementById("ImageHeight");
           ImageHeight.value = arg[2];
       }
   }

   function showSetImageClient() {
       var oWin = radopen('../Dialogs/Design/IconImage.aspx', 'imageWindow');
       oWin.set_visibleTitlebar(true);
       oWin.set_visibleStatusbar(false);
       oWin.set_modal(true);
       oWin.maximize();
       oWin.moveTo(0, 0);
       oWin.add_close(onSetImageClientClose);
       return false;
   }
   function onUploadImageClientClose(sender, eventArgs) {
       var arg = eventArgs.get_argument();
       if (arg) {
           var imagePath = document.getElementById("imagePath");
           imagePath.value = arg[0];
           var ImageWidth = document.getElementById("ImageWidth");
           ImageWidth.value = arg[1];
           var ImageHeight = document.getElementById("ImageHeight");
           ImageHeight.value = arg[2];
       }
   }

   function showUploadImageClient() {
       var oWin = radopen('../Dialogs/Design/UploadImage.aspx', 'imageWindow');
       oWin.set_visibleTitlebar(false);
       oWin.set_visibleStatusbar(false);
       oWin.set_modal(true);
       oWin.setSize(500, 150);
       oWin.moveTo(50, 50);
       oWin.add_close(onUploadImageClientClose);
       return false;
   }
 