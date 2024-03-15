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
          icon="pi pi-user"
          class="mr-2"
          size="xlarge"
          :image="contact.profileImg"
        />
        <input type="file" class="p-button" @change="getProfileImg" />
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
        <Button type="button" label="Save" @click="visible = false"></Button>
      </div>
    </Dialog>
  </div>
</template>

<script>
import Dialog from "primevue/dialog";
import InputMask from "primevue/inputmask";
import Avatar from "primevue/avatar";
import FileUpload from "primevue/fileupload";

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
      contact: {
        name: "",
        email: "",
        phone: "",
        profileImg: null,
      },
    };
  },
  methods: {
    closeModal() {
      this.visible = false;
      this.$emit("close-modal", this.visible);
    },
    onUpload(event) {
      console.log(event);
      // this.contact.profileImg = event.files[0].objectURL;
      // console.log(...event.files);
      // console.log(this.contact.profileImg.slice(""));
    },
    getProfileImg(ev) {
      console.log(ev);
    },
  },
};
</script>
