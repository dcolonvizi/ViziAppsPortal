<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabFAQ.aspx.cs" Inherits="TabFAQ" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps: Build Mobile Apps Online</title>
    		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <meta http-equiv="Expires" content="-1"/>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
        <style type="text/css">
             body
        {
        	 background-color:#bcbcbc;
        }
#accordion { width:700px; margin: 0; padding: 10; }
#accordion p span { cursor:pointer; }

.news-title{
	color:#1e6e86;
	font: bold 14px Arial;
	margin-bottom:10px;
	background:#EDEDED;
	padding:5px 10px;
	}
.news_text{
	font: 14px Arial;
	margin:10px;
	line-height:18px;
	}
.title_text{
	font: bold 20px Arial;
	}	

</style>
  <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
  <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script> 

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>

<script type="text/javascript">
<!--    http: //www.itechroom.com-->
    $(document).ready(function ($) {
        $('#accordion div').hide();
        $('#accordion p span').click(function () {
            $('#accordion div').slideUp();
            $(this).parent().next().slideDown();
            return false;
        });
    });

</script>
</head>
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
     <div align="center" id="header" style="height:60px;  background-color:#0054c2;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                </td>
               
                <td class="style45">
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White;font-family:verdana;font-size:12px;"></asp:Label>
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center">
                    <asp:ImageButton ID="SupportButton" runat="server"  
                        ImageUrl="~/images/SupportButton.png" TabIndex="1000"  style=""/>
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    <asp:ImageButton ID="LogoutButton" runat="server"  
                        ImageUrl="~/images/LogoutButton.png" onclick="LogoutButton_Click" 
                        TabIndex="2000" style=""/>
                </td>
                <td>               
                </td>
                </tr>
                </table>
                </div>  
                  <div align="center" style="width:100%;height: 30px">
               <table border="0" cellpadding="0" cellspacing="0" id="tabs"  style="width:100%;height:30px; border:0px; padding:0px;  vertical-align:top;  margin:0px; background-image:url(images/tabs_section.gif); background-repeat:repeat-x  ">
                    <tr><td align="center" valign="top">
                     <div align="center">
                      
                     <table border="0" cellpadding="0" cellspacing="0" style="height:30px;" ><tr>
                     <td >
                       
                      <telerik:RadMenu ID="TabMenu" runat="server" Skin=""
                                  onitemclick="TabMenu_ItemClick"                                    
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100; top: 0px; left: 0px;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1200"/>
                             <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" TabIndex="1300" ><Items>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="DesignNative"/>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="DesignWeb"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="DesignHybrid"/>
                                 </Items>
                             </telerik:RadMenuItem>

                            <telerik:RadMenuItem ImageUrl="~/images/ProvisionButton.png" HoveredImageUrl="~/images/ProvisionButton_hov.png"
                        SelectedImageUrl="~/images/ProvisionButton_sel.png"  Value="PublishOld" TabIndex="1500"/>
                            <telerik:RadMenuItem ImageUrl="~/images/FAQButton.png" HoveredImageUrl="~/images/FAQButton_hov.png"
                        SelectedImageUrl="~/images/FAQButton_sel.png"  Value="FAQ" Selected="true" TabIndex="1600"/>
                       <telerik:RadMenuItem ImageUrl="~/images/MyProfileButton.png" HoveredImageUrl="~/images/MyProfileButton_hov.png"
                        SelectedImageUrl="~/images/MyProfileButton_sel.png"  Value="MyProfile" TabIndex="1700"/>
                       

                         </Items>
                          </telerik:RadMenu>
                      
                       
                          </td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>  
                <div style="height:15px"></div>
                <div align="center" style="vertical-align:top">
                <table style="vertical-align:top">
                <tr>
                <td style="vertical-align:top;">
                <div class="title_text" 
                        style=" text-align:left;  color: #006699; height: 33px; width:300px; background-color:#ffffff;padding-top:5px;padding-left:5px;"><table  style=" text-align:left;"><tr><td>
                        <img alt="" src="images/video_icon.png" height="28" width="28" /></td><td></td><td>ViziApps Videos</td></tr></table></div>
                <div style="background-color:#ffffff;vertical-align:top; text-align:left;width:300px;font-family:Verdana;font-size:14px;color: #006699;">               
                <p style="margin:10px;"><a style=" text-decoration:none" href="http://www.youtube.com/user/ViziApps">Click here to see all of our tutorial videos for ViziApps Studio on our YouTube Channel.</a></p>
                <hr style=" line-height:1px; margin:0 10px 0 10px;color:#eeeeff; "/>
                                <div style="height:10px"></div>             
                </div>
                </td>
                <td style="vertical-align:top" bgcolor="Silver">
                    &nbsp;</td>
                <td style="vertical-align:top">
               <div class="title_text" 
                        style=" text-align:left;  color: #006699; height: 33px; width:500px; background-color:#ffffff;padding-top:5px;padding-left:5px;"><table  style=" text-align:left;"><tr><td>
                        <img alt="" src="images/faq_icon.png" height="28" width="28" /></td><td></td><td>ViziApps FAQs</td></tr></table></div>
    <div id="accordion" style="text-align:justify;background-color:#ffffff;width:500px">
<p class="news-title"><span>What Features Does ViziApps Have for My Mobile Apps?</span></p>
<div class="news_text">
<strong>What Features Does ViziApps Have for My Mobile Apps?</strong>:
    <br />
    With ViziApps you can:<br />
    <ul><li>Create as many pages as you like.</li>
        <li>Put any of our 18 fields anywhere on a page.</li>
        <li>Re-size, re-format, re-position and edit all fields in many ways</li>
        <li>Access  web data sources easily without coding including Google Spreadsheets, web services and databases.</li>
    </ul>
<ul>
<li>Start actions from button taps. The following are available:
<ul><li>Go to a page</li>
<li>If/Then Go to a page</li>
<li>Go to a previous page</li>
<li>Get or send device data via a web data source</li>
<li>Call phone number</li>
<li>Email message</li>
<li>Text message</li>
<li>Share a message through Facebook, Twitter or Email</li>
<li>Take a photo</li>
<li>Create in-app mobile commerce with credit card swiper</li>
<li>Capture signatures</li></ul></li></ul>
<ul>
<li>Start actions from Table row taps . The following are available:
<ul><li>Go to a page</li>
<li>Go to a page named in a hidden field of the selected row</li>
<li>If/Then Go to a page</li>
<li>Go to a previous page</li>
<li>Select multiple rows</li>
<li>Call phone number</li>
<li>Email message</li>
<li>Text message</li>
<li>Share a message through Facebook, Twitter or Email</li>
<li>Assign table values in the designer or load the values from web data sources</li></ul></li></ul>
<ul><li>Do computations  with Button and table row taps. The following are available:
<li>Assign values across fields</li>
<li>Do arithmetic operations</li>
<li>Get the current time</li>
<li>Show an external web page</li>
<li>Play audio</li></li>
<li>	Use our Storyboard to show you the context of your app</li>
<li>	Share your design with PDF files</li></ul>

</div>

<p class="news-title"><span>What mobile devices can ViziApps apps run on?</span></p>
<div class="news_text">
<strong>What mobile devices can ViziApps apps run on?</strong>:
    ViziApps supports both iPhone and Android-based mobile apps. iPad and Android 
    tablets are also supported for web apps. Support for native apps on iPad and Android-based tablet, BlackBerry, Windows Mobile and other mobile devices is on the ViziApps roadmap.
</div>

<p class="news-title"><span>How much does it cost?</span></p>

<div class="news_text">
<strong>How much does it cost?</strong>:
It's absolutely free to set up, design and test.  In fact, we encourage you to design and test for as long as you like at no cost.   
    For publishing native apps check out our
    <a href="http://viziapps.com/features-pricing">native app pricing</a>. For 
    publishing web apps check out our
    <a href="http://viziapps.com/features-pricing">web app pricing</a>.
</div>

<p class="news-title"><span>How long does it take to setup and launch?</span></p>
<div class="news_text">
<strong>How long does it take to setup and launch?</strong>:
You can set your app up in as little as a few hours.  Once you have your app created, just follow the instructions for 
    publishing it.  You will need to submit your app to an app marketplace (ie. Apple Store, Google Play).  These will require you 
    to register for a developer&#39;s license.  We will provide you with the file to submit your app to each store.  Once submitted, the 
    Apple app review process takes 5 to 10 days. There is no review process for 
    Android.</div>

<p class="news-title"><span>How easy is it to design my mobile app?</span></p>
<div class="news_text">
<strong>How easy is it to design my mobile app?</strong>:
If you know how to create a PowerPoint and build spreadsheets, you can build a native app using the ViziApps App Studio.  Its visual display canvas combined with simple drag-and-drop wizards make it easy to use for everyone!  If you do need assistance, let our <a href="mailto:sales@ViziApps.com?subject=Support Request">support team</a> know how they can help.
</div>

<p class="news-title"><span>Can my app be upgraded once it&#8217;s launched?</span></p>
<div class="news_text">
<strong>Can my app be upgraded once it&#8217;s launched?</strong>: Absolutely! Your 
    content is essential to your business needs. And we all know how quickly those 
    needs change. So yes, make your changes hourly, daily, weekly, whenever! The 
    best thing about it is it&#8217;s free! And your changes show up on the app 
    automatically without any manual intervention.</div>

<p class="news-title"><span>Can ViziApps create my app for me, or perhaps some part of it?</span></p>
<div class="news_text">
<strong>Can ViziApps create my app for me, or perhaps some part of it?</strong>:
Yes. ViziApps can do that for you.
    <a href="mailto:sales@ViziApps.com?subject=Request for Professional Services">Contact 
    ViziApps Sales</a> for a Professiona for a Professional Services quote.
</div>

<p class="news-title"><span>What are my options for publishing my ViziApps native app?</span></p>
<div class="news_text">
    <strong>What are my options for publishing my ViziApps native app? </strong>:

There are three options for publishing your ViziApps native apps: 


<ol><h4>For Google Play publishing:</h4>

<li>Get your own Android account.</li>
<li>When you Provision your app for Publishing, we email you the .apk file so you can publish it on the Google Play. Apps submitted to Google Play do not need approval and are typically posted within one day. </li>
</ol>

<ol><h4>For iPhone App Store native app publishing: </h4>
 
<li>You obtain an <a href="http://developer.apple.com/programs/start/standard/"> Apple Developer Account</a> When acquiring your account, choose the Company Developer type.  You do not need engineering skills to acquire this license.</li>

<li>You invite ViziApps as an admin of your developer team. </li>
<li>If your app is a paid app, you setup a merchant account with Apple for paid apps.</li>
<li>Follow our steps in ViziApps to provision your app (no coding) on a PC or Mac. There is a one-time submission fee of $99 per app.</li>
<li>When you Provision your app for Publishing, we send you the app upload .zip file. </li>
<li>You then submit that file to Apple for review. iPhone apps published in the App Store must be approved by Apple prior to being released. </li>
</ol>

<ol><h4>For iPhone native app Enterprise publishing:</h4>

<li>Get an Apple Enterprise license: See <a href="http://developer.apple.com/programs/start/standard/"> Apple Developer Program</a> </li>

<li>Get the final upload .ipa file from ViziApps from ViziApps&#8217;s enterprise license (unsigned compilation). </li>
<li>Distribute the app with your Apple enterprise license any way you want to employees and partners per Apple license terms. No Apple review of the app is required.</li>
</ol>
</div>

<p class="news-title"><span>What are the steps to getting my ViziApps app into the Apple 
    App Store?</span></p>
<div class="news_text">
    <strong>What are the steps to getting my ViziApps app in to the Apple App Store? </strong>
    :<p class="MsoNormal">
        <a name="OLE_LINK5"></a><a name="OLE_LINK4"></a><strong>Development &amp; Testing</strong></p>
    <p class="MsoNormal">
&nbsp;With ViziApps Studio, your development and testing do not require an Apple 
        Developer Account. You develop directly on ViziApps Studio and test directly on 
        your device using the ViziApp App.
    </p>
    <p class="MsoNormal">
        However, before you can publish an app to the Apple Store, you will need an 
        Apple Developer Account. You can apply here: 
        <a href="http://developer.apple.com">http://developer.apple.com</a>
    </p>
    <p class="MsoNormal">
        No technical skills are required to apply. The cost is $99/yr.
    </p>
    <p class="MsoNormal">
        Apple will also ask you to submit your company&#8217;s tax information, bank account 
        information and agree to their current contracts. Apple needs this information 
        to make payments to you.
    </p>
    <p class="MsoNormal">
       Your account should be setup in 2-3 weeks.
    </p>
    <p class="MsoNormal">
        <strong>Prepare for Publishing</strong>
    </p>
    <p class="MsoNormal">
        Before your app is uploaded, you will need to fill out all of the information 
        that Apple requests about your app. This includes screenshots, version 
        information, marketing description, and support contact information. This must 
        be completed before we submit your app.
    </p>
    <p class="MsoNormal">
        All apps submitted to Apple go through a review process.&nbsp;&nbsp;&nbsp;&nbsp; 
        Apple is very strict about app requirements. You should review their guidelines 
        here and design your app accordingly:
        <a href="https://developer.apple.com/appstore/resources/approval/guidelines.html">
        https://developer.apple.com/appstore/resources/approval/guidelines.htmll</a></p>
    <p class="MsoNormal">
&nbsp;To complete the pre-submittal work:
    </p>
    <ul>
        <li>
            <p class="MsoNormal">
                Navigate to the iTunes Connect area within the iOS Development Center.
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                Go to the Manage Your Apps page and click Add New App.
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                Fill out the forms describing your company and application.
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                Upload the 512 × 512 pixel icon and your application screenshots.
            </p>
        </li>
    </ul>
    <p class="MsoNormal">
        <strong>Publishing</strong>
    </p>
    <p class="MsoNormal">
       Fill out the ViziApps publishing form on the Publish page in the ViziApps 
        Studio. You&#8217;ll need to fill in:</p>
    <ul>
        <li>
            <p class="MsoNormal">
                The name of your published app
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                Transfer your test app design to the published app design
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                The number of users
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                The Application icon (512 x 512 pixels in JPG format)
            </p>
        </li>
        <li>
            <p class="MsoNormal">
                Splash Image (320 x 460 pixels in JPG format)
            </p>
        </li>
    </ul>
    <p class="MsoNormal">
        Then, purchase a ViziApps service plan and submit the form to us. There is a 
        one-time submission fee of $99 per app.
    </p>
    <p class="MsoNormal">
        <strong>Submitting the App to iTunes Connect</strong>
    </p>
    <p class="MsoNormal">
        ViziApps will prepare your iPhone Application (.ipa) file. (Note: we will need 
        your developer account credentials. For security, we recommend that you change 
        your password before giving it to us; then change it again after your app is 
        submitted.) 
    </p>
    <p class="MsoNormal">
        Remember, you&#39;ll need to fill out the required forms on the iTunes Connect site 
        before we submit the application to Apple.
    </p>
    <p class="MsoNormal">
     We then upload and submit your app to Apple for review. iPhone apps published in 
        the App Store must be approved by Apple prior to being released which takes 5-10 
        days. Your service fee only begins after your app has been approved.</p>
</div>

<p class="news-title"><span>How do I set the security of Network Data that my app 
    uses?</p>
    <div class="news_text">
<strong>How do I set the security of Network Data that my app uses? </strong>ViziApps design for your app sets the links between your app running on the 
    device and your Backend Data, which can be accessed and managed via either Google Docs Spreadsheets, 
    a database interface, or web services. ViziApps does not handle or see your 
    data, or that of your app&#8217;s users. You control the security of your Backend 
    Data. You can do that via an SSL connection from your mobile device and your firewall.</div>

<p class="news-title"><span>Is my use of Google Apps Marketplace and Google Docs Spreadsheets secure?</span></p>
<div class="news_text">
<strong>Is my use of Google Apps Marketplace and Google Docs Spreadsheets secure?</strong>:
First of all, there is a big difference between using Google Apps Marketplace and Google Docs Spreadsheets.  Google Apps Marketplace provides you with secure Single Sign-On (SSO) to ViziApps.  You do NOT need to share your spreadsheet 
    credentials publicly as you would if you were only using Google Docs Spreadsheets.  This has a few key benefits and consequences:
<ol>
<li>You maintain secure access to your data, because you can login through your Google Apps Marketplace account without using any password. This prevents ViziApps from knowing your Google account password, while still allowing your mobile app to access your data.</li><br>
 
<li>Since ViziApps cannot use your Google account password, and testing your app on smartphones requires an account password, we provide you with a special password to use just for testing your app on smartphones. This is emailed to you in the welcome email.</li><br>
 
<li>When a device accesses your spreadsheet for Google Apps Marketplace accounts, the request passes through our servers, because our domain is the only domain authorized to access your data for security purposes for Google Apps Marketplace accounts.</li><br>

 
<li>If a user who is not authorized to use your apps tries to sign-on, the app will not run.</li><br>  
 
<li>Also, a downloaded app is only counted as a production user for that app if a person successfully logs in to that app.</li>
</ol>
</div>

<p class="news-title"><span>What are the pros and cons for the 3 Data Management types (Google docs spreadsheets, Direct Database, and Web Service)?</span></p>
<div class="news_text">
<strong>What are the pros and cons for the 3 Data Management types (Google docs spreadsheets, Direct Database, and Web Service)?</strong>:
All our Data Management interfaces are designed for you to use without coding. The connection with our interfaces is done directly from the device to your back end data. There is no pass through our servers except for secure Google App Market accounts that use single sign and Open ID, which require our servers to become the authorized domain for the transactions. 

See the template apps that show off all the Data Management approaches we do, along with the context sensitive Help available in the App Studio.<br>

<br>Here&#8217;s a quick comparison of the three Data Management options:<br>

    <strong>
    <br />
    Google Docs spreadsheet</strong> is the easiest to learn and use and its free. It&#8217;s secure for Google Apps Market accounts. There are some limitations on the queries but it can do most normal stuff. Conditional statements are available to decide which of multiple queries to do. No coding is involved for the interface design.  Only one spreadsheet can interface to your app, but you can use as many worksheets within that spreadsheet as you require.<br />

<p><strong>Direct Database</strong> works with ODBC so it is compatible with all the legacy database servers. It is secure because the DB access is done after a secure web service call is made to the customers&#8217; network and the access is done behind the firewall. All SQL queries are available except for JOINS. The interface design pre-populates the DB fields that are extracted from the standard .sql files. This is easy to use for people who know SQL &#8211; No coding is involved for the interface design. Direct Database access is not as flexible as Web Services because there is no middle ware logic available in SQL.</p>

<p><strong>Web Services</strong> is the most general and most flexible. It can use secure SSL access. Using this will require engineering skills to debug the service on the network end. However there is no coding involved in setting up the interfaces. It extracts the methods and parameters from WSDL files and does live web service testing with live data so the user knows it will work on the device when it works in these studio tests.</p>

</div>

<p class="news-title"><span>Can I test run my app using an emulator?</span></p>
<div class="news_text">
<strong>Can I test run my app using an emulator?</strong>:
We decided not to use emulators because they are very slow compared to our download and run approach. 
    Download takes about 3 seconds. Also, it is hard to get the same result in an emulator for native on-device operations like taking a photo, not to mention keeping up with device OS version changes. 
</div>

<p class="news-title"><span>What is the approval process for my app?</span></p>
<div class="news_text">
<strong>What is the approval process for my app?</strong>:
Apps submitted to the Apple Store must go through an approval process prior to release.  You can expect this process to take 
    5 to 10 days.  An Android app submitted to the Google Play does not go through the same approval process and will be available within a day.
</div>

<p class="news-title"><span>The customers download the apps from the markets.  Do they also have to download the ViziApps app?</span></p>
<div class="news_text">
<strong>The customers download the apps from the markets.  Do they also have to download the ViziApps app?</strong>:
No, the customer downloads your branded app.  They do not download the ViziApps app.
</div>

<p class="news-title"><span>How are you tracking how many users are using the app or are you tracking downloads?</span></p>
<div class="news_text">
<strong>How are you tracking how many users are using the app or are you tracking downloads?</strong>:
ViziApps tracks the number of unique users.  The markets (Apple and Google) track the downloads.
</div>
<p class="news-title"><span>If 1000&#8217;s of users download the app but I have a plan for 50 users 
    what happens?</span></p>

<div class="news_text">
<strong>If 1000&#8217;s of users download the app but I have a publishing plan for 50 users 
    what happens?</strong>:
If you only have a publishing plan for 50 users, you only get charged for users that 
    login to your app with credentials that you supply. For a plan of 50 users, 
    after 50 users login, no more are allowed to login unless you change to a 
    publishing plan with more users.
    <span style="font-size:11.0pt;line-height:115%;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-fareast-theme-font:minor-fareast;mso-hansi-theme-font:
minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">When the 
    number is close to exceeding your plan user count, we will send you an email. </span></div>
<p class="news-title"><span>If I develop an App with ViziApps, who owns the App?</span></p>

<div class="news_text">
<strong>If I develop an App with ViziApps, who owns the App?</strong>: You continue 
    to own your intellectual property: your data, logo, brand, and other 
    intellectual property, as well as any data your users provide directly when 
    using the App.
    <br />
    <br />
    ViziApps retains ownership of ViziApps intellectual property and the processes 
    and resources that ViziApps provides and controls in any display, delivery and 
    capture of data but not the data itself. ViziApps does not own back end 
    resources or processes that the Customer provides and controls.
    <br />
    <br />
    If the Customer terminates the service, ownership does not change from what was 
    owned before or during the Customer &#8216;s service.
    <br />
    <br />
    ViziApps does not provide compiled code to customers.
    <span style="font-size:11.0pt;line-height:115%;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-fareast-theme-font:minor-fareast;mso-hansi-theme-font:
minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">. </span></div>
<p class="news-title">
        <span>What if my question wasn&#8217;t answered here?</span></p>

<div class="news_text">
<strong>What if my question wasn&#8217;t answered here?</strong>:
Send us an 
    <a href="mailto:support@ViziApps.com?subject=Question from the FAQ on the web">email</a>.<br> We&#8217;ll be sure to respond quickly with an answer to your question.
</div>

</div>
</td>
<td></td>
</tr>
</table>

</div>

    </form>
</body>
</html>
