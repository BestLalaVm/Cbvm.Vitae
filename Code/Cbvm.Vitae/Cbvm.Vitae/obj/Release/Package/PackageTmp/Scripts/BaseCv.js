function RowMouseOver(sender, eventsArgs) {
    var tr = eventsArgs._domEvent.target.parentNode;
    tr.style.backgroundColor = "rgb(226, 241, 207)";
    tr.style.cursor = "pointer";
}

function RowMouseOut(sender, eventsArgs) {
    var tr = eventsArgs._domEvent.target.parentNode;
    tr.style.backgroundColor = "";
    tr.style.cursor = "default";
}

function RowClick(sender, eventArgs) {
    var currentRowID = eventArgs.get_id();
    var gridEdit = $("#" + currentRowID + " a.grid-edit");
    var editPage = gridEdit.attr("href");

    var currentPK = gridEdit.attr("PKValue");
    if (typeof RowClickEx == "function") {
        if (!RowClickEx(editPage, currentPK)) return false;
    }
    
    if (editPage) {
        BeginRequest();
        document.location.href = editPage;
        return;
    } else {
        var editedRow = eventArgs.get_itemIndexHierarchical();
        sender.MasterTableView.editItem(editedRow);
    }
    EndRequest();
}

function RowSelected(sender, eventsArgs) {
    if (typeof RowSelectedEx == "function") {
        RowSelectedEx(sender, eventsArgs);
    }
}

function RowDeleting(sender, args) {
    var id = 0;
    var deleteAlertResourceMsg = "确定删除该记录(" + id + ")?";
    if (typeof getDeleteAlertResourceMsg == "function") {
        deleteAlertResourceMsg = getDeleteAlertResourceMsg(id);
    }
    if (confirm(deleteAlertResourceMsg)) {
        return true;
    }
    return false;
}

function ResponseEnd(sender, args) {
    if (typeof ResponseEndEx == "function") {
        ResponseEndEx(sender, args);
    }
    setTimeout(function() {
        ReflashPage();
        EndRequest();
    }, 500);
}

function TabSelected(sender, args) {
    window.parent.ResetSize();
    if (typeof TabSelectedEx == "function") {
        TabSelectedEx(sender, args);
    }
}

function BeginRequest(label, isArea) {
    if (typeof BeginRequestEx == "function") {
        if (!BeginRequestEx(label, isArea)) {
            return;
        }
    }
    if (!label) {
        label = "<b>请稍等,正在执行中...</b>";
    }
    var func = "PFullActionRequest";
    if (isArea) {
        func = "PActionRequest";
    }
    var windowObject = window;
    for (var index = 5; index > 0; index--) {
        windowObject = window;
        for (var i = index; i > 0; i--) {
            if (windowObject["parent"]) {
                windowObject = windowObject["parent"];
            }
        }
        if (typeof windowObject && typeof windowObject[func] == "function") {
            break;
        }
    }
    if (windowObject[func] && typeof windowObject[func] == "function") {
        windowObject[func](label);
    }    
}

function EndRequest(isArea) {
    if (typeof EndRequestEx == "function") {
        if (!EndRequestEx(isArea)) return;
    }
    
    var func = "PFullActionRequestEnd";
    if (isArea) {
        func = "PActionRequestEnd";
    }
    var windowObject = window;
    for (var index = 5; index > 0; index--) {
        windowObject = window;
        for (var i = index; i > 0; i--) {
            if (windowObject["parent"]) {
                windowObject = windowObject["parent"];
            }
        }
        if (typeof windowObject && typeof windowObject[func] == "function") {
            break;
        }
    }
    var callFunc = windowObject[func];
    if (typeof callFunc == "function") {
        callFunc();
    }
}

function BeforeDeletd(id, name) {
    var deleteAlertResourceMsg = "确定删除该记录 " + name + "(" + id + ")?";
    if (typeof getDeleteAlertResourceMsg == "function") {
        deleteAlertResourceMsg = getDeleteAlertResourceMsg(id);
    }
    if (confirm(deleteAlertResourceMsg)) {
        return true;
    }
    return false;
}

$(function() {
    setTimeout(ReflashPage, 500);
    EndRequest();
});

function ReflashPage() {
    if (window.parent != null && typeof window.parent.ResetSize == "function") {
        window.parent.ResetSize();
    }

    var normalHeight = 25;
    if (typeof window.getInputBoxHeight == "function") {
        normalHeight = window.getInputBoxHeight();
    }

    $("input.edit-text").each(function() {
        var current = $(this);
        var value = current.attr("disabled");
        var readonly = current.attr("readonly");
        var broken = false;
        if (typeof jqxInputExtand == "function") {
            broken = jqxInputExtand(current, { height: normalHeight, disabled: value, readonly: readonly });
        }
        if (!broken) {
            current.jqxInput({ height: normalHeight, disabled: value, readonly: readonly });
        }
        current.attr("diabled", value);
    });
    $(".multi-edit-text").each(function () {
        var current = $(this);
        var value = current.attr("disabled");
        current.jqxInput({ disabled: value });
        current.attr("diabled", value);
    });
    
    $("input[type='submit'],input[type='button'],button").each(function () {
        var current = $(this);
        var value = current.attr("disabled");
        var width = current.width();
        if (width < 20) {
            width = 50;
        }
        if (typeof GetComponentSize == "function") {
            var size = GetComponentSize(current, {
                Width: width
            });
            if (size) {
                width = size.Width;
            }
        }
        
        current.jqxButton({ disabled: value, width: width });
    });

    $(".upload-edit-text").jqxInput({ height: normalHeight, width: 120 });
}

jQuery.GetStringFromJson = function(jsonList) {
    if (!jsonList) return null;
    return getJsonStringFromObject(jsonList);

    function getJsonStringFromObject(jsonObject) {
        var jsonString = "";
        if (typeof jsonObject.length == 'undefined') {
            jsonString = "{";
            for (var key in jsonObject) {
                if (jsonString.length > 1) {
                    jsonString = jsonString + ",";
                }
                jsonString = jsonString
                    + "\"" + key + "\"" + ":";
                var value = jsonObject[key];
                switch (typeof value) {
                case 'object':
                    jsonString = jsonString + getJsonStringFromObject(value);
                    break;
                case 'string':
                    jsonString = jsonString + "'" + value + "'";
                    break;
                default:
                    jsonString = jsonString + value;
                    break;
                }
            }
            jsonString = jsonString + "}";
            return jsonString;
        } else {
            jsonString = "[";
            for (var i = 0; i < jsonObject.length; i++) {
                if (jsonString.length > 1) {
                    jsonString = jsonString + ",";
                }
                jsonString = jsonString + getJsonStringFromObject(jsonObject[i]);
            }
            jsonString = jsonString + "]";
            return jsonString;
        }
    }
}
$.isReady = false;
$(window).load(function () {
    if (typeof parent.windowLoadedEx == "function") {
        parent.windowLoadedEx();
    }

    $.isReady = true;
})

function get_TopParent(win) {
    if (win.parent && win.parent.location.href != win.location.href) {
        if (!win.parent.IsTopWindow) {
            return get_TopParent(win.parent);
        } else {
            return win.parent;
        }
    } else {
        return win;
    }
}
