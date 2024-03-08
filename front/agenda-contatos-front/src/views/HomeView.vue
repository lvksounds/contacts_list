<template>
  <Card class="w-9 mx-auto">
    <template #title>Contatos</template>
    <template #content>
      <div>
        <contacts-search-vue></contacts-search-vue>
      </div>
      <Divider></Divider>
      <div>
        <contacts-list-vue></contacts-list-vue>
      </div>
    </template>
  </Card>
</template>

<script>
import axiosInstance from "../services/api";
import { useAuthStore } from "@/stores/auth";
import { mapStores } from "pinia";
import ContactsSearchVue from "@/components/ContactsSearch.vue";
import ContactsListVue from "@/components/ContactsList.vue";

export default {
  components: {
    ContactsSearchVue,
    ContactsListVue,
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  // created() {
  //   const contact = {
  //     userId: this.authStore.userId,
  //     name: "Julia Nicolau",
  //     phone: "999999956",
  //   };
  //   console.log(contact);
  //   const { data } = axiosInstance.post("/create-contact", contact, {
  //     headers: {
  //       Authorization: `Bearer ${this.authStore.auth.token}`,
  //     },
  //   });
  //   console.log(data);
  // },
};
</script>
