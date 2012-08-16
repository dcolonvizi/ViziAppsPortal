        if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        var ArgList = null;

        var combo;
        var do_select = false;
        var selected_item;
        function OnClientRowTypeLoadHandler(sender) {
            combo = sender;
        }

        var gotoPageCombo;
        var gotoPage_do_select = false;
        var table_options;
        var gotoPage_selected_item;
        function OnClientGoToPagesLoadHandler(sender) {
            gotoPageCombo = sender;
            if (gotoPage_do_select ) {
                gotoPage_do_select = false;
                gotoPageCombo.trackChanges();
                var item = gotoPageCombo.findItemByValue(gotoPage_selected_item);
                if (item && gotoPageCombo.get_value() != gotoPage_selected_item) {
                    item.select();
                }
                else {
                    var newGoToPage = document.getElementById("newGoToPage");
                   newGotoPage.value = gotoPage_selected_item;
                }
                gotoPageCombo.commitChanges();
            }

            if (table_options != undefined) {

                var sections = table_options.split('|');
                if (sections.length == 1) {
                    var section1Options = document.getElementById("section1Options");
                    section1Options.value = sections[0].replaceAll(';', '\n');
                }
                else if (sections.length == 2) {
                    var sections2Options1 = document.getElementById("sections2Options1");
                    sections2Options1.value = sections[0].replaceAll(';', '\n');
                    var sections2Options2 = document.getElementById("sections2Options2");
                    sections2Options2.value = sections[1].replaceAll(';', '\n');
                }
                else if (sections.length == 3) {
                    var sections3Options1 = document.getElementById("sections3Options1");
                    sections3Options1.value = sections[0].replaceAll(';', '\n');
                    var sections3Options2 = document.getElementById("sections3Options2");
                    sections3Options2.value = sections[1].replaceAll(';', '\n');
                    var sections3Options3 = document.getElementById("sections3Options3");
                    sections3Options3.value = sections[2].replaceAll(';', '\n');
                }
                else if (sections.length == 4) {
                    var sections4Options1 = document.getElementById("sections4Options1");
                    sections4Options1.value = sections[0].replaceAll(';', '\n');
                    var sections4Options2 = document.getElementById("sections4Options2");
                    sections4Options2.value = sections[1].replaceAll(';', '\n');
                    var sections4Options3 = document.getElementById("sections4Options3");
                    sections4Options3.value = sections[2].replaceAll(';', '\n');
                    var sections4Options4 = document.getElementById("sections4Options4");
                    sections4Options4.value = sections[3].replaceAll(';', '\n');
                }

                table_options = undefined;
            }
        }

        $(document).ready(function () {
           
        });

        function tabSelected(sender,args) {
            if (sender.get_selectedIndex() == 1)
            {
                var field_names = document.getElementById("field_names");
                var list = field_names.value.split(',');
                for (var ii = 0; ii < list.length; ii++) {
                    var class_number = ii+1;
                    var class_name = 'section' + class_number;
                    var selection = $('.' + class_name);
                    $('.' + class_name).text(list[ii]);
                }

                if (ArgList[4].length > 0) {

                    table_options = ArgList[4];

                    var sections = table_options.split('|');
                    if (sections.length == 1) {
                        var section1Options = document.getElementById("section1Options");
                        section1Options.value = sections[0].replaceAll(';', '\n');
                    }
                    else if (sections.length == 2) {
                        var sections2Options1 = document.getElementById("sections2Options1");
                        sections2Options1.value = sections[0].replaceAll(';', '\n');
                        var sections2Options2 = document.getElementById("sections2Options2");
                        sections2Options2.value = sections[1].replaceAll(';', '\n');
                    }
                    else if (sections.length == 3) {
                        var sections3Options1 = document.getElementById("sections3Options1");
                        sections3Options1.value = sections[0].replaceAll(';', '\n');
                        var sections3Options2 = document.getElementById("sections3Options2");
                        sections3Options2.value = sections[1].replaceAll(';', '\n');
                        var sections3Options3 = document.getElementById("sections3Options3");
                        sections3Options3.value = sections[2].replaceAll(';', '\n');
                    }
                    else if (sections.length == 4) {
                        var sections4Options1 = document.getElementById("sections4Options1");
                        sections4Options1.value = sections[0].replaceAll(';', '\n');
                        var sections4Options2 = document.getElementById("sections4Options2");
                        sections4Options2.value = sections[1].replaceAll(';', '\n');
                        var sections4Options3 = document.getElementById("sections4Options3");
                        sections4Options3.value = sections[2].replaceAll(';', '\n');
                        var sections4Options4 = document.getElementById("sections4Options4");
                        sections4Options4.value = sections[3].replaceAll(';', '\n');
                    }
                }
            }
        }

        function changeAction(sender, args) {
            var view_name = null;
            switch (sender.get_value()) {
                case 'page_from_field':
                case 'select':
                case 'if_then_next_page':
                case 'previous_page':
                case 'next_page':
                case 'post':
                case 'call':
                case 'share':
                case 'email':
                case 'sms':
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

            if (validations != null && validations.indexOf('-') >= 0) {
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td>[' + validations + ']</td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');
            }
            else if (validations != null && validations.indexOf('TEXTAREA') >= 0) {
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><textarea class="param" rows="4" cols="50" id="' + id + '" size="30" ></textarea></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');
            }
            else if (validations != null && validations != param && (validations.indexOf('NAME') < 0) && validations != 'URL') {
                var options = '<option>select -&gt;</option>';
                var validation_list = validations.split(',');
                for (var i = 0; i < validation_list.length; i++) {
                    options += '<option>' + validation_list[i] + '</option>';
                }
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><select class="param" id="' + id + '" >"' + options + '"</select></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');
            }
            else
                $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../images/delete_small.gif"  /></td></tr></table></div>');

            $('#' + id).focus();
            $('#' + id).change(function (event) {
                if (validations != null && validations != param) {
                    var match_found = false;
                    if (validations.indexOf('TEXTAREA') >= 0) {
                    }
                    else if (validations.indexOf('NAME') >= 0) {
                        this.value = this.value.replaceAll(' ', '_');
                        if (!IsValidObjectName(this.value)) {
                            alert('entry can only contain either a letter, number, "_" and be 1 to 100 characters long');
                            return;
                        }
                    }
                    else if (validations == 'URL') {
                        this.value = this.value.replaceAll(' ', '%20');
                        if (this.value.indexOf('http://') != 0 && this.value.indexOf('https://') != 0) {
                            alert('entry is not a valid URL');
                            return;
                        }
                    }
                    else if (validations.indexOf('-') >= 0) {//this means validation is a range of numeric values
                        var limits = validations.split('-');
                        var i_value = parseInt(this.value);
                        if (i_value >= parseInt(limits[0]) && i_value <= parseInt(limits[1])) {
                            match_found = true;
                        }
                        if (!match_found) {
                            alert('entry is outside the valid range');
                            return;
                        }
                    }
                    else {//else it is a list of possible values
                        var possible_list = validations.split(',');
                        for (var i = 0; i < possible_list.length; i++) {
                            if (this.value == possible_list[i].replaceAll(' ', '')) {
                                match_found = true;
                                break;
                            }
                        }
                        if (!match_found) {
                            alert('entry does not match possible values');
                            return;
                        }
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
            ArgList = parent.window.getDialogInputArgs();
            for (var i = 0; i < ArgList.length; i++) {
                if (ArgList[i] == null)
                    ArgList[i] = '';
            }
            var tableName = document.getElementById("tableName");
            tableName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Table";                
 
                var tableDisplayName = document.getElementById("tableDisplayName");
                if (ArgList[1].length > 0)
                    tableDisplayName.value = ArgList[1];
                else
                    tableDisplayName.value = ArgList[0];

                var actionsMultiPage = document.getElementById("actionsMultiPage");
                var actions = document.getElementById("actions");
                var view_response_radios = document.getElementsByName("view_response");

                var parts = ArgList[2].split(':');
                do_select = true;
                selected_item = parts[0];

                if (combo.get_value() != selected_item) {
                    setComboValue(combo, selected_item);
                 }

                var field_names = document.getElementById("field_names");
                field_names.value = parts[1];

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
                        case 'page_from_field':
                            setComboValue(actions, 'page_from_field');
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

                            break;
                         case 'previous_page':
                            setComboValue(actions, 'previous_page');
                            break;
                        case 'select':
                            var count_field = document.getElementById("count_field");
                            var count_field_parts = parts[1].split('~');
                            if(count_field_parts != null && count_field_parts.length > 1)
                                count_field.value = count_field_parts[1];
                            setComboValue(actions, 'select');
                            break;
                        case 'call':
                            var phone = document.getElementById("device_field");
                            var phone_parts = parts[1].split('~');
                            phone.value = phone_parts[1];
                            setComboValue(actions, 'call');
                            break;
                        case 'share':
                            setComboValue(actions, 'share');
                            var sub_parts = parts[1].split(',');
                            for (var j = 0; j < sub_parts.length; j++) {
                                var share_parts = sub_parts[j].split('~');
                                var param = share_parts[0].substring(0, 1).toUpperCase() + share_parts[0].substring(1).replaceAll("_", " ");
                                addProperty('addSharePropertyDiv', param, null);
                                $('#' + share_parts[0]).attr('value', share_parts[1].replaceAll('%3A', ':'));
                            }
                            $('#addSharePropertyDiv').css('display', 'block');
                            break;
                         case 'email':
                             setComboValue(actions, 'email');
                             var sub_parts = parts[1].split(',');
                             for (var j = 0; j < sub_parts.length; j++) {
                                 var share_parts = sub_parts[j].split('~');
                                 var param = share_parts[0].substring(0, 1).toUpperCase() + share_parts[0].substring(1).replaceAll("_", " ");
                                 addProperty('addEmailPropertyDiv', param, null);
                                 $('#' + share_parts[0]).attr('value', share_parts[1].replaceAll('%3A', ':'));
                             }
                             $('#addEmailPropertyDiv').css('display', 'block');
                             break;
                         case 'sms':
                             setComboValue(actions, 'sms');
                             var sub_parts = parts[1].split(',');
                             for (var j = 0; j < sub_parts.length; j++) {
                                 var share_parts = sub_parts[j].split('~');
                                 var param = share_parts[0].substring(0, 1).toUpperCase() + share_parts[0].substring(1).replaceAll("_", " ");
                                 addProperty('addSMSPropertyDiv', param, null);
                                 $('#' + share_parts[0]).attr('value', share_parts[1].replaceAll('%3A', ':'));
                             }
                             $('#addSMSPropertyDiv').css('display', 'block');
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

                table_options = ArgList[4];

                var sections = table_options.split('|');
                if (sections.length == 1) {
                    var section1Options = document.getElementById("section1Options");
                    section1Options.value = sections[0].replaceAll(';', '\n');
                }
                else if (sections.length == 2) {
                    var sections2Options1 = document.getElementById("sections2Options1");
                    sections2Options1.value = sections[0].replaceAll(';', '\n');
                    var sections2Options2 = document.getElementById("sections2Options2");
                    sections2Options2.value = sections[1].replaceAll(';', '\n');
                }
                else if (sections.length == 3) {
                    var sections3Options1 = document.getElementById("sections3Options1");
                    sections3Options1.value = sections[0].replaceAll(';', '\n');
                    var sections3Options2 = document.getElementById("sections3Options2");
                    sections3Options2.value = sections[1].replaceAll(';', '\n');
                    var sections3Options3 = document.getElementById("sections3Options3");
                    sections3Options3.value = sections[2].replaceAll(';', '\n');
                }
                else if (sections.length == 4) {
                    var sections4Options1 = document.getElementById("sections4Options1");
                    sections4Options1.value = sections[0].replaceAll(';', '\n');
                    var sections4Options2 = document.getElementById("sections4Options2");
                    sections4Options2.value = sections[1].replaceAll(';', '\n');
                    var sections4Options3 = document.getElementById("sections4Options3");
                    sections4Options3.value = sections[2].replaceAll(';', '\n');
                    var sections4Options4 = document.getElementById("sections4Options4");
                    sections4Options4.value = sections[3].replaceAll(';', '\n');
                }

                style = ArgList[5];
            }
        }
        function FilterOptions(options) {
            var filtered = options.replaceAll("\r\n", ";").replaceAll("\n", ";").replaceAll(";;", ";");
            if (filtered.charAt(filtered.length - 1) == ';')
                return filtered.substring(0,filtered.length - 1);
            else
                return filtered;
        }
        function insertButton() //fires when the Insert Link button is clicked
        {
            var tableName = document.getElementById("tableName");
            if (tableName.value == null || tableName.value == '') {
                alert('Internal Table Name must be filled');
                return;
            }
            if (!IsValidObjectName(tableName.value)) {
                alert('Table Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            tableName.value = tableName.value.replaceAll(" ", "_");
            ArgList[0] = tableName.value;

            var tableDisplayName = document.getElementById("tableDisplayName");
            if (tableDisplayName.value == null || tableDisplayName.value == '') {
                alert('Table Display Name must be filled');
                return;
            }
            ArgList[1] = tableDisplayName.value;
            var field_names = document.getElementById("field_names");
            if (field_names.value == null || field_names.value == '') {
                alert('No Field Names');
                return;
            }

            field_names.value = field_names.value.replaceAll(" ", "_");
            var names = field_names.value.split(',');
            var n_fields = names.length;
            var combo_value = combo.get_value();
            var err = 'The number of fields for the selected table row type do not match';
            switch (combo_value) {
                case '1text|text':
                    if (n_fields != 1) {
                        alert(err);
                        return;
                    }
                    break;
                case '2texts|text,text':
                case '1texthidden|text,hidden':
                case 'imagetext|image,text':
                case 'image1text|image,text':
                    if (n_fields != 2) {
                        alert(err);
                        return;
                    }
                    break;
                case '2textshidden|text,text,hidden':
                case 'image1texthidden|image,text,hidden':
                case 'image2texts|image,text,text':
                    if (n_fields != 3) {
                        alert(err);
                        return;
                    }
                    break;
                case 'image2textshidden|image,text,text,hidden':
                    if (n_fields != 4) {
                        alert(err);
                        return;
                    }
                    break;
            }
            ArgList[2] = combo.get_value() + ":";
            var is_first = true;
            for (var i = 0; i < names.length; i++) {
                names[i] = names[i].trim().replaceAll(" ", "_");
                if (tableName.value == names[i]) {
                    alert('The Table Name and the Table Field Name ' + names[i] + ' cannot be the same');
                    return;
                }
                 if (is_first)
                    is_first = false;
                else
                    ArgList[2] += ",";

                ArgList[2] += names[i];
            }

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
            else if (view == 'page_from_field_view') {
                if (combo_value == '1texthidden|text,hidden' ||
                    combo_value == '2textshidden|text,text,hidden' ||
                    combo_value == 'image1texthidden|image,text,hidden' ||
                    combo_value == 'image2textshidden|image,text,text,hidden') {                  
                     ArgList[3] = 'page_from_field:field~' + names[names.length-1] + ';';
                }
                else{                
                     alert('The action you chose requires a row type with a hidden field.');
                        return;
                    }                        
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
            else if (view == 'select_view') {
                var count_field = document.getElementById("count_field");
                ArgList[3] = 'select:count_field~' + count_field.value.replaceAll(" ", "_") + ";";
            }
            else if (view == 'post_view') {
                    ArgList[3] = "post:;";
            }
            else if (view == 'call_view') {
                var phone = document.getElementById("device_field");
                if (phone.value.length == 0) {
                    alert('The name of the phone field must be set.');
                    return;
                }
                ArgList[3] = 'call:phone_field~' + phone.value.replaceAll(" ", "_") + ";";
            }
            else if (view == 'share_view') {
                ArgList[3] = 'share:';
                error = false;
                $('.param').each(function (index, element) {
                    if (element.value.length == 0) {
                        alert('All parameters must have values.');
                        error = true;
                        return;
                    }
                    if (index > 0)
                        ArgList[3] += ',';
                    ArgList[3] += element.id + '~' + element.value.replaceAll(':', '%3A');
                });
                if (error)
                    return;
                ArgList[3] += ';';
            }
            else if (view == 'email_view') {
                ArgList[3] = 'email:';
                error = false;
                $('.param').each(function (index, element) {
                    if (element.value.length == 0) {
                        alert('All parameters must have values.');
                        error = true;
                        return;
                    }
                    if (index > 0)
                        ArgList[3] += ',';
                    ArgList[3] += element.id + '~' + element.value.replaceAll(':', '%3A');
                });
                if (error)
                    return;
                ArgList[3] += ';';
            }
            else if (view == 'sms_view') {
                ArgList[3] = 'sms:';
                error = false;
                $('.param').each(function (index, element) {
                    if (element.value.length == 0) {
                        alert('All parameters must have values.');
                        error = true;
                        return;
                    }
                    if (index > 0)
                        ArgList[3] += ',';
                    ArgList[3] += element.id + '~' + element.value.replaceAll(':', '%3A');
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

            //save the options
             var options = document.getElementById("options");
             var bar = '|';

            var SectionPages = document.getElementById("SectionPages");
            var selected_index = SectionPages.control.get_selectedIndex();

            if (selected_index == 0) { 
                options.value = FilterOptions(document.getElementById("section1Options").value);
            }
            else if (selected_index == 1) {
                var field1_options = FilterOptions(document.getElementById("sections2Options1").value);
                var n_field1_options = getNOptions(field1_options);
                var field2_options = FilterOptions(document.getElementById("sections2Options2").value);
                var n_field2_options = getNOptions(field2_options);
                if (n_field1_options > 0 || n_field2_options > 0) {
                    if (n_field1_options != n_field2_options) {
                        alert('The number of field options in field 1 (' + n_field1_options + ') does not equal the number of field options in field 2 (' + n_field2_options + ')');
                        return;
                    }
                    options.value = field1_options + bar + field2_options;
                }
            }
            else if (selected_index == 2) {
                var field1_options = FilterOptions(document.getElementById("sections3Options1").value);
                var n_field1_options = getNOptions(field1_options);
                var field2_options = FilterOptions(document.getElementById("sections3Options2").value);
                var n_field2_options = getNOptions(field2_options);
                var field3_options = FilterOptions(document.getElementById("sections3Options3").value);
                var n_field3_options = getNOptions(field3_options);
                if (n_field1_options > 0 || n_field2_options > 0 || n_field3_options > 0) {
                    if (n_field1_options != n_field2_options || n_field1_options != n_field3_options) {
                        alert('The number of field options in the fields do not  match: ' + n_field1_options + ', ' + n_field2_options + ', ' + n_field3_options );
                        return;
                    }
                    options.value = field1_options + bar + field2_options + bar + field3_options;
                }
            }
            else if (selected_index == 3) {
                var field1_options = FilterOptions(document.getElementById("sections4Options1").value);
                var n_field1_options = getNOptions(field1_options);
                var field2_options = FilterOptions(document.getElementById("sections4Options2").value);
                var n_field2_options = getNOptions(field2_options);
                var field3_options = FilterOptions(document.getElementById("sections4Options3").value);
                var n_field3_options = getNOptions(field3_options);
                var field4_options = FilterOptions(document.getElementById("sections4Options4").value);
                var n_field4_options = getNOptions(field4_options);
                if (n_field1_options > 0 || n_field2_options > 0 || n_field3_options > 0 || n_field4_options > 0) {
                    if (n_field1_options != n_field2_options || n_field1_options != n_field3_options || n_field1_options != n_field4_options) {
                        alert('The number of field options in the fields do not  match: ' + n_field1_options + ', ' + n_field2_options + ', ' + n_field3_options + ', ' + n_field4_options);
                        return;
                    }
                    options.value = field1_options + bar + field2_options + bar + field3_options + bar + field3_options;
                }
            }

             ArgList[4] = options.value;

             ArgList[5] = style;

             parent.window.InsertTableCallback(ArgList);
         }

         function getNOptions(options) {
             if (options.length == 0)
                 return 0;
             var n = 1;
             for (var i = 0; i < options.length; i++) {
                 if (options.charAt(i) == ';') {
                     n++;
                 }
             }
             return n;
         }
  
