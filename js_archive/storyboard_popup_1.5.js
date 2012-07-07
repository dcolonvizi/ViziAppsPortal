$(function () {

});

function PageTreeViewNode_Clicked(event, event_args) {
}

function goToPage(page) {
    var PageName = parent.window.document.getElementById("PageName");
    PageName.value = page;
    checkRefresh();
    var GoToPage = parent.window.document.getElementById("GoToPage");
    GoToPage.click();
}

function checkRefresh() {
    var SavedCanvasHtml = parent.window.document.getElementById("SavedCanvasHtml");
    if (SavedCanvasHtml.value.length > 0)
        parent.window.document.getElementById('storyBoard').src = parent.window.document.getElementById('storyBoard').src
}

var saved_field_name;
function onPageViewMouseOver(event, event_args) {
    var node = event_args.get_node()
    var label = node.get_text();

    try{
         var page_name = node.get_parent().get_text();
    }
    catch(ex){
         return;
    }

     var value = node.get_value();
     if (value == null)
         return;
     var parts = value.split(';');
     if ($('#SelectedAppType')[0].value == 'web') {
         var xscale_factor = parseFloat($('#XScaleFactor')[0].value);
         var yscale_factor = parseFloat($('#YScaleFactor')[0].value);
         var left = parseInt(parts[0].split(':')[1]) * xscale_factor;
         var top = parseInt(parts[1].split(':')[1]) * yscale_factor - 6 + 2;
         var width = parseInt(parts[2].split(':')[1]) * xscale_factor;
         var height = parseInt(parts[3].split(':')[1]) * yscale_factor;
     }
     else {
         var left = parseInt(parts[0].split(':')[1]) / 2;
         var top = parseInt(parts[1].split(':')[1]) / 2 - 6 + 2;
         var width = parseInt(parts[2].split(':')[1]) / 2;
         var height = parseInt(parts[3].split(':')[1]) / 2;
     }
    var id = page_name + '_container';
    var image_node = $('[id*="' + page_name + '_PageImage"]')[0];
    top -= image_node.height;
    var div_node = image_node.parentNode;
    div_node.id = id;
    var div_width = image_node.width + 4;
    var div_height = image_node.height + 4;
    $('#' + id).css('height', div_height + 'px');
    $('#' + id).css('width', div_width + 'px');
    $('#' + id).append('<div class="hightlight_field" style="position:relative;left:' + left + 'px;top:' + top + 'px;width:' + width + 'px;height:' + height + 'px;border-style:solid;border-width:3px;border-color:red;"/>');

    //put in button page target
    if (parts.length > 4) {
        var target_page_name = parts[4].split(':')[1];
        image_node2 = $('[id*="' + target_page_name + '_PageImage"]')[0];
        id = target_page_name + '_container';
        top -= image_node2.height;
        div_node = image_node2.parentNode;
        div_node.id = id;
        div_width = image_node2.width + 4;
        div_height = image_node2.height + 4;
        $('#' + id).css('height', div_height + 'px');
        $('#' + id).css('width', div_width + 'px');
        top = -image_node2.height - 4;
        if (target_page_name == page_name)
            top -= height+6; 
        $('#' + id).append('<div class="hightlight_target" style="position:relative;left:0px;top:' + top + 'px;width:' + image_node2.width + 'px;height:' + image_node2.height + 'px;border-style:solid;border-width:3px;border-color:red;"/>');
         if (target_page_name != page_name) {
            var field_offset = $('.hightlight_field').offset();
            var target_offset = $('.hightlight_target').offset();
            var connection_left = parseInt(field_offset.left) + parseInt($('.hightlight_field').width()) / 2;
            var contact_left = connection_left - 5;
            if (field_offset.top < target_offset.top) {
                var connection_height =  target_offset.top - field_offset.top - height -3;
                var connection_top = field_offset.top + height + 3;
                $('#' + id).append('<div class="hightlight_connection" style="position:absolute;left:' + connection_left + 'px;top:' + connection_top + 'px;width:3px;height:' + connection_height + 'px;border-left-style:solid;border-width:3px;border-color:red;"/>');
                var contact_top = connection_top - 5 + connection_height;
                $('#' + id).append('<div class="hightlight_contact" style="position:absolute;left:' + contact_left + 'px;top:' + contact_top + 'px;width:13px;height13px"><img src="../images/red_ball.png"/></div>');
            }
            else {
                var connection_height = field_offset.top - target_offset.top - image_node2.height -3;
                var connection_top = target_offset.top + image_node2.height + 3;
                $('#' + id).append('<div class="hightlight_connection" style="position:absolute;left:' + connection_left + 'px;top:' + connection_top + 'px;width:3px;height:' + connection_height + 'px;border-left-style:solid;border-width:3px;border-color:red;"/>');
                var contact_top = connection_top - 5;
                $('#' + id).append('<div class="hightlight_contact" style="position:absolute;left:' + contact_left + 'px;top:' + contact_top + 'px;width:13px;height13px"><img src="../images/red_ball.png"/></div>');
            }
        }
    }
}

function onPageViewMouseOut(event, event_args) {
    $('.hightlight_field').remove();
    $('.hightlight_target').remove();
    $('.hightlight_connection').remove();
    $('.hightlight_contact').remove();
}

function xmlencode(string) {
    return string.replace(/\&/g, '&' + 'amp;').replace(/</g, '&' + 'lt;')
        .replace(/>/g, '&' + 'gt;').replace(/\'/g, '&' + 'apos;');
        //.replace(/\"/g, '&' + 'quot;');
}

function exportStoryBoard() {
    var storyboard_html = document.getElementById("storyboard_html");
    var treeview_container = document.getElementById("treeview_container");
    storyboard_html.value = xmlencode(treeview_container.innerHTML);
    var ExportDesignClick = document.getElementById("ExportDesignClick");
    ExportDesignClick.click();
}

function refresh() {
    var form1 = document.getElementById("form1");
    form1.submit();
}