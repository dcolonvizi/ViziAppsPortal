
   $(window).bind('orientationchange', function (event) {
        var orientation = event.orientation;
        var device_type = getDeviceType();
        if (device_type == 'android') { //bug in orientationchange only in android
            if (orientation == 'portrait')
                orientation = 'landscape';
            else
                orientation = 'portrait';
        }
        //alert('designed for :' + design_device_type + '; device_type :' + device_type + '; orientation :' + orientation + '; width :' + width + '; height :' + height);
        // return;
        var width_factor = 1.0;
        var height_factor = 1.0;
        switch (device_type) {
            case 'android':
                width_factor = android_landscape_width_factor;
                height_factor = android_landscape_height_factor;
                break;
            default:
                width_factor = ios_landscape_width_factor;
                height_factor = ios_landscape_height_factor;
                break;
        }
        if (orientation == 'portrait') {
            if (previous_orientation && previous_orientation == 'landscape') {
                width_factor = 1.0 / width_factor;
                height_factor = 1.0 / height_factor;
            }
            else {
                previous_orientation = orientation;
                var width_factor = 1.0;
                var height_factor = 1.0;
            }
        }
        previous_orientation = orientation;

        $('.rescale-xy-on-orientationchange').each(function (index) {
            var left = parseInt($(this).css('left').replace('px', '')) * width_factor;
            $(this).css('left', left + 'px');
            var top = parseInt($(this).css('top').replace('px', '')) * height_factor;
            $(this).css('top', top + 'px');
        });
        $('.rescale-width-height-on-orientationchange').each(function (index) {
            var width = parseInt($(this).css('width').replace('px', '')) * width_factor;
            $(this).css('width', width + 'px');
            var height = parseInt($(this).css('height').replace('px', '')) * height_factor;
            $(this).css('height', height + 'px');
        });
        $('div[data-role="page"]').each(function (index) {
            if (does_background_image_exist) {
                var background_size_value = $(this).css('background-size').replace(/px/g, ' ');
                var units = background_size_value.split(' ');
                var width = parseInt(units[0]) * width_factor;
                var height = parseInt(units[2]) * height_factor;
                $(this).css('background-size', width + 'px ' + height + 'px');
            }
        });
    });
