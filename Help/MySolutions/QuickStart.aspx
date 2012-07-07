<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuickStart.aspx.cs" Inherits="Help_QuickStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ViziApps Quick Start</title>
</head>
<body style="font-family:Verdana;font-size:14px;">
    <form id="form1" runat="server">
    <div align="center">
    <iframe width="560" height="315" src="http://www.youtube.com/embed/U5Mb_7leAu8" frameborder="0" allowfullscreen></iframe>
    </div>
<div style="width:700px;">
 <p>
        Here is the fastest way to see ViziApps at work in&nbsp;about a minute. Just 
        follow these steps:</p>
    <ul>
        <li style="height: 52px">Close the dialog with the 4 buttons on it. You will see 
            your account page which will list all the apps you add or create in your 
            account.</li>
        <li style="height: 66px">Add a copy of a template app into your account. 
            To start, click the <strong>Add Template App</strong> button. This will bring up 
            the cover flow of images that show the actual screen shots of the template apps 
            on an iPhone and Android phone.<br />
        </li>
        <li style="height: 47px">Select the screenshots of the template app you want.&nbsp; This will bring up a dialog box 
            with enlarged screenshots that will enable you to create your own name for the app.<br />
        </li>
        <li style="height: 44px">Create a name for the app and click <strong>Add to My 
            Account</strong>. 
            This saves the app to your account.<br />
        </li>
        <li style="height: 82px">Click on the new app in your solutions display to see its 
            design on the <strong>Display Design</strong> page. Later you can edit the app&#39;s 
            field properties on each of the pages. But for now, you are going to see this 
            app running on your smartphone or tablet as is. See 
            thumbnail images of all the pages of the app in the Story Board inset.<br />
        </li>
        <li style="height: 50px">To prepare your app for download, click on <strong>Select 
            App For Testing on Device</strong> near the top of the <strong>Design</strong> page. 
            This selects the app for download to your device.<br />
        </li>
        <li style="height: 58px">Now install ViziApps on your smartphone or tablet: <br />

                <asp:button ID="Button1" runat="server" 
                    Text="Download ViziApps for iPhone or iPad" Width="256px" 
                    PostBackUrl="http://itunes.apple.com/us/app/viziapps/id500576230?ls=1&mt=8"                    
                
                
                onclientclick="alert('When you see ViziApps in the AppStore you will see a description for an example app. ViziApps enables any app to be created from our app Studio and downloaded.');"></asp:button>
                &nbsp;&nbsp;
                <asp:button ID="Button2" runat="server" 
                    Text="Download ViziApps for Android phone or tablet" Width="310px"                     
                    
                
                onclientclick="window.open('DownloadAndroid.htm','name','top=50,left=100,height=350,width=800,resizable=no,scrollbars=no,toolbar=no,status=no');" ></asp:button>

        </li>
        <li style="height: 35px">Start ViziApps on your device. You will see a splash screen followed by a 
            login page.<br />
        </li>
        <li style="height: 38px">Login using the same username and password you used to 
            login to the App Studio.<br />
        </li>
        <li style="height: 41px">That&#39;s it. You should now see the app running on your 
            Apple or Android device.</li>
        <li>If you would like to try a different template app, follow the same steps above. Once provisioned, restart 
            your app 
            on your device and you will see the new app downloaded and running.</li>
    </ul>
    </div>
    </form>
</body>
</html>
