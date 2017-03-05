import Vue from 'vue'
import Router from 'vue-router'
import hello from 'components/Hello'
import login from 'components/login'
import $ from 'jquery'
//require('bootstrap')
Vue.use(Router)

export default new Router({
    routes: [{
        path: '/hello',
        name: 'Hello',
        redirect: function() {
            if (!hello.isLogin()) {
                return '/login';
            }
        },
        component: hello
    }, {
        path: '/login',
        name: 'login',
        component: login
    }]
})