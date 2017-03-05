<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <button v-on:click="login">login</button>
    <div>
</template>

<script>
    import $ from 'jquery'
    import Router from 'vue-router'
    import 'jquery.cookie'

    export default {
        name: 'foot',
        data() {
            return {
                msg: '点击这里登陆'
            }
        },
        methods: {
            login: function() {
                $.cookie('token', 'asdasdad');
                router.push('/hello');
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h1,
    h2 {
        font-weight: normal;
    }
</style>