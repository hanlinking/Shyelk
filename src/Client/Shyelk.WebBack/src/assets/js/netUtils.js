import $ from 'jquery';
import NProgress from 'nprogress'
import BaseUtils from './base'
import SecurityUtils from './security'

var htmlid = BaseUtils.NewUUID().substring(0, 6);
var loadingobj = (function() {
    var div = document.createElement("div")
    div.className = "spinner";
    div.innerHTML = '<div class="double-bounce1"></div><div class="double-bounce2"></div>'
    return div
})();
var loadingStyle = (function() {
    var style = '';
    style += '<style>';
    style += '.spinner {width: 100%;height: 100%;position:absoulte;z-index:999;top:0px;left:0px;}' ;
    style += '.double-bounce1, .double-bounce2 {width: 100%;height: 100%;border-radius: 50%;background-color: #67CF22;opacity: 0.6;position: absolute;top: 0;left: 0;-webkit-animation: bounce 2.0s infinite ease-in-out;animation: bounce 2.0s infinite ease-in-out;}';
    style += '.double-bounce2 {-webkit-animation-delay: -1.0s;animation-delay: -1.0s;}';
    style += '@-webkit-keyframes bounce {0%, 100% { -webkit-transform: scale(0.0) }50% { -webkit-transform: scale(1.0) }}' ;
    style += '@keyframes bounce {0%, 100% { transform: scale(0.0);-webkit-transform: scale(0.0);} 50% { transform: scale(1.0);-webkit-transform: scale(1.0);}}';
    style += '<style>';
    return style;
})()

var UIAction = {
    addStyle: false,
    ShowLoading: function(id) {
        if (!UIAction.addStyle) {
            UIAction.addStyle = true;
            $('body').append(loadingStyle);
        }
        $(id).append(loadingobj);
        return loadingobj;
    },
    HideLoading: function(item) {
        item.remove();
    }
}
var defaultoption = {
    url: "",
    type: "GET",
    data: "",
    contentType: "application/json",
    dataType: "json",
    error: function(xhr, status, error) {

    },
    cache: false,
    headers: {},
    crossDomain: true
}

var netUtils = {
    ajax: function(options, data, success, coverparent) {
        var ajaxSetting = this.AjaxSetting(options, data, success, coverparent);
        $.ajax(ajaxSetting)
    },
    AjaxSetting: function(options, data, success, coverparent) {
        var option = {};
        if (typeof options == 'string') {
            var service = BaseUtils.GetService(options);
            option.url = service.url;
            option.type = service.type;
            if (service.isAuth && !SecurityUtils.checkLogin()) {
                SecurityUtils.ShowLoginModal();
                return;
            }
        } else {
            option = $.extend(defaultoption, options)
        }
        var loadingindex;
        option.beforeSend = function(xhr, setting) {
            //loadingindex = UIAction.ShowLoading(coverparent);
        };
        option.complete = function(xhr, status) {
            //UIAction.HideLoading(loadingindex);
            if (status == '403') {
                SecurityUtils.ShowLoginModal();
            }
        }
        if ($.isFunction(success)) {
            option.success = function(xhr, status) {
                success(xhr, status);
            }
        }
        return option;
    }
}

export default netUtils