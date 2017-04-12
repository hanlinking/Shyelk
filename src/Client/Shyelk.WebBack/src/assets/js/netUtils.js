import $ from 'jquery';
import NProgress from 'nprogress'
var defaultoption = {

}
var netUtils = {
    ajax: function(options, success, isNeedLogin) {
        if (typeof options == 'string') {

        } else {
            $.extend(defaultoption, options)
        }
        NProgress.remove();
        NProgress.start();
        window.setTimeout(function() {
            NProgress.done();
            //NProgress.remove();
        }, 10000)
    }
}
export default netUtils