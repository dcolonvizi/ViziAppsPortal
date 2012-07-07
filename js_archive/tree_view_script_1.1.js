/************************************** Common functions ***************************/
function ClientNoNodeDragStart(sender, eventArgs) {

    eventArgs.set_cancel(true);

}

var success_node;
var error_node;

var sourceNode_parent;

function clientSideEdit(sender, args, type) {
    var destinationNode = args.get_destNode();

    if (destinationNode) {

        var sourceNodes = args.get_sourceNodes();
        if (sourceNodes.length == 1) {
            var sourceNode = sourceNodes[0];

            //clone node
            var tree = sender;
            tree.trackChanges();
            var clone_node = new Telerik.Web.UI.RadTreeNode();
            clone_node.set_text(sourceNode.get_text());
            clone_node.set_value(sourceNode.get_value());
            clone_node.set_imageUrl(sourceNode.get_imageUrl());
            clone_node.set_cssClass("DraggedNode");
            clone_node.background = sourceNode.background;
            clone_node.set_category(sourceNode.get_category());
            sourceNode_parent.get_nodes().add(clone_node);
            tree.commitChanges();

            if (args.get_dropPosition() == "over") {
                sourceNode.set_cssClass("DraggedNode");
                destinationNode.get_nodes().add(sourceNode);
                destinationNode.set_expanded(true);
                var tree_edits;
                if (type == 'request') {
                    tree_edits = document.getElementById("RequestTreeEdits");
                    var method = destinationNode.get_parent();
                    var web_service = method.get_parent();
                    var event_field = web_service.get_parent();
                    tree_edits.value += sourceNode.get_value() +
                           ';event_field:' + event_field.get_text() + ';web_service:url(' + web_service.get_text() + ');method:' + method.get_text() +
                            ';web_service_method_input:' + destinationNode.get_text() + '|';
                }
                else {
                    tree_edits = document.getElementById("ResponseTreeEdits");
                    var destination_parent = destinationNode.get_parent();
                    if (destination_parent.get_category() == 'table') {
                        tree_edits.value += 'table:' + destination_parent.get_text() +
                        ';output_field:' + destinationNode.get_text();
                    }
                    else {
                        tree_edits.value += 'output_field:' + destinationNode.get_text();
                    }
                    //web_service and method are in sourceNode
                    tree_edits.value += ';' + sourceNode.get_value() +
                                        ';web_service_response:' + sourceNode.get_text() + ';';
                    //get success phrase

                    if (destinationNode.get_value() == 'status') {
                        var sourceNodeChild = sourceNode.get_lastChild();
                        var success_phrase = 'success';
                        if (sourceNodeChild != null) {
                            success_phrase = sourceNodeChild.get_element().innerText;
                        }

                        var tree = document.getElementById("PhoneResponseTreeView");
                        success_node = new Telerik.Web.UI.RadTreeNode();
                        success_node.set_text("success phrase");
                        success_node.set_cssClass("DraggedNode");
                        sourceNode.get_nodes().add(success_node);
                        error_node = new Telerik.Web.UI.RadTreeNode();
                        error_node.set_text("error alert");
                        error_node.set_cssClass("DraggedNode");
                        sourceNode.get_nodes().add(error_node);
                        radprompt('<span style=\'font-size:14px;color: #333399;\'>Enter a unique phrase contained in the status of successful web response.</span>', promptCallBackFn1, 500, 100, null, 'Success Phrase', success_phrase);
                    }
                    else {
                        tree_edits.value += '|';
                    }

                }
            }
        }
    }
}

function promptCallBackFn1(arg) {
    success_node.set_value(arg);
    tree_edits = document.getElementById("ResponseTreeEdits");
    tree_edits.value += 'status_success_phrase:' + arg + ';';
    var tree = document.getElementById("PhoneResponseTreeView");
    var success_phrase = new Telerik.Web.UI.RadTreeNode();
    success_phrase.set_text(arg);
    success_phrase.set_allowEdit("true");
    success_phrase.set_cssClass("DraggedNode");
    success_node.get_nodes().add(success_phrase);

    radprompt('<span style=\'font-size:14px;color: #333399;\'>Enter the alert phrase that the phone user will see when there is an error in the web response.</span>', promptCallBackFn2, 600, 100, null, 'Error Alert', 'The web request returned an error.');

}

function promptCallBackFn2(arg) {
    error_node.set_value(arg);
    tree_edits = document.getElementById("ResponseTreeEdits");
    tree_edits.value += 'status_error_alert:' + arg + ';|';
    var error_alert = new Telerik.Web.UI.RadTreeNode();
    error_alert.set_text(arg);
    error_alert.set_allowEdit("true");
    error_alert.set_cssClass("DraggedNode");
    error_node.get_nodes().add(error_alert);
    success_node.get_parent().expand();
}

function WebServiceInputTreeViewClicked(sender, args) {
    var node = args.get_node();
    var node_info = document.getElementById("ClickedNodeInfo");
    node_info.value = node.get_text();

    var category = node.get_category();
    if (category == "save") {
        node_info.value = node.get_parent().get_parent().get_parent().get_text() + ';' + node.get_parent().get_text(); //event;method
        var SaveMethodCall = document.getElementById("SaveMethodCall");
        SaveMethodCall.click();
    }
    else if (category == "delete") {
        node_info.value = node.get_parent().get_parent().get_parent().get_text() + ';' + node.get_parent().get_text(); //event;method
        var ResetMethodCall = document.getElementById("ResetMethodCall");
        ResetMethodCall.click();
    }
}

function PhoneResponseTreeViewNodeClicked(sender, args) {
    var node = args.get_node();
    var node_info = document.getElementById("ClickedNodeInfo");
    node_info.value = node.get_text();
}

/******************************* PhoneRequestTreeView functions ******************************/
function onPhoneRequestTreeViewDragStart(sender, eventArgs) {

    var sourceNodes = eventArgs.get_sourceNodes();
    var sourceNode = sourceNodes[0];
    var value = sourceNode.get_category();
    if (value != "request")
        eventArgs.set_cancel(true);
    else
        sourceNode_parent = sourceNode.get_parent();
}

function onPhoneRequestTreeViewNodeDropping(sender, args) {
    var dest = args.get_destNode();
    var value = dest.get_category();
    if (value == "input")
        clientSideEdit(sender, args, "request");
    args.set_cancel(true);
}

function Undo(sender, args) {
    var node = args.get_node();
    var node_info = document.getElementById("ClickedNodeInfo");
    node_info.value = node.get_text();
}

/******************************* WebServiceResponseTreeView functions ******************************/
function onWebServiceResponseTreeViewDragStart(sender, eventArgs) {

    var sourceNodes = eventArgs.get_sourceNodes();
    var sourceNode = sourceNodes[0];
    var value = sourceNode.get_category();
    if (value != "response")
        eventArgs.set_cancel(true);
    else
        sourceNode_parent = sourceNode.get_parent();
}

function onWebServiceResponseTreeViewNodeDropping(sender, args) {
    var dest = args.get_destNode();
    var value = dest.get_category();
    if (value == "output")
        clientSideEdit(sender, args, "response");
    args.set_cancel(true);
}

function onWebServiceResponseTreeViewNodeClick(sender, args) {

    var node = args.get_node();
    var category = node.get_category();
    if (category == "method") {
        var oWnd = radopen("TestWebService.aspx", "TestWebServiceDialog");
    }
}
