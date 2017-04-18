import $ from 'jquery';
import NProgress from 'nprogress'
import BaseUtils from './base'
var defaultoption = {

}
var netUtils = {
    ajax: function(options, success, isNeedLogin, coverparent) {
        if (typeof options == 'string') {

        } else {
            $.extend(defaultoption, options)
        }
        //$.ajax(options);
        var result = BaseUtils.StringFormat("我是{0},{1},{2}", BaseUtils.NewUUID(), BaseUtils.NewUUID(), BaseUtils.NewUUID());
        alert(result);
    }
}
var loading = (function() {
    var uuid = BaseUtils.NewUUID();
    var html = '';
    html += 'style'
})()
export default netUtils