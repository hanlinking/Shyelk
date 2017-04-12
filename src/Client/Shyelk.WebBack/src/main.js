// The Vue build version to load with the `import` command
/// <reference path="../typings/index.d.ts" />
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import nprogress from 'nprogress'
import 'font-awesome-webpack'
//import 'assets/libs/Font-Awesome/css/font-awesome.min.css'
import 'assets/libs/bootstrap/dist/css/bootstrap.min.css'
import 'assets/libs/bootstrap/dist/js/bootstrap.min'
import 'assets/css/site.css'
import '../node_modules/nprogress/nprogress.css'


Vue.config.productionTip = false
    /* eslint-disable no-new */
new Vue({
    el: '#app',
    router,
    template: '<App/>',
    components: { App }
})