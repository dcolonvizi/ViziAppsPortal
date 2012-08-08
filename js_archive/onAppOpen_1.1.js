            if (window.attachEvent) {
                window.attachEvent("onload", initDialog);
            }
            else if (window.addEventListener) {
                window.addEventListener("load", initDialog, false);
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
                    $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td>[' + validations + ']</td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../../images/delete_small.gif"  /></td></tr></table></div>');
                }
                else if (validations != null && validations.indexOf('TEXTAREA') >= 0) {
                    $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><textarea class="param" rows="4" cols="50" id="' + id + '" size="30" ></textarea></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../../images/delete_small.gif"  /></td></tr></table></div>');
                }
                else if (validations != null && validations != param && (validations.indexOf('NAME') < 0) && validations != 'URL') {
                    var options = '<option>select -&gt;</option>';
                    var validation_list = validations.split(',');
                    for (var i = 0; i < validation_list.length; i++) {
                        options += '<option>' + validation_list[i] + '</option>';
                    }
                    $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><select class="param" id="' + id + '" >"' + options + '"</select></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../../images/delete_small.gif"  /></td></tr></table></div>');
                }
                else
                    $('#' + prop_id).before('<div id="' + div_id + '"><table style="width: 600px"><tr><td align="left" valign="top" style="width:180px" >' + param + '</td><td valign="top"><input class="param" type="text" id="' + id + '" size="30" /></td><td style="width:20px"><img id="' + delete_id + '" alt="delete" src="../../images/delete_small.gif"  /></td></tr></table></div>');

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
                    setActionValue();
                });

                $('#' + delete_id).click(function (event) {
                    var delete_id = event.target.attributes[2].value;
                    var div_id = delete_id.substring(0, delete_id.length - 7) + '_div';
                    $('#' + div_id).remove();
                    $('#' + prop_id).css('display', 'block');
                    setActionValue();
                });
            }

            function changeAction(sender, args) {
                var view_name = null;
                switch (sender.get_value()) {
                    case 'post':
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

            function initDialog() {
                var action_value = document.getElementById("action_value");
                if (action_value.value.length > 0) {
                    var actions = action_value.value.split(';');
                    var parts = actions[0].split(':');
                    var action = parts[0];
                    var sub_parts = parts[1].split(',');

                    if (action == 'capture_process_document') {
                       
                        for (var j = 0; j < sub_parts.length; j++) {
                            var share_parts = sub_parts[j].split('~');
                            var param = share_parts[0].substring(0, 1).toUpperCase() + share_parts[0].substring(1).replaceAll("_", " ");
                            addProperty('addCaptureProcessDocumentPropertyDiv', param, null);
                            $('#' + share_parts[0]).attr('value', share_parts[1].replaceAll('%3A', ':'));
                        }
                        $('#addCaptureProcessDocumentPropertyDiv').css('display', 'block');
                    }
                    else if (action == 'manage_document_case') {
                       
                        for (var j = 0; j < sub_parts.length; j++) {
                            var share_parts = sub_parts[j].split('~');
                            var param = share_parts[0].substring(0, 1).toUpperCase() + share_parts[0].substring(1).replaceAll("_", " ");
                            if (param == 'About box text') {
                                addProperty('addManageDocumentCasePropertyDiv', param, 'TEXTAREA');
                                $('#' + share_parts[0]).attr('value', unescape(share_parts[1]));
                            }
                            else {
                                addProperty('addManageDocumentCasePropertyDiv', param, null);
                                $('#' + share_parts[0]).attr('value', share_parts[1].replaceAll('%3A', ':'));
                            } 
                        }
                        $('#addManageDocumentCasePropertyDiv').css('display', 'block');
                    }
                }
            }
            function setActionValue() {
                var actionsMultiPage = document.getElementById("actionsMultiPage");
               var page_view = actionsMultiPage.control.get_selectedPageView();
               var view = page_view.get_id();
               var action_value = document.getElementById("action_value");
               if (view == 'post_view') {
                     action_value = ";";
               }
               else if (view == 'capture_process_document_view') {
                  action_value.value = 'capture_process_document:';
                    error = false;
                    $('.param').each(function (index, element) {
                        if (element.value.length == 0) {
                            alert('All parameters must have values.');
                            error = true;
                            return;
                        }
                        if (index > 0)
                            action_value.value += ',';
                        action_value.value += element.id + '~' + element.value.replaceAll(':', '%3A');
                    });
                    if (error)
                        return;
                    action_value.value += ';';
                }
                else if (view == 'manage_document_case_view') {
                    action_value.value = 'manage_document_case:';
                    error = false;
                    $('.param').each(function (index, element) {
                        if (element.value.length == 0) {
                            alert('All parameters must have values.');
                            error = true;
                            return;
                        }
                        if (index > 0)
                            action_value.value += ',';
                        if (element.value.indexOf('<') >= 0 || element.value.indexOf('\n') >= 0)
                            action_value.value += element.id + '~' + escape(element.value);
                        else
                            action_value.value += element.id + '~' + element.value.replaceAll(':', '%3A');
                    });
                    if (error)
                        return;
                    action_value.value += ';';
                }
                else
                    action_value.value = "";

                var docompute = document.getElementById("docompute");
                if (docompute.checked) {
                    var compute = document.getElementById("compute");
                    if (compute.value == null || compute.value == '') {
                        alert('Compute field must be filled');
                        return;
                    }
                    action_value.value += 'compute:' + compute.value.replaceAll("=", "~").replaceAll(";", "|").replaceAll(">", "#gt").replaceAll("<", "#lt") + '|'; // '=' is a special character, '~' is substituting for it
                }
            }
            function PopUp(url, features) {
                var PUtest = window.open(url, '_blank', features);
                if (PUtest == null) {
                    alert('For correct operation, popups need to be allowed from this website.');
                }
            }
            