<template>
    <Left-Menu></Left-Menu>
</template>

<script>
    import LeftMenu from './Menu'
    export default {
        name: 'hello',
        data() {
            return {
                msg: 'Welcome to Your Vue.js App'
            }
        },
        components:{
            LeftMenu
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>