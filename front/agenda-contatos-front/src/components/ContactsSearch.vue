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

  <create-contact-modal
    v-if="createContactModal"
    :openModal="createContactModal"
    @close-modal="getModalState"
    @create-contact-event="handleNewContactCreation"
  ></create-contact-modal>
  <Toast />
</template>

<script>
import { useAuthStore } from "@/stores/auth";
import { mapStores } from "pinia";
import Toast from "primevue/toast";
import CreateContactModal from "./CreateContactModal.vue";

export default {
  emits: ["input-sender", "contact-creation-event"],
  components: {
    CreateContactModal,
    Toast,
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  data() {
    return {
      createContactModal: false,
      valorInput: "",
    };
  },
  methods: {
    createNewContact() {
      this.createContactModal = true;
    },
    sendValue(value) {
      this.$emit("input-sender", value.target.value);
    },
    getModalState(btnInfo) {
      this.createContactModal = btnInfo;
    },
    handleNewContactCreation(messageData) {
      this.$toast.add({
        severity: "info",
        summary: "Success!",
        detail: messageData.message,
        life: 3000,
      });
      this.$emit("contact-creation-event", true);
    },
  },
};
</script>
