        if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        var ArgList = null;

       function initDialog() {
            ArgList = parent.window.getDialogInputArgs();
            for (var i = 0; i < ArgList.length; i++) {
                if (ArgList[i] == null)
                    ArgList[i] = '';
            }

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Slider";                
 
                var sliderName = document.getElementById("sliderName");
                sliderName.value = ArgList[0];

                /*var selectType_radios = document.getElementsByName("selectType");
                if (ArgList[1] == 'horizontal') {
                    selectType_radios[0].checked = true;
                    selectType_radios[1].checked = false;
                }
                else if (ArgList[1] == 'vertical') {
                    selectType_radios[0].checked = false;
                    selectType_radios[1].checked = true;
                }*/

                var min_value = document.getElementById("min_value");
                var max_value = document.getElementById("max_value");
                var parse = ArgList[2].split(':');
                min_value.value = parse[0];
                max_value.value = parse[1];

                style = ArgList[3];
            }
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var sliderName = document.getElementById("sliderName");
            if (sliderName.value == null || sliderName.value == '') {
                alert('Slider Name must be filled');
                return;
            }
            if (!IsValidObjectName(sliderName.value)) {
                alert('Slider Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            sliderName.value = sliderName.value.replaceAll(" ", "_");


            ArgList[0] = sliderName.value;

           /* var selectType_radios = document.getElementsByName("selectType");
            for (var i = 0; i < selectType_radios.length; i++) {
                if (selectType_radios[i].checked) {
                    var rad_val = selectType_radios[i].value;
                }
            }

            ArgList[1] = rad_val;*/
            ArgList[1] = 'horizontal';

            var min_value = document.getElementById("min_value");
            var max_value = document.getElementById("max_value");

            var fmin_value = parseFloat(min_value.value);
            if (isNaN(fmin_value)) {
                alert('The minimum value is not a number.');
                return;
            }
            var fmax_value = parseFloat(max_value.value);
            if (isNaN(fmax_value )) {
                alert('The maximum value is not a number');
                return;
            }
 
            if (fmin_value >= fmax_value) {
                alert('The minimum value must be less than the maximum value');
                return;
            }
 

            ArgList[2] = min_value.value + ':' + max_value.value;
            ArgList[3] = style;

            parent.window.InsertSliderCallback(ArgList);
        }
 