window.WB = {
    AjaxForm: {
        
    }
};

WB.AjaxForm.ContentComponent = function($) {
    var context = document;
    var Self = this;
    var validateComponentCollection = new Array();
    var hintTextComponentCollection = new Array();
    var emailValidateComponentCollection = new Array();

    var requiredSelector = ["input[IsRequired='true']", "select[IsRequired='true']", "textarea[IsRequired='true']"];
    var hintTextSelector = ["input[EmptyMessage]", 'textarea[EmptyMessage]'];
    var emailValidateSelector = ["input[emailValidated='true']"];

    var init = function() {
        $.each(requiredSelector, function() {
            $(this.toString()).each(function() {
                var component = new ValidateComponent(this);
                validateComponentCollection.push(component);
            });
        });

        $.each(hintTextSelector, function() {
            $(this.toString()).each(function() {
                var component = new HintTextComponent(this);
                hintTextComponentCollection.push(component);
            });
        });

        $.each(emailValidateSelector, function() {
            $(this.toString()).each(function() {
                var component = new EmailValidateComponent(this);
                emailValidateComponentCollection.push(component);
            });
        });
    }

    Self.Validated = function() {
        var isValid = true;
        $.each(validateComponentCollection, function() {
            var component = this;
            var flag = component.Validated();
            isValid = isValid && flag;
        });
        $.each(emailValidateComponentCollection, function() {
            var component = this;
            var flag = component.Validated();
            isValid = isValid && flag;
        });
        return isValid;
    }

    var HintTextComponent = function(selector) {
        var SelfComponent = this;
        SelfComponent.Control = null;

        var cnnConfig = {
            EmptyMessage: null
        };

        var init = function() {
            if (!$.isFunction(selector)) {
                SelfComponent.Control = $(selector);
            } else {
                SelfComponent.Control = selector;
            }
            SelfComponent.Control.wrap($("<div class='contentWrap' style='position:relative;display:inline-block;'></div>"));
            var lineHeight = (SelfComponent.Control.height() + 5) + "px";
            var emptyMessage = SelfComponent.Control.attr("EmptyMessage");
            var lbl = $("<label style='position:absolute;left:6px;color:#c1c1c1;cursor:text;'>" + emptyMessage + "</label>");
            lbl.css("line-height", lineHeight);
            lbl.insertAfter(SelfComponent.Control).click(function() {
                lbl.hide();
                SelfComponent.Control.focus();
            });
            SelfComponent.Control.blur(function() {
                if (this.value == "") {
                    //lbl.show();
                    lbl.css("display", "inline-block");
                }
            }).focus(function() {
                lbl.hide();
            })
            if (SelfComponent.Control.val() != "") {
                lbl.hide();
            }
        }
        init();
    }

    var ValidateComponent = function(selector) {
        var SelfComponent = this;
        SelfComponent.Control = null;
        var INVALID_VALUE_CLASS = "Invalid-Value";

        var cnnConfig = {
            ErrorMessage: null
        };

        var init = function() {
            if (!$.isFunction(selector)) {
                SelfComponent.Control = $(selector);
            } else {
                SelfComponent.Control = selector;
            }

            SelfComponent.Control.change(function() {
                SelfComponent.Validated();
            })
        }

        SelfComponent.Validated = function () {
            if (typeof ComponentValidateEx == "function") {
                var result = ComponentValidateEx(SelfComponent.Control,SelfComponent.Control.val(), INVALID_VALUE_CLASS);
                if (!result) {
                    return false;
                }
            }
            if (SelfComponent.Control.val() == "") {
                SelfComponent.Control.addClass(INVALID_VALUE_CLASS);
                return false;
            } else {
                SelfComponent.Control.removeClass(INVALID_VALUE_CLASS);
            }
            return true;
        }
        init();
    }

    var EmailValidateComponent = function(selector) {
        var SelfComponent = this;
        SelfComponent.Control = null;
        var INVALID_VALUE_CLASS = "Invalid-Value Invalid-email";
        var regularExpression = /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/;

        var cnnConfig = {
            ErrorMessage: null
        };

        var init = function() {
            if (!$.isFunction(selector)) {
                SelfComponent.Control = $(selector);
            } else {
                SelfComponent.Control = selector;
            }

            SelfComponent.Control.change(function() {
                SelfComponent.Validated();
            });
        }

        SelfComponent.Validated = function() {
            if (!regularExpression.test(SelfComponent.Control.val())) {
                SelfComponent.Control.addClass(INVALID_VALUE_CLASS);
                return false;
            } else {
                SelfComponent.Control.removeClass(INVALID_VALUE_CLASS);
            }
            return true;
        }
        init();
    }
    init();
}

var ContentComponentInstance = null;
$(function() {
    ContentComponentInstance = new WB.AjaxForm.ContentComponent(jQuery);
});