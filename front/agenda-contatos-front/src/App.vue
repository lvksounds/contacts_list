<template>
  <nav>
    <nav>
      <router-link :to="{ name: 'login' }">Login</router-link>
      |
      <router-link :to="{ name: 'home' }">Home</router-link>
    </nav>
  </nav>
  <router-view />
</template>

<script>
import LoginView from "./views/LoginView.vue";
export default {
  components: {
    LoginView,
  },
};
</script>

<style>
nav {
  padding: 30px;
}

nav a {
  margin: 3px;
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
