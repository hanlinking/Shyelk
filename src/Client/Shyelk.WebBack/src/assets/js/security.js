import 'jquery.cookie'

export default {
    checkLogin: function() {
        return true;
    },
    SetLoginCookie: function(option) {

    },
    signOut: function() {
        $.cookie('se_token', null, { path: '/' })
    }
}