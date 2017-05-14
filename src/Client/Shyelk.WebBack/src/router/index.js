import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Login'
import Main from '@/components/Main'
import securityModule from '../assets/js/security'
import BaseUtils from '../assets/js/base'

Vue.use(Router)

const router = new Router({
    routes: [{
        path: '/login',
        name: 'Login',
        component: Login
    }, {
        path: '/',
        redirect: 'login'
    }, {
        path: '/Main',
        name: 'Main',
        component: Main,
        children: [{
            path: '/dashbord'
        }]
    }]
})
router.beforeEach((to, from, next) => {
    if (to.name != 'Login' && !securityModule.checkLogin()) {
        var url = BaseUtils.StringFormat("login?redirectUrl={0}", to.path);
        next(url);
    } else {
        next();
    }
});
export default router;