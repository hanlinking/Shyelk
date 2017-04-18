<template>
    <div>
        <div class="login">
    
            <form class="login-form">
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-fw fa-user"></i>账号</span>
                    <input type="text"
                           class="form-control"
                           v-model="username"
                           placeholder="账号">
                </div>
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-fw fa-lock"></i>密码</span>
                    <input type="password"
                           class="form-control"
                           v-model="password"
                           placeholder="密码">
                </div>
                <div class="input-group">
                    <span class="input-group-addon">&nbsp;验证码</span>
                    <input type="text"
                           class="form-control"
                           v-model="verifycode"
                           placeholder="验证码">
                    <div class="input-group-addon verify-code-container" v-on:click="refresh">
                        <img class="verify-code-img"
                             src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAFAAAAAeCAYAAAC7Q5mxAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJqSURBVGhD7ZjLjYMwEIYpj4JcQO5pgQOXlMCZUyQK2FsuKAVESgXeHz/IGOz4tRA28ieNtDgWg3/mxVa8kEURMJMiYCZFwEwcAj540555dbZYO/BR7Sq8E7A7olA95xc88tKuWP8Q8G7jxll3U38fhJ/aLt5s+P2p9u4IPNs4mIDP5iXUXa3NIE869VuHfTuzk4DkkNpiDquj78dVVMj9VwJrUJZq1PBKWT2VKJxTX8NYQiWAR85P1UlczDwGzoYHH4fWaCA11qK5M1M4wwLTzisgCNkz0XcQq5uqqQLCsvR6D48OAdvWFAxrdbSIpOgbhZ6sXyCwD5rCPoG8yEisG3mOsel4k3FLPJFFQNDf1iksI5K+PQ86KqxdkogYIoorkp0p+4YRwVC1vOkRKErIVPAEdgGtiCiE41CfV88htcAp9XBlWI/owmODYBB1MA943kpAPJqvsOvUzOme+iVFitgz2Th0KqcCr2sBnal6RAE1MdGMRiKEE6kcUZIsCAEnqIh9h7dj+WQTwsZ8yunocH0pBB1a10pfs1H7vAJidGGv+i5SmVzHAo8SIwpvaPXLCBRrmJVifNHuaYhIonMyXxPRL+LdvsBo7hkyyLiNnAVTZsAJeJSs6yBuTGbAKqZ5UJxFn5ivk9IObBMoZMwR85+se6850BykU+ohPEqCG0kK9IDa7irlQubAiZAX8YF/KsCrZFMBbfjqo5VF6s8W3n3/GniX7CrgnJKB0Xdg9heQ1rOUr4iDsZ+AyzqY/U17DLYXcFn8Qwbdf8T2Aupm8WXCaWYBJ3ZtJF9CETCTImAmRcBMioBZcP4LIsbnMrQOZdUAAAAASUVORK5CYII=" />
                    </div>
                </div>
                <div class="login-form-group">
                    <div style="float:left">
                        <div class="checkbox">
                            <label>
                                <input type="checkbox"
                                       id="henry"
                                       value="henry"
                                       v-model="isremember">记住密码
                            </label>
                        </div>
                    </div>
                </div>
                <div class="login-form-group">
                    <button v-on:click.prevent="login"
                            class="btn btn-block btn-primary">登&nbsp;录</button>
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
export default {
    name: 'Login',
    data: function () {
        return {
            username: "",
            password: "",
            isremember: false,
            verifycode: "",
            antiforgetcode:""
        }
    },
    methods: {
        login: function (e) {
            this.username = "asdasdasd";
            this.password = "luhanlin1";
            this.isremember = true;
            this.verifycode = "";
            NetUtils.ajax();
            // router.push('Main')
        },
        refresh:function (e) {
            this.antiforgetcode=""
            alert("asdasd");
        }
    },
    created: function (e) {
        var islogin = securityModule.checkLogin()
        if(islogin){
            // router.push('Main');
        }else{
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