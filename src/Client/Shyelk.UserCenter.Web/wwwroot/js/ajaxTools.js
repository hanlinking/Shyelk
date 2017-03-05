var ajaxTools = (function(_this, window) {
    _this.ajax = function(option) {
        var ajaxOption = {};
        ajaxOption.url = Option.url;
        ajaxOption.method = option.method || 'get';
        var xmlhttp;
        if (window.XMLHttpRequest) { // code for IE7+, Firefox, Chrome, Opera, Safari
            xmlhttp = new XMLHttpRequest();
        } else { // code for IE6, IE5
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        xmlhttp.onreadystatechange = function() {
            if (xmlhttp.readyState == 1) {
                if (condition) {

                }
            }
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                document.getElementsByClassName('ajax-loading')[0].style.display = "none";
                document.getElementsByClassName(target)[0].innerHTML = xmlhttp.responseText;
            }
        }
        xmlhttp.open(method, url, true);
        xmlhttp.send(data);
    };

    return _this;
})(ajaxTools || {}, window)