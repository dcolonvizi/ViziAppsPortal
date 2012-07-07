<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProvisionHelp.aspx.cs" Inherits="Help_ProvisionHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Publish Help</title>
    <style type="text/css">
        .style2
        {
            font-size: 12pt;
            font-family: Calibri;
            color: #000099;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
     <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
     <div style="width:750px;">
<P>
			<span class="body" style="font-size: 12pt; font-family: Calibri">The <b>Publish</b> page
                lets you to put app you designed on a supported smartphone or tablet and run it. 
            You can test your app for free and then later request that your app be 
            published through an app store like the Apple 
            AppStore or the Google Play. </span></p>
    <p>
        <span class="body" style="font-size: 12pt; font-family: Calibri">First select an 
        app to test or publish in the drop-down list.</span></p>
    <P class="style2">
			<strong>Testing</strong></p>
    <ul>
        <li style="height: 89px">
            <span class="body" style="font-size: 12pt; font-family: Calibri">To test your app on a device click on 
            <strong>Test App</strong>. Only one app can be tested at a time. Once you 
            select your app for testing, that will be the app that can be downloaded and 
            run on your 
            smartphone or tablet. To test another app just select it from the drop-down list and click 
            on&nbsp; 
            <strong>Test App</strong>. The new app will replace the previous app.</span></li>
        <li>
            <P class="body" style="font-size: 12pt; font-family: Calibri">
			To run your app on the phone, first download the ViziApps app to your phone from 
            the app store. Start the ViziApps app on your phone and login with the same credentials you use to 
            login to your portal. Your login will download the app you chose for testing 
            and then your app will start to run.</p>
        </li>
        <li>
            <P class="body" style="font-size: 12pt; font-family: Calibri">
			    If you edit and save your app on the portal, the next time you open ViziApps 
                on the phone, the app will automatically update to your phone.</p>
        </li>
    </ul>
    <span class="body" style="font-size: 12pt; font-family: Calibri">
    <P class="style2">
			<strong>Publishing</strong></p>
        <P>
            <span class="body" style="font-size: 12pt; font-family: Calibri">Once an app has
                been selected for testing, it is available for publishing. To publish your app:</span>
    </P><ul>
        <li style="height: 96px">First, look at the service plans of Basic, Pro, Deluxe and Premium 
            in our pricing schedule and decide which plan, with or without data access, best fits 
            with your app distribution. Then purchase the service in the ViziApps Store by 
            clicking on <strong>Purchase ViziApps Services.</strong><br /><span class="body" style="font-size: 12pt; font-family: Calibri"><span style="font-size:12.0pt">
            <asp:Button ID="Pricing" runat="server" 
                    Text="See Pricing Plans" Width="138px" />
             </span>
    </span>
             </li>
             <li style="height: 46px">
    <span class="body" style="font-size: 12pt; font-family: Calibri">
                 For more 
             information on purchasing a production service <span style="font-size:12.0pt">
             click:  
                <asp:Button ID="PaymentHelp" runat="server" 
                    Text="Payment Help" Width="98px" />
            &nbsp;&nbsp;  
                </span>
    </span>
             </li>
             <li style="height: 95px">
                 <span class="body" style="font-size: 12pt; font-family: Calibri">After you buy a 
                 service plan, click on <strong>Publish App</strong> and fill out the <strong>Application Form for 
            Publishing. </strong>&nbsp;If you selected a service plan with a limited number of 
                 end-users then you need a way to authorize them. For any end user to use your 
                 app, with a limited end-user service, they would need to login to your app once 
                 with credentials, that you create, to run your app. </span>
             </li>
             <li style="height: 92px">To create user cred<span class="body" style="font-size: 12pt; font-family: Calibri">entials 
                 for a limited number of users, select the&nbsp;<strong>User 
            Credential </strong>tab in the <strong>Application Form for Publishing</strong> 
            and upload the credentials that you create for all your users upto the limit of your 
                 paid service.&nbsp; If you paid for unlimited use for your production 
            app, no user credentials are needed since the app will be available to everyone.
                 </span>
             </li>
             <li style="height: 51px">
    <span class="body" style="font-size: 12pt; font-family: Calibri">
                 Once you pay for a production app service, the submission button will appear. 
                 You can still save your edits on the <strong>Application Form for 
            Publishing</strong>&nbsp; without paying, but it does not submit it for processing.
    </span></li>
             <li><span class="body" style="font-size: 12pt; font-family: Calibri">
                 When you click on the submission button our support team will&nbsp; be notified 
                 and we will process your production app through the app store for you and then 
            notify you when your app is ready.</span></li>
        </UL>
    </span>
    </div>
    </form>
</body>
</html>
