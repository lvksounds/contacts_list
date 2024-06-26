<template>
  <div>
    <Dialog
      v-model:visible="visible"
      modal
      header="Edit Contact"
      :style="{ width: '35rem' }"
      :closable="false"
    >
      <span class="p-text-secondary block mb-5"
        >Edit your Contact informations.</span
      >
      <div class="flex align-items-center gap-1 mb-5">
        <label for="email" class="font-semibold w-5rem">Profile Image</label>
        <Avatar
          class="mr-2"
          size="xlarge"
          v-bind:image="contact.profileImg"
          v-if="contact.profileImg"
          shape="circle"
        />
        <input
          type="file"
          class="p-fileupload p-fileupload-basic p-component"
          @change="getProfileImg"
          accept="image/*"
        />
      </div>
      <div class="flex align-items-center gap-1 mb-3">
        <label for="username" class="font-semibold w-4rem">Name</label>
        <InputText
          id="username"
          class="flex-auto"
          autocomplete="off"
          v-model="contact.name"
        />
      </div>
      <div class="flex align-items-center gap-1 mb-3">
        <label for="email" class="font-semibold w-4rem">Phone</label>
        <InputMask
          id="basic"
          v-model="contact.phone"
          mask="99-999999999"
          class="flex-auto"
        />
      </div>
      <div class="flex align-items-center gap-1 mb-5">
        <label for="email" class="font-semibold w-4rem">Email</label>
        <InputText
          id="email"
          class="flex-auto"
          autocomplete="off"
          v-model="contact.email"
        />
      </div>
      <div class="flex justify-content-end gap-2">
        <Button
          type="button"
          label="Cancel"
          severity="secondary"
          @click="closeModal"
        ></Button>
        <Button
          type="button"
          label="Save"
          @click="updateContact"
          :loading="loading"
        ></Button>
      </div>
    </Dialog>
  </div>
</template>

<script>
import Dialog from "primevue/dialog";
import InputMask from "primevue/inputmask";
import Avatar from "primevue/avatar";
import FileUpload from "primevue/fileupload";

import { mapStores } from "pinia";
import { useAuthStore } from "@/stores/auth";
import { updateContact } from "@/services/contactsService";

export default {
  emits: ["close-modal", "update-contact-event"],
  components: {
    Dialog,
    InputMask,
    Avatar,
    FileUpload,
  },
  props: {
    openModal: Boolean,
    contact: {
      contactId: String,
      userId: String,
      name: String,
      email: String,
      phone: String,
      profileImg: null,
    },
  },
  data() {
    return {
      visible: this.openModal,
      profileImgPreview: null,
      loading: false,
    };
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  methods: {
    closeModal() {
      this.visible = false;
      this.$emit("close-modal", this.visible);
    },

    async updateContact() {
      this.loading = true;
      const formData = new FormData();
      formData.append("contactId", this.contact.contactId);
      formData.append("userId", this.contact.userId);
      formData.append("name", this.contact.name);
      formData.append("email", this.contact.email);
      formData.append("phone", this.contact.phone);

      if (this.contact.profileImg !== null)
        formData.append("profileImg", this.contact.profileImg.split(",")[1]);

      try {
        return await updateContact(formData).then((res) => {
          let successData = {
            message: "Contact updated successfully.",
            hasNewContact: true,
          };
          this.loading = false;
          this.visible = false;

          this.$emit("close-modal", this.visible);
          this.$emit("update-contact-event", successData);
        });
      } catch (error) {
        console.log(error?.response?.data);
      }
    },
    getProfileImg(ev) {
      const file = ev.target.files[0];
      const reader = new FileReader();

      reader.onload = (e) => {
        this.contact.profileImg = e.target.result;
        this.profileImgPreview = e.target.result;
      };
      reader.readAsDataURL(file);
    },
  },
};
</script>
