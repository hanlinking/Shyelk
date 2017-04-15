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
                    <div class="input-group-addon verify-code-container">
                        <img class="verify-code-img"
                             src="data:image/png;base64,R0lGODlhNAAVAPcAAAAAAAAAMwAAZgAAmQAAzAAA/wArAAArMwArZgArmQArzAAr/wBVAABVMwBVZgBVmQBVzABV/wCAAACAMwCAZgCAmQCAzACA/wCqAACqMwCqZgCqmQCqzACq/wDVAADVMwDVZgDVmQDVzADV/wD/AAD/MwD/ZgD/mQD/zAD//zMAADMAMzMAZjMAmTMAzDMA/zMrADMrMzMrZjMrmTMrzDMr/zNVADNVMzNVZjNVmTNVzDNV/zOAADOAMzOAZjOAmTOAzDOA/zOqADOqMzOqZjOqmTOqzDOq/zPVADPVMzPVZjPVmTPVzDPV/zP/ADP/MzP/ZjP/mTP/zDP//2YAAGYAM2YAZmYAmWYAzGYA/2YrAGYrM2YrZmYrmWYrzGYr/2ZVAGZVM2ZVZmZVmWZVzGZV/2aAAGaAM2aAZmaAmWaAzGaA/2aqAGaqM2aqZmaqmWaqzGaq/2bVAGbVM2bVZmbVmWbVzGbV/2b/AGb/M2b/Zmb/mWb/zGb//5kAAJkAM5kAZpkAmZkAzJkA/5krAJkrM5krZpkrmZkrzJkr/5lVAJlVM5lVZplVmZlVzJlV/5mAAJmAM5mAZpmAmZmAzJmA/5mqAJmqM5mqZpmqmZmqzJmq/5nVAJnVM5nVZpnVmZnVzJnV/5n/AJn/M5n/Zpn/mZn/zJn//8wAAMwAM8wAZswAmcwAzMwA/8wrAMwrM8wrZswrmcwrzMwr/8xVAMxVM8xVZsxVmcxVzMxV/8yAAMyAM8yAZsyAmcyAzMyA/8yqAMyqM8yqZsyqmcyqzMyq/8zVAMzVM8zVZszVmczVzMzV/8z/AMz/M8z/Zsz/mcz/zMz///8AAP8AM/8AZv8Amf8AzP8A//8rAP8rM/8rZv8rmf8rzP8r//9VAP9VM/9VZv9Vmf9VzP9V//+AAP+AM/+AZv+Amf+AzP+A//+qAP+qM/+qZv+qmf+qzP+q///VAP/VM//VZv/Vmf/VzP/V////AP//M///Zv//mf//zP///wAAAAAAAAAAAAAAACH5BAEAAPwALAAAAAA0ABUAAAj/ADNlIiaQ4MCCCA8qNMgwYcOFDgXum0ixosWLGDGambgx40SJFTt63Cdy5L4zJi2CLJmypcuKICeyQdmSZUqaHmNSlHdmppk2Zs64iUYR2BmgaIhO1AcJaM8zoSoydcpmE0WdE/OdMRP14lQ4++YB7SqP60SebMDuQ2s13yUzbz5musizjSaMZZPu0xdMKFFkSvdC8rvPqN59fduEMqNprkV5QO94DdbmcLCgXaVeOhOXb5szpyYaPUMUa9igPqEuHUx6Yryjdy2u29pV2M800TyrNX2S4rGeYPNFIryP3ViK+Zr+DLxvdlA3kDLzljo4qVgzuCf+PmPn4rygcfdasUrKt2f46RT1fWYTSp+uykrZYrysmClnime2xtmHvugZvW+lUdRxFpXHlXBtvBHfZ92ZNoxI+hylV37sTXRZdsMExhcbuLVRXXxBgcKfYxUBo9hGRpkl3hlg1RWVPsAcZptqa/Vk1VTnkYhcUJ8leJGJP7U20XdbbaVgRcL99JNVcr3kZEgu9ffklCrpSBKVFeH0ZCaaDNNYJl4qBKZAYwo0DJkGkUlmlwKxqeaXaGYSEAA7" />
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
            verifycode: ""
        }
    },
    methods: {
        login: function (e) {
            this.username = "asdasdasd";
            this.password = "luhanlin1";
            this.isremember = true;
            this.verifycode = "asdas";
            NetUtils.ajax();
            // router.push('Main')
        }
    },
    created: function (e) {
        var islogin = securityModule.checkLogin()
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