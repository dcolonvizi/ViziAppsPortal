
    if (window.attachEvent)
    {
        window.attachEvent("onload", initDialog);
    }
    else if (window.addEventListener)
    {
        window.addEventListener("load", initDialog, false);
    }

    var ArgList = null;

    function getRadWindow()
    {
   	    if (window.radWindow)
	    {
		    return window.radWindow;
	    }
	    if (window.frameElement && window.frameElement.radWindow)
	    {
		    return window.frameElement.radWindow;
	    }
	    return null;
    }

    var combo;
    var do_select = false;
    var selected_item;
    var picker_options;
    function OnClientPickerTypeLoadHandler(sender) {
        combo = sender;
       
        if (picker_options != undefined) {
           
                var sections = picker_options.split('|');
                if (sections.length == 1 || getAppType() == 'web' || getAppType() == 'hybrid') {
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
          
            picker_options = undefined;
        }
       
    }
    
    function initDialog() {
        ArgList = parent.window.getDialogInputArgs();
        for (var i = 0; i < ArgList.length; i++) {
            if (ArgList[i] == null)
                ArgList[i] = '';
        }
        var PickerName = document.getElementById("PickerName");
        PickerName.value = ArgList[0];

        if (ArgList[0].length > 0) {
            var insertButtonID = document.getElementById("insertButtonID");
            insertButtonID.value = "Update Picker";         

            var parts = ArgList[1].split(':');
            do_select = true;
            selected_item = parts[0];

            var section_names = document.getElementById("section_names");
            section_names.value = parts[1];

            picker_options = ArgList[2];

            OnClientPickerTypeLoadHandler(combo);
           /* var sections = picker_options.split('|');
            if (sections.length == 1) {
                var section1Options = document.getElementById("section1Options");
                section1Options.value = sections[0].replaceAll(';', '\n');
            }*/

            if (combo.get_value() != selected_item) {
                combo.trackChanges();
                var item = combo.findItemByValue(selected_item);
                if (item) {
                    item.select();
                }
                combo.commitChanges();
            }


            style = ArgList[3];
        }
    }
    function FilterOptions(options)
    {
        var filtered = options.replaceAll("\r\n", ";").replaceAll("\n", ";").replaceAll(";;", ";");
        if (filtered[filtered.Length - 1] == ';')
            return filtered.remove(filtered.Length - 1);
        else
            return filtered;
    }
    function insertPicker() //fires when the Insert Link Picker is clicked
    {
        var combo_value = combo.get_value();
        if (combo_value != "date" && combo_value != "time") {

            //first save the options
            var SectionPages = document.getElementById("SectionPages");
            var selected_index = SectionPages.control._selectedIndex;
            var options = document.getElementById("options");
            var bar = '|';
            if (selected_index == 1) { //if 0 do nothing
                options.value = FilterOptions(document.getElementById("section1Options").value);
            }
            else if (selected_index == 2) {
                options.value = FilterOptions(document.getElementById("sections2Options1").value) +
            bar + FilterOptions(document.getElementById("sections2Options2").value);
            }
            else if (selected_index == 3) {
                options.value = FilterOptions(document.getElementById("sections3Options1").value) +
            bar + FilterOptions(document.getElementById("sections3Options2").value) +
            bar + FilterOptions(document.getElementById("sections3Options3").value);

            }
            else if (selected_index == 4) {
                options.value = FilterOptions(document.getElementById("sections4Options1").value) +
            bar + FilterOptions(document.getElementById("sections4Options2").value) +
            bar + FilterOptions(document.getElementById("sections4Options3").value) +
            bar + FilterOptions(document.getElementById("sections4Options4").value);
            }
        }

        var PickerName = document.getElementById("PickerName");
        if (PickerName.value == null || PickerName.value == '') {
            alert('Internal Picker Name must be filled');
            return;
        }
        if (!IsValidObjectName(PickerName.value)) {
            alert('Picker Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
            return;
        }
        PickerName.value = PickerName.value.replaceAll(" ", "_");


        ArgList[0] = PickerName.value;

        if (combo_value != "date" && combo_value != "time")
        {
            var section_names = document.getElementById("section_names");
            if (section_names.value == null || section_names.value == '') {
                alert('No Section Names');
                return;
            }

            section_names.value = section_names.value.replaceAll(" ", "_");
            var names = section_names.value.split(',');
            var n_fields = names.length;
            var err = 'The number of fields for the selected picker type do not match';
            switch (combo_value) {
                case '1_section':
                    if (n_fields != 1) {
                        alert(err);
                        return;
                    }
                    break;
                 case '2_sections':
                    if (n_fields != 2) {
                        alert(err);
                        return;
                    }
                    break;
                case '3_sections':
                    if (n_fields != 3) {
                        alert(err);
                        return;
                    }
                    break;
                case '4_sections':
                    if (n_fields != 4) {
                        alert(err);
                        return;
                    }
                    break;
            }

            ArgList[1] = combo_value + ":";
            var is_first = true;
            for(var i=0;i<names.length;i++) {
                names[i] = names[i].trim().replaceAll(" ", "_");
                if (PickerName.value == names[i]) {
                    alert('The Picker Name and the Picker Section Name ' + names[i] + ' cannot be the same');
                    return;
                }
                if (is_first)
                    is_first = false;
                else
                    ArgList[1] += ",";

                ArgList[1] += names[i];
            }

            var options = document.getElementById("options");
            ArgList[2] = options.value;
        }
        else {
            ArgList[1] = combo_value;
            ArgList[2] = '';
        }

        ArgList[3] = style;

        parent.window.InsertPickerCallback(ArgList);
    }
