<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyProfile.ascx.cs" Inherits="Controls_MyProfile" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
    
 <style type="text/css">

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
         width: 158px;
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
         width: 158px;
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
 </style>
    
 <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
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
                        <telerik:AjaxUpdatedControl ControlID="PhoneTextBox" LoadingPanelID="LoadingPanelIDProfile"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
    <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
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
<table style="background-color:white;"><tr><td style="width:16px;">&nbsp;&nbsp; &nbsp;</td><td>
<div align="left" id="MyProfileContainer"  style="background-color: #ffffff;">

 
<table border="0" cellpadding="0" cellspacing="0" width="868">
    <tr>
    
    <td bgcolor="#0054c2">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="#ffffff" Text="My Account"></asp:Label></td>
    </tr>
    <tr>
        <td  >      

         
<div>
 
<telerik:RadAjaxLoadingPanel ID="LoadingPanelIDProfile" runat="server" Skin="Default">
       </telerik:RadAjaxLoadingPanel>
       <div align="left">
<table border="0" bordercolor="#003399" cellpadding="0" cellspacing="0" frame="box" 
               style="width: 671px; font-family: Verdana; font-size: 12px;">
    <tr>
        <td style="height: 30px; width: 16px;">
            &nbsp;</td>
        <td style="width: 158px; height: 30px; font-family:Arial; font-size:12px;" valign="top" >
            &nbsp;</td>
        <td colspan="2" style="height: 30px; width: 43px;" valign="top">
            &nbsp;</td>
        <td style="width: 7px; height: 30px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
            &nbsp;</td>
        <td style="width: 158px; height: 30px; font-family:Arial; font-size:12px;" valign="top" >
            Username</td>
        <td colspan="2" style="height: 30px; width: 43px;" valign="top">
            <asp:Label ID="UsernameLabel" runat="server" Font-Bold="True" 
                Font-Names="Arial" Font-Size="11pt"></asp:Label></td>
        <td style="width: 7px; height: 30px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td style="width: 158px; height: 30px;" valign="top">
            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Password"></asp:Label></td>
        <td colspan="2" style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="PasswordTextBox" runat="server" Width="155px" Font-Size="10pt" TextMode="Password" Font-Names="Arial">********</asp:TextBox></td>
        <td style="width: 7px; height: 30px;">
        </td>
    </tr>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td style="width: 158px; height: 30px;" valign="top">
            Confirm Password</td>
        <td style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="ConfirmPasswordBox" runat="server" Font-Size="10pt" TextMode="Password"
                Width="155px" Font-Names="Arial">********</asp:TextBox></td>
        <td style="width: 7px; height: 30px;">
        </td>
    </tr>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD vAlign="top" style="width: 158px; height: 30px;">
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Company"></asp:Label></TD>
			<TD colSpan="2" style="height: 30px; width: 43px;" valign="top">
                <asp:TextBox ID="CompanyTextBox" runat="server" Font-Size="10pt" AutoCompleteType="Company" Font-Names="Arial"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;</TD>
			<TD style="width: 7px; height: 30px;"></TD>
		</TR>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td style="width: 158px; height: 30px;" valign="top">
            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Role"></asp:Label></td>
        <td colspan="2" style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="RoleTextBox" runat="server" Font-Size="10pt" AutoCompleteType="JobTitle" Font-Names="Arial"></asp:TextBox></td>
        <td style="width: 7px; height: 30px;">
        </td>
    </tr>
		<tr>
			<td style="height: 30px; width: 16px;">&nbsp;</td>
			<td vAlign="top" style="width: 158px; height: 30px;">
				<div align="left"><span class="style3">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Text="First Name"></asp:Label></span>&nbsp;</div>
			</td>
			<td colSpan="2" style="height: 30px; width: 43px;" valign="top"><asp:textbox id="FirstNameTextBox" runat="server" Font-Size="10pt" AutoCompleteType="FirstName" Font-Names="Arial"></asp:textbox>&nbsp;&nbsp;
				</td>
			<td style="width: 7px; height: 30px;">&nbsp;</td>
		</tr>
		<tr>
			<td style="height: 30px; width: 16px;">&nbsp;</td>
			<td vAlign="top" style="width: 158px; height: 30px;">
				<div align="left"><span class="style3">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Last Name"></asp:Label></span>&nbsp;</div>
			</td>
			<td colSpan="2" style="height: 30px; width: 43px;" valign="top"><asp:textbox id="LastNameTextBox" runat="server" Font-Size="10pt" AutoCompleteType="LastName" Font-Names="Arial"></asp:textbox>&nbsp;&nbsp;
				</td>
			<td style="width: 7px; height: 30px;">&nbsp;</td>
		</tr>
		<TR>
			<TD style="width: 16px; height: 80px;"></TD>
			<TD vAlign="top" style="width: 158px; height: 80px;">
				<div align="left"><SPAN class="style3">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Street Address"></asp:Label></SPAN>&nbsp;</div>
			</TD>
			<TD colSpan="2" valign="top" style="width: 43px; height: 80px;"><SPAN class="style3"><asp:textbox id="StreetTextBox" runat="server" Height="64px" TextMode="MultiLine" Width="400px"
						Font-Size="10pt" Font-Names="Arial" AutoCompleteType="BusinessStreetAddress"></asp:textbox>&nbsp;</SPAN></TD>
			<TD style="width: 7px; height: 80px;"></TD>
		</TR>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD vAlign="top" style="width: 158px; height: 30px;">
                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Text="City"></asp:Label></TD>
			<TD colSpan="2" style="height: 30px; width: 43px;" valign="top"><asp:textbox id="CityTextBox" runat="server" Font-Size="10pt" AutoCompleteType="BusinessCity" Font-Names="Arial"></asp:textbox>&nbsp;
                </TD>
			<TD style="width: 7px; height: 30px;"></TD>
		</TR>
		<TR>
			<TD style="height: 30px; width: 16px;"></TD>
			<TD vAlign="top" style="width: 158px; height: 30px;">
                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Text="State"></asp:Label></TD>
			<TD colSpan="2" style="height: 30px; width: 43px;" valign="top"><asp:dropdownlist id="StateList" runat="server" Font-Size="10pt" Font-Names="Arial">
					<asp:ListItem Value="->">Select-&gt;</asp:ListItem>
					<asp:ListItem Value="AK">AK</asp:ListItem>
					<asp:ListItem Value="AL">AL</asp:ListItem>
					<asp:ListItem Value="AR">AR</asp:ListItem>
					<asp:ListItem Value="AS">AS</asp:ListItem>
					<asp:ListItem Value="AZ">AZ</asp:ListItem>
					<asp:ListItem Value="CA">CA</asp:ListItem>
					<asp:ListItem Value="CO">CO</asp:ListItem>
					<asp:ListItem Value="CT">CT</asp:ListItem>
					<asp:ListItem Value="DC">DC</asp:ListItem>
					<asp:ListItem Value="DE">DE</asp:ListItem>
					<asp:ListItem Value="FL">FL</asp:ListItem>
					<asp:ListItem Value="FM">FM</asp:ListItem>
					<asp:ListItem Value="GA">GA</asp:ListItem>
					<asp:ListItem Value="GU">GU</asp:ListItem>
					<asp:ListItem Value="HI">HI</asp:ListItem>
					<asp:ListItem Value="IA">IA</asp:ListItem>
					<asp:ListItem Value="ID">ID</asp:ListItem>
					<asp:ListItem Value="IL">IL</asp:ListItem>
					<asp:ListItem Value="IN">IN</asp:ListItem>
					<asp:ListItem Value="KS">KS</asp:ListItem>
					<asp:ListItem Value="KY">KY</asp:ListItem>
					<asp:ListItem Value="LA">LA</asp:ListItem>
					<asp:ListItem Value="MA">MA</asp:ListItem>
					<asp:ListItem Value="ME">ME</asp:ListItem>
					<asp:ListItem Value="MD">MD</asp:ListItem>
					<asp:ListItem Value="MH">MH</asp:ListItem>
					<asp:ListItem Value="MI">MI</asp:ListItem>
					<asp:ListItem Value="MN">MN</asp:ListItem>
					<asp:ListItem Value="MO">MO</asp:ListItem>
					<asp:ListItem Value="MP">MP</asp:ListItem>
					<asp:ListItem Value="MS">MS</asp:ListItem>
					<asp:ListItem Value="MT">MT</asp:ListItem>
					<asp:ListItem Value="NC">NC</asp:ListItem>
					<asp:ListItem Value="ND">ND</asp:ListItem>
					<asp:ListItem Value="NE">NE</asp:ListItem>
					<asp:ListItem Value="NH">NH</asp:ListItem>
					<asp:ListItem Value="NJ">NJ</asp:ListItem>
					<asp:ListItem Value="NM">NM</asp:ListItem>
					<asp:ListItem Value="NV">NV</asp:ListItem>
					<asp:ListItem Value="NY">NY</asp:ListItem>
					<asp:ListItem Value="OH">OH</asp:ListItem>
					<asp:ListItem Value="OK">OK</asp:ListItem>
					<asp:ListItem Value="OR">OR</asp:ListItem>
					<asp:ListItem Value="PA">PA</asp:ListItem>
					<asp:ListItem Value="PR">PR</asp:ListItem>
					<asp:ListItem Value="PW">PW</asp:ListItem>
					<asp:ListItem Value="RI">RI</asp:ListItem>
					<asp:ListItem Value="SC">SC</asp:ListItem>
					<asp:ListItem Value="SD">SD</asp:ListItem>
					<asp:ListItem Value="TN">TN</asp:ListItem>
					<asp:ListItem Value="TX">TX</asp:ListItem>
					<asp:ListItem Value="UT">UT</asp:ListItem>
					<asp:ListItem Value="VI">VI</asp:ListItem>
					<asp:ListItem Value="VT">VT</asp:ListItem>
					<asp:ListItem Value="VA">VA</asp:ListItem>
					<asp:ListItem Value="WA">WA</asp:ListItem>
					<asp:ListItem Value="WI">WI</asp:ListItem>
					<asp:ListItem Value="WV">WV</asp:ListItem>
					<asp:ListItem Value="WY">WY</asp:ListItem>
				</asp:dropdownlist></TD>
			<TD style="width: 7px; height: 30px;"></TD>
		</TR>
		<TR>
			<TD class="style1"></TD>
			<TD vAlign="top" class="style2">
                <asp:Label ID="PostalCodeLabel" runat="server" Font-Names="Arial" 
                    Font-Size="10pt" Text="Postal Code" Width="120px"></asp:Label></TD>
			<TD colSpan="2" valign="top" class="style5"><asp:textbox id="PostalCodeTextBox" runat="server" Font-Size="10pt" AutoCompleteType="BusinessZipCode" Font-Names="Arial"></asp:textbox>
                </TD>
			<TD class="style4"></TD>
		</TR>
		<TR>
			<TD class="style6"></TD>
			<TD vAlign="top" class="style7">
                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Country"></asp:Label></TD>
			<TD colSpan="2" valign="top" class="style8"><asp:textbox id="CountryTextBox" runat="server" Font-Size="10pt" AutoCompleteType="BusinessCountryRegion" Font-Names="Arial">United States</asp:textbox>
                </TD>
			<TD class="style9"></TD>
		</TR>
    <tr>
        <td style="height: 30px; width: 16px;">
        </td>
        <td style="width: 158px; height: 30px;" valign="top">
            <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Phone"></asp:Label></td>
        <td colspan="2" style="height: 30px; width: 43px;" valign="top">
            <asp:TextBox ID="PhoneTextbox" runat="server" Font-Size="10pt" AutoCompleteType="BusinessPhone" Font-Names="Arial"></asp:TextBox>&nbsp;
            </td>
        <td style="width: 7px; height: 30px;">
        </td>
    </tr>
		<TR>
			<TD style="height: 37px; width: 16px;"></TD>
			<TD style="width: 158px; height: 37px;" valign=top>
                <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Email "></asp:Label></TD>
			<TD colSpan="2" valign="top" style="height: 37px; width: 43px;"><asp:textbox id="EmailTextBox" runat="server" Width="365px" Font-Size="10pt" AutoCompleteType="Email" Font-Names="Arial"></asp:textbox>&nbsp;
                </TD>
			<TD style="width: 7px; height: 37px;"></TD>
		</TR>
    <tr>
        <td style="width: 16px; height: 37px">
        </td>
        <td style="width: 158px; height: 37px" valign="top">
            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Default Time Zone"></asp:Label></td>
        <td colspan="2" style="width: 43px; height: 37px" valign="top">
            <asp:DropDownList ID="TimeZoneList" runat="server" Font-Names="Arial" Font-Size="9pt"
                Width="332px" ForeColor="Black">
            </asp:DropDownList></td>
        <td style="width: 7px; height: 37px">
        </td>
    </tr>
    <tr>
        <td style="width: 16px; height: 37px">
            &nbsp;</td>
        <td style="width: 158px; height: 37px" valign="top">
            Restrict Sessions to 1 user</td>
        <td colspan="2" style="width: 43px; height: 37px" valign="top">
            <asp:CheckBox ID="Force1UserSessions" runat="server" />
        </td>
        <td style="width: 7px; height: 37px">
            &nbsp;</td>
    </tr>
	<TR>
		<TD height="43" style="width: 16px"></TD>
		<TD height="43" style="width: 158px"></TD>
		<TD vAlign="middle" colSpan="2" height="43" style="font-weight: bold; font-size: 9pt; color: #999999; font-family: Arial; width: 43px;">
            &nbsp;<asp:Button ID="UpdateProfile" runat="server" Font-Names="Arial" Font-Size="10pt"
                OnClick="UpdateProfile_Click" Text="Update Profile" CausesValidation="False"/>&nbsp;
        </TD>
		<TD height="43" style="width: 7px"></TD>
	</TR>
    <tr>
        <td height="43" style="width: 16px">
        </td>
        <td colspan="4" height="43">
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
              <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
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
