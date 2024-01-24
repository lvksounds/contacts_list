<template>
  <div class="card flex justify-content-center">
    <!-- <Button label="Login" icon="pi pi-user" @click="visible = true" /> -->

    <Modal
      v-model:visible="visible"
      modal
      :pt="{
        mask: {
          style: 'backdrop-filter: blur(2px)',
        },
      }"
    >
      <template #container="{ closeCallback }">
        <div
          class="flex flex-column px-8 py-3 gap-2 overflow-auto"
          style="
            border-radius: 2%;
            background-image: radial-gradient(
              circle at left top,
              var(--blue-100),
              var(--blue-400)
            );
          "
        >
          <h3 class="text-white">Create a new login</h3>
          <form
            @submit.prevent="createNewUser"
            class="flex flex-column px-8 py-3 gap-2 overflow-auto"
          >
            <div class="inline-flex flex-column gap-2">
              <label for="name" class="text-primary-50 font-semibold"
                >Name</label
              >
              <InputText
                id="name"
                class="bg-white-alpha-20 border-none p-3 text-primary-50"
                v-model="newUser.name"
              ></InputText>
            </div>
            <div class="inline-flex flex-column gap-2">
              <label for="email" class="text-primary-50 font-semibold"
                >E-mail</label
              >
              <InputText
                id="email"
                class="bg-white-alpha-20 border-none p-3 text-primary-50"
                v-model="newUser.email"
              ></InputText>
            </div>
            <div class="inline-flex flex-column gap-2">
              <label for="username" class="text-primary-50 font-semibold"
                >Username</label
              >
              <InputText
                id="username"
                class="bg-white-alpha-20 border-none p-3 text-primary-50"
                v-model="newUser.username"
              ></InputText>
            </div>

            <div class="inline-flex flex-column gap-2">
              <label for="password" class="text-primary-50 font-semibold"
                >Password</label
              >
              <InputText
                id="password"
                class="bg-white-alpha-20 border-none p-3 text-primary-50"
                type="password"
                v-model="newUser.password"
              ></InputText>
            </div>
            <div class="inline-flex flex-column gap-2">
              <label
                for="confirm-password"
                class="text-primary-50 font-semibold"
                >Confirm Password</label
              >
              <InputText
                id="confirm-password"
                class="bg-white-alpha-20 p-3 text-primary-50"
                type="password"
                v-model="newPasswordValidate"
                :class="{
                  'border-red-500': isWrong,
                  'border-green-500': isCorrect,
                  'border-none': newPasswordValidate === '',
                }"
                @change="validateNewPassword"
              ></InputText>
            </div>
            <div class="flex align-items-center gap-2">
              <Button
                label="Create"
                type="submit"
                text
                class="p-3 w-full text-primary-50 border-1 border-white-alpha-30 hover:bg-white-alpha-10"
              ></Button>
              <Button
                label="Cancel"
                @click="closeBtn"
                text
                class="p-3 w-full text-primary-50 border-1 border-white-alpha-30 hover:bg-white-alpha-10"
              ></Button>
            </div>
          </form>
        </div>
      </template>
    </Modal>
  </div>
</template>

<script>
export default {
  props: {
    modalClick: Boolean,
  },
  data() {
    return {
      modalinfo: null,
      visible: this.modalClick,
      newUser: {
        name: "",
        email: "",
        username: "",
        password: "",
      },
      newPasswordValidate: "",
      isWrong: false,
      isCorrect: false,
    };
  },
  methods: {
    closeBtn() {
      this.visible = false;
      this.$emit("modal-event", this.visible);
    },
    createNewUser() {
      console.log(
        this.newUser.name,
        this.newUser.email,
        this.newUser.username,
        this.newUser.password
      );
    },
    validateNewPassword() {
      if (this.newPasswordValidate !== "") {
        if (this.newPasswordValidate !== this.newUser.password) {
          this.isWrong = true;
          this.isCorrect = false;
        } else if (this.newPasswordValidate === this.newUser.password) {
          this.isWrong = false;
          this.isCorrect = true;
        }
      }
    },
  },
};
</script>
