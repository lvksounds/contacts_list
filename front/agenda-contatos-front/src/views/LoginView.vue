<template>
  <Card class="w-6 m-auto mt-7">
    <template #title>LOGIN</template>
    <template #content>
      <div class="card">
        <div class="flex flex-column md:flex-row">
          <div
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
              @click="submitUser"
              label="Login"
              icon="pi pi-user"
              class="w-10rem mx-auto"
            ></Button>
          </div>
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
export default {
  components: {
    CreateUserModal,
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
    submitUser() {
      console.log(this.User.username, this.User.password);
    },
    openModal() {
      this.modalClick = true;
    },
    getModalState(btnInfo) {
      this.modalClick = btnInfo;
    },
  },
};
</script>
