<template>
    <div>
        <div class="login">
    
            <form class="login-form">
                <input type="hidden" v-model="antiforgetcode">
                <div class="input-group">
                    <span class="input-group-addon">
                        <i class="fa fa-fw fa-user"></i>账号</span>
                    <input type="text" class="form-control" v-model="username" placeholder="账号">
                </div>
                <div class="input-group">
                    <span class="input-group-addon">
                        <i class="fa fa-fw fa-lock"></i>密码</span>
                    <input type="password" class="form-control" v-model="password" placeholder="密码">
                </div>
                <div class="input-group">
                    <span class="input-group-addon">&nbsp;验证码</span>
                    <input type="text" class="form-control" v-model="verifycode" placeholder="验证码">
                    <div class="input-group-addon verify-code-container" id="vcode-container" v-on:click="refresh">
                        <img class="verify-code-img" v-bind:src="image" />
                    </div>
                </div>
                <div class="login-form-group">
                    <div style="float:left">
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" id="henry" value="henry" v-model="isremember">记住密码
                            </label>
                        </div>
                    </div>
                </div>
                <div class="login-form-group">
                    <button v-on:click.prevent="login" class="btn btn-block btn-primary">登&nbsp;录</button>
                </div>
            </form>
        </div>
        <footerbar></footerbar>
    </div>
</template>

<script>
import footerbar from '@/components/footerbar.vue'
import securityModule from 'assets/js/security'
import router from '@/router'
import NetUtils from 'assets/js/netUtils'
import BaseUtils from 'assets/js/base'

export default {
    name: 'Login',
    data: function () {
        return {
            username: "",
            password: "",
            isremember: false,
            verifycode: "",
            image: "",
            antiforgetcode: ""
        }
    },
    methods: {
        login: function (e) {
            var _this = this;
            _this.username = "asdasdasd";
            _this.password = "luhanlin1";
            _this.isremember = true;
            _this.verifycode = "";
            var url = _this.$route.query.redirectUrl;
            if (url==undefined) {
                router.push('Main')
            } else {
                router.push(url)
            }
            // NetUtils.ajax("00000001", null, function (response) {
            //     var url = _this.$route.query.redirectUrl;
            //     if (url) {
            //         router.push(url)
            //     }
            //     router.push('Main')
            // }, "body");
        },
        refresh: function (e) {
            var _this = this;
            NetUtils.ajax("00000000", null, function (response) {
                _this.antiforgetcode = response.antiForgetCode;
                _this.image = response.image;
            }, "#vcode-container");
        }
    },
    created: function (e) {
        var islogin = securityModule.checkLogin()
        if (islogin) {
            router.push('Main');
        } else {
            this.refresh(e);
        }
    },
    components: {
        footerbar
    }
}
</script>

<style scoped>
.login {
    position: absolute;
    top: 0px;
    left: 0px;
    bottom: 0px;
    right: 0px;
    margin: auto;
    height: 30%;
    width: 30%;
    min-height: 300px;
    min-width: 400px;
    padding: 25px;
}

.login-form {
    background-color: #99CCFF;
    border: 1px;
    box-shadow: 0px 0px 14px 0px;
    border-radius: 5px;
    padding: 10px 20px;
}

.login-form .input-group {
    margin-top: 15px;
}

.login-form .input-group:first-child {
    margin-top: 10px;
}

.login-form-group {
    width: 100%;
    margin-top: 10px;
    margin-bottom: 10px;
    display: table;
}

.verify-code-container {
    padding: 0px 0px 0px 1px;
    width: 30%;
}

.verify-code-img {
    height: 32px;
    width: 93px;
}
</style>