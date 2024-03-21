<template v-if="!authStore.isAuthenticated">
  <Card class="w-10 m-auto mt-7">
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
            <Message v-if="authStore.hasError" severity="error" class="p-0"
              >Usuário ou senha incorretos.</Message
            >
            <Button
              type="submit"
              label="Login"
              icon="pi pi-user"
              class="w-10rem mx-auto"
              :loading="loading"
            ></Button>
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
    @success-message="getCreatedUserInfo"
  ></create-user-modal>

  <SuccessModal
    v-if="createdNewUser"
    :has-message="createdNewUser"
    :message="successMessage"
  ></SuccessModal>
</template>

<script>
import CreateUserModal from "@/components/CreateUserModal.vue";
import SuccessModal from "@/components/SuccessModal.vue";

import { useAuthStore } from "@/stores/auth";

import { mapStores } from "pinia";

export default {
  components: {
    CreateUserModal,
    SuccessModal,
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  data() {
    return {
      modalClick: false,
      User: {
        username: "".trim(),
        password: "".trim(),
      },
      createdNewUser: false,
      successMessage: "",
      loading: false,
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
        this.loading = true;
        await this.authStore.login(this.User);

        if (this.authStore.hasError === false) {
          this.$router.push("/home");
          this.loading = false;
        }
      } catch (error) {
        this.hasError = true;
        console.log(error?.response?.data);
      }
    },
    getCreatedUserInfo(successMessage, isCreated) {
      this.successMessage = successMessage;
      this.createdNewUser = isCreated;
    },
  },
};
</script>
