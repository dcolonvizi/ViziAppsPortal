(function ($) {
    // VERTICALLY ALIGN FUNCTION
    $.fn.vAlign = function () {
        return this.each(function (i) {
            var ah = $(this).height();
            var ph = $(this).parent().height();
            var mh = (-ph/2) -(4*ah/3);
            $(this).css('top', mh);
        });
    };
})(jQuery);

function xmlEncode(string) {
    return string.replace(/\&/g, '&' + 'amp;').replace(/</g, '&' + 'lt;')
        .replace(/>/g, '&' + 'gt;').replace(/\'/g, '&' + 'apos;').replace(/\"/g, '&' + 'quot;');
}

//this function is called from editor_cusomt_tools.js
function insertHtml(html_field, id) {
    $("form").append(html_field);
    enableDragAndResizing(id);
    getCanvasHtml();
}

function getCanvasHtml() {
    var form1 = document.getElementById("form1");
    var SavedCanvasHtml = parent.window.document.getElementById("SavedCanvasHtml");
    SavedCanvasHtml.value = xmlEncode(form1.innerHTML);
}

var prev_selected;
var is_control_down = false;

$(function () {
    createDragResize('[title^="MobiFlex"]');
    //checkRefreshStoryBoard();
    sessionStorage["next_copy_number"] = '1'; //initialize
    $(document).keydown(function (e) {
        var code = e.keyCode;
        if (code == '46' || code == '8') { //PC delete is 46 , MAC delete is backspace 8
            $('.helper_box').remove();
            deleteSelectedElement();
            e.stopPropagation();
            e.preventDefault();
            getCanvasHtml();
        }
        else if (code == '37') {
            var left = parseInt(prev_selected.css('left').replace('px', '')) - 1;
            prev_selected.css('left', left + 'px');
            createHelper(prev_selected);
            e.stopPropagation();
            e.preventDefault();
            getCanvasHtml();
        }
        else if (code == '38') {
            var top = parseInt(prev_selected.css('top').replace('px', '')) - 1;
            prev_selected.css('top', top + 'px');
            createHelper(prev_selected);
            e.stopPropagation();
            e.preventDefault();
            getCanvasHtml();
        }
        else if (code == '39') {
            var left = parseInt(prev_selected.css('left').replace('px', '')) + 1;
            prev_selected.css('left', left + 'px');
            createHelper(prev_selected);
            e.stopPropagation();
            e.preventDefault();
            getCanvasHtml();
        }
        else if (code == '40') {
            var top = parseInt(prev_selected.css('top').replace('px', '')) + 1;
            prev_selected.css('top', top + 'px');
            createHelper(prev_selected);
            e.stopPropagation();
            e.preventDefault();
            getCanvasHtml();
        }
        else if (code == '17' || code == '224' || code == '91' || code == '93') {
            is_control_down = true;
        }
        else if (code == '67' && is_control_down) {
            copySelectedElement();
        }
        else if (code == '86' && is_control_down) {
            pasteSelectedElement();
        }
    });
    $(document).keyup(function (e) {
        var code = e.keyCode;
        if (code == '17' || code == '224' || code == '91' || code == '93') {
            is_control_down = false;
        }
    });
});

function enableDragAndResizing(id) {
     createDragResize('[id=' + id + ']');
}

var box;
var frame_height;
function createDragResize(selector) {
    var CanvasFrame = parent.window.document.getElementById("CanvasFrame");
    box = new Array(4);
    box[0] = 0;
    box[1] = 0;
    box[2] = parseInt(CanvasFrame.style.width.replace('px', ''));
    box[3] = parseInt(CanvasFrame.style.height.replace('px', ''));
    $(selector).draggable({ distance: 5,
        drag: function (event, ui) {
            createHelper(this);
            createAlignment(this);
            $(this).draggable("option", "distance", 1);
        },
        stop: function (event, ui) {
            removeAlignment();
            $(this).draggable("option", "distance", 5);
            getCanvasHtml();
        },
        containment: box,
        scroll: false
    });
    $(selector).resizable({
        resize: function (event, ui) {
            $('p').vAlign();          
            createHelper(this);
        },
        stop: function (event, ui) {
            getCanvasHtml();
        }
    });
    $(".ui-resizable-se").removeClass('ui-icon');

    $(selector).css("border", "2px dotted transparent");
    $(selector).click(function (event) {
        //iframe focus enables the delete and arrow keys
        var iframe = parent.window.document.getElementById("canvas");
        iframe.focus();
        if (prev_selected)
            prev_selected.css("border-color", "transparent");
        $(this).css("border-color", "blue");
        prev_selected = $(this);
        setSelectedHtml($(this).clone());
        event.stopImmediatePropagation();
    });
    $(selector).mouseover(function () {
        $(this).css("cursor", "move");
        $(this).find(".ui-resizable-se").addClass('ui-icon');
        createHelper(this);
        if (!prev_selected || (prev_selected && prev_selected[0].id != this.id))
            $(this).css("border-color", "grey");
    }).mouseout(function () {
        $(this).find(".ui-resizable-se").removeClass('ui-icon');
        $('.helper_box').remove();
        if (!prev_selected || (prev_selected && prev_selected[0].id != this.id))
            $(this).css("border-color", "transparent");
    });

    $(selector).dblclick(function (event) {
        if (prev_selected) {
            parent.window.doEditProperties();
        }
    });

    $("*").click(function () {
        if (prev_selected) {
            prev_selected.css("border-color", "transparent");
            prev_selected = null;
            clearSelectedHtml();
            $('.helper_box').remove();
        }
    });
}

function createHelper(this_value) {
    if($('form').find('.helper_box'))
            $('.helper_box').remove();
    $('form').append('<div class="helper_box" style="z-index:100000;background-color:gray;white-space:nowrap;"/>');
    $('form').find('.helper_box').append($(this_value).attr('id') +
            '<br/>x:' + $(this_value).css('left').replace('px', '') +
            ', y:' + $(this_value).css('top').replace('px', '') +
            ', w:' + $(this_value).css('width').replace('px', '') +
            ', h:' + $(this_value).css('height').replace('px', ''));
    $('form').find('.helper_box').css('position', 'absolute');
    var top = parseInt($(this_value).css('top').replace('px', '')) + parseInt($(this_value).height()) + 10;
    var left = parseInt($(this_value).css('left').replace('px', ''));
    var CanvasFrame = parent.window.document.getElementById("CanvasFrame");
    if (top >  CanvasFrame.clientHeight - 30)
        top = parseInt($(this_value).css('top').replace('px', '')) - 30;
    if (left > CanvasFrame.clientWidth - 148) {
        left = parseInt($(this_value).css('left').replace('px', '')) - 152;
        top = parseInt($(this_value).css('top').replace('px', '')) ;
    }

    $('form').find('.helper_box').css('top', top + 'px');    
    $('form').find('.helper_box').css('left', left + 'px');
}

function createAlignment(this_value) {
    $('form').append('<div class="top_align_box" style="z-index:100000;position:absolute;width:321px;height:540px;border-top:1px dotted gray;"/>');
    var top = parseInt($(this_value).css('top').replace('px', '')) + 2;
    $('form').find('.top_align_box').css('top', $(this_value).css('top'));
    $('form').find('.top_align_box').css('left', '0px');

    $('form').append('<div class="left_align_box" style="z-index:100000;position:absolute;width:321px;height:540px;border-left:1px dotted gray;"/>');
    $('form').find('.left_align_box').css('top', '0px');
    var left = parseInt($(this_value).css('left').replace('px', '')) + 2;
    $('form').find('.left_align_box').css('left', left + 'px');

    $('form').append('<div class="right_align_box" style="z-index:100000;position:absolute;width:321px;height:540px;border-left:1px dotted gray;"/>');
    $('form').find('.right_align_box').css('top', '0px');
    var right = parseInt($(this_value).css('left').replace('px', '')) + parseInt($(this_value).width());
    $('form').find('.right_align_box').css('left', right + 'px');

    $('form').append('<div class="bottom_align_box" style="z-index:100000;position:absolute;width:321px;height:540px;border-top:1px dotted gray;"/>');
    var bottom = parseInt($(this_value).css('top').replace('px', '')) + parseInt($(this_value).height());
    $('form').find('.bottom_align_box').css('top', bottom + 'px');
    $('form').find('.bottom_align_box').css('left', '0px');
}
function removeAlignment() {
    $('.top_align_box').remove();
    $('.left_align_box').remove();
    $('.right_align_box').remove();
    $('.bottom_align_box').remove();
}
function setSelectedHtml(el) {
    var prev_selected_html = document.getElementById("prev_selected_html");
    prev_selected_html.value = el.wrap("<div>").parent().html();
}
function clearSelectedHtml() {
    var prev_selected_html = document.getElementById("prev_selected_html");
    prev_selected_html.value = "";
}

function copySelectedElement() {
    //sessionStorage only stores strings
    if (prev_selected) {
        sessionStorage["prev_copied_html"] = outerHTML(prev_selected);
        var CopyClickImage = parent.window.document.getElementById("CopyClickImage");
        CopyClickImage.border = "1";
    }
    else {
        alert("There is no selected field to copy");
        var CopyClickImage = parent.window.document.getElementById("CopyClickImage");
        CopyClickImage.border = "";
    }
}

function updateNextCopyNumber() {
    var next_copy_number = parseInt(sessionStorage["next_copy_number"]) + 1;
    sessionStorage["next_copy_number"] = '' + next_copy_number;
}

function pasteSelectedElement() {
    var prev_copied_html = sessionStorage["prev_copied_html"];

    if (prev_copied_html) {
        //add new suffix for new name
        var start = prev_copied_html.indexOf('id="') + 4;
        var end = prev_copied_html.indexOf('"', start);
        var id = prev_copied_html.substring(start, end);

        if (id.length > 1 && id.substring(id.length - 1) == (parseInt(sessionStorage["next_copy_number"])-1) + '')
            prev_copied_id = id.substring(0,id.length - 1) + sessionStorage["next_copy_number"];
        else if (id.length > 2 && id.substring(id.length - 2) == (parseInt(sessionStorage["next_copy_number"]) - 1) + '')
            prev_copied_id = id.substring(0,id.length - 2) + sessionStorage["next_copy_number"];
        else
            prev_copied_id = id + sessionStorage["next_copy_number"];
 
        //modify the id 
        prev_copied_html = prev_copied_html.replace('"' + id + '"', '"' + prev_copied_id + '"');

        //check for table and picker fields which have subfields that needs to be unique

        start = prev_copied_html.indexOf('title="MobiFlex Table"');
        if (start > 0) {
            start = prev_copied_html.indexOf('fields="') + 8;
            end = prev_copied_html.indexOf('"', start);
            var fields = prev_copied_html.substring(start, end);
            var parts = fields.split(':');
            parts = parts[1].split(',');
            for (var i = 0; i < parts.length; i++) {
                if (i == 0) {
                    prev_copied_html = prev_copied_html.replace(':' + parts[i], ':' + parts[i] + sessionStorage["next_copy_number"]);
                }
                else {
                    prev_copied_html = prev_copied_html.replace(',' + parts[i], ',' + parts[i] + sessionStorage["next_copy_number"]);
                }
            }
        }
        else {
            start = prev_copied_html.indexOf('title="MobiFlex Picker"');
            if (start > 0) {
                start = prev_copied_html.indexOf('type="') + 6;
                end = prev_copied_html.indexOf('"', start);
                var type = prev_copied_html.substring(start, end);
                var parts = type.split(':');
                parts = parts[1].split(',');
                for (var i = 0; i < parts.length; i++) {
                    if (i == 0) {
                        prev_copied_html = prev_copied_html.replace(':' + parts[i], ':' + parts[i] + sessionStorage["next_copy_number"]);
                    }
                    else {
                        prev_copied_html = prev_copied_html.replace(',' + parts[i], ',' + parts[i] + sessionStorage["next_copy_number"]);
                    }
                } 
            }
        }

        updateNextCopyNumber();

        //offset by +10,+10
         start = prev_copied_html.indexOf('left:') + 5;
         end = prev_copied_html.indexOf('px', start);
         var left = prev_copied_html.substring(start, end);
         var new_left  = parseInt(left) + 10;
         prev_copied_html = prev_copied_html.replace('left:' + left, 'left:' + new_left);
 
         start = prev_copied_html.indexOf('top:') + 4;
         end = prev_copied_html.indexOf('px', start);
         var top = prev_copied_html.substring(start, end);
         var new_top  = parseInt(top) + 10;
         prev_copied_html = prev_copied_html.replace('top:' + top, 'top:' + new_top);

         //increase z-index
         start = prev_copied_html.indexOf('z-index:');
         if (start > 0) {
             start += 8;
             end = prev_copied_html.indexOf(';', start);
             var z_index = prev_copied_html.substring(start, end);
             var i_new_z_index = parseInt(z_index) + 1;
             var new_z_index = '' + i_new_z_index;
             prev_copied_html = prev_copied_html.replace(z_index + ';', new_z_index + ';');
         }
        
        //insert copy into html
         insertHtml(prev_copied_html, prev_copied_id);
         getCanvasHtml();
        sessionStorage["prev_copied_html"] = prev_copied_html;

        //clear selected element
        if (prev_selected != null) {
            prev_selected.css("border-color", "transparent");
            prev_selected = null;
            var CopyClickImage = parent.window.document.getElementById("CopyClickImage");
            CopyClickImage.border = "";
        }
        clearSelectedHtml();
    }
    else
        alert("There is no field to paste");
}

function deleteSelectedElement() {
    if (prev_selected) {
        prev_selected.remove();
        prev_selected = null;
        clearSelectedHtml();
    }
}


function getSelectedElement() {
    return prev_selected;
}

function checkFocus() {
    alert(parent.window.document.activeElement + ' has focus');
}

function checkRefreshStoryBoard() {
    //is storyBoard up?
    if (parent.window.isMyPopUpWindowOpen()) {
        //should Storyboard be refreshed
        var ShouldRefreshStoryBoard = parent.window.document.getElementById("ShouldRefreshStoryBoard");
        if (ShouldRefreshStoryBoard.value = "true") {
            ShouldRefreshStoryBoard.value = '';
            parent.window.refreshMyPopUpWindow();
        }
        else if (ShouldRefreshStoryBoard.value = "close") {
            ShouldRefreshStoryBoard.value = '';
            parent.window.closeMyPopUpWindow();
        }
    }
}

function outerHTML(node) {
    if (node.outerHTML)
        return node.outerHTML;
    else
        return new XMLSerializer().serializeToString(node[0]);
}

function noScrollIE() {
    parent.window.document.body.scroll = "no";
    parent.window.document.body.style.overflow = 'hidden';
}
function scrollIE() {
    parent.window.document.body.scroll = "yes";
    parent.window.document.body.style.overflow = 'scroll';
}
function noScrollNS() {
 
    if (!isFireFoxBrowser()) {
        parent.window.document.documentElement.style.overflowX = 'hidden';  // horizontal scrollbar will be hidden
        parent.window.document.documentElement.style.overflowY = 'hidden';  // vertical scrollbar will be hidden 
    }
}
function scrollNS() {
    if (!isFireFoxBrowser()) {
        parent.window.document.documentElement.style.overflowX = 'scroll';  // horizontal scrollbar will be hidden
        parent.window.document.documentElement.style.overflowY = 'scroll';  // vertical scrollbar will be hidden 
    }
}

function stopScrolling(body) {
    body.focus();
    if (isOperaBrowser())
        return;
    if (isIEBrowser())
        noScrollIE();
    else
        noScrollNS();
}

//this function has not bee tested
function resumeScrolling() {
    if (isOperaBrowser())
        return;
    if (isIEBrowser())
        scrollIE();
    else
        scrollNS();
}
