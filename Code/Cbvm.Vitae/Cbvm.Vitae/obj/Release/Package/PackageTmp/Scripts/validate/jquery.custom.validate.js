(function ($) {
    $.validator.addMethod("regex", function (value, element, regexpress) {
        var re = new RegExp(regexpress);
        return this.optional(element) || re.test(value);
    }, "字符格式无效!");
    
    //Phone
    $.validator.addMethod("customPhone", function (value, element) {
        return this.optional(element) || /^1[3|4|5|8][0-9]\d{8}$/.test(value) || /^(\d{3,4}-?)?\d{7,8}$/.test(value);
    });

    //IDentityNum
    $.validator.addMethod("identityNum", function (value, element) {
        //return this.optional(element) || /\d{17}[[0-9],0-9xX]/.test(value);
        return this.optional(element) || /\d{17}(\d|[xX\*]){1}/.test(value);
    });
})(jQuery);