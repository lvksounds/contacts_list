<template>
  <div>
    <Dialog
      v-model:visible="visible"
      modal
      header="New Contact"
      :style="{ width: '35rem' }"
    >
      <span class="p-text-secondary block mb-5">Create a new Contact</span>
      <div class="flex align-items-center gap-1 mb-5">
        <label for="email" class="font-semibold w-5rem">Profile Image</label>
        <Avatar
          class="mr-2"
          size="xlarge"
          v-bind:image="profileImgPreview"
          v-if="profileImgPreview"
          shape="circle"
        />
        <!-- <Avatar image="/images/avatar/amyelsner.png" class="mr-2" size="xlarge" shape="circle" /> -->
        <!-- <img
          v-if="profileImgPreview"
          :src="profileImgPreview"
          alt="Pré-visualização da imagem"
        /> -->
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
        <Button type="button" label="Save" @click="saveNewContact"></Button>
      </div>
    </Dialog>
  </div>
</template>

<script>
import Dialog from "primevue/dialog";
import InputMask from "primevue/inputmask";
import Avatar from "primevue/avatar";
import FileUpload from "primevue/fileupload";
import axiosInstance from "@/services/api";
import { mapStores } from "pinia";
import { useAuthStore } from "@/stores/auth";

export default {
  emits: ["close-modal"],
  components: {
    Dialog,
    InputMask,
    Avatar,
    FileUpload,
  },
  props: {
    openModal: Boolean,
  },
  data() {
    return {
      visible: this.openModal,
      profileImgPreview: null,
      contact: {
        name: "",
        email: "",
        phone: "",
        profileImg: null,
      },
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
    async saveNewContact() {
      //this.visible = false
      console.log(this.contact.profileImg.split(",")[1]);
      const formData = new FormData();
      formData.append("name", this.contact.name);
      formData.append("email", this.contact.email);
      formData.append("phone", this.contact.phone);
      formData.append("profileImg", this.contact.profileImg.split(",")[1]);

      try {
        const { data } = await axiosInstance.post(
          "/create-contact/",
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
              Authorization: `Bearer ${this.authStore.auth.token}`,
            },
          }
        );
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
