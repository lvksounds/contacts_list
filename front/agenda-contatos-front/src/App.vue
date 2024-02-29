<template>
  <nav>
    <nav class="flex flex-row align-items-center">
      <router-link class="col-1" :to="{ name: 'login' }">Login</router-link>
      |
      <router-link class="col-1" :to="{ name: 'home' }">Home</router-link>

      <div
        class="align-self-end col-10 flex flex-row justify-content-end align-items-center"
        v-if="auth.isAuthenticated"
      >
        <div class="flex flex-row gap-2 align-items-center">
          <p class="p-0 m-0">Olá {{ auth.userName }}</p>
          <Button severity="help" class="p-1" rounded @click="logout"
            >Sair</Button
          >
        </div>
      </div>
      <div
        v-else
        class="align-self-end col-10 flex flex-row justify-content-end align-items-center"
      >
        Olá Visitante
      </div>
    </nav>
  </nav>
  <router-view />
</template>

<script setup>
import { useAuthStore } from "@/stores/auth";
import { computed, watch, onMounted, onBeforeUpdate } from "vue";

const auth = useAuthStore();

function logout() {
  auth.clear();
}
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
