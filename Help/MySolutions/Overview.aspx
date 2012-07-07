<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Overview.aspx.cs" Inherits="Help_Overview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Overview</title>
    <style type="text/css">
        .style1
        {
            color: #000099;
        }
            .config_menu
    {
        height: 55px;
         width: 75px;
         white-space:pre-wrap;
         overflow:visible;
    }
            </style>
             <script  language="javascript" type="text/javascript">
                 function PopUp(url, features) {
                     var PUtest = window.open(url, '_blank', features);
                     if (PUtest == null) {
                         alert('For correct operation, popups need to be allowed from this website.');
                     }
                 }
        </script>   
</head>
<body>
    <form id="form1" runat="server">
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <div style="font-family:Verdana;font-size:14px;width:770px;">
    <p>
        ViziApps enables you to create either native apps or web apps for your mobile 
        devices. A native app can access the camera, GPS, microphone, device contacts, device 
        calendar and is submitted to an app store for publication. A web app runs on the 
        Internet Browser of any smartphone or tablet but cannot access the native 
        resources on the devices that native apps can. The web app is hosted by us and 
        can be distributed the same way you distribute the links of your company website.</p>
        <p>
            <strong>Creating</strong> a mobile <strong>native app</strong> with ViziApps involves the following 
        <strong>design 
        phases</strong>:</p>
        <ul>
            <li style="height:54px;">Before you dive in, we suggest you get an overall quick feel 
                for the process by following the steps you see when you click on <strong>1 
                Minute Quick Start.</strong></li>
            <li style="height:77px;">To start out on designing your app, either choose an app 
                from our templates and modify it for your app or design your app from scratch. 
                It will typically take you hours to days to finish a production quality app.</li>
            <li style="height:126px;">Create the <strong>fields</strong> on each page of 
                your mobile app using 
                the <span class="style1"><strong>Display Design</strong></span> tab. This is like using 
                PowerPoint. You insert, drag and scale any of the 17 fields on a page, including 
                custom images and backgrounds. Create as many pages as you want.<br /> 
                <table style="width: 298px"><tr><td>   
                   <asp:ImageButton ID="LayoutVideo" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                        
                        CssClass="config_menu" ImageUrl="~/images/layout_video_button.png"  
                         OnClientClick= "PopUp('../Design/LayoutVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"

                        />
                    </td><td>
                   <asp:ImageButton ID="BasicFieldsVideo" runat="server" CausesValidation="False"                        
                        CssClass="config_menu" Height="55px" Width="75px" ImageUrl="~/images/basic_fields_video_button.png" 
                         OnClientClick= "PopUp('../Design/BasicFieldsVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"

                        />
                    </td><td>
                   <asp:ImageButton ID="SeeAllFields" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                       
                        CssClass="config_menu" 
                       ImageUrl="~/images/all_fields_button.png" 
                       OnClientClick= "PopUp('../Design/ViewAllNativeFields.htm', 'height=700, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;"
/>
                    </td></tr></table>
            </li>
            <li style="height:86px;">Each field has a property sheet where you can customize the 
                look and/or actions of the fields.&nbsp; For buttons, switches and tables, 
                choose any of 6 actions, including network data access, next page, previous 
                page, share message including facebook, email, SMS, 
                phone call and Excel-like formulas, for when users tap their fingers on these 
                fields.</li>
            <li style="height:74px;">After you finish the first page and save it, we recommend 
                you take advantage of our storyboard feature. This view will show you all the 
                pages that you saved at once while you are working on any one page. It gives a 
                better sense of context and page flow reference. </li>
            <li style="height:98px;">Configure the <strong>data exchange</strong> between your app and your back office 
                using the <span class="style1"><strong>Manage Data</strong></span> tab. You can 
                choose among 3 network data interface types: Google Docs Spreadsheets, direct database 
                access via a web service or you can use a general web service. You can configure 
                the data interface for your app using easy-to-use graphical tools. There is no 
                coding involved and a number of context help to guide you through the process.</li>
            <li style="height:47px;">To see your app run on your device in test mode, you can 
                either select your app for testing in the <span class="style1"><strong>Design</strong></span> 
                Tab or go 
                to the<strong> <span class="style1">Publish</span> tab</strong> and<strong> Select</strong> 
                the app you want to see on your device.</li>
            <li style="height:63px;"><strong>Install</strong> the ViziApps App on your iPhone, 
                iPad or Android.<br />
                <asp:Button ID="DownloadViziAppsiPhone" runat="server" 
                    Text="Download ViziApps for iPhone or iPad" Width="256px" 
                    PostBackUrl="http://itunes.apple.com/us/app/viziapps/id500576230?ls=1&mt=8"  
                        
                    
                    
                    onclientclick="alert('When you see ViziApps in the AppStore you will see a description for an example app. ViziApps enables any app to be created from our app Studio and downloaded.');"/>
                &nbsp;&nbsp;
                <asp:Button ID="DownloadViziAppsAndroid" runat="server" 
                    Text="Download ViziApps for Android phone or tablet" Width="310px"                     
                    
                    onclientclick="window.open('DownloadAndroid.htm','name','top=50,left=100,height=350,width=800,resizable=no,scrollbars=no,toolbar=no,status=no');" />
            </li>
            <li style="height:69px;">To<strong> Download</strong> your design to your phone or 
                tablet and see it working<strong> </strong>in test mode,<strong> login</strong> to 
                the ViziApps app on your device the same way 
                you log into this portal. It will automatically download the app that you 
                selected for testing from the portal.</li>
            <li style="height:82px;">To publish your app in production, buy the publishing 
                service you want and then go to the<strong> <span class="style1">Publish</span> 
                tab, </strong> click on<strong> Publish App </strong> and<strong> </strong> 
                fill in the publication form. Click <strong>Submit</strong> when you 
                are done and we will submit your app with your app name, logo and splash screen 
                to the iPhone Appstore and/or Android Play for distribution to your 
                users.</li>
        </ul>
        <p>
        <strong>Creating</strong> a mobile <strong>web app</strong> with ViziApps involves 
            similar 
        <strong>design 
        phases </strong>but is simpler:</p>
        <ul>
            <li style="height:78px;">Designing your web app with the Studio is similar to 
                designing your native app, but with some different field types. You can see how 
                your web app will finally appear in <strong>Preview Mode</strong>. Then to see 
                it on your phone or tablet, just point the camera of your device with an app 
                that reads QR codes at the QR code image in the Studio and in seconds your web 
                app will appear on your device. </li>
        </ul>
        <p>
            &nbsp;</p>
    </div>
    </form>
    
    </body>
</html>
