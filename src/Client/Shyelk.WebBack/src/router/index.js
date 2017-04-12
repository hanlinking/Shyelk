import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Login'
import Main from '@/components/Main'
import securityModule from '../assets/js/security'

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
    console.log(to);
    if (to.name != 'Login' && !securityModule.checkLogin()) {
        next('login');
    } else {
        next();
    }
});
export default router;