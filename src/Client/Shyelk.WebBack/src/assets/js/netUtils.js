import $ from 'jquery';
import NProgress from 'nprogress'
import BaseUtils from './base'

var defaultoption = {
    url: "",
    type: "GET",
    data: "",
    contentType: "application/json",
    complete: function(xhr, status) {

    },
    dataType: "json",
    beforeSend: function(xhr, setting) {

    },
    error: function(xhr, status, error) {

    },
    cache: false,
    headers: {},
    crossDomain: true
}

var netUtils = {
    ajax: function(options, data, success, isNeedLogin, coverparent) {
        var ajaxSetting = netUtils.AjaxSetting();
        if (typeof options == 'string') {

        } else {
            $.extend(netUtils.AjaxSetting(), options)
        }

        $.ajax({

            })
            //$.ajax(options);
        var result = BaseUtils.StringFormat("我是{0},{1},{2}", BaseUtils.NewUUID(), BaseUtils.NewUUID(), BaseUtils.NewUUID());
        alert(result);
    },
    AjaxSetting: function name(params) {

        return defaultoption;
    }
}
var loading = (function() {
    var uuid = BaseUtils.NewUUID();
    var html = '';
    html += 'style'
})();
export default netUtils