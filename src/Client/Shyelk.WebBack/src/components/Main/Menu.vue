<template>
    <div class="left-menu-container" v-bind:class="[isShow ? showClass : '']">
        <div class="touch-btn" v-on:click="isShow=!isShow">
            <i class="fa fa-angle-double-right" aria-hidden="true"></i>
        </div>
        <div class="menu">
            <div class="user-container">
                <div class="user-header">
                    <img v-bind:src="User.header">
                </div>
                <div class="user-btns">
                    <div>
                        <input id="settings" type="button" class="user-btn" value="Setting"/>
                    </div>
                    <div>
                        <input id='signout' type="button" class="user-btn" value="SignOut" v-on:click="signout"/>
                    </div>
                </div>
            </div>
            <div>
                <div></div>
                <div></div>
            </div>
        </div>
    </div>
</template>
<script>
    import defaultheader from 'assets/images/default_user.jpg';
    import securityModule from 'assets/js/security';
    import router from '@/router'

    export default{
        name:'left-menu',
        data(){
            return{
                isShow:false,
                showClass:'left-menu-container-show',
                User:{
                    header:defaultheader
                }
            }
        },
        methods:{
            signout:function () {
                securityModule.signOut();
                router.push('login')
            }
        }
    }    
</script>
<style scoped>
    .left-menu-container{
        position: fixed;
        height: 100%;
        width: 300px;
        z-index: 999;
        border-right: 1px;
        box-shadow: 0px 0px 10px 1px;
        left: -300px;
        transition:left 0.5s;
        -webkit-transition: left 0.5s;
        -o-transition: left 0.5s;
        -moz-transition: left 0.5s;
    }
    .left-menu-container-show{
        left:0px;
    }
    .touch-btn{
        width: 40px;
        height: 40px;
        position: absolute;
        right: -18px;
        border: 1px;
        border-radius: 20px;
        top: 0px;
        bottom: 0px;
        margin: auto;
        z-index: 0;
        box-shadow: inherit;
        cursor: pointer;
        background-color: #99CCFF;
        text-align: right;
        line-height: 32px;
        font-size: 25px;
        padding: 4px;
    }
    .menu{
        position: absolute;
        z-index: 1;
        height: 100%;
        width: 100%;
        background: #99CCFF;
    }
    .user-container{
        position: relative;
        padding: 2px;
    }
    .user-header{
        width: 60px;
        height: 60px;
        margin:10px auto;
        border-radius: 30px;
        overflow: hidden;
        box-shadow: 0px 0px 5px 0px;
    }
    .user-header>img{
        height: 100%;
        width: 100%;
    }
    .user-btns{
        display: table;
        width: 100%;
    }
    .user-btns>div{
        display: table-cell;
        width: 50%;
        padding: 1px;
    }
    .user-btn{
        width: 100%;
    }

</style>