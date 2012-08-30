<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabMyProfile.aspx.cs" Inherits="MyProfile" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps: Build Mobile Apps Online</title>
    		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <meta http-equiv="Expires" content="-1"/>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>


		<link href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
       <style type="text/css">
        body
        {
        	 background-color:#bcbcbc;
        }

     .style3
     {
         width: 105px;
         height: 32px;
     }
     .style1
     {
         width: 16px;
         height: 37px;
     }
     .style2
     {
               width: 129px;
               height: 37px;
           }
     .style5
     {
         width: 43px;
         height: 37px;
     }
     .style4
     {
         width: 7px;
         height: 37px;
     }
     .style6
     {
         width: 16px;
         height: 38px;
     }
     .style7
     {
         width: 129px;
         height: 38px;
     }
     .style8
     {
         width: 43px;
         height: 38px;
     }
     .style9
     {
         width: 7px;
         height: 38px;
     }
           .style10
           {
               height: 30px;
               width: 129px;
           }
           .style11
           {
               height: 80px;
               width: 129px;
           }
           .style12
           {
               width: 129px;
           }
           .textbox
           {}
 </style>
  <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
         <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="UpdateProfile" >
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="CompanyTextBox"  LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="RoleTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="FirstNameTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="LastNameTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="StreetTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="CityTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="StateList" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="PostalCodeTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="CountryTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="EmailTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                        <telerik:AjaxUpdatedControl ControlID="Message" 
                            LoadingPanelID="LoadingPanelIDProfile"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>

    </AjaxSettings>
	</telerik:RadAjaxManager>

     <div align="center" id="header" style="height:60px;  background-color:#0054c2;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                </td>
               
                <td class="style45">
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White"></asp:Label>
                    
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
                        TabIndex="1750" style="height: 18px"/>
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
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1175"/>
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
                        SelectedImageUrl="~/images/FAQButton_sel.png"  Value="FAQ" TabIndex="1600"/>
                       <telerik:RadMenuItem ImageUrl="~/images/MyProfileButton.png" HoveredImageUrl="~/images/MyProfileButton_hov.png"
                        SelectedImageUrl="~/images/MyProfileButton_sel.png"  Value="MyProfile"  Selected="true" TabIndex="1700"/>
                       

                         </Items>
                          </telerik:RadMenu>
                      
                       
                          </td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>   
      <div align="center" style="width:100%">
     <div align="center" style=" background-color:#bcbcbc; width:750px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:868px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
<table style="width:750px; background-color:white;"><tr><td style="width:16px;">&nbsp;&nbsp; &nbsp;</td><td>
<div align="left" id="MyProfileContainer"  
        style="background-color: #ffffff; width: 709px;">

 
<table border="0" cellpadding="0" cellspacing="0" style="width: 693px">
    <tr>
    
    <td bgcolor="#0054c2">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="#ffffff" Text="My Account"></asp:Label></td>
    </tr>
    <tr>
        <td  >      

         
<div style="width: 679px">
 
<telerik:RadAjaxLoadingPanel ID="LoadingPanelIDProfile" runat="server" Skin="Default">
       </telerik:RadAjaxLoadingPanel>
       <div align="left">
<table border="0"  cellpadding="0" cellspacing="0" frame="box" style="bordercolor:#003399;width: 671px">
    <tr>
        <td style="height: 30px; width: 16px;">
            &nbsp;</td>
        <td style="font-family:Arial; font-size:12px;" valign="top" class="style10" >
            &nbsp;</td>
        <td style="height: 30px; width: 43px;" valign="top">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
            &nbsp;</td>
        <td style="font-family:Arial; font-size:12px;" valign="top" class="style10" >
            Username</td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:Label ID="UsernameLabel" runat="server" Font-Bold="True" 
                Font-Names="Arial" Font-Size="11pt"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td valign="top" class="style10">
            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Password"></asp:Label></td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="PasswordTextBox" runat="server" Width="175px" Font-Size="10pt" TextMode="Password" Font-Names="Arial">********</asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td valign="top" class="style10">
            Confirm Password</td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="ConfirmPasswordBox" runat="server" Font-Size="10pt" TextMode="Password"
                Width="175px" Font-Names="Arial">********</asp:TextBox></td>
    </tr>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD valign="top" class="style10">
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Company"></asp:Label></TD>
			<TD style="height: 30px; width: 43px;" valign="top">
                <asp:TextBox ID="CompanyTextBox" runat="server" Font-Size="10pt" Width="175px" AutoCompleteType="Company" Font-Names="Arial"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;</TD>
		</TR>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td valign="top" class="style10">
            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Role"></asp:Label></td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="RoleTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="JobTitle" Font-Names="Arial"></asp:TextBox></td>
    </tr>
		<tr>
			<td style="height: 30px; width: 16px;">&nbsp;</td>
			<td valign="top" class="style10">
				<div align="left"><span class="style3">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Text="First Name"></asp:Label></span>&nbsp;</div>
			</td>
			<td style="height: 30px; width: 43px;" valign="top"><asp:textbox id="FirstNameTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="FirstName" Font-Names="Arial"></asp:textbox>&nbsp;&nbsp;
				</td>
		</tr>
		<tr>
			<td style="height: 30px; width: 16px;">&nbsp;</td>
			<td valign="top" class="style10">
				<div align="left"><span class="style3">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Last Name"></asp:Label></span>&nbsp;</div>
			</td>
			<td style="height: 30px; width: 43px;" valign="top"><asp:textbox id="LastNameTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="LastName" Font-Names="Arial"></asp:textbox>&nbsp;&nbsp;
				</td>
		</tr>
		<TR>
			<TD style="width: 16px; height: 80px;"></TD>
			<TD valign="top" class="style11">
				<div align="left"><SPAN class="style3">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Street Address"></asp:Label></SPAN>&nbsp;</div>
			</TD>
			<TD valign="top" style="width: 43px; height: 80px;"><SPAN class="style3">
                <asp:textbox id="StreetTextBox" runat="server" Height="64px" 
                    TextMode="MultiLine" Width="330px"
						Font-Size="10pt" Font-Names="Arial" AutoCompleteType="BusinessStreetAddress"></asp:textbox>&nbsp;</SPAN></TD>
		</TR>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD valign="top" class="style10">
                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Text="City"></asp:Label></TD>
			<TD style="height: 30px; width: 43px;" valign="top"><asp:textbox id="CityTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="BusinessCity" Font-Names="Arial"></asp:textbox>&nbsp;
                </TD>
		</TR>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD valign="top" class="style10">
                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Text="State"></asp:Label></TD>


			<TD style="height: 30px; valign="top"><asp:dropdownlist id="StateList" runat="server" Font-Size="10pt" Font-Names="Arial">
					<asp:ListItem Value="->">Select-&gt;</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
					<asp:ListItem Value="AL">Alabama</asp:ListItem>
					<asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
					<asp:ListItem Value="CA">California</asp:ListItem>
					<asp:ListItem Value="CO">Colorado</asp:ListItem>
					<asp:ListItem Value="CT">Connecticut</asp:ListItem>
					<asp:ListItem Value="DC">District of Columbia</asp:ListItem>
					<asp:ListItem Value="DE">Deleware</asp:ListItem>
					<asp:ListItem Value="FL">Florida</asp:ListItem>
					<asp:ListItem Value="GA">Georgia</asp:ListItem>
					<asp:ListItem Value="HI">Hawaii</asp:ListItem>
					<asp:ListItem Value="ID">Idaho</asp:ListItem>
  					<asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
					<asp:ListItem Value="KS">Kansas</asp:ListItem>
					<asp:ListItem Value="KY">Kentucky</asp:ListItem>
					<asp:ListItem Value="LA">Louisiana</asp:ListItem>
					<asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
					<asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
					<asp:ListItem Value="MO">Missouri</asp:ListItem>
					<asp:ListItem Value="MS">Mississippi</asp:ListItem>
					<asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
					<asp:ListItem Value="NC">North Carolina</asp:ListItem>
					<asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
					<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
					<asp:ListItem Value="OR">Oregon</asp:ListItem>
					<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
					<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
					<asp:ListItem Value="SC">South Carolina</asp:ListItem>
					<asp:ListItem Value="SD">South Dakota</asp:ListItem>
					<asp:ListItem Value="TN">Tennessee</asp:ListItem>
					<asp:ListItem Value="TX">Texas</asp:ListItem>
					<asp:ListItem Value="UT">Utah</asp:ListItem>
					<asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
					<asp:ListItem Value="WA">Washington</asp:ListItem>
					<asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
					<asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="AB">Alberta</asp:ListItem>
                    <asp:ListItem Value="BC">British Columbia</asp:ListItem>
                    <asp:ListItem Value="MB">Manitoba</asp:ListItem>
                    <asp:ListItem Value="NB">New Brunswick</asp:ListItem>
                    <asp:ListItem Value="NL">Newfoundland and Labrador </asp:ListItem>
                    <asp:ListItem Value="NT">Northwest Territories  </asp:ListItem>
                    <asp:ListItem Value="NS">Nova Scotia</asp:ListItem>
                    <asp:ListItem Value="NU">Nunavut</asp:ListItem>
                    <asp:ListItem Value="ON">Ontario</asp:ListItem>
                    <asp:ListItem Value="PE">Prince Edward Island</asp:ListItem>
                    <asp:ListItem Value="QC">Quebec</asp:ListItem>
                    <asp:ListItem Value="SK">Saskatchewan</asp:ListItem>
                    <asp:ListItem Value="YT">Yukon</asp:ListItem>
                    <asp:ListItem Value="OTHER">Other</asp:ListItem>
				</asp:dropdownlist></TD>
		</TR>
		<TR>
			<TD class="style1"></TD>
			<TD valign="top" class="style2">
                <asp:Label ID="PostalCodeLabel" runat="server" Font-Names="Arial" 
                    Font-Size="10pt" Text="Postal Code" Width="120px"></asp:Label></TD>
			<TD valign="top" class="style5"><asp:textbox id="PostalCodeTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="BusinessZipCode" Font-Names="Arial"></asp:textbox>
                </TD>
		</TR>
		<TR>
			<TD class="style6"></TD>
			<TD valign="top" class="style7">
                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Country"></asp:Label></TD>
			<TD valign="top" class="style8"><asp:textbox id="CountryTextBox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="BusinessCountryRegion" Font-Names="Arial">United States</asp:textbox>
                </TD>
		</TR>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td valign="top" class="style10">
            <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Phone"></asp:Label></td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="PhoneTextbox" runat="server" Width="175px" Font-Size="10pt" AutoCompleteType="BusinessPhone" Font-Names="Arial"></asp:TextBox>&nbsp;
            </td>
    </tr>
		<TR>
			<TD style="height: 37px; width: 16px;"></TD>
			<TD valign=top class="style2">
                <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Email "></asp:Label></TD>
			<TD valign="top" style="height: 37px; width: 43px;"><asp:textbox id="EmailTextBox" 
                    runat="server" Width="330px" Font-Size="10pt" AutoCompleteType="Email" 
                    Font-Names="Arial"></asp:textbox>&nbsp;
                </TD>
		</TR>
    <tr>
        <td style="width: 16px; height: 37px">
        </td>
        <td valign="top" class="style2">
            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Default Time Zone"></asp:Label></td>
        <td style="width: 43px; height: 37px" valign="top">
            <asp:DropDownList ID="TimeZoneList" runat="server" Font-Names="Arial" Font-Size="9pt"
                Width="332px" ForeColor="Black">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 16px; height: 37px">
            &nbsp;</td>
        <td valign="top" class="style2">
            Restrict Sessions to 1 User</td>
        <td style="width: 43px; height: 37px" valign="top">
            <asp:CheckBox ID="Force1UserSessions" runat="server" />
        </td>
         
    </tr>
    <tr><td><br />
         <br /></td></tr>
    
    
    <!-- Additions for updating CC details on CheddarGetter  -->
    
    <tr>
    <td colspan=3>
    <hr width="75%"  style="color:White"/>
    </td></tr>
    <tr>
    <td style="width: 16px; height: 37px">
            &nbsp;</td>
    <td ><b>Update Credit Card </b>  </td>
    <td>
        <asp:CheckBox ID="Update_CC_Details_CheckBox" runat="server" />
        <br />
        <br />
    </td>
    </tr>  
    <tr>
        <td style="height: 30px; width: 16px;"> </td>
        <td colspan=2><small><b>Note:</b>Updating your credit card details from here will affect all your publishing services. If you wish you change the credit card information for just one App do it from the <b>Modify Publishing Service</b> page. </small>
    </td>
    </tr>
    <tr><td  colspan =2>&nbsp;</td>   </tr>
    <tr>
          <td style="width: 16px; height: 37px"> &nbsp;</td>
          <td class="subscriptionlabel_title">Cardholder&#39;s First Name<br /></td>
          <td class="style4"> 
            <asp:textbox id="CCFirstNameTextbox" runat="server" Font-Size="10pt" CssClass="textbox" 
			Width="175px" AutoCompleteType="FirstName"  
			CausesValidation="True"></asp:textbox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        ControlToValidate="CCFirstNameTextbox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
		</asp:RequiredFieldValidator>             
        </td>
        </tr>
        <tr>
                  <td style="width: 16px; height: 37px"> &nbsp;</td>
                 <td class="subscriptionlabel_title">Cardholder&#39;s Last Name </td>
                 <td class="style2">  <asp:TextBox ID="CCLastNameTextBox" runat="server" Width="175px" CssClass="textbox" 	Font-Size="10pt" AutoCompleteType="LastName" CausesValidation="True"></asp:TextBox>
                    <br />
            
                    	<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                       CssClass="validatortext"  
                                        ControlToValidate="CCLastNameTextbox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
			</asp:RequiredFieldValidator>                   
                        
        </td>
        </tr>


        <tr>
        <td style="width: 16px; height: 37px"> &nbsp;</td>
        <td class="subscriptionlabel_title">Credit Card Number<br /></td>
        <td class="style4"> 
                   <asp:textbox id="CCNumberTextBox" runat="server"  Width="175px" CssClass="textbox" Font-Size="10pt"  
				CausesValidation="True"></asp:textbox>
                   <br />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCNumberTextbox"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RequiredFieldValidator>  
        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Credit card has to be numeric"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="CCNumberTextbox"
                                        ValidationExpression="^[0-9]+$"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RegularExpressionValidator>
        
        </td>
        </tr>

        <tr> 
        <td style="width: 16px; height: 37px"> &nbsp;</td>      
        <td class="subscriptionlabel_title">CVV Code<br /> </td>
        <td class="style2"> <asp:TextBox ID="CCCardCodeTextBox" runat="server" Font-Size="10pt" Width="46px" CssClass="textbox"	CausesValidation="True"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCCardCodeTextbox"
                                        SetFocusOnError="True" 
					                    ValidationGroup="formdata">
				            </asp:RequiredFieldValidator>  
                            
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
					runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="CVV Code has to be numeric"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCCardCodeTextbox"
                                        ValidationExpression="^[0-9]+$"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata"></asp:RegularExpressionValidator>

        
     </td>
     </tr>
    <tr>
    <td style="width: 16px; height: 37px"> &nbsp;</td>
        <td class="subscriptionlabel_title">Expiration <br /><small><small>(mm/yyyy)</small></small></td>


        <td class="style4"> 
		<asp:TextBox ID="CCExpirationTextBox" 
			runat="server" 
			Font-Size="10pt" 
	                Width="84px" 
                    CssClass="textbox" 
        	        CausesValidation="True"></asp:TextBox>

        <br />

        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Not in (mm/yyyy) format !!"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ValidationExpression="(0[1-9]|1[012])[- /.](19|20)\d\d"
					ControlToValidate="CCExpirationTextbox"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RegularExpressionValidator>  

            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCExpirationTextbox"
                                        SetFocusOnError="True" 
       					ValidationGroup="formdata">
		    </asp:RequiredFieldValidator>

        </td>

        </tr> 


        <tr>
        <td style="width: 16px; height: 37px"> &nbsp;</td>
        <td class="subscriptionlabel_title">Cardholder's Postal Code<br /> </td>

        <td class="style2"> 
            <asp:TextBox ID="CCZipTextBox" 
                         runat="server"  
                         Font-Size="10pt" 
                         Width="72px" 
                         CssClass="textbox" 
                         CausesValidation="True" 
                         ValidationGroup="formdata"></asp:TextBox>

            <br />
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        ControlToValidate="CCZipTextbox"
                                        SetFocusOnError="True" 
					                    ValidationGroup="formdata">
		    </asp:RequiredFieldValidator>
        
            
            
        </td>
        </tr>

   <!-- End CC Details -->


    
	<TR>
		<TD height="43" style="width: 16px"></TD>
		<TD height="43" class="style12"></TD>
		<TD valign="middle" height="43" 
            style="font-weight: bold; font-size: 9pt; color: #999999; font-family: Arial; width: 43px;">
            &nbsp;<asp:Button ID="UpdateProfile" runat="server" Font-Names="Arial" Font-Size="10pt"
                OnClick="UpdateProfile_Click" Text="Update Profile" CausesValidation="False"/>&nbsp;
        </TD>
	</TR>
    <tr>
        <td height="43" style="width: 16px">
        </td>
        <td colspan="2" height="43">
					<asp:label id="Message" runat="server" ForeColor="Maroon" Font-Names="Arial" Font-Size="10pt" Width="583px" Font-Bold="True"></asp:label></td>
    </tr>
</table>
</div>
</div>

       
        </td>
    </tr>
</table>

  
</div>
</td><td style="width:16px;">&nbsp;&nbsp; &nbsp;</td></tr></table>
              <div align="center" style=" background-color:#bcbcbc; width:750px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_bottom_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:868px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_bottom_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
                </div>


                <!-- Adding a RadNotification for CheddarGetter error notifications -->
                    <telerik:RadNotification ID="RadNotification1"  runat="server"  
                        VisibleOnPageLoad="false"  
                        Width="300px" 
                        Height="150px"
                        EnableRoundedCorners="True"
                        ContentIcon="Billing/images/billing_images/warning.png"
                        Animation="Fade" 
                        AnimationDuration="1000"
                        EnableShadow="True" 
                        Position="Center"
                        Title="Notification Title" 
                        Text="Notification"
                        Style="z-index: 35000" 
                        AutoCloseDelay="5000" 
                        ForeColor="Red"
                        Font-Bold="True" 
                        BorderStyle="Groove"
                        BorderColor="#5370A6"
                        TitleIcon="Billing/images/billing_images/warning_title.png">

                 </telerik:RadNotification>

                <!-- End RadNotification for CheddarGetter error notifications -->

    </form>
</body>
</html>
