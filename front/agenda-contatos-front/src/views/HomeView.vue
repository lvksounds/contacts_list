<template>
  <Card class="w-9 mx-auto">
    <template #title>Contacts</template>
    <template #content>
      <div>
        <contacts-search-vue
          @input-sender="getSearchValue"
          @contact-creation-event="getContactCreationEvent"
        ></contacts-search-vue>
      </div>
      <Divider></Divider>
      <div>
        <contacts-list-vue
          :nameInput="inputValue"
          :hasNewContact="hasNewContact"
        ></contacts-list-vue>
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
  data() {
    return {
      inputValue: "",
      hasNewContact: null,
    };
  },
  components: {
    ContactsSearchVue,
    ContactsListVue,
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  methods: {
    getSearchValue(value) {
      this.inputValue = value;
    },
    getContactCreationEvent(value) {
      this.hasNewContact = value;
      setTimeout(() => {
        this.hasNewContact = null;
      }, 3000);
    },
  },
};
</script>
