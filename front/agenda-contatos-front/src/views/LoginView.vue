<template>
  <Card class="w-6 m-auto mt-7">
    <template #title>LOGIN</template>
    <template #content>
      <div class="card">
        <div class="flex flex-column md:flex-row">
          <form
            @submit.prevent="Login"
            class="w-full md:w-5 flex flex-column align-items-center justify-content-center gap-3 py-5"
          >
            <div
              class="flex flex-wrap justify-content-center align-items-center gap-2"
            >
              <label class="w-6rem">Username</label>
              <InputText
                id="username"
                type="text"
                class="w-12rem"
                v-model="User.username"
              />
            </div>
            <div
              class="flex flex-wrap justify-content-center align-items-center gap-2"
            >
              <label class="w-6rem">Password</label>
              <InputText
                id="password"
                type="password"
                class="w-12rem"
                v-model="User.password"
              />
            </div>
            <Button
              type="submit"
              label="Login"
              icon="pi pi-user"
              class="w-10rem mx-auto"
            ></Button>
            <!-- <Button
              @click="loginTest"
              label="teste"
              icon="pi pi-user"
              class="w-10rem mx-auto"
            ></Button> -->
          </form>
          <div class="w-full md:w-2">
            <Divider layout="vertical" class="hidden md:flex"
              ><b>OR</b></Divider
            >
            <Divider layout="horizontal" class="flex md:hidden" align="center"
              ><b>OR</b></Divider
            >
          </div>
          <div
            class="w-full md:w-5 flex align-items-center justify-content-center py-5"
          >
            <Button
              label="Sign Up"
              icon="pi pi-user-plus"
              severity="success"
              class="w-10rem"
              @click="openModal"
            ></Button>
          </div>
        </div>
      </div>
    </template>
  </Card>
  <create-user-modal
    v-if="modalClick"
    :modalClick="modalClick"
    @modal-event="getModalState"
  ></create-user-modal>
</template>

<script>
import CreateUserModal from "@/components/CreateUserModal.vue";
import axiosInstance from "@/services/api";

import { mapStores } from "pinia";

import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    token: localStorage.getItem("token"),
    user: JSON.parse(localStorage.getItem("user")),
  }),
  actions: {
    setToken(tokenValue) {
      localStorage.setItem("token", tokenValue);
      this.token = tokenValue;
    },
    setUser(userValue) {
      localStorage.setItem("user", userValue);
      this.user = userValue;
    },
  },
});

export default {
  components: {
    CreateUserModal,
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  data() {
    return {
      modalClick: false,
      User: {
        username: "",
        password: "",
      },
    };
  },
  methods: {
    openModal() {
      this.modalClick = true;
    },
    getModalState(btnInfo) {
      this.modalClick = btnInfo;
    },
    async Login() {
      try {
        const { data } = await axiosInstance.post(`/auth`, this.User);
        this.authStore.setToken(data.token);
        this.authStore.setUser(JSON.stringify(data.user));
      } catch (error) {
        console.log(error?.response?.data);
      }
    },
  },
};
</script>
