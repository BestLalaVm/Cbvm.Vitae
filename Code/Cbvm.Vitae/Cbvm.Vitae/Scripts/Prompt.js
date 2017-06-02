
function showAlterResultMsg(isSucess, msg) {
    var caption = "提示信息";
    showAlertMsg(caption, msg);
}

var CloseCallBackHandler = null;
function showAlertMsg(caption, message, width, height) {
    if (!width) {
        width = "400px";
    }
    if (!height) {
        height = "120px";
    }

    var frameMain = $(".lk-dialog .lk-main");
    if (frameMain.length == 0) {
        var frameContainer = $("<div class='lk-dialog' style='box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);border: 1px solid #aaa;border-radius: 5px;min-width: 440px;background-color: #eaeaea;'/>");
        frameMain = $("<div class='lk-main' style='z-index:99999999;position:fixed;background-color:#fff;'></div>");
        frameContainer.append($("<div class='lk-overlay' style='position:fixed;width:100%;height:100%;top:0px;left:0px;filter:alpha(opacity=75);-moz-opacity:0.75;opacity:0.75;z-index:99999998;background-color:#fff;'></div>")).append(frameMain);
        $("body").append(frameContainer);
    }
    var msgHtml = " <div class='dialog-head'><h3 style='font-size:17px;'>" + caption + "</h3><a class='dialog-close'></a></div>" +
        " <div class='dialog-container' style='width:" + width + ";height:" + height + ";'>" + message + "</div>" +
        "<div class='dialog-action'><input type='button' onclick='removeFrameDialog();' value='关闭' style='width:50px'></div>";
    frameMain.html(msgHtml);

    $(".dialog-action input[type='button']").jqxButton();
    frameMain.find(".dialog-close").click(removeFrameDialog);

    var documentHeight = $(window).height();
    var documentWidth = $(window).width();

    var left = ((documentWidth - parseFloat(width)) / 2) - 50;
    var top = ((documentHeight - parseFloat(height)) / 2) - 60;

    frameMain.css("top", top).css("left", left);
    //ReflashPage();
}

function ConfirmMsg(caption, message,action, width, height) {
    if (!width) {
        width = "300px";
    }
    if (!height) {
        height = "120px";
    }

    var frameMain = $(".lk-dialog .lk-main");
    if (frameMain.length == 0) {
        var frameContainer = $("<div class='lk-dialog' style='box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);border: 1px solid #aaa;border-radius: 5px;min-width: 440px;background-color: #eaeaea;'/>");
        frameMain = $("<div class='lk-main' style='z-index:99999999;position:fixed;background-color:#fff;'></div>");
        frameContainer.append($("<div class='lk-overlay' style='position:fixed;width:100%;height:100%;top:0px;left:0px;filter:alpha(opacity=75);-moz-opacity:0.75;opacity:0.75;z-index:99999998;background-color:#fff;'></div>")).append(frameMain);
        $("body").append(frameContainer);
    }
    var msgHtml = " <div class='dialog-head'><h3 style='font-size:17px;'>" + caption + "</h3><a class='dialog-close'></a></div>" +
        " <div class='dialog-container' style='width:" + width + ";height:" + height + " margin-top:50px;margin-left:50px;'><span class='dialog_icon'></span>" + message + "</div>" +
        "<div class='dialog-action'><input type='button' onclick='Continue2Action(true,'" + action + "');' value='确定'><input type='button' onclick='removeFrameDialog();' value='取消'></div>";
    frameMain.html(msgHtml);

    $(".dialog-action input[type='button']").jqxButton();
    frameMain.find(".dialog-close").click(removeFrameDialog);

    var documentHeight = $(window).height();
    var documentWidth = $(window).width();

    var left = ((documentWidth - parseFloat(width)) / 2) - 50;
    var top = ((documentHeight - parseFloat(height)) / 2) - 60;

    frameMain.css("top", top).css("left", left);
    //ReflashPage();
}

function ShowIframeForm(caption, url, width, height, config) {
    if (!width) {
        width = "800px";
    }
    if (!height) {
        height = "200px";
    }

    var overflow = "auto";
    if (config && config.hideOverFlow) {
        overflow = "hidden";
    }
    var frameMain = $(".lk-dialog .lk-main");
    if (frameMain.length == 0) {
        var frameContainer = $("<div class='lk-dialog' style='box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);border: 1px solid #aaa;border-radius: 5px;min-width: 440px;background-color: #eaeaea;'/>");
        frameMain = $("<div class='lk-main' style='top:20px;left:200px;z-index:99999999;position:fixed;background-color:#fff;overflow: hidden;border: 1px solid #aaa;box-shadow: 0 0 8px rgba(0, 0, 0, 0.2); border-radius: 5px;background-color: #eaeaea;'></div>");
        frameContainer.append($("<div class='lk-overlay' style='position:fixed;width:100%;height:100%;top:0px;left:0px;filter:alpha(opacity=75);-moz-opacity:0.75;opacity:0.75;z-index:99999998;background-color:#fff;'></div>")).append(frameMain);
        $("body").append(frameContainer);
    }
    var msgHtml = " <div class='dialog-head' style='background-color: #eaeaea;padding: 5px 15px;line-height: 25px;font-weight: bold;border-radius: 5px 5px 0 0;border-bottom: 1px solid #ccc;cursor: move;'><h3 style='font-size:17px;'>" + caption + "</h3><a class='dialog-close'></a></div>" +
        " <div class='dialog-container' style='width:" + width + ";height:" + height + ";overflow:" + overflow + ";'><iframe width='100%' height='100%' src='" + url + "' frameborder='0' scrolling='no' id='frameDialogContainer'/></div>" +
        "<div class='dialog-action' style='background-color: #eaeaea;padding: 5px 12px;text-align: right;line-height: 25px;border-top: 1px solid #ccc;'><input type='button' onclick='removeFrameDialog();' value='关闭'></div>";
    frameMain.html(msgHtml);

    $(".dialog-action input[type='button']").jqxButton();
    frameMain.find(".dialog-close").click(removeFrameDialog);

    var conWindow = frameMain.find("iframe")[0].contentWindow;
    var interval = setInterval(function () {
        if (conWindow.$ && conWindow.$.isReady) {
            window.clearInterval(interval);
            getFrameSize("frameDialogContainer");

            var documentHeight = $(window).height();
            var documentWidth = $(window).width();
            var left = ((documentWidth - frameMain.width()) / 2);
            var top = ((documentHeight - frameMain.height()) / 2);

            frameMain.css("top", top).css("left", left);
        }
    }, 200);

    if (config) {
        if (config.isShowFooter==false) {
            $(".dialog-action").hide();
        }

        if (typeof config.closeCallBackHandler == "function") {
            CloseCallBackHandler = config.closeCallBackHandler;
        }
    }


    //ReflashPage();
}

function getFrameSize(iframename) {
    var ffHeight = 0;
    var pTar = null;
    var h = 400;
    if (document.getElementById) {
        pTar = document.getElementById(iframename);
    } else {
        eval('pTar = ' + iframename + ';');
    }
    if (pTar) {
        //begin resizing iframe   
        pTar.style.display = "block"
        if (pTar.contentDocument && pTar.contentDocument.body && pTar.contentDocument.body.offsetHeight) {
            //ns6 syntax
            h = pTar.contentDocument.body.offsetHeight + ffHeight;
        } else if (pTar.Document && pTar.Document.body && pTar.Document.body.scrollHeight) {
            //ie5+ syntax  
            h = pTar.Document.body.scrollHeight + ffHeight;
        }
        if (h > 400) {
            pTar.height = h + 1;
        } else {
            pTar.height = 400;
        }
    }
}

function removeFrameDialog() {
    if (CloseCallBackHandler) {
        CloseCallBackHandler();
    }

    $(".lk-dialog").remove();
    
}

function Continue2Action(value, action) {
    if (typeof Continue2ActionEx == "function") {
        Continue2ActionEx(value, action);
    }
}
