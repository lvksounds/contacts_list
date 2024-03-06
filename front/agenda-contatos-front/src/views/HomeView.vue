<template>
  <Card>
    <template #title> Agenda </template>
  </Card>
</template>

<script>
import axiosInstance from "../services/api";
import { useAuthStore } from "@/stores/auth";
import { mapStores } from "pinia";

export default {
  computed: {
    ...mapStores(useAuthStore),
  },
  created() {
    const contact = {
      userId: this.authStore.userId,
      name: "Julia Nicolau",
      phone: "999999956",
    };
    console.log(contact);
    const { data } = axiosInstance.post("/create-contact", contact);
    console.log(data);
  },
};
</script>
