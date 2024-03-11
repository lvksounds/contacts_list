<template>
  <div class="flex flex-row">
    <div
      class="flex flex-row gap-2 align-items-center w-full justify-content-center"
    >
      <i class="pi pi-search p-2" style="font-size: 1.5rem"></i>
      <InputText
        id="username"
        aria-describedby="username-help"
        class="w-9 h-2rem p-2"
        v-model="valorInput"
        @keyup="sendValue"
      />
      <div></div>
      <Button severity="primary" @click="createNewContact" class="p-2 gap-1">
        new
        <i class="pi pi-plus"></i>
      </Button>
    </div>
  </div>
</template>

<script>
import axiosInstance from "../services/api";
import { useAuthStore } from "@/stores/auth";
import { mapStores } from "pinia";
import { computed } from "vue";

export default {
  computed: {
    ...mapStores(useAuthStore),
  },
  data() {
    return {
      valorInput: "",
    };
  },
  methods: {
    createNewContact() {
      const contact = {
        userId: this.authStore.userId,
        name: "Lucas Oliveira",
        phone: "999412841",
        isFavorite: false,
      };
      const { data } = axiosInstance.post("/create-contact", contact, {
        headers: {
          Authorization: `Bearer ${this.authStore.auth.token}`,
        },
      });
    },
    sendValue(value) {
      this.$emit("input-sender", value.target.value);
    },
  },
};
</script>
