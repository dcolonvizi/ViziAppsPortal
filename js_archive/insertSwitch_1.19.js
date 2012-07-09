       if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        var ArgList = null;

        function onDefaultValueLoad(sender, args) {
            if (ArgList != null && ArgList[0].length > 0) {
                if(sender._toggleStatesData[0].text.toLowerCase() ==  ArgList[2])
                      sender.set_selectedToggleStateIndex(0);
                   else
                       sender.set_selectedToggleStateIndex(1);
                   ArgList = null;
            }
        }

        function changeAction(sender, args) {
            var view_name = null;
            switch (sender.get_value()) {
                case 'if_then_next_page':
                case 'previous_page':
                case 'next_page':
                case 'post':
                case 'call':
                case 'share':
                case 'email':
                case 'sms':
                case 'take_photo':
                case 'capture_signature':
                case 'login_to_mcommerce':
                case 'init_card_swiper':
                case 'manual_card_charge':
                case 'void_charge':
                case 'capture_process_document':
                case 'manage_document_case':
                    view_name = sender.get_value() + '_view';
                    break;
                default:
                    view_name = 'no_view';
                    break;
            }
            var actionsMultiPage = document.getElementById("actionsMultiPage");
            for (var i = 0; i < actionsMultiPage.control._pageViewData.length; i++) {
                if (actionsMultiPage.control._pageViewData[i].id == view_name) {
                    actionsMultiPage.control._selectPageViewByIndex(i);
                    //clear all param class selections
                    $('.param').each(function (index, element) {
                        $(this).remove();
                    });
                    return;
                }
            }
            actionsMultiPage.control._selectPageViewByIndex(0);
            //clear all param class selections
            $('.param').each(function (index, element) {
                $(this).remove();
            });
        }

        function setComboValue(comboBox, value) {
            if (comboBox.control) {
                comboBox.control.trackChanges();
                var item = comboBox.control.findItemByValue(value);
                if (item) {
                    item.select();
                }
                comboBox.control.commitChanges();
            }
            else {
                comboBox.trackChanges();
                var item = comboBox.findItemByValue(value);
                if (item) {
                    item.select();
                }
                comboBox.commitChanges();
            }
        }

        var gotoPageCombo;
        function OnClientGoToPagesLoadHandler(sender) {
            gotoPageCombo = sender;
        }

        var page_if_true;
        function OnClientpage_if_trueLoadHandler(sender) {
            page_if_true = sender;
        }

        var page_if_false;
        function OnClientpage_if_falseLoadHandler(sender) {
            page_if_false = sender;
        }

        var next_page_value;
        function GoToPages_SelectedIndexChanged(sender, args) {
            next_page_value = sender.get_text();
        }

        var next_page_condition_value;
        function next_page_condition_Changed(sender, arg) {
            next_page_condition_value = sender.value;
        }

        var page_if_true_value;
        function page_if_true_SelectedIndexChanged(sender, args) {
            page_if_true_value = sender.get_text();
        }

        var page_if_false_value;
        function page_if_false_SelectedIndexChanged(sender, args) {
            page_if_false_value = sender.get_text();
        }
        
        function contains(string_var, string_val) {
            return (string_var.indexOf(string_val)  >= 0)? true : false;
        }
        function addPropertySelected(sender, div_id) {
            addProperty(div_id, sender.options[sender.selectedIndex].label, sender.value);
            sender.selectedIndex = 0;
        }

        $(function () {
        });

        function addProperty(prop_id, param, validations) { //if the selection option has no value attribute then param == validations
            if (param == undefined)
                return;
            var id = param.replaceAll(' ', '_').toLowerCase();
            var exists = document.getElementById(id);
            if (exists != null)
                return;
            var delete_id = id + "_delete";
            var div_id = id + "_div";
            $('#' + prop_id).css('display', 'none');

            if (validations != null && validations != param) {
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 410px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td>[' + validations + ']</td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');
            }
            else
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 410px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');

            $('#' + id).focus();
            $('#' + id).change(function (event) {
                if (validations != null && validations != param) {
                    var possible_list = validations.split(',');
                    var match_found = false;
                    for (var i = 0; i < possible_list.length; i++) {
                        if (this.value == possible_list[i]) {
                            match_found = true;
                            break;
                        }
                    }
                    if (!match_found) {
                        alert('entry does not match possible values');
                        return;
                    }
                }
                event.cancelBubble = true;
                if (event.stopPropagation)
                    event.stopPropagation();
                $('#' + prop_id).css('display', 'block');
            });

            $('#' + delete_id).click(function (event) {
                var delete_id = event.target.attributes[2].value;
                var div_id = delete_id.substring(0, delete_id.length - 7) + '_div';
                $('#' + div_id).remove();
                $('#' + prop_id).css('display', 'block');
            });
        }
        
         function initDialog() {
            var actionsMultiPage = document.getElementById("actionsMultiPage");
            var actions = document.getElementById("actions").control;
           

            ArgList = parent.window.getDialogInputArgs();
            for (var i = 0; i < ArgList.length; i++) {
                if (ArgList[i] == null)
                    ArgList[i] = '';
            }

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Switch";                
 
                var SwitchName = document.getElementById("switchName");
                SwitchName.value = ArgList[0];

                var SwitchType = document.getElementById("SwitchType");               
                var item = SwitchType.control.findItemByValue(ArgList[1]);
                item.select();
                 
                var actionsMultiPage = document.getElementById("actionsMultiPage");
                var actions = document.getElementById("actions");
                var AccountType = document.getElementById("AccountType");
                var view_response_radios = document.getElementsByName("view_response");

                var parse = ArgList[3].split(';');
                for (var i = 0; i < parse.length; i++) {
                    var parts = parse[i].split(':');
                    switch (parts[0]) {
                        case 'post':
                            setComboValue(actions, 'post');                           
                            break;
                        case 'next_page':
                            gotoPage_do_select = true;
                            var page_parts = parts[1].split('~');
                            gotoPage_selected_item = page_parts[1];
                            setComboValue(actions, 'next_page');
                            gotoPageCombo = document.getElementById("GoToPages").control;
                            if (gotoPageCombo.get_text() != gotoPage_selected_item) {
                                gotoPageCombo.trackChanges();
                                gotoPageCombo.set_text(gotoPage_selected_item);
                                gotoPageCombo.commitChanges();
                            }
                            break;
                        case 'if_then_next_page':
                            gotoPage_do_select = true;
                            setComboValue(actions, 'if_then_next_page');
                            var if_parts = parts[1].split(',');

                            var subparts = if_parts[0].split('~');
                            var next_page_condition = document.getElementById("next_page_condition");
                            next_page_condition.value = subparts[1].replaceAll("#gt", ">").replaceAll("#lt", "<").replaceAll("#eq", "=");

                            page_if_true.trackChanges();
                            subparts = if_parts[1].split('~');
                            page_if_true.set_text(subparts[1]);
                            page_if_true.commitChanges();

                            page_if_false.trackChanges();
                            subparts = if_parts[2].split('~');
                            page_if_false.set_text(subparts[1]);
                            page_if_false.commitChanges();

                            break; case 'previous_page':
                            setComboValue(actions, 'previous_page');
                            break;
                        case 'call':
                            var phone = document.getElementById("phone_field");
                            var phone_parts = parts[1].split('~');
                            phone.value = phone_parts[1];
                            setComboValue(actions, 'call');
                            break;
                        case 'share':
                            setComboValue(actions, 'share');
                            var sub_parts = parts[1].split(',');
                            for (var j = 0; j < sub_parts.length; j++) {
                                var share_parts = sub_parts[j].split('~');
                                switch (share_parts[0]) {
                                    case 'subject_field':
                                        var subject = document.getElementById("share_subject_field");
                                        subject.value = share_parts[1];
                                        break;
                                    case 'message_field':
                                        var message = document.getElementById("message_field");
                                        message.value = share_parts[1];
                                        break;
                                    case 'media_link_field':
                                        var media_link = document.getElementById("media_link_field");
                                        media_link.value = share_parts[1];
                                        break;
                                    case 'facebook_account_field':
                                        var facebook_account = document.getElementById("facebook_account_field");
                                        facebook_account.value = share_parts[1];
                                        break;
                                    case 'twitter_account_field':
                                        var twitter_account = document.getElementById("twitter_account_field");
                                        twitter_account.value = share_parts[1];
                                        break;
                                    case 'foursquare_account_field':
                                        var foursquare_account = document.getElementById("foursquare_account_field");
                                        foursquare_account.value = share_parts[1];
                                        break;
                                    case 'sms_phone_field':
                                        var sms_phone = document.getElementById("sms_phone_field");
                                        sms_phone.value = share_parts[1];
                                        break;
                                    case 'to_email_field':
                                        var to_email = document.getElementById("to_email_field");
                                        to_email.value = share_parts[1];
                                        break;
                                }
                            }

                            break;
                        case 'email':
                            var sub_parts = parts[1].split(',');
                            for (var j = 0; j < sub_parts.length; j++) {
                                var share_parts = sub_parts[j].split('~');
                                switch (share_parts[0]) {
                                    case 'message_field':
                                        var message = document.getElementById("message_field2");
                                        message.value = share_parts[1];
                                        break;
                                    case 'media_link_field':
                                        var media_link = document.getElementById("media_link_field2");
                                        media_link.value = share_parts[1];
                                        break;
                                    case 'to_email_field':
                                        var to_email = document.getElementById("to_email_field2");
                                        to_email.value = share_parts[1];
                                        break;
                                    case 'subject_field':
                                        var subject_field = document.getElementById("subject_field");
                                        subject_field.value = share_parts[1];
                                        break;
                                }
                            }
                            setComboValue(actions, 'email');
                            break;
                        case 'sms':
                            var sub_parts = parts[1].split(',');
                            for (var j = 0; j < sub_parts.length; j++) {
                                var share_parts = sub_parts[j].split('~');
                                switch (share_parts[0]) {
                                    case 'message_field':
                                        var message = document.getElementById("message_field3");
                                        message.value = share_parts[1];
                                        break;
                                    case 'media_link_field':
                                        var media_link = document.getElementById("media_link_field3");
                                        media_link.value = share_parts[1];
                                        break;
                                    case 'sms_phone_field':
                                        var sms_phone = document.getElementById("sms_phone_field2");
                                        sms_phone.value = share_parts[1];
                                        break;
                                }
                            }
                            setComboValue(actions, 'sms');
                            break;
                        case 'take_photo':
                            setComboValue(actions, 'take_photo');
                            var image_field = document.getElementById("image_field"); 
                            var compression = document.getElementById("compression");
                            var icon_field = document.getElementById("icon_field");
                            var icon_width = document.getElementById("icon_width");
                            var icon_height = document.getElementById("icon_height");
                            var photo_parts = parts[1].split(',');

                            if (photo_parts.length > 0) {
                                for (var j = 0; j < photo_parts.length; j++) {
                                    var photo_attributes = photo_parts[j].split('~');
                                    switch (photo_attributes[0]) {
                                        case 'image_field':
                                            image_field.value = photo_attributes[1];
                                            break;
                                        case 'compression':
                                            compression.value = photo_attributes[1];
                                            break;
                                        case 'icon_field':
                                            icon_field.value = photo_attributes[1];
                                            break;
                                        case 'icon_width':
                                            icon_width.value = photo_attributes[1];
                                            break;
                                        case 'icon_height':
                                            icon_height.value = photo_attributes[1];
                                            break;
                                    }
                                }
                            }

                            else {
                                icon_field.value = '';
                                icon_width.value = '60';
                                icon_height.value = '80';
                            }
                            break; 
                        case 'capture_doc':
                            setComboValue(actions, 'capture_doc');
                            var doc_selection_field = document.getElementById("doc_case_field");
                            var sub_parts = parts[1].split(',');
                            for (var j = 0; j < sub_parts.length; j++) {
                                var share_parts = sub_parts[j].split('~');
                                if (share_parts.length >= 2) {
                                    switch (share_parts[0]) {
                                        case 'doc_case_field':
                                            var doc_selection_field = document.getElementById("doc_case_field");
                                            doc_selection_field.value = share_parts[1];
                                            break;
                                    }
                                }
                            }
                            break;
                        case 'compute':
                            var compute = document.getElementById("compute");
                            compute.value = parts[1].replaceAll("~", "=").replaceAll("|", ";"); // '=' is a special character, '~' is substituting for it
                            var docompute = document.getElementById("docompute");
                            docompute.checked = true;
                            break;
                        default:
                            break;
                    }
                }
                style = ArgList[4];
            }
        }

        function insertButton() //fires when the Insert Link Switch is clicked
        {
            ArgList = new Array(5);
             var switchName = document.getElementById("switchName");
            if (switchName.value == null || switchName.value == '') {
                alert('Internal Switch Name must be filled');
                return;
            }
            if (!IsValidObjectName(switchName.value)) {
                alert('switch Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            switchName.value = switchName.value.replaceAll(" ", "_");

            var phone = document.getElementById("phone");
            var link = document.getElementById("link");

            ArgList[0] = switchName.value;
           
            var SwitchType = document.getElementById("SwitchType");
            ArgList[1] = SwitchType.control.get_value(); 

            var DefaultValue = document.getElementById("DefaultValue");
            ArgList[2] = DefaultValue.control._toggleStatesData[DefaultValue.control.get_selectedToggleStateIndex()].text.toLowerCase();

            var actionsMultiPage = document.getElementById("actionsMultiPage");
            var page_view = actionsMultiPage.control.get_selectedPageView();
            var view = page_view.get_id();

            if (view == 'next_page_view') {
                next_page_value = document.getElementById("GoToPages").control.get_text();
                if (next_page_value == null || next_page_value.length == 0 || contains(next_page_value, 'Choose Field or Enter')) {
                    alert('The name of the next page must be set');
                    return;
                }
                ArgList[3] = 'next_page:page~' + next_page_value.replaceAll(" ", "_") + ";";
            }
            else if (view == 'if_then_next_page_view') {
                var next_page_condition = document.getElementById("next_page_condition");
                next_page_condition_value = next_page_condition.value;
                var page_if_true = document.getElementById("page_if_true");
                page_if_true_value = page_if_true.control.get_text();
                var page_if_false = document.getElementById("page_if_false");
                page_if_false_value = page_if_false.control.get_text();
                if (next_page_condition_value == null || next_page_condition_value.length == 0) {
                    alert('The if condition must be set');
                    return;
                }
                if (!contains(next_page_condition_value, '>') &&
                    !contains(next_page_condition_value, '<') &&
                    !contains(next_page_condition_value, '=')) {
                    alert('The if condition must contain a logical operation');
                    return;
                }
                if (page_if_true_value.length == 0 || contains(page_if_true_value, 'Choose Field or Enter')) {
                    alert('The page for the true condition must be set');
                    return;
                }
                if (page_if_false_value.length == 0 || contains(page_if_false_value, 'Choose Field or Enter')) {
                    alert('The page for the false condition must be set');
                    return;
                }
                ArgList[3] = 'if_then_next_page:next_page_condition_value~' + next_page_condition_value.replaceAll("<", "#lt").replaceAll(">", "#gt").replaceAll("=", "#eq") + ',page_if_true_value~' + page_if_true_value.replaceAll(" ", "_") + ',page_if_false_value~' + page_if_false_value.replaceAll(" ", "_") + ";";
            }
            else if (view == 'previous_page_view') {
                ArgList[3] = 'previous_page;';
            }
            else if (view == 'post_view') {
                    ArgList[3] = "post:;";
            }
            else if (view == 'call_view') {
                var phone = document.getElementById("phone_field");
                if (phone.value.length == 0) {
                    alert('The name of the phone field must be set.');
                    return;
                }
                if (!IsValidObjectName(phone.value)) {
                    alert('Phone field can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                    return;
                }
                ArgList[3] = 'call:phone_field~' + phone.value.replaceAll(" ", "_") + ";";
            }
            else if (view == 'share_view') {
                var subject = document.getElementById("share_subject_field");
                var message = document.getElementById("message_field");
                var media_link = document.getElementById("media_link_field");
                var facebook_account = document.getElementById("facebook_account_field");
                var twitter_account = document.getElementById("twitter_account_field");
                var foursquare_account = document.getElementById("foursquare_account_field");
                var sms_phone = document.getElementById("sms_phone_field");
                var to_email = document.getElementById("to_email_field");
                ArgList[3] = 'share:subject_field~' + subject.value.replaceAll(" ", "_") +
                             ',message_field~' + message.value.replaceAll(" ", "_") +
                              ',media_link_field~' + media_link.value.replaceAll(" ", "_") +
                             /*',facebook_account_field~' + facebook_account.value +
                              ',twitter_account_field~' + twitter_account.value +
                            ',foursquare_account_field~' + foursquare_account.value +*/
                             ',sms_phone_field~' + sms_phone.value.replaceAll(" ", "_") +
                             ',to_email_field~' + to_email.value.replaceAll(" ", "_") + ';';
            }
            else if (view == 'email_view') {
                var to_email_field2 = document.getElementById("to_email_field2");
                var subject_field = document.getElementById("subject_field");
                var message_field2 = document.getElementById("message_field2");
                var media_link_field2 = document.getElementById("media_link_field2");
                ArgList[3] = 'email:to_email_field~' + to_email_field2.value.replaceAll(" ", "_") +
                    ',subject_field~' + subject_field.value.replaceAll(" ", "_") +
                    ',media_link_field~' + media_link_field2.value.replaceAll(" ", "_") +
                    ',message_field~' + message_field2.value.replaceAll(" ", "_") + ';';
            }
            else if (view == 'sms_view') {
                var sms_phone_field2 = document.getElementById("sms_phone_field2");
                var message_field3 = document.getElementById("message_field3");
                var media_link_field3 = document.getElementById("media_link_field3");
                ArgList[3] = 'sms:sms_phone_field~' + sms_phone_field2.value.replaceAll(" ", "_") +
                ',media_link_field~' + media_link_field3.value.replaceAll(" ", "_") +
                ',message_field~' + message_field3.value.replaceAll(" ", "_") + ';';
            }
            else if (view == 'take_photo_view') {
                ArgList[3] = 'take_photo:';
                var image_field = document.getElementById("image_field");
                if (image_field.value == null || image_field.value == '') {
                    alert('Photo image field must be filled');
                    return;
                }
                if (!IsValidObjectName(image_field.value)) {
                    alert('Photo image field can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                    return;
                }

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

                ArgList[3] += 'image_field~' + image_field.value.replaceAll(" ", "_") + ',compression~' + compression.value;

                var icon_field = document.getElementById("icon_field");
                var icon_width = document.getElementById("icon_width");
                var icon_height = document.getElementById("icon_height");
                varicon_attributes = '';
                if (icon_field.value != '')
                    ArgList[3] += ",icon_field~" + icon_field.value.replaceAll(" ", "_") + ",icon_width~" + icon_width.value + ",icon_height~" + icon_height.value;

                ArgList[3] += ';';
            }
            else if (view == 'capture_doc_view') {
                var doc_case_field = document.getElementById("doc_case_field");
                ArgList[3] = 'capture_doc:doc_case_field~' + doc_case_field.value + ';';
            }
            else if (view == 'mobile_commerce_view') {
                var mobile_commerce_username = document.getElementById("mobile_commerce_username");
                if (mobile_commerce_username.value.length == 0) {
                    alert('The mobile commerce username must be set.');
                    return;
                }
                var mobile_commerce_password = document.getElementById("mobile_commerce_password");
                if (mobile_commerce_password.value.length == 0) {
                    alert('The mobile commerce password must be set.');
                    return;
                }
                ArgList[3] = 'mobile_commerce:mobile_commerce_username~' + mobile_commerce_username.value + ',mobile_commerce_password~' + mobile_commerce_password.value;
                error = false;
                $('.param').each(function (index, element) {
                    if (element.value.length == 0) {
                        alert('All parameters must have values.');
                        error = true;
                        return;
                    }
                    ArgList[3] += ',' + element.id + '~' + element.value;
                });
                if (error)
                    return;
                ArgList[3] += ';';
            }
            else
                ArgList[3] = "";

            var docompute = document.getElementById("docompute");
            if (docompute.checked) {
                var compute = document.getElementById("compute");
                if (compute.value == null || compute.value == '') {
                    alert('Compute field must be filled');
                    return;
                }
                ArgList[3] += 'compute:' + compute.value.replaceAll("=", "~").replaceAll(";", "|") + ';'; // '=' is a special character, '~' is substituting for it
            }
            ArgList[4] = style;

            parent.window.InsertSwitchCallback(ArgList);
        }
        function getActionsIndex(arg) {
            var actions = document.getElementById("actions");
            var items = actions.control.get_visibleItems();
            for (var i = 0; i < items.length; i++) {
                if (items[i].get_value() == arg) {
                    return i;
                }
            }
            return 0;
        }
        function IsNumeric(input) {
            return /^-?(0|[1-9]\d*|(?=\.))(\.\d+)?$/.test(input);
        } 