function setStoryBoardWindow(popup) {
    storyBoardWindow = popup;
}

function getStoryBoardWindow() {
    try{
        return storyBoardWindow;
    }
    catch(ex){
         return null;
    }
 }

function NamedPopUp(url, name, features) {
    try {
        if (isMyPopUpWindowOpen()) {
            closeMyPopUpWindow();
        }
        var MyPopUpWindow = window.open(url, name, features);
        if (MyPopUpWindow == null) {
            alert('For correct operation, popups need to be allowed from this website.');
            return;
        }
        setStoryBoardWindow(MyPopUpWindow);
    }
    catch (err) {
        var txt = "There was an error on this page.\n\n";
        txt += "Error description: " + err.description + "\n\n";
        txt += "Click OK to continue.\n\n";
        alert(txt);
    }
}

function isMyPopUpWindowOpen() {
    var MyPopUpWindow = getStoryBoardWindow();
    return (MyPopUpWindow && MyPopUpWindow.value != '' && !MyPopUpWindow.closed);
}

function closeMyPopUpWindow() {
    var MyPopUpWindow = getStoryBoardWindow();
    MyPopUpWindow.close();
    setStoryBoardWindow(null);
}

function refreshMyPopUpWindow() {
    if (isMyPopUpWindowOpen()) {
        var MyPopUpWindow = getStoryBoardWindow();
        var form1 = MyPopUpWindow.document.getElementById("form1");
        form1.submit();
    }
}

function StoryBoardUnloading() {
    setStoryBoardWindow(null);
}

