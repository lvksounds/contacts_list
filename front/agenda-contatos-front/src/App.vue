<template>
  <div class="nav">
    <div class="flex flex-row align-items-center">
      <router-link class="col-1 text-center" :to="{ name: 'login' }"
        >Login</router-link
      >
      |
      <router-link class="col-1 text-center" :to="{ name: 'home' }"
        >Home</router-link
      >
      <template v-if="authStore.isAuthenticated">
        <div
          class="align-self-end col-10 flex flex-row justify-content-end align-items-center border border-damger"
        >
          <div class="flex flex-row gap-2 align-items-center">
            <p class="p-0 m-0">Olá {{ authStore.userName }}</p>
            <Button severity="help" class="p-1" rounded @click="logout"
              >Sair</Button
            >
          </div>
        </div>
      </template>
      <template v-else>
        <div
          class="align-self-end col-10 flex flex-row justify-content-end align-items-center"
        >
          Olá Visitante
        </div>
      </template>
    </div>
  </div>
  <router-view />
</template>

<script setup>
import { useAuthStore } from "@/stores/auth";
import { useRouter } from "vue-router";

const router = useRouter();
const authStore = useAuthStore();

function logout() {
  authStore.clear();
  router.push("/");
}
</script>

<style>
.nav {
  padding: 30px;
}

.nav a {
  margin: 3px;
  font-weight: bold;
  color: #2c3e50;
}

.nav a.router-link-exact-active {
  color: #3b82f6;
}
</style>
